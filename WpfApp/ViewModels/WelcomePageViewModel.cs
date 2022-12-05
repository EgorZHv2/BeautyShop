using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.States;

namespace WpfApp.ViewModels
{
    public class WelcomePageViewModel:BaseViewModel
    {
        public  WelcomePageViewModel()
        {
            Password = "0000";
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand Login
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (Password == "0000")
                    {
                        IdentityState.Role = "Admin";
                        MessageBox.Show("Вы успешно вошли как администратор");
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                });
            }
        }
    }
}
