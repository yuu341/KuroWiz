using KuroWiz.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuroWiz.Parse
{
    class SumomoParser
    {
        public void Parse(string target)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(target);
            Quiz = new T_QUIZ();
            Solve(doc.DocumentNode);
        }

        private void Solve(HtmlAgilityPack.HtmlNode node)
        {
            if (solved)
            {
                return;
            }
            if (Include(node,"kizi"))
            {
                kizi = true;
            }
            if (Include(node, "kizi"))
            {
                var secondary = GetList(node, "table")[0];

                var thirds = GetList(secondary, "tr");

                Quiz.Category = GetData(thirds[1]);
                Quiz.Difficulty = GetData(thirds[2]);
                Quiz.SUMMARY = GetData(thirds[3]);
                Quiz.C1 = GetData(thirds[6]);
                Quiz.C2 = GetData(thirds[7]);
                Quiz.C3 = GetData(thirds[8]);
                Quiz.C4 = GetData(thirds[9]);
                Quiz.ANS = GetData(thirds[11]);

            }

            if (node.ChildNodes == null || node.ChildNodes.Count == 0)
                return;

            foreach (var each in node.ChildNodes)
            {
                Solve(each);
            }

            kizi = false;
        }
        private string GetData(HtmlAgilityPack.HtmlNode node)
        {
            return GetList(node, "td")[0].InnerHtml.Trim();
        }
        private bool Include(HtmlAgilityPack.HtmlNode node,string attr)
        {
            return node.Attributes.SingleOrDefault(p => p.Name == "class" && p.Value == attr) != null;
        }
        private List<HtmlAgilityPack.HtmlNode> GetList(HtmlAgilityPack.HtmlNode node, string name)
        {
            return node.ChildNodes.Where(p => p.Name == name).ToList();
        }

        private bool kizi = false;
        private bool solved
        {
            get
            {
                if (string.IsNullOrEmpty(Quiz.ANS))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Quiz.C1))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Quiz.C2))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Quiz.C3))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Quiz.C4))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Quiz.Category))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Quiz.Difficulty))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(Quiz.SUMMARY))
                {
                    return false;
                }
                return true;
                
            }

        }
        public T_QUIZ Quiz { get; set; }
    }
}
