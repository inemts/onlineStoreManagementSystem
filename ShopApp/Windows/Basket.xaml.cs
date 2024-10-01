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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        private int _id;
        private Database db = Database.GetDatabase();
        public Basket(int idUser)
        {
            InitializeComponent();
            _id = idUser;

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
                    if (product.Name.Contains(userSearch.Text) || product.Price.ToString().Contains(userSearch.Text))
                    {
                        products.Add(product);
                    }
                }
                productsList.ItemsSource = null;
                productsList.ItemsSource = products;
            }
        }

        private void Leave(object sender, RoutedEventArgs e)
        {
            BuyerWindow window = new BuyerWindow(_id);
            window.Show();
            this.Close();
        }

        private void UpdateTable()
        {
            productsList.ItemsSource = null;
            productsList.ItemsSource = db.GetListForBasket(_id);
        }

        private void DeleteFromBasket(object sender, RoutedEventArgs e)
        {
            ProductModel product = (ProductModel) productsList.SelectedItem;

            if (product == null)
            {
                MessageBox.Show("Для удаления необходимо выбрать элемент");
            }

            else
            {
                db.DeleteFromBasket(_id);
            }


        }

        private void BuyProducts(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы успешно приобрели товары!");

            db.DeleteFromBasket(_id);

            UpdateTable();
        }
    }
}
