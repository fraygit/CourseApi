using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApi.Data.Interface;
using CourseApi.Data.Model;
using CourseCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace CourseTest
{
    [TestClass]
    public class TestCourseService
    {
        [TestMethod]
        public async Task TestValidationTitle()
        {
            var courseRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(courseRepository.Object);

            var response = await courseService.Add(new CourseCore.Model.RequestAddCourse
            {
                Title = "",
                Description = "sss",
                Type = "public"
            });

            Assert.AreEqual(response.ResponseCode, -1);
        }

        [TestMethod]
        public async Task TestValidationDescription()
        {
            var courseRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(courseRepository.Object);

            var response = await courseService.Add(new CourseCore.Model.RequestAddCourse
            {
                Title = "sss",
                Description = "",
                Type = "public"
            });

            Assert.AreEqual(response.ResponseCode, -1);
        }

        [TestMethod]
        public async Task TestValidationType()
        {
            var courseRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(courseRepository.Object);

            var response = await courseService.Add(new CourseCore.Model.RequestAddCourse
            {
                Title = "sss",
                Description = "sds",
                Type = ""
            });

            Assert.AreEqual(response.ResponseCode, -1);
        }

        [TestMethod]
        public async Task TestTitleMax()
        {
            var courseRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(courseRepository.Object);

            var response = await courseService.Add(new CourseCore.Model.RequestAddCourse
            {
                Title = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                Description = "sds",
                Type = "OtherType"
            });

            Assert.AreEqual(response.ResponseCode, -2);
        }

        [TestMethod]
        public async Task TestValidationInvalidType()
        {
            var courseRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(courseRepository.Object);

            var response = await courseService.Add(new CourseCore.Model.RequestAddCourse
            {
                Title = "sss",
                Description = "sds",
                Type = "OtherType"
            });

            Assert.AreEqual(response.ResponseCode, -3);
        }

        [TestMethod]
        public async Task TestCourseCode()
        {
            var courseRepository = new Mock<ICourseRepository>();
            courseRepository.Setup(n => n.CreateSync(new CourseApi.Data.Model.Course())).Returns(Task.FromResult(true));

            var courseCode = string.Format("TESTCODE{0}", DateTime.UtcNow.ToString("yyyyMMdd"));

            var courseService = new CourseService(courseRepository.Object);

            var response = await courseService.Add(new CourseCore.Model.RequestAddCourse
            {
                Title = "Test Code",
                Description = "sds",
                Type = "private"
            });

            Assert.AreEqual(response.CourseCode, courseCode);
        }

        [TestMethod]
        public async Task TestRecentlyAdded()
        {
            var courseRepository = new Mock<ICourseRepository>();

            var courses = new List<Course>();
            courses.Add(new Course
            {
                Title = "2",               
            });
            courses.Add(new Course
            {
                Title = "1",
            });
            courses.Add(new Course
            {
                Title = "b",
            });
            courses.Add(new Course
            {
                Title = "a",
            });
            courses.Add(new Course
            {
                Title = "c",
            });
            courseRepository.Setup(n => n.GetRecentlyAdded()).Returns(Task.FromResult(courses));
            var courseService = new CourseService(courseRepository.Object);

            var response = await courseService.ListRecentlyAdded();

            Assert.AreEqual(response.ElementAt(0).Title, "1");
            response.GetEnumerator().MoveNext();
            Assert.AreEqual(response.ElementAt(1).Title, "2");
            response.GetEnumerator().MoveNext();
            Assert.AreEqual(response.ElementAt(2).Title, "a");
        }
    }
}
