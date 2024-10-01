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
    /// Логика взаимодействия для ListOfUsers.xaml
    /// </summary>
    public partial class ListOfUsers : Window
    {
        private Database db = Database.GetDatabase();
        public ListOfUsers(string userRole)
        {
            InitializeComponent();
            UpdateTable();
        }

        private void UpdateTable()
        {
            usersList.ItemsSource = null;
            usersList.ItemsSource = db.GetListOfUsers();
        }

        private void Leave(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow("Администратор");
            admin.Show();
            this.Close();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable();
            List<UserModel> users = new List<UserModel>();
            if (userSearch.Text == "" || userSearch.Text == string.Empty)
            {
                UpdateTable();
            }
            else
            {
                foreach (UserModel user in usersList.Items)
                {
                    //Проверка на содержание введенного текста пользователем во всех колонках в таблице.
                    if (user.FullName.Contains(userSearch.Text) || user.Login.Contains(userSearch.Text) || user.Password.ToString().Contains(userSearch.Text) || user.Role.Contains(userSearch.Text))
                    {
                        users.Add(user);
                    }
                }
                usersList.ItemsSource = null;
                usersList.ItemsSource = users;
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            UserModel selected = (UserModel)usersList.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Для удаления необходимо выбрать элемент");
            }

            else
            {
                db.DeleteUser(selected.Id);
                UpdateTable();
            }
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            AddingUser addingUser = new AddingUser();
            addingUser.ShowDialog();

            UpdateTable();
        }
    }
}
