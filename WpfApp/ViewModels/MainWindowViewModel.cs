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
        public ICommand AddService
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
    }
}
