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
    public class UserModel: INotifyPropertyChanged
    {
        public string BaseID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public UserPersonalInfoModel PersonalInformation { get; set; }
        public Role Role { get; set; }
        private ImageSource _imageURL;

        public event PropertyChangedEventHandler PropertyChanged;

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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class UserPersonalInfoModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

}
