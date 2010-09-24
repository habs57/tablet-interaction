using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleMVVM;

namespace TablectionServer
{
    public class MainWindowVM : ViewModelBase
    {
        
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

        }

        protected bool StartServerCommand_CanExecute(object sender)
        {
            return true;
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

        }

        protected bool StopServerCommand_CanExecute(object sender)
        {
            return true;
        }

        #endregion //StopServerCommand

        
        #region ResetServerCommand

        private RelayCommand _ResetServerCommand;
        public RelayCommand ResetServerCommand
        {
            get
            {
                if (_ResetServerCommand == null)
                {
                    _ResetServerCommand = new RelayCommand(ResetServerCommand_Execute, ResetServerCommand_CanExecute);
                }
                return _ResetServerCommand;
            }
        }

        protected void ResetServerCommand_Execute(object sender)
        {

        }

        protected bool ResetServerCommand_CanExecute(object sender)
        {
            return true;
        }

        #endregion //ResetServerCommand

        
        #region Logs

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
