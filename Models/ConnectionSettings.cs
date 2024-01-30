using MyDiary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Models
{
    public class ConnectionSettings : IDataErrorInfo
    {
        private bool _isServerAddressValid = true;
        private bool _isServerNameValid = true;
        private bool _isDatabaseNameValid = true;
        private bool _isUserNameValid = true;
        private bool _isDatabasePasswordValid = true;

        public string ServerAddress
        {
            get
            {
                return Settings.Default.ServerAddress;
            }
            set
            {
                Settings.Default.ServerAddress = value;
            }
        }
        public string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public string DatabaseName
        {
            get
            {
                return Settings.Default.DbName;
            }
            set
            {
                Settings.Default.DbName = value;
            }
        }
        public string UserName
        {
            get
            {
                return Settings.Default.UserName;
            }
            set
            {
                Settings.Default.UserName = value;
            }
        }
        public string DatabasePassword
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ServerAddress):

                        if (string.IsNullOrWhiteSpace(ServerAddress))
                        {
                            Error = "Podaj adres serwera";
                            _isServerAddressValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                        }

                        break;

                    case nameof(ServerName):

                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Podaj nazwę serwera";
                            _isServerNameValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                        }

                        break;

                    case nameof(DatabaseName):

                        if (string.IsNullOrWhiteSpace(DatabaseName))
                        {
                            Error = "Podaj nazwę bazy danych";
                            _isDatabaseNameValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                        }

                        break;

                    case nameof(UserName):

                        if (string.IsNullOrWhiteSpace(UserName))
                        {
                            Error = "Podaj nazwę serwera";
                            _isUserNameValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                        }

                        break;

                    case nameof(DatabasePassword):

                        if (string.IsNullOrWhiteSpace(DatabasePassword))
                        {
                            Error = "Podaj nazwę serwera";
                            _isDatabasePasswordValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                        }

                        break;

                    default:

                        break;

                }

                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isServerAddressValid &&
                       _isServerNameValid &&
                       _isDatabaseNameValid &&
                       _isUserNameValid &&
                       _isDatabasePasswordValid;
            }
        }

        public void Save()
        {
            Settings.Default.Save();
        }
    }
}
