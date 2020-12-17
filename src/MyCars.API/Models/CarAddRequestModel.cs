using System;
using System.ComponentModel.DataAnnotations;

namespace MyCars.API.Models
{
    public class CarAddRequestModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "Максимальная длина имени автомобиля 32 символа")]
        public string CarName { get; set; }
        [Required]
        public DateTime IssueYear { get; set; }
        [StringLength(32, ErrorMessage = "Максимальная длина VIN автомобиля 32 символа")]
        public string VIN { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Максимальная длина марки автомобиля 32 символа")]
        public string Brand { get; set; }
        [StringLength(10, ErrorMessage = "Максимальная длина VIN автомобиля 10 символов")]
        public string Numberplate { get; set; }
    }
}