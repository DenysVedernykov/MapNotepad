using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Repository
{
    public interface IRepository
    {
        Task<AOResult<int>> InsertAsync<T>(T entity) where T : IEntityBase, new();
        Task<AOResult<int>> UpdateAsync<T>(T entity) where T : IEntityBase, new();
        Task<AOResult<int>> DeleteAsync<T>(T entity) where T : IEntityBase, new();
        Task<AOResult<List<T>>> GetAllRowsAsync<T>() where T : IEntityBase, new();
    }
}
