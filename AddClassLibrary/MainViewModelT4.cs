
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddClassLibrary;
using System.Windows.Input;

namespace AddClassLibrary
{
    partial class MainViewModel
    {
        private ICommand _ClearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_ClearCommand == null)
                {
                    // イベントハンドラを登録する。自分自身と、ClearCommand、エラー有無を渡す。
                    _ClearCommand = new DelegateCommand(this, ExecuteClearCommand);
                }
                return _ClearCommand;
            }
        }

        private ICommand _SearchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_SearchCommand == null)
                {
                    // イベントハンドラを登録する。自分自身と、SearchCommand、エラー有無を渡す。
                    _SearchCommand = new DelegateCommand(this, ExecuteSearchCommand);
                }
                return _SearchCommand;
            }
        }

        private ICommand _UpdateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (_UpdateCommand == null)
                {
                    // イベントハンドラを登録する。自分自身と、UpdateCommand、エラー有無を渡す。
                    _UpdateCommand = new DelegateCommand(this, ExecuteUpdateCommand, CanExecuteUpdateCommand);
                }
                return _UpdateCommand;
            }
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    // イベントハンドラを登録する。自分自身と、DeleteCommand、エラー有無を渡す。
                    _DeleteCommand = new DelegateCommand(this, ExecuteDeleteCommand, CanExecuteDeleteCommand);
                }
                return _DeleteCommand;
            }
        }

        private ICommand _AboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (_AboutCommand == null)
                {
                    // イベントハンドラを登録する。自分自身と、AboutCommand、エラー有無を渡す。
                    _AboutCommand = new DelegateCommand(this, ExecuteAboutCommand);
                }
                return _AboutCommand;
            }
        }

        private ICommand _HelpCommand;
        public ICommand HelpCommand
        {
            get
            {
                if (_HelpCommand == null)
                {
                    // イベントハンドラを登録する。自分自身と、HelpCommand、エラー有無を渡す。
                    _HelpCommand = new DelegateCommand(this, ExecuteHelpCommand);
                }
                return _HelpCommand;
            }
        }


//        private bool CanExecuteUpdateCommand() => !HasErrors;
//        private void ExecuteUpdateCommand() { }
      }
}


