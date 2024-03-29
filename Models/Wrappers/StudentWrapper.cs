﻿using System.ComponentModel;

namespace MyDiary.Models.Wrappers
{
    public class StudentWrapper : IDataErrorInfo
    {

        private bool _isFirstNameValid;
        private bool _isLastNameValid;

        public StudentWrapper()
        {
            Group = new GroupWrapper();
        }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Physics { get; set; }
        public string Technology { get; set; }
        public string PolishLanguage { get; set; }
        public string ForeignLanguage { get; set; }
        public bool AdditionalClasses { get; set; }
        public GroupWrapper Group { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Podaj imię ucznia.";
                            _isFirstNameValid = false;
                        }


                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }


                        break;

                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Podaj nazwisko ucznia.";
                            _isLastNameValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
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
                return _isFirstNameValid && _isLastNameValid && Group.IsGroupValid;
            }
        }
    }
}
