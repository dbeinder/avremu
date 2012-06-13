using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace avrEmu
{
    class Preprocessor
    {
        List<string> inputToList = new List<string>();
        
        public List<string> afterPrePro = new List<string>();
        
        public List<int> lineMapping = new List<int>();
        
        Dictionary<string, string> replacer = new Dictionary<string, string>();

        //Regex regEx = new Regex();

        void Init(Dictionary<string, string> initDict)
        {
        }

        void AddReplacer()
        {
            replacer.Add("RAMEND", "132");
        }
        public List<string> PreProcess(string input)
        {
            string[] inputToArray = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("(((132+10)*10+(20-15))/5)");
            inputToList.AddRange(inputToArray);

            inputToList = CleanUp(inputToList);

            SearchForDef(inputToList);

            AddReplacer();
            
            inputToList = ReplaceDef(inputToList);
            
            inputToList = Calculate(inputToList);

            afterPrePro.AddRange(inputToList);
            //    //line = Calculate(line);
            //    //line = Calculate("((1+1)*1-1)");
            return afterPrePro;
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
                        helpstring = line[i].Substring(0, a) + line[i].Substring(a + 1);

                        line[i] = helpstring;
                        a = 0;
                    }
                }

                // save numbers of lines in which there is something
                if (inputToList[i] != "")
                {
                    lineMapping.Add(i);
                }

            }
            // Delete lines in which there is nothing
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i] == "")
                {
                    line.RemoveAt(i);
                    i--;
                }
            }

            return line;

        }

        private void SearchForDef(List<string> line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                // .def ...
                string[] elementsb = line[i].Split(' ');
                if (line[i][0] == '.')
                {
                    // if .def than add to dictionary replacer
                    if (elementsb[0] == ".def")
                    {
                        replacer.Add(elementsb[1], elementsb[3]);
                    }
                }
            }
        }

        private List<string> ReplaceDef(List<string> line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                string[] elementsb = line[i].Split(' ');
                // search for replacers
                foreach (KeyValuePair<string, string> kvp in replacer)
                {

                    for (int a = 1; a < elementsb.Length; a++)
                    {
                        elementsb[a] = elementsb[a].Replace(kvp.Key, kvp.Value);
                    }
                }
                // replace elements
                line[i] = "";
                for (int a = 0; a < elementsb.Length; a++)
                {
                    line[i] += elementsb[a] + " ";
                }

            }

            return line;
        }

        private List<string> Calculate(List<string> line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                line[i] = SearchForClip(line[i]);

                line[i] = CalculateNumbers(line[i]);
            }
            return line;
        }

        private string SearchForClip(string line)
        {
            int count = 0;
            int start = 0;
            int countOfClips = 0;
            do
            {
                countOfClips = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    count = 0;
                    if (line[i] == '(')
                    {
                        countOfClips++;
                        start = i + 1;
                        do
                        {
                            count++;
                            if (line[count + start - 1] == '(')
                            {
                                countOfClips++;
                                start += count;
                                count = 0;
                            }
                        } while (line[count + start - 1] != ')');

                        line = line.Substring(0, start - 1) + CalculateNumbers(line.Substring(start, count - 1)) + line.Substring(count + start);
                        i = -1;
                    }
                }
            } while (countOfClips != 0);
            return line;
        }

        private string CalculateNumbers(string line)
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
                    if (line[i] == plusminus[r])
                    {
                        int a = TestForIntLeft(line, i);
                        int b = TestForIntRight(line, i);

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

        private int TestForIntLeft(string line, int startPoint)
        {
            int count = startPoint;
            string helpstring = "";
            int a;
            bool integer = true;
            do
            {
                if (int.TryParse(Convert.ToString(line[count - 1]), out a))
                {
                    helpstring = line[count - 1] + helpstring;
                    count--;
                }
                else
                    integer = false;
            } while (integer == true && count > 0);
            if (helpstring != "")
                a = Convert.ToInt32(helpstring);
            return a;
        }

        private int TestForIntRight(string line, int startPoint)
        {
            int count = startPoint;
            string helpstring = "";
            int b;
            bool integer = true;
            do
            {
                if (int.TryParse(Convert.ToString(line[count + 1]), out b))
                {
                    helpstring += line[count + 1];
                    count++;
                }
                else
                    integer = false;
            } while (integer == true && count + 1 < line.Length);
            if (helpstring != "")
                b = Convert.ToInt32(helpstring);
            return b;
        }
    }
}

