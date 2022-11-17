using Blog.Models;
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
            ReadUsers();
        }

        public static void ReadUsers()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var users = connection.GetAll<User>();

                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }
        }
    }
}
