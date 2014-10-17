using KuroWiz.DB;
using KuroWiz.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace KuroWiz.Pages.Quiz
{
    class QuizViewModel : RaiseBase
    {
        public QuizViewModel()
        {
            Entities = new KuroWizEntities();
        }

        public RelayCommand<object> SearchCommand
        {
            get
            {
                return new RelayCommand<object>(Search);
            }
        }
        public void Search(object src)
        {
            KuroWizEntities ent = new KuroWizEntities();

            var list = ent.T_QUIZ.Where(p => true);

            if (!string.IsNullOrEmpty(Opt.Free))
            {
                string search = Opt.Free;

                if (search.EndsWith("\r\n"))
                {
                    foreach (var each in search.Split('\n'))
                    {
                        if (string.IsNullOrEmpty(each))
                            continue;
                        var word = each.TrimEnd('\r');
                        list = list.Where(p => p.SUMMARY.Contains(word)
                            || p.ANS.Contains(word)
                            || p.C1.Contains(word)
                            || p.C2.Contains(word)
                            || p.C3.Contains(word)
                            || p.C4.Contains(word)
                            );
                    }
                }
            }

            List<int> optDf = new List<int>();
            List<int> optCt = new List<int>();
            if (Opt.Df1)
            {
                optDf.Add(1);
            }
            if (Opt.Df2)
            {
                optDf.Add(2);
            }
            if (Opt.Df3)
            {
                optDf.Add(3);
            }

            if (Opt.Ct1)
            {
                optCt.Add(1);
            }
            if (Opt.Ct2)
            {
                optCt.Add(2);
            }
            if (Opt.Ct3)
            {
                optCt.Add(3);
            }
            if (Opt.Ct4)
            {
                optCt.Add(4);
            }
            if (Opt.Ct5)
            {
                optCt.Add(5);
            }
            if (Opt.Ct6)
            {
                optCt.Add(6);
            }

            list = list.Where(p => optDf.Contains(p.DIFFICULTY_CD)
                && optCt.Contains(p.CATEGORY_CD));

            Total = list.Count();

            QuizList = list.ToList();
            Rand = new Random( DateTime.Now.Millisecond );
            //Answers = QuizList.Where(p => p.SUMMARY.Contains(search));
            //SearchString = string.Empty;

        }

        private List<T_QUIZ> QuizList { get; set; }
        private Random Rand { get; set; }

        public QuizSearchOptions Opt
        {
            get
            {
                return _Opt ?? (_Opt = new QuizSearchOptions());
            }
        }
        private QuizSearchOptions _Opt;

        public string SearchedTotal
        {
            get
            {
                return Total + "件";
            }
        }
        private int Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                Raise();
                Raise("SearchedTotal");
            }
        }
        private int _Total;

        public T_QUIZ Quiz
        {
            get
            {
                return _Quiz;
            }
            set
            {
                _Quiz = value;
                Raise();
            }
        }
        private T_QUIZ _Quiz;
        public List<T_QUIZ> AnsweredList { get; set; }

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

        private void SetQuiz()
        {
            IsCollect = null;
            var quiz = QuizList[Rand.Next() % Total];
            Quiz = new T_QUIZ();
            Quiz.C1 = quiz.C1;
            Quiz.C2 = quiz.C2;
            Quiz.C3 = quiz.C3;
            Quiz.C4 = quiz.C4;
            Quiz.ANS = quiz.ANS;
            Quiz.SUMMARY = quiz.SUMMARY;
            Quiz.CATEGORY_CD = quiz.CATEGORY_CD;
            Quiz.DIFFICULTY_CD = quiz.DIFFICULTY_CD;
            Raise("Quiz");
        }
        
        public RelayCommand<string> StartCommand
        {
            get
            {
                return new RelayCommand<string>(Start);
            }
        }

        private void Start(object args)
        {
            SetQuiz();
        }

        public RelayCommand<string> AnswerCommand
        {
            get
            {
                return new RelayCommand<string>(Answer);
            }
        }
        private void Answer(object args)
        {
            if (IsCollect.HasValue)
            {
                SetQuiz();
                return;
            }
            switch ((int.Parse(args.ToString())))
            {
                case 1: IsCollect = Quiz.C1 == Quiz.ANS; break;
                case 2: IsCollect = Quiz.C2 == Quiz.ANS; break;
                case 3: IsCollect = Quiz.C3 == Quiz.ANS; break;
                case 4: IsCollect = Quiz.C4 == Quiz.ANS; break;
            }

            Quiz.C1 = ((string)((Quiz.ANS == Quiz.C1) ? "○" : "×")) + Quiz.C1;
            Quiz.C2 = ((string)((Quiz.ANS == Quiz.C2) ? "○" : "×")) + Quiz.C2;
            Quiz.C3 = ((string)((Quiz.ANS == Quiz.C3) ? "○" : "×")) + Quiz.C3;
            Quiz.C4 = ((string)((Quiz.ANS == Quiz.C4) ? "○" : "×")) + Quiz.C4;
            Raise("Quiz");
        }
        
        public SolidColorBrush CollectColor
        {
            get
            {
                if (!IsCollect.HasValue)
                    return new SolidColorBrush(Colors.Gray);
                if (IsCollect.Value)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }
        }
        private bool? IsCollect
        {
            get
            {
                return _IsCollect;
            }
            set
            {
                _IsCollect = value;
                Raise();
                Raise("CollectColor");
            }
        }
        private bool? _IsCollect;
    }
}
