using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCars.Identity.Models
{
    public class ApplicationRole : IdentityRole<int, IdentityUserRole<int>>
    {
    }
}
