using Infrastructure.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    internal class EditServiceWindowViewModel:BaseViewModel
    {
        public Action CloseAction { get; set; }
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
            set
            {
                imgPath = value;
                OnPropertyChanged();
            }
        }
        public ICommand CreateService
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if(service.DurationInSeconds < 0 || service.DurationInSeconds > 240)
                    {
                        MessageBox.Show("Длительность должна быть меньше 4х часов и больше нуля");
                        return;
                    }
                    Service newservice = new Service()
                    {
                        Cost = service.Cost,
                        Discount = service.Discount,
                        DurationInSeconds = service.DurationInSeconds * 60,
                        ID = service.Id,
                        Title = service.Title
                    };
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(ImgPath);

                    //File.Copy(ImgPath, Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\..\\" + "\\Resources\\Услугисалонакрасоты\\" + filename));
                    newservice.MainImagePath = filename;
                    ApplicationDbContext.GetContext().Service.AddOrUpdate(newservice);
                    ApplicationDbContext.GetContext().SaveChanges();
    
                    MessageBox.Show("Услуга изменена");
                    

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
