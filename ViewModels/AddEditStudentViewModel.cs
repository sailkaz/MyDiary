﻿using MyDiary.Commands;
using MyDiary.Models.Domains;
using MyDiary.Models.Wrappers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyDiary.ViewModels
{
    public class AddEditStudentViewModel : ViewModelBase
    {
        private readonly Repository _repository = new Repository();
        public AddEditStudentViewModel(StudentWrapper student)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);


            if (student == null)
                Student = new StudentWrapper();

            else
            {
                Student = student;
                IsUpdate = true;
            }


            InitGroups();
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private StudentWrapper _student;

        public StudentWrapper Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "-- Brak --" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Student.Group.Id;
        }

        private void Confirm(object obj)
        {
            if (!Student.IsValid)
                return;
            {

            }
            if (!IsUpdate)
                AddStudent();

            else
                UpdateStudent();

            CloseWindow(obj as Window);
        }

        private void AddStudent()
        {
            _repository.AddStudent(Student);
        }

        private void UpdateStudent()
        {
            _repository.UpdateStudent(Student);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
