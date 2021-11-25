using MapNotepad.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MapNotepad.Services.Repository
{
    public interface IRepositoryService
    {
        Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new();

        Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new();

        Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new();

        Task<List<T>> GetAllRowsAsync<T>() where T : IEntityBase, new();

        Task<int> ExecuteScalarAsync(string querty);
    }
}
