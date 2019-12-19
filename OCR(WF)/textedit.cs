using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OCR_WF_
{
    class textedit
    {

        public static string Edit(string str)
        {
            string raw = str.Replace(" ", String.Empty).Replace("０", "0").Replace("１", "1").Replace("２", "2").Replace("３", "3").Replace("４", "4").Replace("５", "5").Replace("６", "6").Replace("７", "7").Replace("８", "8").Replace("９", "9").Replace("", string.Empty).Replace("조격파","로격파").Replace("로격파", string.Empty).Trim();


            var listA = new List<(string Nickname, string Monster, string Damage)>();

            //사용법은 위처럼 하고 

            List<string> RawList = (from string a in Regex.Split(raw, "데미지")
                                        /*where !a.Contains("격파") */
                                    select a.Trim()).ToList();

            foreach (var s in RawList)
            {
                string nick = Regex.Match(s, ".*(?=님이)").ToString();
                string mon = Regex.Match(s, @"(?<=님이)\s?.+(?=몬스터를)").ToString().Trim();

                string dem = Regex.Match(s, @"(?<=몬스터를)\s?.+").ToString().Trim();
                if (!string.IsNullOrWhiteSpace(nick + mon + dem))
                    listA.Add((nick, mon, dem));
            }
            StreamWriter csv = new StreamWriter("aa.csv", true, Encoding.Unicode);
            foreach (var s in listA)
            {
                    csv.WriteLine(s.Nickname + "\t" + s.Monster + "\t" + s.Damage + "\t" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            csv.Close();



            StringBuilder sb = new StringBuilder();
            foreach(var n in listA)
            {
                sb.AppendLine($"이름 : {n.Nickname} 몹 이름 : {n.Monster} 뎀지 : {n.Damage}");
            }

                return sb.ToString(); 
        }
    }
}
