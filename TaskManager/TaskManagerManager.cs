using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager
{
    public class TaskManagerManager : INotifyPropertyChanged
    {
        private SQLconnection sqlConn;

        public TaskManagerManager()
        {           
            SendSetConnectionRequest();
            //SetDataBaseTasksList();
        }

        //elementy wyświetlane w oknie programu
        public ObservableCollection<string> DatabaseTaks { get { return sqlConn.DataBaseSays; } }
        public bool IsConnectionOpen { get { return sqlConn.IsConnectionOpen; } }

        public void AddTaskToDataBase(string taskPriority)
        {
            sqlConn.AddTaskToDataBase(taskPriority);
            SetDataBaseTasksList();
        }

        public void DeleteTaskFromDataBase()
        {
            DatabaseTaks.Clear();
            sqlConn.DeleteTaskFromDataBase();
        }

        public void SendSetConnectionRequest()
        {
            sqlConn = new SQLconnection();
            OnProperyChanged("IsConnectionOpen");
        }

        public void DataBaseCloseConnection()
        {
            sqlConn.CloseSQLConnection();
            OnProperyChanged("IsConnectionOpen");
        }

        public void SetDataBaseTasksList()
        {
            DatabaseTaks.Clear();
            sqlConn.ImportTasksFromDataBase();
        }

        //propertychanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
