using MahApps.Metro.Controls;
using MyDiary.Models;
using MyDiary.Models.Wrappers;
using MyDiary.ViewModels;

namespace MyDiary.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddEditStudentView.xaml
    /// </summary>
    public partial class AddEditStudentView : MetroWindow
    {
        public AddEditStudentView(StudentWrapper student = null)
        {
            InitializeComponent();
            DataContext = new AddEditStudentViewModel(student);
        }
    }
}
