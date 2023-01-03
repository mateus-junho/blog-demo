
using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace BlogDemo.Repositories
{
    public class UserRepository
    {
        private readonly SqlConnection connection;

        public UserRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<User> GetAll()
            => connection.GetAll<User>();

        public User Get(int id)
            => connection.Get<User>(id);

        public void Add(User user)
            => connection.Insert<User>(user);
    }
}
