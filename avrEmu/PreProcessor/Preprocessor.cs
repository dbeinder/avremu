using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Preprozessor
{
    class Preprocessor
    {
        List<string> afterPrePro = new List<string>();
        Dictionary<string, string> replacer = new Dictionary<string, string>();

        // Regex regEx = new Regex();

        void Init(Dictionary<string, string> initDict)
        {
        }

        void AddReplacer()
        {
            replacer.Add("RAMEND", "132");
        }
        public string PreProcess(string line)
        {
            AddReplacer();

            // search for comments and delete them
            line = line.Replace('\t', ' ');
            string[] elements = line.Split(';');
            line = elements[0];

            // delete blanks
            line = line.Trim();

            string newline = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ' && line[i + 1] == ' ')
                {
                }
                else newline += line[i];

            }
            line = newline;

            if (line == "")
            {
                afterPrePro.Add(null);
                return null;
            }
            else
            {
                // .def ...
                string[] elementsb = line.Split(' ');
                if (line[0] == '.')
                {
                    if (elementsb[0] == ".def")
                    {
                        replacer.Add(elementsb[1], elementsb[3]);
                    }
                }

                // search for replacers
                foreach (KeyValuePair<string, string> kvp in replacer)
                {
                    for (int i = 1; i < elementsb.Length; i++)
                    {
                        elementsb[i] = elementsb[i].Replace(kvp.Key, kvp.Value);
                    }
                }
                line = "";
                for (int i = 0; i < elementsb.Length; i++)
                {
                    line += elementsb[i] + " ";
                }

                line = Calculate("(((132+10)*10+(20-15))/5)");
                //line = Calculate(line);
                //line = Calculate("((1+1)*1-1)");
                return line = line.Trim();
            }
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
                                    line = line.Replace(line.Substring(count, i - count),Calculate(line.Substring(count, i - count)));
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
