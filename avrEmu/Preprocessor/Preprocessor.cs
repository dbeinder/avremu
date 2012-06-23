using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace avrEmu
{
    class Preprocessor
    {
        private List<string> inputToList;
        private List<int> linesToRemove;
        private Dictionary<string, int> jumpMarks;
        private Dictionary<string, string> definitions = new Dictionary<string, string>();

        public List<string> ProcessedLines { get; private set; }

        public List<int> LineMapping { get; private set; }

        private Dictionary<string, string> replacer = new Dictionary<string, string>();


        public Preprocessor(Dictionary<string, string> constants)
        {
            foreach (KeyValuePair<string, string> item in constants)
                this.replacer.Add(item.Key, item.Value);
        }

        public List<string> Process(string input)
        {
            inputToList = new List<string>();
            ProcessedLines = new List<string>();
            LineMapping = new List<int>();
            linesToRemove = new List<int>();
            jumpMarks = new Dictionary<string, int>();
            definitions = new Dictionary<string, string>();

            string[] inputToArray = input.Split(Environment.NewLine.ToCharArray());

            inputToList.AddRange(inputToArray);

            SearchForDef(inputToList);

            inputToList = FindJumpMarks(inputToList);

            inputToList = CleanUp(inputToList, linesToRemove);

            inputToList = Replace(inputToList);

            inputToList = ReplaceJumpMarks(inputToList);

            inputToList = Calculate(inputToList);

            inputToList = SearchForHighAndLow(inputToList);



            ProcessedLines.AddRange(inputToList);

            return ProcessedLines;
        }


        private List<string> CleanUp(List<string> line, List<int> linesToRemove)
        {
            for (int i = 0; i < line.Count; i++)
            {
                if (linesToRemove.Contains(i))
                    continue;

                // search for comments and delete them
                line[i] = line[i].Replace('\t', ' ');
                string[] elements = line[i].Split(';');
                line[i] = elements[0];

                // delete WhiteSpaces
                line[i] = line[i].Trim();
                line[i] = line[i].ToLower();

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
                    LineMapping.Add(i);
                }
            }

            // Build new list with all line with content
            List<string> cleanedLines = new List<string>();
            foreach (int contentLine in LineMapping)
                cleanedLines.Add(line[contentLine]);

            return cleanedLines;
        }

        private void SearchForDef(List<string> line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                // .def ...
                if (line[i].Length < 1)
                    continue;

                if (line[i][0] == '.')
                {
                    string[] elementsb = line[i].Split(' ');

                    // if .def than add to dictionary replacer
                    if (elementsb[0] == ".def")
                    {
                        definitions.Add(elementsb[1], elementsb[3]);
                    }
                    linesToRemove.Add(i);
                }
            }
        }

        private List<string> Replace(List<string> line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                string[] elementsb = line[i].Split(' ');
                // search for replacers
                foreach (KeyValuePair<string, string> kvp in replacer.Concat(definitions))
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

        private List<string> SearchForHighAndLow(List<string> line)
        {
            for (int a = 0; a < line.Count; a++)
            {
                bool integer = true;
                string helpstring = "";
                int count;
                int b;
                for (int i = 0; i < line[a].Length; i++)
                {
                    if (i + 4 < line[a].Length && line[a].Substring(i, 4) == "high")
                    {
                        count = i + 4;
                        do
                        {
                            if (int.TryParse(Convert.ToString(line[a][count]), out b))
                            {
                                helpstring += line[a][count];
                                count++;
                            }
                            else
                                integer = false;
                        } while (integer == true && count < line[a].Length);
                        b = Convert.ToInt32(helpstring);

                        b = (b & 0xff00) / 256;
                        line[a] = line[a].Replace("high" + helpstring, Convert.ToString(b));
                        i = 0;
                    }

                    if (i + 4 < line[a].Length && line[a].Substring(i, 3) == "low")
                    {
                        count = i + 3;
                        do
                        {
                            if (int.TryParse(Convert.ToString(line[a][count]), out b))
                            {
                                helpstring += line[a][count];
                                count++;
                            }
                            else
                                integer = false;
                        } while (integer == true && count < line[a].Length);
                        b = Convert.ToInt32(helpstring);

                        b = b & 0x00ff;
                        line[a] = line[a].Replace("low" + helpstring, Convert.ToString(b));
                        i = 0;
                    }
                }
            }

            return line;
        }
        private List<string> FindJumpMarks(List<string> line)
        {
            for (int i = 0; i < line.Count; i++)
            {
                for (int a = 0; a < line[i].Length; a++)
                {
                    if (line[i][a] == ':')
                    {
                        string helpstring = line[i].Substring(0, a);
                        line[i] = line[i].Substring(a + 1);
                        line[i] = line[i].Trim();

                        if (line[i] == "")
                        {
                            linesToRemove.Add(i);
                        }

                        jumpMarks.Add(helpstring, i);
                    }
                }
            }

            return line;
        }

        private List<string> ReplaceJumpMarks(List<string> line)
        {
            int max = LineMapping.Max();

            foreach (KeyValuePair<string, int> kvp in jumpMarks)
            {
                int target = kvp.Value;

                while (LineMapping.IndexOf(target) == -1 && target < max)
                    target++;

                for (int i = 1; i < line.Count; i++)
                {
                    line[i] = line[i].Replace(kvp.Key, (LineMapping.IndexOf(target) - i).ToString()).Trim();
                }
            }

            return line;
        }
    }
}

