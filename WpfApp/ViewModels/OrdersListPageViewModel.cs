using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class OrdersListPageViewModel:BaseViewModel
    {
        public OrdersListPageViewModel()
        {
            var clientservice = ApplicationDbContext.GetContext().ClientService;

            List<OrdersListModel> upcomingEntries = new List<OrdersListModel>();

            foreach (var item in clientservice)
            {
               
                    upcomingEntries.Add(new OrdersListModel()
                    {
                        ClientFirstName = item.Client.FirstName,
                        ClientLastName = item.Client.LastName,
                        ClientPatronymic = item.Client.Patronymic,
                        Email = item.Client.Email,
                        Phone = item.Client.Phone,
                        NameService = item.Service.Title.Length > 20 ? item.Service.Title.Substring(0, 20) + "..." : item.Service.Title,
                        StartTime = item.StartTime,
                        RestTime = (item.StartTime - DateTime.Now).TotalMinutes / 60 > 0 ?
                            $"{(item.StartTime - DateTime.Now).TotalMinutes / 60} часов {(item.StartTime - DateTime.Now).TotalMinutes % 60} минут" :
                            $"{(item.StartTime - DateTime.Now).TotalMinutes % 60} минут"
                    });
               

               
            }
            Orders = upcomingEntries;
        }
        private List<OrdersListModel> orders;
        public List<OrdersListModel> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }
    }
}
