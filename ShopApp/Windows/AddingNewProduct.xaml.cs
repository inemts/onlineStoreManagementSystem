using CartridgeManagementSystem.Classes;
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

namespace CartridgeManagementSystem.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddingNewCartridge.xaml
    /// </summary>
    public partial class AddingNewCartridge : Window
    {
        public AddingNewCartridge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод добавления нового товара
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>

        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == string.Empty || DescriptionTextBox.Text == string.Empty || PriceTextBox.Text == string.Empty || CategoryComboBox.SelectedItem == null || ContainsInWarehouseComboBox.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Database.GetDatabase().AddNewProduct(NameTextBox.Text, DescriptionTextBox.Text, int.Parse(PriceTextBox.Text), CategoryComboBox.Text, ContainsInWarehouseComboBox.Text);
                MessageBox.Show("Добавлен новый товар");
                this.Close();
            }
        }
    }
}
