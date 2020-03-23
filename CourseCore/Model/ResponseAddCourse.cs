using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCore.Model
{
    public class ResponseAddCourse
    {
        public int ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
        public string CourseCode { get; set; }
    }
}
