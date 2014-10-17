﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KuroWiz.Utils
{
    class RaiseBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 基本呼び出さない
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 変更通知
        /// </summary>
        /// <param name="name"></param>
        public void Raise([CallerMemberName]string name = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
