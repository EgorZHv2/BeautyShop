using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class OrdersListModel
    {
        public string NameService { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientPatronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime StartTime { get; set; }
        public string RestTime { get; set; }
    }
}
