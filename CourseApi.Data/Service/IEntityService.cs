using CourseApi.Data.Entities.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Data.Service
{
    public interface IEntityService<T> where T : IMongoEntity
    {
        void Create(T entity);
        Task<List<T>> ListAll();
        Task<bool> CreateSync(T entity);
        Task<DeleteResult> Delete(string id);
        Task<ReplaceOneResult> Update(string id, T entity);
        Task<T> Get(string id);
    }
}
