using MapNotepad.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MapNotepad.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private Lazy<SQLiteAsyncConnection> _database;

        public RepositoryService()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapNotepad.db3");
                var database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<User>().Wait();
                database.CreateTableAsync<UserPin>().Wait();

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
            return _database.Value.Table<T>().ToListAsync();
        }

        public Task<int> ExecuteScalarAsync(string querty)
        {
            return _database.Value.ExecuteScalarAsync<int>(querty);
        }

        #endregion

    }
}
