using CourseApi.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using CourseCore.Model;
using CourseCore;
using CourseApi.Data.Model;

namespace CourseWebApi.Controllers
{
    public class CourseController : ApiController
    {
        private ICourseRepository _courseRepository;

        public CourseController(ICourseRepository _courseRepository)
        {
            this._courseRepository = _courseRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> ListRecentlyAdded()
        {
            var courseService = new CourseService(_courseRepository);
            return await courseService.ListRecentlyAdded();
        }

        [HttpPost]
        public async Task<ResponseAddCourse> Add(RequestAddCourse request)
        {
            var courseService = new CourseService(_courseRepository);
            return await courseService.Add(request);

        }


    }
}
