using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuroWiz.Pages.Quiz
{
    class QuizSearchOptions
    {
        public QuizSearchOptions()
        {
            Ct1 = true;
            Ct2 = true;
            Ct3 = true;
            Ct4 = true;
            Ct5 = true;
            Ct6 = true;
            Df1 = true;
            Df2 = true;
            Df3 = true;
        }
        public bool Ct1 { get; set; }
        public bool Ct2 { get; set; }
        public bool Ct3 { get; set; }
        public bool Ct4 { get; set; }
        public bool Ct5 { get; set; }
        public bool Ct6 { get; set; }
        public bool Df1 { get; set; }
        public bool Df2 { get; set; }
        public bool Df3 { get; set; }

        public string Free { get; set; }
        public int FailParcent { get; set; }
        public int SeeUpper { get; set; }
        public int SeeLower { get; set; }
    }
}
