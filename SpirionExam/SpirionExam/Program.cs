using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SpirionExam
{
    enum listOfNames
    {
        christoper,
        christian,
        christine,
        sidney,
        aaron,
        selene,
        keyn
    }
    enum ValidationKey
    {
        Alpha,
        Numbers,
        AlphaNumeric
    }
    public class Search
    {
        public List<string> FindText(string fText, string toBeReplaced)
        {
            List<string> textList = new List<string>();


            foreach (string n in Enum.GetNames(typeof(listOfNames)))
            {
                string replacedName = string.Empty;
                if (n.Contains(fText))
                {
                    var repNum = n.Length;
                    StringBuilder sn = new StringBuilder(n);
                    var fLetter = fText[0];
                    int index = n.IndexOf(fLetter);
                    int i = 0;
                    for (int pos = index; pos < repNum; pos++)
                    {
                        if (i < fText.Length)
                        {
                            //Changing character into X
                            sn[pos] = Convert.ToChar(toBeReplaced);
                            replacedName = sn.ToString();
                            i++;
                        }
                    }
                    //Added to the list
                    textList.Add(replacedName);
                }
            }
            return textList;

        }

        public bool ValidateInputText(string iText, string validationKeys)
        {
            var vKeys = string.Empty;
            if (validationKeys == "Alpha")
                vKeys = @"^[a-zA-Z]+$";
            else if (validationKeys == "AlphaNumeric")
                vKeys = @"[a-zA-Z0-9]+$";
            else if (validationKeys == "Numbers")
                vKeys = @"[0-9]+$";

           return Regex.IsMatch(iText, vKeys);
        }

        public void DisplayResult(List<string> result)
        {
            if (result.Count == 0)
                Console.WriteLine("No Matched Found");
            else
            {
                foreach (string r in result)
                {
                    Console.WriteLine(char.ToUpper(r[0]) + r.Substring(1));
                }
            }
        }

    }

    class SearchName : Search
    {
        public List<string> SearchingOfName(string findName, string toBeReplaced)
        {
            return base.FindText(findName, toBeReplaced);
        }
        public new void DisplayResult(List<string> result)
        {
            base.DisplayResult(result);
        }
        public new bool ValidateInputText(string iText, string validationKeys)
        {
            return base.ValidateInputText(iText, validationKeys);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("List of names:");
                foreach (string n in Enum.GetNames(typeof(listOfNames)))
                {
                    Console.WriteLine(char.ToUpper(n[0]) + n.Substring(1));
                }

                Console.WriteLine("Search Name:");
                var nameToSearch = Console.ReadLine().ToLower();

                SearchName sName = new SearchName();
                var isOkay = sName.ValidateInputText(nameToSearch, Enum.GetName(typeof(ValidationKey), ValidationKey.Alpha));
                if (isOkay)
                {
                    List<string> nameList = new List<string>(sName.SearchingOfName(nameToSearch, "X")); ;

                    Console.WriteLine();
                    Console.WriteLine("Result:");

                    sName.DisplayResult(nameList);
                }
                else
                {
                    throw new Exception($"Enter {Enum.GetName(typeof(ValidationKey), ValidationKey.Alpha)} Only");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

