using ShopApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Interfaces
{
    internal interface IRepository
    {
        public List<string> GetUserRoleByLoginPassword(string userLogin, string userPassword);
        public void AddNewBuyer(string fullName, string login, string password);
        public List<ProductModel> GetListOfProducts();
        public List<ProductModel> GetListOfProductsForBuyer();
        public void AddNewProduct(string name, string description, int price, string category, string containsInWareHouse);
        public void DeleteProduct(int id);
        public List<UserModel> GetListOfUsers();
        public void DeleteUser(int id);
        public void AddNewUser(string fullName, string login, string password, string role);
        public void EditProduct(string name, string description, int price, string category, string containsInWareHouse, int id);
        public List<ProductModel> GetListForBasket(int idUser);
        public void AddToBasket(int idProduct, int idUser);
        public void DeleteFromBasket(int idUser);

    }
}
