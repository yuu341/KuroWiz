using KuroWiz.DB;
using KuroWiz.Utils;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KuroWiz.Pages.Settings
{
    class SettingsViewModel : RaiseBase
    {
        public SettingsViewModel()
        {
            Entities = new KuroWizEntities();

        }

        public KuroWizEntities Entities
        {
            get
            {
                return _Entities;
            }
            set
            {
                _Entities = value;
                Raise();
            }
        }
        private KuroWizEntities _Entities;

        public RelayCommand<string> ClearDBCommand
        {
            get
            {
                return new RelayCommand<string>(ClearDB);
            }
        }
        private void ClearDB(object args)
        {
            KuroWizEntities ent = new KuroWizEntities();
            var result = MessageBox.Show("DBのクイズをすべて消去します。消したら戻せません。", "警告", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                ent.Database.ExecuteSqlCommand("TRUNCATE TABLE T_QUIZ");
                ent.Database.ExecuteSqlCommand("DBCC CHECKIDENT (T_QUIZ ,0 ,0)");

                MessageBox.Show("初期化しました。");
            }
        }

        public RelayCommand<string> ImportCSVCommand
        {
            get
            {
                return new RelayCommand<string>(ImportCSV);
            }
        }
        private void ImportCSV(object args)
        {
            
        }

        public RelayCommand<string> ExportCSVCommand
        {
            get
            {
                return new RelayCommand<string>(ExportCSV);
            }
        }
        private void ExportCSV(object args)
        {
            CsvFactory fact = new CsvFactory();

            KuroWizEntities ent = new KuroWizEntities();

            var list = ent.T_QUIZ.ToList().OrderBy(p => p.QUIZ_ID);

            using (var writer = new StreamWriter("export.csv"))
            {
                var csv = fact.CreateWriter(writer);
                csv.Configuration.RegisterClassMap<QuizMap>();
                foreach (var each in list)
                {
                    csv.WriteRecord(each);
                }
                MessageBox.Show("完");
            }

        }

        public sealed class QuizMap : CsvClassMap<T_QUIZ>
        {
            public QuizMap()
            {
                Map(p => p.CATEGORY_CD);
                Map(p => p.DIFFICULTY_CD);
                Map(p => p.SUMMARY);
                Map(p => p.C1);
                Map(p => p.C2);
                Map(p => p.C3);
                Map(p => p.C4);
                Map(p => p.ANS);
            }
        }
    }
}
