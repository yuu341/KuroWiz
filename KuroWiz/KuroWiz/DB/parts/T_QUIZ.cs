using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuroWiz.DB
{
    public partial class T_QUIZ
    {
        public string Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;

                if (value.Contains("理"))
                {
                    CATEGORY_CD = 1;
                }
                if (value.Contains("文"))
                {
                    CATEGORY_CD = 2;
                }
                if (value.Contains("アニメ"))
                {
                    CATEGORY_CD = 3;
                }
                if (value.Contains("生活"))
                {
                    CATEGORY_CD = 4;
                }
                if (value.Contains("芸能"))
                {
                    CATEGORY_CD = 5;
                }
                if (value.Contains("スポーツ"))
                {
                    CATEGORY_CD = 6;
                }
                if (value.Contains("イベント"))
                {
                    CATEGORY_CD = 7;
                }
            }
        }
        private string _Category;

        public string Difficulty
        {
            get
            {
                return _Difficulty;
            }
            set
            {
                _Difficulty = value;
                if (value.Contains("1") || value.Contains("単"))
                {
                    DIFFICULTY_CD = 1;
                }
                if (value.Contains("2"))
                {
                    DIFFICULTY_CD = 2;
                }
                if (value.Contains("3"))
                {
                    DIFFICULTY_CD = 3;
                }
                if (value.Contains("?") || value.Contains("？"))
                {
                    DIFFICULTY_CD = 4;
                }
            }
        }
        private string _Difficulty;
    }
}
