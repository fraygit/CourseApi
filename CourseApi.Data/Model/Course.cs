using CourseApi.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Data.Model
{
    public class Course : MongoEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string CourseCode { get; set; }
    }
}
