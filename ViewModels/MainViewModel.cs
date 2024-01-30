using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using MyDiary.Commands;
using MyDiary.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MyDiary.Models.Wrappers;
using MyDiary.Models.Domains;

namespace MyDiary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            SetConnectionPathCommand = new RelayCommand(SetConnectionPath);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);

            LoadedWindow(null);
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand SetConnectionPathCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
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
            groups.Insert(0, new Models.Domains.Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia",
                $"Czy napewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }


        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }


        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>
                (_repository.GetStudents(SelectedGroupId));
        }

        private void SetConnectionPath(object obj)
        {
            var setConnectionPathWindow = new ConnectionPathView(true);
            setConnectionPathWindow.ShowDialog();
        }
        private bool IsValidConnectionPath()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }

            catch (Exception)

            {
                return false;
            }
        }
        private async void LoadedWindow(object obj)
        {

            if (!IsValidConnectionPath())
            {
                var metrowindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metrowindow.ShowMessageAsync("Błąd połączenia.",
                    $"Nie można połączyć się z bazą danych. Czy chcesz zmienić ustawienia?",
                    MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }

                else
                {
                    var connectionPathWinodow = new ConnectionPathView(false);
                    connectionPathWinodow.ShowDialog();
                }
            }

            else
            {
                InitGroups();
                RefreshDiary();
            }

        }

    }
}