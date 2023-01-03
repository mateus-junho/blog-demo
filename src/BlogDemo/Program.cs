using Blog.Models;
using BlogDemo.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;

namespace BlogDemo
{
    internal class Program
    {
        private const string CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Database=Blog;Trusted_Connection=True;MultipleActiveResultSets=true";

        static void Main(string[] args)
        { 
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                GetUsers(connection);
                //Getuser();
                //AddUser();
                //UpdateUser();
                //DeleteUser();
            }
        }

        public static void GetUsers(SqlConnection connection)
        {
            var repository = new UserRepository(connection);

            foreach (var user in repository.GetAll())
            {
                Console.WriteLine(user.Name);
            }
        }

        public static void GetUser(SqlConnection connection, int id)
        {
            var user = connection.Get<User>(id);
            Console.WriteLine(user.Name);
        }

        public static void AddUser(SqlConnection connection)
        {
            var user = new User()
            {
                Name = "Mateus Almeida",
                Email = "mateus@email.com",
                PasswordHash = "HASH",
                Bio = "my bio",
                Image = "https://",
                Slug = "mateus-almeida",
            };

            connection.Insert(user);
            Console.WriteLine("user created");
        }

        public static void UpdateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Id = 2,
                Name = "Mateus Almeida",
                Email = "mateus@email.com",
                PasswordHash = "HASH",
                Bio = "my bio edited",
                Image = "https://",
                Slug = "mateus-almeida",
            };

            connection.Update(user);
            Console.WriteLine("user updated");
        }

        public static void DeleteUser(SqlConnection connection)
        {
            var user = connection.Get<User>(2);
            connection.Delete(user);
            Console.WriteLine("user deleted");
        }
    }
}
