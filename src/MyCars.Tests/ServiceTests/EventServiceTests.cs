using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCars.Core.Entities;
using MyCars.Core.Exceptions;
using MyCars.Core.Repositories.Interfaces;
using MyCars.Core.Services;
using MyCars.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCars.Tests.ServiceTests
{
    [TestClass]
    public class EventServiceTests
    {
        private readonly IEventRepository _mockEventRepository;
        private readonly IEventService _eventService;
        private readonly IList<Event> eventList;
        private readonly IList<EventType> eventTypeList;
        private readonly IList<Car> carList;

        public EventServiceTests()
        {
            // create some mock events to play with
            eventList = new List<Event>
            {
                new Event
                {
                    EventId = 1,
                    EventDate = new DateTime(2020, 9, 29, 12, 0, 35),
                    EventTypeId = 1,
                    CarId = 1,
                    Milieage = 23456,
                    Text = "It's my first event!!!",
                    UserId = 1,
                    Created = new DateTime(2020, 10, 6, 12, 0, 35),
                    Modified = new DateTime(2020, 10, 6, 12, 0, 35)
                },
                new Event
                {
                    EventId = 2,
                    EventDate = new DateTime(2020, 11, 10, 10, 0, 35),
                    EventTypeId = 2,
                    CarId = 2,
                    Milieage = 232456,
                    Text = "It's my second event!",
                    UserId = 1,
                    Created = new DateTime(2020, 11, 14, 10, 12, 23),
                    Modified = new DateTime(2020, 11, 14, 12, 53, 14)
                },
                new Event
                {
                    EventId = 3,
                    EventDate = new DateTime(2020, 11, 23, 12, 0, 35),
                    EventTypeId = 1,
                    CarId = 3,
                    Milieage = 23456,
                    Text = "Bla bla bla bla whatever",
                    UserId = 2,
                    Created = new DateTime(2020, 11, 23, 22, 10, 5),
                    Modified = new DateTime(2020, 11, 28, 16, 40, 24)
                }
            };

            eventTypeList = new List<EventType>()
            {
                new EventType()
                {
                    EventTypeId = 1,
                    EventTypeName = "Ремонт"
                },
                new EventType()
                {
                    EventTypeId = 2,
                    EventTypeName = "Комментарий"
                }
            };

            carList = new List<Car>()
            {
                new Car
                {
                    CarId = 1,
                    CarName = "Моя первая машина",
                    Brand = "Nissan Qashqai",
                    IssueYear = new DateTime(2009, 1, 1),
                    VIN = "AHDN29ADXGP2",
                    Numberplate = "М329ОР",
                    UserId = 1,
                    Created = new DateTime(2020, 10, 6, 12, 0, 35),
                    Modified = new DateTime(2020, 10, 6, 12, 0, 35)
                },
                new Car
                {
                    CarId = 2,
                    CarName = "Жигуль",
                    Brand = "LADA 2105",
                    IssueYear = new DateTime(2001, 1, 1),
                    VIN = "XGDW31ATIYP1",
                    Numberplate = "РО450Т",
                    UserId = 1,
                    Created = new DateTime(2020, 11, 14, 10, 12, 23),
                    Modified = new DateTime(2020, 11, 14, 12, 53, 14)
                },
                new Car
                {
                    CarId = 3,
                    CarName = "Honda",
                    Brand = "Honda Civic",
                    IssueYear = new DateTime(2010, 1, 1),
                    VIN = "VGDD12ATNYP4",
                    Numberplate = "ТВ421Р",
                    UserId = 2,
                    Created = new DateTime(2020, 11, 23, 22, 10, 5),
                    Modified = new DateTime(2020, 11, 28, 16, 40, 24)
                }
            };

            // Mock the Events Repository using Moq
            Mock<IEventRepository> mockEventRepository = new Mock<IEventRepository>();

            // Return all the events
            mockEventRepository.Setup(mr => mr.GetAllByUserId(It.IsAny<int>()))
                .Returns((int userId) =>
                {
                    return eventList.Where(e => e.UserId == userId)
                        .Select(e =>
                        {
                            e.EventType = eventTypeList.Where(et => et.EventTypeId == e.EventTypeId).FirstOrDefault();
                            e.Car = carList.Where(c => c.CarId == e.CarId).FirstOrDefault();

                            return e;
                        }).ToList();
                });

            // Return event by id
            mockEventRepository.Setup(mr => mr.GetById(It.IsAny<int>()))
                .Returns((int eventId) => eventList.Where(e => e.EventId == eventId)
                .FirstOrDefault());

            // Add event
            mockEventRepository.Setup(mr => mr.Add(It.IsAny<Event>()))
                .Returns((Event eventModel) =>
                {
                    int newId = eventList.Count + 1;

                    eventModel.EventId = newId;

                    eventList.Add(eventModel);

                    Event newEvent = eventList.Where(e => e.EventId == newId).FirstOrDefault();

                    if(newEvent != null)
                    {
                        newEvent.EventType = eventTypeList.Where(et => et.EventTypeId == newEvent.EventTypeId).FirstOrDefault();
                        newEvent.Car = carList.Where(c => c.CarId == newEvent.CarId).FirstOrDefault();
                    }

                    return newEvent;
                });

            // Delete event by id
            mockEventRepository.Setup(mr => mr.DeleteById(It.IsAny<int>()))
                .Returns((int eventId) =>
                {
                    var eventModel = eventList.Where(eventFromList => eventFromList.EventId == eventId).FirstOrDefault();

                    if (eventModel != null)
                    {
                        return eventList.Remove(eventModel);
                    }
                    else
                    {
                        return false; // Нужно выбросить исключение NotFound, но это Moq
                    }
                });

            // Update event
            mockEventRepository.Setup(mr => mr.Update(It.IsAny<Event>()))
                .Returns((Event newEvent) =>
                {
                    eventList.Where(eventModel => eventModel.EventId == newEvent.EventId)
                        .Select(e =>
                        {
                            e.EventDate = newEvent.EventDate;
                            e.EventTypeId = newEvent.EventTypeId;
                            e.CarId = newEvent.CarId;
                            e.Milieage = newEvent.Milieage;
                            e.Text = newEvent.Text;
                            e.Created = newEvent.Created;
                            e.Modified = newEvent.Modified;
                            e.EventType = eventTypeList.Where(et => et.EventTypeId == newEvent.EventTypeId).FirstOrDefault();
                            e.Car = carList.Where(c => c.CarId == newEvent.CarId).FirstOrDefault();

                            return e;
                        }).ToList();

                    return eventList.FirstOrDefault(e => e.EventId == newEvent.EventId);
                });

            _mockEventRepository = mockEventRepository.Object;
            _eventService = new EventService(_mockEventRepository);
        }

        // Проверка на получение всех событий пользователя
        [TestMethod]
        public void CanReturnAllUsersCars()
        {
            IList<Event> eventsFirstUser = _eventService.GetAllByUserId(1).ToList();
            IList<Event> eventsSecondUser = _eventService.GetAllByUserId(2).ToList();

            Assert.IsNotNull(eventsFirstUser);
            Assert.AreEqual(2, eventsFirstUser.Count);

            Assert.IsNotNull(eventsSecondUser);
            Assert.AreEqual(1, eventsSecondUser.Count);
        }

        /// <summary>
        /// Проверка на получение автомобиля пользователя
        /// </summary>
        [TestMethod]
        public void CanReturnEventByIdWithAllowedUser()
        {
            Event eventModel = _eventService.GetById(1, 1);

            Assert.IsNotNull(eventModel);
            Assert.IsInstanceOfType(eventModel, typeof(Event));
            Assert.AreEqual(1, eventModel.EventId);
        }

        /// <summary>
        /// Проверка на ошибку при попытке получения чужого события
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CanNotReturnEventByIdWithNotAllowedUser()
        {
            Event eventModel = _eventService.GetById(3, 1);
        }

        /// <summary>
        /// Проверка на удаление события пользоваетелем
        /// </summary>
        [TestMethod]
        public void CanDeleteEventByIdWithAllowedUser()
        {
            bool success = _eventService.DeleteById(1, 1);
            var deletedEvent = eventList.Where(e => e.EventId == 1).FirstOrDefault();

            Assert.IsTrue(success);
            Assert.IsNull(deletedEvent);
        }

        /// <summary>
        /// Проверка на ошибку при попытке удаления чужого события
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CanNotDeleteEventByIdWithNotAllowedUser()
        {
            bool success = _eventService.DeleteById(3, 1);
        }

        /// <summary>
        /// Проверка на обновление события
        /// </summary>
        [TestMethod]
        public void CanUpdateEventWithAllowedUser()
        {
            Event newEvent = new Event()
            {
                EventId = 3,
                EventDate = new DateTime(2020, 11, 24, 13, 22, 33),
                EventTypeId = 2,
                CarId = 2,
                Milieage = 234511,
                Text = "New Text"
            };

            Event updatedEvent = _eventService.Update(newEvent, 2);

            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual(new DateTime(2020, 11, 24, 13, 22, 33), updatedEvent.EventDate);
            Assert.AreEqual(2, updatedEvent.EventTypeId);
            Assert.AreEqual(2, updatedEvent.CarId);
            Assert.AreEqual(2, updatedEvent.EventType.EventTypeId);
            Assert.AreEqual(2, updatedEvent.Car.CarId);
            Assert.AreEqual(234511, updatedEvent.Milieage);
            Assert.AreEqual("New Text", updatedEvent.Text);
        }

        /// <summary>
        /// Проверка на ошибку при попытке обновления чужого события
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CanNotUpdateEventWithNotAllowedUser()
        {
            Event newEvent = new Event()
            {
                EventId = 3,
                EventDate = new DateTime(2020, 11, 24, 13, 22, 33),
                EventType = new EventType()
                {
                    EventTypeId = 2
                },
                Car = new Car()
                {
                    CarId = 2
                },
                Milieage = 234511,
                Text = "New Text"
            };

            Event updatedEvent = _eventService.Update(newEvent, 1);
        }
    }
}
