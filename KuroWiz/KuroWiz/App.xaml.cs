using CsvHelper;
using KuroWiz.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KuroWiz
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //StreamReader reader = new StreamReader("wiz.csv");
            //CsvHelper.CsvReader csv = new CsvReader(reader);

        }

        public List<T_QUIZ> QuizList
        {
            get
            {
                return _QuizList;
            }
            set
            {
                _QuizList = value;
            }
        }
        private List<T_QUIZ> _QuizList;
    }
}
