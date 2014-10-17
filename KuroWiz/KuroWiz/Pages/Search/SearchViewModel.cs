using KuroWiz.DB;
using KuroWiz.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace KuroWiz.Pages.Search
{
    class SearchViewModel : RaiseBase
    {
        public SearchViewModel()
        {
            Entities = new KuroWizEntities();
            QuizList = Entities.T_QUIZ.ToList();
        }

        public RelayCommand<TextChangedEventArgs> SearchCommand
        {
            get
            {
                return new RelayCommand<TextChangedEventArgs>(Search);
            }
        }
        public void Search(object src)
        {
            TextChangedEventArgs args = src as TextChangedEventArgs;
            var search = (args.Source as TextBox).Text;
            if (search.EndsWith("\r\n"))
            {
                var list = QuizList.Where( p=> true );
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
                Answers = list;
                //Answers = QuizList.Where(p => p.SUMMARY.Contains(search));
                //SearchString = string.Empty;
            }
        }

        public SearchOptions Opt
        {
            get
            {
                return _Opt ?? (_Opt = new SearchOptions());
            }
        }
        private SearchOptions _Opt;

        public IEnumerable<T_QUIZ> Answers
        {
            get
            {
                return _Answers;
            }
            set
            {
                _Answers = value;
                Raise();
                Raise("DetailAnswers");
            }
        }
        private IEnumerable<T_QUIZ> _Answers;

        public IEnumerable<T_QUIZ> DetailAnswers
        {
            get
            {
                var result = Answers.AsQueryable<T_QUIZ>();

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

                result = result.Where(p => optDf.Contains(p.DIFFICULTY_CD)
                    && optCt.Contains(p.CATEGORY_CD));

                return result;

            }
        }

        public string SearchString
        {
            get
            {
                return _SearchString ?? "";
            }
            set
            {
                _SearchString = value;
                Raise();
            }
        }
        private string _SearchString;
        private List<T_QUIZ> QuizList
        {
            get;
            set;
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
    }
}
