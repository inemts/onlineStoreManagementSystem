using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using ShopApp.Classes;
using ShopApp.Interfaces;

namespace CartridgeManagementSystem.Classes
{
    internal class Database : IRepository
    {
        private static Database _database;

        SqliteConnection connection = new SqliteConnection("Data Source=ShopApp.db");
        private Database(){}
        public static Database GetDatabase()
        {
            if (_database == null)
            {
                _database = new Database();
            }

            return _database;
        }
        
        /// <summary>
        /// Функция получения роли пользователя по его логину и паролю из БД
        /// </summary>
        /// <param name="userLogin">Логин пользователя</param>
        /// <param name="userPassword">Пароль пользователя</param>
        /// <returns>Роль пользователя</returns>
        public List<string> GetUserRoleByLoginPassword(string userLogin, string userPassword)
        {

          
            List<string> userData = new List<string>(); 


            connection.Open();
            string sqlExpression = $"SELECT Users.Id, Roles.Name FROM Users JOIN Roles ON Users.IdRole = Roles.Id WHERE Users.Login = '{userLogin}' AND Users.Password = '{userPassword}'";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        userData.Add(reader.GetInt32(0).ToString());
                        userData.Add(reader.GetString(1));
                    }
                }

                else
                {
                    userData.Add("null");
                    return userData;
                }
            }

            connection.Close();

            return userData;
        }

        public void AddNewBuyer(string fullName, string login, string password)
        {
            connection.Open();
            string sqlExpression = $"INSERT INTO Users (FullName, Login, Password, IdRole) VALUES ('{fullName}', '{login}', '{password}', 3)";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<ProductModel> GetListOfProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            connection.Open();
            string sqlExpression = $"SELECT * FROM Products";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        ProductModel product = new ProductModel();
                        product.Id = reader.GetInt32(0);
                        product.Name = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Price = reader.GetInt32(3);
                        product.IdCategory = reader.GetInt32(4);
                        
                        if (reader.GetInt32(5) == 1)
                        {
                            product.ContainsInWarehouse = "На складе";
                        }
                        else
                        {
                            product.ContainsInWarehouse = "На витрине";
                        }

                        products.Add(product);

                    }
                }

            }
            

            foreach (ProductModel prod in products)
            {
                sqlExpression = $"SELECT Name FROM Category WHERE Category.Id = {prod.IdCategory}";
                command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            prod.Category = reader.GetString(0);
                        }
                    }
                }
            }

            connection.Close();

            return products;
        }

        public List<ProductModel> GetListOfProductsForBuyer()
        {
            List<ProductModel> products = new List<ProductModel>();

            connection.Open();
            string sqlExpression = $"SELECT * FROM Products";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        ProductModel product = new ProductModel();
                        product.Id = reader.GetInt32(0);
                        product.Name = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Price = reader.GetInt32(3);
                        product.IdCategory = reader.GetInt32(4);

                        if (reader.GetInt32(5) == 1)
                        {
                            product.ContainsInWarehouse = "На складе";
                        }
                        else
                        {
                            product.ContainsInWarehouse = "На витрине";
                            products.Add(product);
                        }
                    }
                }

            }

            foreach (ProductModel prod in products)
            {
                sqlExpression = $"SELECT Name FROM Category WHERE Category.Id = {prod.IdCategory}";
                command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            prod.Category = reader.GetString(0);

                        }
                    }

                }
            }

            connection.Close();

            return products;
        }

        public void AddNewProduct(string name, string description, int price, string category, string containsInWareHouse)
        {
            connection.Open();

            int idContains = containsInWareHouse == "На складе" ? 1 : 0;
            string sqlExpression = $"INSERT INTO Products (Name, Description, Price, IdCategory, IdContainsWarehouse) VALUES ('{name}', '{description}', {price}, (SELECT Category.Id FROM Category WHERE Category.Name = '{category}'), {idContains})";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteProduct(int id)
        {
            connection.Open();

            string sqlExpression = $"DELETE FROM Products WHERE Products.Id = {id}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<UserModel> GetListOfUsers()
        {
            List<UserModel> users = new List<UserModel> ();

            connection.Open();
            string sqlExpression = $"SELECT * FROM Users";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        UserModel user = new UserModel();

                        user.Id = reader.GetInt32(0); ;
                        user.FullName = reader.GetString(1);
                        user.Login = reader.GetString(2);
                        user.Password = reader.GetString(3);

                        if (reader.GetInt32(4) == 1)
                        {
                            user.Role = "Администратор";
                        }

                        else if (reader.GetInt32(4) == 2)
                        {
                            user.Role = "Менеджер";
                        }

                        else
                        {
                            user.Role = "Покупатель";
                        }

                        users.Add(user);
                    }
                }

            }

            connection.Close();
            return users;
        }

        public void DeleteUser(int id)
        {
            connection.Open();

            string sqlExpression = $"DELETE FROM Users WHERE Products.Id = {id}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddNewUser(string fullName, string login, string password, string role)
        {
            connection.Open();

            string sqlExpression = $"INSERT INTO Users(FullName, Login, Password, IdRole) VALUES('{fullName}', '{login}', '{password}', (SELECT Roles.Id FROM Roles WHERE Roles.Name = '{role}'))";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditProduct(string name, string description, int price, string category, string containsInWareHouse, int id)
        {
            connection.Open();

            int idContains = containsInWareHouse == "На складе" ? 1 : 0;
            string sqlExpression = $"UPDATE Products SET Name = '{name}', Description = '{description}', Price = {price}, IdCategory = (SELECT Category.Id FROM Category WHERE Category.Name = '{category}'), IdContainsWarehouse = {idContains} WHERE Products.id = {id}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<ProductModel> GetListForBasket(int idUser)
        {
            List<ProductModel> products = new List<ProductModel>(); 

            connection.Open();
            string sqlExpression = $"SELECT Orders.Id, Products.Name, Products.Price FROM Orders JOIN Products ON Orders.IdProduct = Products.Id WHERE Orders.IdUser = {idUser}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        products.Add(new ProductModel() {Id = reader.GetInt32(0), Name = reader.GetString(1), Price = reader.GetInt32(2)});
                        
                    }
                }
            }

            connection.Close();

            return products;
        }

        public void AddToBasket(int idProduct, int idUser)
        {
            connection.Open();

            string sqlExpression = $"INSERT INTO Orders(IdProduct, IdUser) VALUES({idProduct}, {idUser})";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        

        public void DeleteFromBasket(int idUser)
        {
            connection.Open();

            string sqlExpression = $"DELETE FROM Orders WHERE Orders.IdUser = {idUser}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

      
    }
}
