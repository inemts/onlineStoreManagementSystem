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

namespace ShopApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddingUser.xaml
    /// </summary>
    public partial class AddingUser : Window
    {
        public AddingUser()
        {
            InitializeComponent();
        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty || RoleComboBox.SelectedItem == null || FullNameTextBox.Text == string.Empty )
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Database.GetDatabase().AddNewUser(FullNameTextBox.Text, LoginTextBox.Text, PasswordTextBox.Text, RoleComboBox.Text);
                MessageBox.Show("Пользователь добавлен.");
                this.Close();
            }
        }
    }
}
