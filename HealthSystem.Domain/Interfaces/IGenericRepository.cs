using HealthSystem.Application.DTOs.Create;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IGenericRepository<T> where T : Base
    {
        Task<List<T>> GetAll();
        Task<T> GetByIdAsync(Guid id);
        void Insert(T obj);
        Task Delete(T entity);
        void Save();
    }