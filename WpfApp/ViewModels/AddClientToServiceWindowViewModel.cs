using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    public class AddClientToServiceWindowViewModel:BaseViewModel
    {
      public  AddClientToServiceWindowViewModel()
      {
            Clients = ApplicationDbContext.GetContext().Client.ToList();
            Startdate = DateTime.Now;
      }
        public Service Service { get; set; }
        private Client client;
        public Client Client
        {
            get { return client; }
            set { client = value; OnPropertyChanged(); }
        }
        private List<Client> clients = new List<Client>();
        public List<Client> Clients 
        { 
            get { return clients; }
            set
            {
                clients = value; OnPropertyChanged();
            }
        }
    
        private DateTime startdate;
        public DateTime Startdate
        {
            get { return startdate; }
            set
            {
                startdate = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddClientsToService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ClientService entity = new ClientService()
                    {
                        ServiceID = Service.ID,
                        ClientID = Client.ID,
                        StartTime = Startdate
                    };
                    ApplicationDbContext.GetContext().ClientService.Add(entity);
                    ApplicationDbContext.GetContext().SaveChanges();
                    MessageBox.Show("Клиент записан на услугу");

                });
            }
        }


    }
}
