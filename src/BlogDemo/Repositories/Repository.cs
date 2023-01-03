using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly SqlConnection connection;

        public Repository(SqlConnection connection)
            => this.connection = connection;

        public IEnumerable<T> Get() => connection.GetAll<T>();

        public T Get(int id) => connection.Get<T>(id);

        public void Create(T model) => connection.Insert<T>(model);

        public void Update(T model) => connection.Update<T>(model);

        public void Delete(T model) => connection.Delete<T>(model);

        public void Delete(int id)
        {
            var model = connection.Get<T>(id);
            connection.Delete<T>(model);
        }
    }
}
