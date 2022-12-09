using Infrastructure.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp.Commands;
using WpfApp.Models;


namespace WpfApp.ViewModels
{
    public class ServicesListPageViewModel:BaseViewModel
    {
        List<ServicesListItemModel> list = new List<ServicesListItemModel>();
        public   ServicesListPageViewModel()
        {

            LoadData();
        }

        private ServicesListItemModel selectedservice;
        public ServicesListItemModel SelectedService
        {
            get { return selectedservice; }
            set { selectedservice = value;
                OnPropertyChanged();
            }
        }

        private void LoadData()
        {
            list = new List<ServicesListItemModel>();
            foreach (Service s in ApplicationDbContext.GetContext().Service.ToList())
            {
               
                ServicesListItemModel model = new ServicesListItemModel();
                model.Id = s.ID;

                model.Title = s.Title;

                model.Discount = s.Discount;
                model.DurationInMinutes = s.DurationInSeconds / 60;
                model.ImgPath = "\\Resources\\" + s.MainImagePath;

                //if (File.Exists(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\..\\" + "\\Resources\\" + s.MainImagePath)))
                //{
                //    model.ImgPath = "..\\..\\Resources\\ServiceImages\\" + s.MainImagePath;

                //}
                //else
                //{
                //    model.ImgPath = "..\\..\\Resources\\Img\\beauty_logo.png";
                //}

                //if (!String.IsNullOrEmpty(s.MainImagePath))
                //{
                //    model.ImgPath = "..\\..\\Resources\\ServiceImages\\" + s.MainImagePath;
                //}
                //else
                //{
                //    model.ImgPath = "/Resources/Img/beauty_logo.png";
                //}


                if (s.Discount == 0)
                {
                    model.DiscountVisibility = Visibility.Hidden;
                    model.Cost = Math.Round(s.Cost, 2);
                }
                else
                {
                    model.DiscountVisibility = Visibility.Visible;
                    model.СrossedCost = Math.Round(s.Cost, 2);
                    model.Cost = Math.Round(model.СrossedCost * ((decimal)((100 - (decimal)model.Discount) / 100)), 2);

                }
                list.Add(model);


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

        public ICommand EditService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                   

                });
            }
        }
        public ICommand DeleteService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var service = ApplicationDbContext.GetContext().Service.Find(SelectedService.Id);
                    ApplicationDbContext.GetContext().Service.Remove(service);
                    ApplicationDbContext.GetContext().SaveChanges();
                    LoadData();

                });
            }
        }

    }
}
