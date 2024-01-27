using HealthSystem.Application.DTOs.Create;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IGenericRepository<T> where T : Base
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        void Insert(T obj);
        void Delete(T entity);
        void Save();
    }