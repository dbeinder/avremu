using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Preprozessor
{
    class Preprocessor
    {
        List<string> inputToList = new List<string>();
        List<string> afterPrePro = new List<string>();
        List<int> lineMapping = new List<int>();
        Dictionary<string, string> replacer = new Dictionary<string, string>();

        //Regex regEx = new Regex();

        void Init(Dictionary<string, string> initDict)
        {
        }

        void AddReplacer()
        {
            replacer.Add("RAMEND", "132");
        }
        public string PreProcess(string input)
        {
            string[] inputToArray = "jhasdkl\nhjkasdhfdk\nsafjkjkh".Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            inputToList.AddRange(inputToArray);

            inputToList = CleanUp(inputToList);


            return inputToList[1];
            //AddReplacer();                


            ////Environment.NewLine(

            //else
            //{
            //    // .def ...
            //    string[] elementsb = line.Split(' ');
            //    if (line[0] == '.')
            //    {
            //        if (elementsb[0] == ".def")
            //        {
            //            replacer.Add(elementsb[1], elementsb[3]);
            //        }
            //    }

            //    // search for replacers
            //    foreach (KeyValuePair<string, string> kvp in replacer)
            //    {
            //        for (int i = 1; i < elementsb.Length; i++)
            //        {
            //            elementsb[i] = elementsb[i].Replace(kvp.Key, kvp.Value);
            //        }
            //    }
            //    line = "";
            //    for (int i = 0; i < elementsb.Length; i++)
            //    {
            //        line += elementsb[i] + " ";
            //    }

            //    line = Calculate("(((132+10)*10+(20-15))/5)");
            //    //line = Calculate(line);
            //    //line = Calculate("((1+1)*1-1)");
            //    return line = line.Trim();
            //}
        }


        private List<string> CleanUp(List<string> line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                // search for comments and delete them
                line[i] = line[i].Replace('\t', ' ');
                string[] elements = line[i].Split(';');
                line[i] = elements[0];

                // delete WhiteSpaces
                line[i] = line[i].Trim();

                string helpstring = "";
                for (int a = 0; a < line[i].Length; a++)
                {
                    if (line[i][a] == ' ' && line[i][a + 1] == ' ')
                    {
                        for (int b = a; b < line[i].Length - 1; b++)
                        {
                            helpstring = line[i].Substring(0, b - 1);
                            helpstring += line[i][b + 1];
                        }
                        line[i] = helpstring;
                        a = 0;
                    }
                }

                // save numbers of lines in which
                if (inputToList[i] != "")
                {
                    lineMapping.Add(i);
                }
               
            }
            for (int i = 0; i < line.Count; i++)
            {
                 if (line[i] == "")
                {
                    line.RemoveAt(i);
                }
            }
           
            return line;

        }

        private string Calculate(string line)
        {

            List<char> plusminus = new List<char>();
            plusminus.Add('*');
            plusminus.Add('/');
            plusminus.Add('+');
            plusminus.Add('-');
            for (int r = 0; r < plusminus.Count; r++)
            {

                for (int i = 0; i < line.Length; i++)
                {
                    bool integer = true;
                    if (line[i] == plusminus[r])
                    {
                        int a;
                        string helpstring = "";
                        int b;
                        int count = i;
                        int c;
                        do
                        {
                            if (int.TryParse(Convert.ToString(line[count - 1]), out a))
                            {
                                helpstring = line[count - 1] + helpstring;
                                count--;
                            }
                            else if (line[i - 1] == ')')
                            {

                                do
                                {
                                    count--;
                                } while (line[count] != '(');
                                if (int.TryParse(line.Substring(count + 1, i - count - 2), out c))
                                {
                                    line = line.Replace("(" + Convert.ToString(c) + ")", Convert.ToString(c));
                                    i = 0;
                                    r = 0;
                                }
                                else
                                {
                                    line = line.Replace(line.Substring(count, i - count), Calculate(line.Substring(count, i - count)));
                                    i = 0;
                                    r = 0;
                                }

                                count = i;

                            }
                            else if (helpstring != "")
                            {
                                a = Convert.ToInt32(helpstring);
                                integer = false;
                            }
                            else
                                integer = false;

                        } while (integer == true && i != 0);

                        helpstring = "";
                        integer = true;
                        count = i;
                        do
                        {
                            if (int.TryParse(Convert.ToString(line[count + 1]), out b))
                            {
                                helpstring += line[count + 1];
                                count++;
                            }
                            else if (line[i + 1] == '(')
                            {
                                do
                                {
                                    count++;
                                } while (line[count] != ')');
                                //line = line.Substring(i + 2, count - i - 2);
                                if (int.TryParse(line.Substring(i + 2, count - i - 2), out c))
                                {
                                    line = line.Replace("(" + Convert.ToString(c) + ")", Convert.ToString(c));
                                    i = 0;
                                    r = 0;
                                }
                                else
                                {
                                    line = line.Replace(line.Substring(i + 1, count - i - 1), Calculate(line.Substring(i + 1, count - i)));
                                    i = 0;
                                    r = 0;
                                }
                                count = i;
                            }
                            else if (helpstring != "")
                            {
                                b = Convert.ToInt32(helpstring);
                                integer = false;
                            }
                            else
                                integer = false;

                        } while (integer == true && i != 0);

                        if (plusminus[r] == '*')
                            line = line.Replace(Convert.ToString(a) + plusminus[r] + Convert.ToString(b), Convert.ToString(a * b));
                        else if (plusminus[r] == '/')
                            line = line.Replace(Convert.ToString(a) + plusminus[r] + Convert.ToString(b), Convert.ToString(a / b));
                        else if (plusminus[r] == '+')
                            line = line.Replace(Convert.ToString(a) + plusminus[r] + Convert.ToString(b), Convert.ToString(a + b));
                        else if (plusminus[r] == '-')
                            line = line.Replace(Convert.ToString(a) + plusminus[r] + Convert.ToString(b), Convert.ToString(a - b));
                    }

                }

            }
            return line;
        }
    }
}
