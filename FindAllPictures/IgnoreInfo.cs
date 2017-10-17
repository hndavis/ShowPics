using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FindAllPictures
{
    public sealed class IgnoreInfo
    {
        IgnorComper igComparer;
        //private HashSet<string> IgnoreDirs;
        Dictionary<int,List<string>> master ;//= new 
        private static readonly Lazy<IgnoreInfo> lazy = 
            new Lazy<IgnoreInfo>(()=> new IgnoreInfo());

        public static IgnoreInfo Instance { get { return lazy.Value; } }

        private IgnoreInfo()
        {
            //igComparer= new IgnorComper();
            //IgnoreDirs = new HashSet<string>(igComparer);
            master = new Dictionary<int, List<string>>();
        
            var profileDir = System.Environment.GetEnvironmentVariable("userprofile");
            var ignorePath = profileDir + "\\" + "FAP\\Ignore.xml";
            System.Xml.XmlDocument ignoreDoc = new XmlDocument();
            ignoreDoc.Load(ignorePath);
            foreach (XmlNode ig in ignoreDoc.ChildNodes[1].ChildNodes)
            {
                var ignoreDir = ig.Attributes.GetNamedItem("Directory").Value;
                List<string> ignoreMatches;
                if (!master.TryGetValue(ignoreDir.Length, out ignoreMatches))
                {
                    ignoreMatches = new List<string>();
                    master.Add(ignoreDir.Length, ignoreMatches);
                  
                }
                ignoreMatches.Add(ig.Attributes.GetNamedItem("Directory").Value);
               
            }
          
        }

        public bool CanIgnorePath(string path)
        {
            foreach ( var vp in master)
            {
                if (path.Length >= vp.Key)
                {
                    var possibleMatch = (path.Substring(0, vp.Key));
                    var ignoreList = vp.Value;
                    foreach (var excludeDir in ignoreList)
                    {
                        if (excludeDir.ToLower() == possibleMatch.ToLower())
                            return true;
                    }
                }

            }

            return false;
        }
    }
    class IgnorComper : IEqualityComparer<String>
    {
        public bool Equals(string x, string y)
        {
            if (x == y)
                return true;

            return false;
        }

        public int GetHashCode(string obj)
        {
            string s = obj as string;
            int M = 1;
          // return s.GetHashCode();

            int sum = 0;
            foreach (var ch in s)
            {
                sum += ch;
            }
            return sum % M;


        }
    }


}
