using Infrastructure.Data;
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
            List<ServicesListItemModel> list = new List<ServicesListItemModel>();
            foreach( Service s in ApplicationDbContext.GetContext().Service.ToList())
            {
                
                list.Add(new ServicesListItemModel
                {
                    Id = s.ID,
                    Title = s.Title,
                    Cost = Math.Round(s.Cost,2),
                    Discount = s.Discount,
                    DurationInMinutes = s.DurationInSeconds/60,
                    ImgPath = "/Resources/ServiceImages/"+ s.MainImagePath
                    

                });
                
            }
            Services = list;
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
