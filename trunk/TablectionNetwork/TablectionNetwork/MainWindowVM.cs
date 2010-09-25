using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleMVVM;
using TablectionServer.Network;

namespace TablectionServer
{
    public class MainWindowVM : ViewModelBase
    {

        private TablectionAsyncServer _server = null;
        private TablectionAsyncServer Server
        {
            get
            {
                if (_server == null)
                {
                    _server = new TablectionAsyncServer(this.Logger);
                    _server.Error += new EventHandler<TablectionServerErrorEventArgs>(_server_Error);
                }
                return _server;
            }
        }

        void _server_Error(object sender, TablectionServerErrorEventArgs e)
        {
            
        }

        public MainWindowVM()
        {

        }

        #region IsServerCanConfig

        private bool _IsServerCanConfig = true;
        public bool IsServerCanConfig
        {
            get { return _IsServerCanConfig; }
            set
            {
                if (_IsServerCanConfig.Equals(value) == false)
                {
                    _IsServerCanConfig = value;
                    OnPropertyChanged("IsServerCanConfig");
                }
            }
        }

        #endregion //IsServerCanConfig



        #region Port

        private int _Port = 0;
        public int Port
        {
            get { return _Port; }
            set
            {
                if (_Port.Equals(value) == false)
                {
                    _Port = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        #endregion //Port

        
        #region ServerIP

        private string _ServerIP = "127.0.0.0";
        public string ServerIP
        {
            get { return _ServerIP; }
            set
            {
                if (_ServerIP.Equals(value) == false)
                {
                    _ServerIP = value;
                    OnPropertyChanged("ServerIP");
                }
            }
        }

        #endregion //ServerIP

        
        #region ClientsCount

        private int _ClientsCount = 0;
        public int ClientsCount
        {
            get { return _ClientsCount; }
            set
            {
                if (_ClientsCount.Equals(value) == false)
                {
                    _ClientsCount = value;
                    OnPropertyChanged("ClientsCount");
                }
            }
        }

        #endregion //ClientsCount

        
        #region StartServerCommand

        private RelayCommand _StartServerCommand;
        public RelayCommand StartServerCommand
        {
            get
            {
                if (_StartServerCommand == null)
                {
                    _StartServerCommand = new RelayCommand(StartServerCommand_Execute, StartServerCommand_CanExecute);
                }
                return _StartServerCommand;
            }
        }

        protected void StartServerCommand_Execute(object sender)
        {
            this.Server.BeginStartListening(this.Port);
            this.IsServerCanConfig = false;
        }

        protected bool StartServerCommand_CanExecute(object sender)
        {
            return (this.Server.IsRunning == false);
        }

        #endregion //StartServerCommand

        
        #region StopServerCommand

        private RelayCommand _StopServerCommand;
        public RelayCommand StopServerCommand
        {
            get
            {
                if (_StopServerCommand == null)
                {
                    _StopServerCommand = new RelayCommand(StopServerCommand_Execute, StopServerCommand_CanExecute);
                }
                return _StopServerCommand;
            }
        }

        protected void StopServerCommand_Execute(object sender)
        {
            this.Server.EndListening();
            this.IsServerCanConfig = true;
        }

        protected bool StopServerCommand_CanExecute(object sender)
        {
            return (this.Server.IsRunning == true);
        }

        #endregion //StopServerCommand

        
               
        #region Logs

        private Logger _Logger = null;
        private Logger Logger
        {
            get 
            {
                if (_Logger == null)
                {
                    _Logger = new Logger();
                    _Logger.LogHandler = new Action<string>(LogCallback);
                }
                return _Logger;
            }
        }

        private void LogCallback(string log)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(log);
            builder.AppendLine(this.Logs);
            this.Logs = builder.ToString();
        }

        private string _Logs = string.Empty;
        
        public string Logs
        {
            get { return _Logs; }
            set
            {
                if (_Logs.Equals(value) == false)
                {
                    _Logs = value;
                    OnPropertyChanged("Logs");
                }
            }
        }

        #endregion //Logs
              
    }
}
