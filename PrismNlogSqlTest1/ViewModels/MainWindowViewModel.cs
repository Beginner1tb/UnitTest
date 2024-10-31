using Prism.Commands;
using Prism.Mvvm;
using DllNlogTest1.Interfaces;
using DllSqlTest1.Interfaces;
using System;

namespace PrismNlogSqlTest1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly INlogRepositories _logRepositories;
        private readonly ISqlRepositories _sqlRepositories;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _logcontent;

        public string Logcontent
        {
            get { return _logcontent; }
            set { SetProperty(ref _logcontent, value); }
        }

        public DelegateCommand NlogCommand { get; private set; }

        public MainWindowViewModel(INlogRepositories logRepositories, ISqlRepositories sqlRepositories)
        {
           
            NlogCommand = new DelegateCommand(NlogInfoRecord);
            _logRepositories = logRepositories;
            _sqlRepositories = sqlRepositories;
        }

        private void NlogInfoRecord()
        {
            _logRepositories.LogInfo(Logcontent);
        }
    }
}
