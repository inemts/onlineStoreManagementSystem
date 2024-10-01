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
    /// Логика взаимодействия для EditingProduct.xaml
    /// </summary>
    public partial class EditingProduct : Window
    {
        private ProductModel _product;
        public EditingProduct(ProductModel product)
        {
            InitializeComponent();
            NameTextBox.Text = product.Name;
            DescriptionTextBox.Text = product.Description;
            PriceTextBox.Text = product.Price.ToString();

            _product = product;
            
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == string.Empty || DescriptionTextBox.Text == string.Empty || PriceTextBox.Text == string.Empty || CategoryComboBox.SelectedItem == null || ContainsInWarehouseComboBox.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Database.GetDatabase().EditProduct(NameTextBox.Text, DescriptionTextBox.Text, int.Parse(PriceTextBox.Text), CategoryComboBox.Text, ContainsInWarehouseComboBox.Text, _product.Id );
                this.Close();
            }
        }
    }
}
