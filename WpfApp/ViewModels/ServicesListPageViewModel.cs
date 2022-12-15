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
        private List<ServicesListItemModel> searchlist = new List<ServicesListItemModel>();
       
        public   ServicesListPageViewModel()
        {
            Selectedsort = sorts[0];
            Selectedfilter = filters[0];
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
        private string search;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged();
                if (!String.IsNullOrEmpty(value))
                {
                    searchlist = list.Where(x => x.Title.ToLower().Contains(value.ToLower())).ToList();
                    Services = searchlist;
                }
                else
                {
                    Services = searchlist;
                }
            }
        }


        private List<FilterModel> filters = new List<FilterModel>()
        {
            new FilterModel { Id = 0, Name = "Фильтр" },
            new FilterModel { Id = 1, Name = "от 0 до 5%"},
            new FilterModel { Id = 2, Name = "от 5% до 15%"},
            new FilterModel { Id = 3, Name = "от 15% до 30%"},
            new FilterModel { Id = 4, Name = "от 30% до 70%"},
            new FilterModel { Id = 5, Name = "от 70% до 100%"}
        };
        public List<FilterModel> Filters
        {
            get { return filters; }
            set
            {
                filters = value;
                OnPropertyChanged();
            }
        }

        private FilterModel selectedfilter;
        public FilterModel Selectedfilter
        {
            get { return selectedfilter; }
            set
            {
                selectedfilter = value;
                OnPropertyChanged();
                if (selectedfilter.Id == 1)
                {
                    searchlist = list.Where(x => x.Discount >= 0 && x.Discount <= 5).ToList();
                }
                else if (selectedfilter.Id == 2)
                {
                    searchlist = list.Where(x => x.Discount > 5 && x.Discount <= 15).ToList();
                }
                else if (selectedfilter.Id == 3)
                {
                    searchlist = list.Where(x => x.Discount > 15 && x.Discount <= 30).ToList();
                }
                else if (selectedfilter.Id == 4)
                {
                    searchlist = list.Where(x => x.Discount > 30 && x.Discount <= 70).ToList();
                }
                else if (selectedfilter.Id == 5)
                {
                    searchlist = list.Where(x => x.Discount > 70 && x.Discount <= 100).ToList();
                }
                else
                {
                    searchlist = list;
                }

                Services = searchlist;
            }
        }

        private List<SortModel> sorts = new List<SortModel>() {
            new SortModel { Id = 0, Name = "Сортировка" },
            new SortModel { Id = 1, Name = "По возрастанию" },
            new SortModel { Id = 2, Name = "По убыванию" },};
        public List<SortModel> Sorts
        {
            get { return sorts; }
            set
            {
                sorts = value;
                OnPropertyChanged();
            }
        }

        private SortModel selectedsort;
        public SortModel Selectedsort
        {
            get { return selectedsort; }
            set
            {
                selectedsort = value; OnPropertyChanged();
                if (selectedsort.Id == 1)
                {
                    searchlist = list.OrderBy(x => x.Cost).ToList();
                    Services = searchlist;
                }
                else if (selectedsort.Id == 2)
                {
                    searchlist = list.OrderBy(x => x.Cost).Reverse().ToList();
                    Services = searchlist;
                }
            }

        }

    }
}
