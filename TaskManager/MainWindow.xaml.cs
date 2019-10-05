using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ItemsControl itemsControl = new ItemsControl();
        TaskManagerManager TskMan;

        public MainWindow()
        {
            InitializeComponent();
            itemsControl.ItemsSource =  TasksList.ItemsSource;
            TskMan = this.FindResource("TskMan") as TaskManagerManager;
            SetPriorityComboBox();
        }

        public void SetPriorityComboBox()
        {
            Priority.Items.Add("Wysoki");
            Priority.Items.Add("Normalny");
            Priority.Items.Add("Niski");
            Priority.SelectedIndex = 1;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            TskMan.SetDataBaseTasksList();            
            //TasksList.ItemsSource = null;           
        }

        private void CloseConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            TskMan.DataBaseCloseConnection();
            Environment.Exit(0);
            //TasksList.ItemsSource = itemsControl.ItemsSource;
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            TskMan.AddTaskToDataBase(Priority.SelectedItem.ToString());
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TskMan.DeleteTaskFromDataBase();
        }
    }
}
