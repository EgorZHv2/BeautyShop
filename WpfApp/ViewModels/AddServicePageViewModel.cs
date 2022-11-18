using Infrastructure.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string imgPath;
        public string ImgPath
        {
            get { return imgPath; }
            set { imgPath = value;
            OnPropertyChanged();}
        }
        public ICommand CreateService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                   Service newservice =  new Service()
                    {
                        Cost = service.Cost,
                        Discount = service.Discount,
                        DurationInSeconds = service.DurationInSeconds*60,
                        ID = service.Id,
                        Title = service.Title
                    };
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(ImgPath);
                   
                    File.Copy(ImgPath, Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\..\\" + "\\Resources\\ServiceImages\\" + filename));
                    newservice.MainImagePath = filename;
                    ApplicationDbContext.GetContext().Service.Add(newservice);
                    ApplicationDbContext.GetContext().SaveChanges();
                });
            }
        }
        public ICommand AddImage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files(*.JPEP;*.JPG;*.PNG)|*.jpeg;*.jpg;*.png";
                    openFileDialog.ShowDialog();
                    ImgPath = openFileDialog.FileName;
                    
                });
            }
        }
    }

}
