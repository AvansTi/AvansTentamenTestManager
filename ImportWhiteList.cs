using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansTentamenManager
{
    class ImportWhiteList
    {
        #region singleton
        private static ImportWhiteList instance;
       public  static ImportWhiteList Instance { get {
                if (instance == null)
                    instance = new ImportWhiteList();
                return instance;
            } }
        #endregion

        List<string> whitelist = new List<string>();
        List<string> blacklist = new List<string>();

        public ImportWhiteList()
        {
            try
            {
                whitelist = File.ReadAllLines("whitelist.txt").ToList();
            }
            catch (FileNotFoundException) { }
            try
            {
                blacklist = File.ReadAllLines("blacklist.txt").ToList();
            }
            catch (FileNotFoundException) { }

        }

        public bool IsWhiteListed(string line)
        {
            bool whitelisted = whitelist.Any(l => line.StartsWith(l));
            if(!whitelisted)
            {
                if (!blacklist.Any(l => line.StartsWith(l)))
                {
                    string input = Interaction.InputBox("Whitelist", "Whitelist?", line, -1, -1);
                    if (input == "")
                        blacklist.Add(input);
                    else
                    {
                        whitelist.Add(input);
                        whitelisted = true;
                    }


                    File.WriteAllLines("whitelist.txt", whitelist);
                    File.WriteAllLines("blacklist.txt", blacklist);
                }
            }
            return whitelisted;
        }


    }
}
