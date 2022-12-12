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
using System.Windows.Navigation;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.States;
using WpfApp.Views.Pages;
using WpfApp.Views.Windows;

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
                model.Description = s.Description;
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
                    if (IdentityState.Role == "Admin")
                    {
                        EditServiceWindow window = new EditServiceWindow();
                        EditServiceWindowViewModel viewmodel = new EditServiceWindowViewModel();
                        viewmodel.Service = new CreateServiceModel()
                        {
                            Id = SelectedService.Id,
                            Title = SelectedService.Title,
                            Description = SelectedService.Description,
                            Cost = SelectedService.СrossedCost,
                            Discount = SelectedService.Discount,
                            DurationInSeconds = SelectedService.DurationInMinutes,
                            ImgPath = SelectedService.ImgPath
                        };
                        viewmodel.ImgPath = SelectedService.ImgPath;
                        window.DataContext = viewmodel;

                        window.ShowDialog();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Сначала войдите как админ");
                    }
                    


                });
            }
        }
        public ICommand DeleteService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (IdentityState.Role == "Admin")
                    {
                        var service = ApplicationDbContext.GetContext().Service.Find(SelectedService.Id);
                        ApplicationDbContext.GetContext().Service.Remove(service);
                        ApplicationDbContext.GetContext().SaveChanges();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Сначала войдите как админ");
                    }
                   

                });
            }
        }

        public ICommand AddClientToService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (IdentityState.Role == "Admin")
                    {
                        AddClientToServiceWindow window = new AddClientToServiceWindow();
                        AddClientToServiceWindowViewModel viewmodel = new AddClientToServiceWindowViewModel();
                        Service service = ApplicationDbContext.GetContext().Service.Find(SelectedService.Id);
                        service.DurationInSeconds /= 60;
                        viewmodel.Service = service;
                        window.DataContext = viewmodel;
                        window.ShowDialog();
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
