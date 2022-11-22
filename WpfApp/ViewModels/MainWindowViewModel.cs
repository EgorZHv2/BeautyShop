using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Commands;
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
           
        }
        public ICommand GoToAddServicePage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AddServicePage pg = new AddServicePage();
                    PageInFrame = pg;
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
                    //AddClientToServicePage pg = new AddClientToServicePage();
                    //PageInFrame = pg;
                });
            }
        }
        public ICommand GoToOrdersListPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                   OrdersListPage pg = new OrdersListPage();
                    PageInFrame = pg;
                });
            }
        }
    }
}
