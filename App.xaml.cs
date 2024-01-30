﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MyDiary
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var metroWindow = Current.MainWindow as MetroWindow;
            metroWindow.ShowMessageAsync("Coś poszło nie tak",
                "Wystąpił nieoczekiwany wyjątek" +
                Environment.NewLine +
                e.Exception.Message);

            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen("/Images/drop.jpg");
            splash.Show(true);
            splash.Close(new TimeSpan( 0, 0, 1));

        }
    }
}
