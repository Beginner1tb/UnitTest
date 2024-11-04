using Prism.Commands;
using Prism.Mvvm;
using PrismNlogSqlTest1.Services1;
using System;
using PrismNlogSqlTest1.Services1.Interfaces;

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
        public DelegateCommand SqlCommand { get; private set; }

        public MainWindowViewModel(INlogRepositories logRepositories, ISqlRepositories sqlRepositories)
        {
           
            NlogCommand = new DelegateCommand(NlogInfoRecord);
            SqlCommand=new DelegateCommand(SqlQuery);
            _logRepositories = logRepositories;
            _sqlRepositories = sqlRepositories;
            Username="u6";  
        }

        private void SqlQuery()
        {
            int priorityNum = _sqlRepositories.GetPriorityNum(Username);
            _logRepositories.LogInfo($"Priority number for user {Username} is {priorityNum}.");
        }

        private void NlogInfoRecord()
        {
            _logRepositories.LogInfo(Logcontent);
        }
    }
}
