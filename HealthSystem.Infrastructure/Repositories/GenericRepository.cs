using HealthSystem.Domain.Entities;
using HealthSystem.Domain.Interfaces;
using HealthSystem.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HealthSystem.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Base
{
    private readonly PatientsContext _patientsContext;

    public GenericRepository(PatientsContext patientsContext)
    {
        _patientsContext = patientsContext;
    }

    public async Task<List<T>> GetAll()
    {
        return await _patientsContext.Set<T>().ToListAsync(); 
    }

    public async Task<T> GetByIdAsync(Guid Id)
    {
        return await _patientsContext.Set<T>().FirstOrDefaultAsync(p => p.Id == Id);
    }

    public void Insert(T obj)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        _patientsContext.Set<T>().Remove(entity); 
    }
}
