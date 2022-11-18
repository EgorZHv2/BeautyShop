using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class ServicesListItemModel
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public Nullable<double> Discount { get; set; }
        public int DurationInMinutes { get; set; }

    }
}
