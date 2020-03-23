using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCore.Model
{
    public class RequestAddCourse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
