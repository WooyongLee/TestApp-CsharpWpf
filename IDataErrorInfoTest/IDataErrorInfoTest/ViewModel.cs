using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IDataErrorInfoTest
{
    public class Position : INotifyPropertyChanged, IDataErrorInfo
    {
        private double lon;
        private double height;
        private bool isValidateLon = true;

        public double Lon                // 경도
        {
            get
            {
                return lon; 
            }

            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }
        public double Height // 고도 (MSL)
        {
            get { return height; }
            set
            {
                height = value;
                NotifyPropertyChanged("Height");
            }
        }
        public bool IsValidateLon // 위도가 타당한 지 여부
        {
            get { return isValidateLon; }
            set
            {
                isValidateLon = value;

                // Lon이 범위 밖
                if (this.Lon > 180 || this.Lon < -180)
                {
                    isValidateLon = false;
                }

                NotifyPropertyChanged("IsValidateLon");
            }
        }

        public Position()
        {

        }

        // INotifyPropertyChanged Interface 구현
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // IDataErrorInfo Interface 구현
        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "Lon")
                {
                    if (this.Lon > 180 || this.Lon < -180)
                    {
                        result = "Longitude have to get In Range of +-180";
                        this.IsValidateLon = false;
                    }
                    else
                    {
                        this.IsValidateLon = true;
                    }
                }

                else if (columnName == "Height")
                {
                    if (this.Height > 8000 || this.Height < 0)
                    {
                        result = "Height Is Out of range";
                        this.IsValidateLon = false;
                    }
                }

                
                return result;
            }
        }
    }
}
