using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    internal class AddServicePageViewModel:BaseViewModel
    {
        private CreateServiceModel service = new CreateServiceModel();
        public CreateServiceModel Service
        {
            get { return service; }
            set
            {
                service = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ApplicationDbContext.GetContext().Service.Add(new Service()
                    {
                        Cost = service.Cost,
                        Discount = service.Discount,
                        DurationInSeconds = service.DurationInSeconds*60,
                        ID = service.Id,
                        MainImagePath = service.ImgPath,
                        Title = service.Title
                    });
                    ApplicationDbContext.GetContext().SaveChanges();
                });
            }
        }
    }

}
