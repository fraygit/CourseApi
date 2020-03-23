using CourseApi.Data.Interface;
using CourseApi.Data.Model;
using CourseApi.Data.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Data.Repository
{
    public class CourseRepository : EntityService<Course>, ICourseRepository
    {
        public async Task<List<Course>> GetRecentlyAdded()
        {
            try
            {
                var builder = Builders<Course>.Filter;
                var recentCourses = await ConnectionHandler.MongoCollection.Find(new BsonDocument()).SortByDescending(n => n.DateCreated).Limit(5).ToListAsync();
                if (recentCourses.Any())
                    return recentCourses;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Course> GetByCourseCode(string courseCode)
        {
            try
            {
                var builder = Builders<Course>.Filter;
                var filter = builder.Eq("CourseCode", courseCode);
                var courses = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
                if (courses.Any())
                    return courses.FirstOrDefault();
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
