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
            var sqlConnection = new SqlConnection(CONNECTION_STRING);
            sqlConnection.Open();

            Console.Clear();
            Console.WriteLine("Blog menu");
            Console.WriteLine("__________");
            Console.WriteLine();
            Console.WriteLine("Tag manager");
            Console.WriteLine("1 - List tags");
            Console.WriteLine("2 - Add tag");
            Console.WriteLine("3 - Update tag");
            Console.WriteLine("4 - Delete tag");
            Console.WriteLine();
            var option = short.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 1:
                    LoadListTags(sqlConnection);
                    break;
                case 2:
                    LoadTagCreation(sqlConnection);
                    break;
                case 3:
                    LoadTagUpdate(sqlConnection);
                    break;
                case 4:
                    LoadTagDeletion(sqlConnection);
                    break;
                default: break;
            }

            Console.ReadKey();
            sqlConnection.Close();
        }

        public static void LoadListTags(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Tags List");
            Console.WriteLine("__________");
            ListTags(connection);
            Console.ReadKey();
        }

        private static void ListTags(SqlConnection connection)
        {
            var repository = new Repository<Tag>(connection);
            var tags = repository.Get();
            foreach (var item in tags)
                Console.WriteLine($"{item.Id} - {item.Name} ({item.Slug})");
        }

        public static void LoadTagCreation(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("New tag");
            Console.WriteLine("__________");
            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            CreateTag(connection, new Tag
            {
                Name = name,
                Slug = slug
            });

            Console.ReadKey();
        }

        public static void CreateTag(SqlConnection connection, Tag tag)
        {
            try
            {
                var repository = new Repository<Tag>(connection);
                repository.Create(tag);
                Console.WriteLine("Tag successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating tag");
                Console.WriteLine(ex.Message);
            }
        }

        public static void LoadTagUpdate(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Tag update");
            Console.WriteLine("__________");
            Console.Write("Id: ");
            var id = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            UpdateTag(connection, new Tag
            {
                Id = int.Parse(id),
                Name = name,
                Slug = slug
            });

            Console.ReadKey();
        }

        public static void UpdateTag(SqlConnection connection, Tag tag)
        {
            try
            {
                var repository = new Repository<Tag>(connection);
                repository.Update(tag);
                Console.WriteLine("Tag updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating tag");
                Console.WriteLine(ex.Message);
            }
        }

        public static void LoadTagDeletion(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Excluir uma tag");
            Console.WriteLine("-------------");
            Console.Write("Qual o id da Tag que deseja exluir? ");
            var id = Console.ReadLine();

            DeleteTag(connection, int.Parse(id));

            Console.ReadKey();
        }

        public static void DeleteTag(SqlConnection connection, int id)
        {
            try
            {
                var repository = new Repository<Tag>(connection);
                repository.Delete(id);
                Console.WriteLine("Tag deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting tag");
                Console.WriteLine(ex.Message);
            }
        }

        private static void ReadWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var users = repository.GetWithRoles();

            foreach (var user in users)
            {
                Console.WriteLine(user.Email);
                foreach (var role in user.Roles) Console.WriteLine($" - {role.Slug}");
            }
        }
    }
}
