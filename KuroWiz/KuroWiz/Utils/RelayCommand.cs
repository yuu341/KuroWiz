using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KuroWiz.Utils
{
    public class RelayCommand<T> : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 一度だけ実行させたいときに呼び出す。
        /// ex)
        /// return new RelayCommand().OneTime;
        /// </summary>
        public RelayCommand<T> OneTime
        {
            get
            {
                this.OneTimeFlg = true;
                return this;
            }
        }
        private bool OneTimeFlg { get; set; }
        private bool IsClicked { get; set; }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (OneTimeFlg && IsClicked)
            {
                return false;
            }
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (OneTimeFlg && IsClicked)
            {
                return;
            }
            IsClicked = true;

            _execute(parameter);
        }

        [Conditional("DEBUG")]
        private void PushTestCase(Action<object> exe, object param)
        {
        }
    }
}
