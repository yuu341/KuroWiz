using KuroWiz.DB;
using KuroWiz.Parse;
using KuroWiz.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KuroWiz.Pages.Entry
{
    class EntryViewModel : RaiseBase
    {
        public EntryViewModel()
        {
            Entities = new KuroWizEntities();
        }

        public RelayCommand<string> GetResourceCommand
        {
            get
            {
                return new RelayCommand<string>(GetResource);
            }
        }


        private async void GetResource(object src)
        {
            HttpClient c = new HttpClient();
            cnt = 0;
            Message = string.Empty;
            
            for (int i = int.Parse(StartIdx); i <= int.Parse(EndIdx); i++)
            {
                var result = await c.GetStringAsync(new Uri(URL + i));
                SolveQuiz(result);
            }
        }
        int cnt = 0;

        public RelayCommand<string> RegisterCommand
        {
            get
            {
                return new RelayCommand<string>(Register);
            }
        }
        private void Register(object args)
        {
            Entities.T_QUIZ.Add((T_QUIZ)args);
            Entities.SaveChanges();
        }

        public string URL
        {
            get
            {
                return _URL;
            }
            set
            {
                _URL = value;
                Raise();
            }
        }
        private string _URL;

        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
                Raise();
            }
        }
        private string _Message;

        private void SolveQuiz(string target)
        {
            SumomoParser parser = new SumomoParser();
            parser.Parse(target);

            Quiz = parser.Quiz;
            Register(parser.Quiz);

            Message += (cnt++) + "\n";
            //Raise("Quiz");
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

        public T_QUIZ Quiz
        {
            get
            {
                return _Quiz;
            }
            set
            {
                _Quiz = value;
                //Raise();
            }
        }
        private T_QUIZ _Quiz;

        public string StartIdx
        {
            get
            {
                return _StartIdx;
            }
            set
            {
                _StartIdx = value;
                Raise();
            }
        }
        private string _StartIdx;

        public string EndIdx
        {
            get
            {
                return _EndIdx;
            }
            set
            {
                _EndIdx = value;
                Raise();
            }
        }
        private string _EndIdx;
    }
}
