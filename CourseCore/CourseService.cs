using CourseApi.Data.Interface;
using CourseApi.Data.Model;
using CourseCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseCore
{
    public class CourseService
    {
        private ICourseRepository _courseRepository;

        public CourseService(ICourseRepository _courseRepository)
        {
            this._courseRepository = _courseRepository;
        }

        // assumption: the list is a separate end point
        public async Task<IEnumerable<Course>> ListRecentlyAdded()
        {
            var recentAddedCourses = await _courseRepository.GetRecentlyAdded();
            if (recentAddedCourses != null)
            {
                return recentAddedCourses.OrderBy(n => n.Title);
            }
            return null;
        }

        public async Task<ResponseAddCourse> Add(RequestAddCourse request)
        {
            if (!string.IsNullOrEmpty(request.Title) && !string.IsNullOrEmpty(request.Description) && !string.IsNullOrEmpty(request.Type))
            {
                if (request.Title.Length <= 200)
                {
                    if (request.Type.ToLower() == "private" || request.Type.ToLower() == "public")
                    {
                        var courseCode = string.Format("{0}{1}", Regex.Replace(request.Title, @"\s+", "").ToUpper().Trim(), DateTime.UtcNow.ToString("yyyyMMdd"));

                        // I assume that it is not allowed to have an existing coursecode
                        var checkExistingCourseCode = await _courseRepository.GetByCourseCode(courseCode);
                        if (checkExistingCourseCode == null)
                        {

                            var newCourse = new CourseApi.Data.Model.Course
                            {
                                Title = request.Title,
                                Description = request.Description,
                                Type = request.Type.ToLower(),
                                CourseCode = courseCode
                            };

                            await _courseRepository.CreateSync(newCourse);
                            // assumption: returns the course code and the list is on a different end point
                            return new ResponseAddCourse
                            {
                                ResponseCode = 0,
                                CourseCode = courseCode
                            };
                        }
                        return new ResponseAddCourse
                        {
                            ResponseCode = -4,
                            ErrorMessage = "Course already existing."
                        };
                    }
                    return new ResponseAddCourse
                    {
                        ResponseCode = -3,
                        ErrorMessage = "Invalid course type."
                    };
                }
                return new ResponseAddCourse
                {
                    ResponseCode = -2,
                    ErrorMessage = "Title exceeds 2000 maximum characters."
                };
            }
            return new ResponseAddCourse
            {
                ResponseCode = -1,
                ErrorMessage = "Please fill in all required fields."
            };
        }
    }
}
