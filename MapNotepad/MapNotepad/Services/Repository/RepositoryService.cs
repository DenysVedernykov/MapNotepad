using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {

        #region -- Private properties --

        private Lazy<SQLiteAsyncConnection> _database;

        #endregion

        public RepositoryService()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapNotepad.db3");
                var database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<User>().Wait();
                database.CreateTableAsync<UserPin>().Wait();
                database.CreateTableAsync<PhotoPin>().Wait();
                database.CreateTableAsync<Event>().Wait();

                return database;
            });
        }

        #region -- IRepositoryService implementation --

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllRowsAsync<T>() where T : IEntityBase, new()
        {
            Task<List<T>> result = _database.Value.Table<T>().ToListAsync();
            return result;
        }

        #endregion

    }
}
