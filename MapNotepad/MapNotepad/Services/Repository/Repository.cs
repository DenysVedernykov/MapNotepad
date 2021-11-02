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
    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> _database;

        public Repository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapNotepad.db3");
                var database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<User>().Wait();
                database.CreateTableAsync<Pin>().Wait();
                database.CreateTableAsync<PhotoPin>().Wait();
                database.CreateTableAsync<Events>().Wait();

                return database;
            });
        }

        public Task<AOResult<int>> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            Task<AOResult<int>> result = null;
            
            try
            {
                if (entity == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    var response = _database.Value.InsertAsync(entity);
                    if (response == null)
                    {
                        result.Result.SetFailure();
                    }
                    else
                    {
                        result.Result.SetSuccess(response.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception Repository InsertAsync", ex);
            }

            return result;
        }

        public Task<AOResult<int>> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            Task<AOResult<int>> result = null;

            try
            {
                if (entity == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    var response = _database.Value.UpdateAsync(entity);
                    if (response == null)
                    {
                        result.Result.SetFailure();
                    }
                    else
                    {
                        result.Result.SetSuccess(response.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception Repository UpdateAsync", ex);
            }

            return result;
        }
        public Task<AOResult<int>> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            Task<AOResult<int>> result = null;

            try
            {
                if (entity == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    var response = _database.Value.DeleteAsync(entity);
                    if (response == null)
                    {
                        result.Result.SetFailure();
                    }
                    else
                    {
                        result.Result.SetSuccess(response.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception Repository DeleteAsync", ex);
            }

            return result;
        }
        public Task<AOResult<List<T>>> GetAllRowsAsync<T>() where T : IEntityBase, new()
        {
            Task<AOResult<List<T>>> result = null;

            try
            {
                Task<List<T>> response = _database.Value.Table<T>().ToListAsync();
                if (response == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    result.Result.SetSuccess(response.Result);
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception Repository GetAllRowsAsync", ex);
            }

            return result;
        }
    }
}
