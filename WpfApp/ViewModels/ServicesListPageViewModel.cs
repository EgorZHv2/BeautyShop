using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class ServicesListPageViewModel:BaseViewModel
    {
        public   ServicesListPageViewModel()
        {
            ServicesListItemModel model1 = new ServicesListItemModel();
            ServicesListItemModel model2 = new ServicesListItemModel();
            ServicesListItemModel model3 = new ServicesListItemModel();
            ServicesListItemModel model4 = new ServicesListItemModel();

            Services = new List<ServicesListItemModel> { model1, model2, model3, model4 };
        }
        private List<ServicesListItemModel> services;
        public List<ServicesListItemModel> Services
        {
            get { return services; }
            set { services = value;
                OnPropertyChanged();
            }
        }
    }
}
