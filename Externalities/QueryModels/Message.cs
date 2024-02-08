using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Externalities.QueryModels
{
    public class Message : BaseModel
    {
        public string? content { get; set; }
        public DateTimeOffset timestapm { get; set; }
        public int userId { get; set; }
        public int roomId { get; set; }
    }
}