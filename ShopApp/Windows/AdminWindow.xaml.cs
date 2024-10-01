using CartridgeManagementSystem.Classes;
using ShopApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShopApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Database db = Database.GetDatabase();
        private string role;
        public AdminWindow(string userRole)
        {
            InitializeComponent();
            UpdateTable();
            role = userRole;

            if (userRole == "Администратор")
            {
                AddNewProductButton.Visibility = Visibility.Visible;
                DeleteProductButton.Visibility = Visibility.Visible;
                EditProductButton.Visibility = Visibility.Visible;

            }

            else
            {
                AddNewProductButton.Visibility = Visibility.Hidden;
                DeleteProductButton.Visibility = Visibility.Hidden;
                EditProductButton.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateTable()
        {
            productsList.ItemsSource = null;
            productsList.ItemsSource = db.GetListOfProducts();
        }

        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            CartridgeManagementSystem.Windows.AddingNewCartridge addingNewProduct = new();
            addingNewProduct.ShowDialog();

            UpdateTable();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable();
            List<ProductModel> products = new List<ProductModel>();
            if (userSearch.Text == "" || userSearch.Text == string.Empty)
            {
                UpdateTable();
            }
            else
            {
                foreach (ProductModel product in productsList.Items)
                {
                    //Проверка на содержание введенного текста пользователем во всех колонках в таблице.
                    if (product.Name.Contains(userSearch.Text) || product.Description.Contains(userSearch.Text) || product.Price.ToString().Contains(userSearch.Text) || product.Category.Contains(userSearch.Text) || product.ContainsInWarehouse.Contains(userSearch.Text))
                    {
                        products.Add(product);
                    }
                }
                productsList.ItemsSource = null;
                productsList.ItemsSource = products;
            }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            ProductModel product = (ProductModel) productsList.SelectedItem;
            if (product == null)
            {
                MessageBox.Show("Для удаления необходимо выбрать элемент");
            }

            else
            {
                db.DeleteProduct(product.Id);
                UpdateTable();
            }
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            ProductModel product = (ProductModel)productsList.SelectedItem;
            if (product == null)
            {
                MessageBox.Show("Для редактирования необходимо выбрать элемент");
            }

            else
            {
                EditingProduct editing = new EditingProduct(product);
                editing.ShowDialog();
                UpdateTable();

            }
        }

        private void ShowUserList(object sender, RoutedEventArgs e)
        {
            ListOfUsers listUsers = new ListOfUsers(role);
            listUsers.Show();
            this.Close();
        }
    }
}
