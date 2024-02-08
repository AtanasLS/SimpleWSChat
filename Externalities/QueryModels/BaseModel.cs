using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Externalities.QueryModels
{
    public class BaseModel
    {
        [Required]
        public int id { get; set; }
    }
}