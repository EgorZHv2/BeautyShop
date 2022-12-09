using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.States;
using WpfApp.ViewModels;
using WpfApp.Views.Pages;

namespace WpfApp.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
       
        private Page pageInFrame;
        public Page PageInFrame
        {
            get { return pageInFrame; }
            set { pageInFrame = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            PageInFrame = new ServicesListPage();
        }
        public ICommand GoToAddServicePage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (IdentityState.Role == "Admin")
                    {
                        AddServicePage pg = new AddServicePage();
                        PageInFrame = pg;
                    }
                    else
                    {
                        MessageBox.Show("Сначала войдите как админ");
                    }
                    
                });
            }
        }
        public ICommand GoToServicesListPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                   ServicesListPage pg = new ServicesListPage();
                    PageInFrame = pg;
                });
            }
        }
        public ICommand GoToAddClientToServicePage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    WelcomePage pg = new WelcomePage();
                    PageInFrame = pg;

                });
            }
        }
        public ICommand GoToOrdersListPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (IdentityState.Role == "Admin")
                    {
                        OrdersListPage pg = new OrdersListPage();
                        PageInFrame = pg;
                    }
                    else
                    {
                        MessageBox.Show("Сначала войдите как админ");
                    }
                    
                });
            }
        }
    }
}
