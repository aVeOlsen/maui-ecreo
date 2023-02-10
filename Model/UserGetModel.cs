using MauiEcreoLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.Model
{
    public class UserGetModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string BaseID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool AdminStatus { get; set; }

        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }


        private ImageSource _imageURL;

        [NotMapped]
        public ImageSource ImageURL
        {
            get { return _imageURL; }
            set
            {
                _imageURL = value;
                OnPropertyChanged("ImageURL");
            }
        }


        public Role Role { get; set; }
        public AbsenceStatusRole CurrentAbsenceStatus { get; set; }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
