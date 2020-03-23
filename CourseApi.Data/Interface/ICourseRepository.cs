using CourseApi.Data.Model;
using CourseApi.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Data.Interface
{
    public interface ICourseRepository : IEntityService<Course>
    {
        Task<List<Course>> GetRecentlyAdded();
        Task<Course> GetByCourseCode(string courseCode);
    }
}
