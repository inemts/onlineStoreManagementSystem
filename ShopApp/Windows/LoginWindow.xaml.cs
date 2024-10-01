using CartridgeManagementSystem.Classes;
using ShopApp.Classes;
using ShopApp.Windows;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Database _database = Database.GetDatabase();
        public string userRole = "";
        public LoginWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Метод очищения текстовых полей
        /// </summary>
        /// <param name="sender">Объект строки TextBox</param>
        /// <param name="e">Объект события</param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == "логин" || (sender as TextBox).Text.ToLower() == "пароль")
            {
                (sender as TextBox).Text = string.Empty;
                (sender as TextBox).Foreground = Brushes.Black;
            }
        }

        /// <summary>
        /// Метод возврата стирающихся надписей "Логин" и "Пароль" на свои места в текстовых полях.
        /// </summary>
        /// <param name="sender">Объект строки TextBox</param>
        /// <param name="e">Объект события</param>
        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == string.Empty)
            {
                switch ((sender as TextBox).Name)
                {
                    case "LoginTextBox":
                        (sender as TextBox).Text = "Логин";
                        break;
                    case "PasswordTextBox":
                        (sender as TextBox).Text = "Пароль";
                        break;
                }
                (sender as TextBox).Foreground = Brushes.Gray;
            }
        }

        /// <summary>
        /// Метод входа в систему по логину и паролю.
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void Login(object sender, RoutedEventArgs e)
        {

            if ((LoginTextBox.Text != string.Empty && LoginTextBox.Text.ToLower() != "логин") &&
                (PasswordTextBox.Text != string.Empty && PasswordTextBox.Text.ToLower() != "пароль"))
            {
                if ((sender as Button).Content.ToString().ToLower() == "регистрация")
                {
                    List<string> fullNames = new List<string>
                    {
                        "Иван Иванов",
                        "Ольга Петрова",
                        "Александр Смирнов",
                        "Мария Попова",
                        "Сергей Кузнецов",
                        "Елена Козлова",
                        "Михаил Васильев",
                        "Анна Лебедева",
                        "Алексей Соколов",
                        "Наталья Гончарова",
                        "Дмитрий Медведев",
                        "Екатерина Романова",
                        "Владимир Краснов",
                        "Татьяна Фёдорова",
                        "Николай Колобов",
                        "Галина Волкова",
                        "Пётр Захаров",
                        "Елизавета Николаева",
                        "Андрей Комаров",
                        "Вероника Борисова"
                    };

                    Database.GetDatabase().AddNewBuyer(fullNames[new Random().Next(0, fullNames.Count)], LoginTextBox.Text, PasswordTextBox.Text);
                    this.Close();
                }

                else
                {

                    List<string> userData = _database.GetUserRoleByLoginPassword(LoginTextBox.Text, PasswordTextBox.Text);

                    if (userData[0] == "null")
                    {
                        this.Close();
                    }

                    else
                    {
                        switch (userData[1])
                        {
                            case "Администратор":
                                AdminWindow adminWindow = new AdminWindow("Администратор");
                                adminWindow.Show();
                                this.Close();
                                break;
                            case "Менеджер":
                                adminWindow = new AdminWindow("Менеджер");
                                adminWindow.Show();
                                this.Close();
                                break;
                            case "Покупатель":
                                BuyerWindow buyerWindow = new BuyerWindow(int.Parse(userData[0]));
                                buyerWindow.Show();
                                this.Close();
                                break;
                         
                        }


                    }
                }
               
            }

            
        }
    }
}
