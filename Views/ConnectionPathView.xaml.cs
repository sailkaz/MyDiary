using MahApps.Metro.Controls;
using MyDiary.ViewModels;

namespace MyDiary.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ConnectionPathView.xaml
    /// </summary>
    public partial class ConnectionPathView : MetroWindow
    {
        public ConnectionPathView(bool canCloseWindow)
        {
            InitializeComponent();
            DataContext = new ConnectionPathViewModel(canCloseWindow);
        }
    }
}
