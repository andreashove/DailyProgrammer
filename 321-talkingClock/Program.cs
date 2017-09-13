using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace _321_talkingClock
{
    class Program
    {
        static void Main(string[] args)
        {
            string _inputTimestamp;
            var _hours = LoadJson();

            

            Dictionary<string, string> _ones = new Dictionary<string, string>{
                { "0", "" },
                { "1", "one" },
                { "2","two"},
                { "3","three"},
                { "4","four"},
                { "5","five"},
                { "6", "six" },
                { "7","seven"},
                { "8","eight"},
                { "9","nine"}

            };

            Dictionary<string, string> _tens = new Dictionary<string, string>{
                { "0", "oh" },
                { "1", "useHourForThis" },
                { "2","twenty"},
                { "3","thirty"},
                { "4","fourty"},
                { "5","fifty"}
                
            };

            Dictionary<string, string> _tensOfOnes = new Dictionary<string, string>{
                { "10", "ten" },
                { "11", "eleven" },
                { "12", "twelve" },
                { "13", "thirteen" },
                { "14", "fourteen" },
                { "15", "fifteen" },
                { "16", "sixteen" },
                { "17", "seventeen" },
                { "18", "eighteen" },
                { "19", "nineteen" }


            };

            bool _notDone = true;


            while (_notDone)
            {
                string _inHour = "";
                string _inMinute = "";
                string _minuteTens = "";
                string _minuteOnes = "";
                string _amOrPm = "";
                string minutes2 = "";
                string ones = "";

                Console.Write("Input a time (hh:mm) or hit Enter to get current time: ");
                _inputTimestamp = Console.ReadLine();

                if (_inputTimestamp == "")
                {
                    _inputTimestamp = DateTime.Now.Hour.ToString("00.##")+":"+DateTime.Now.Minute.ToString("00.##");
                }
                else if (_inputTimestamp.Length != 5 || _inputTimestamp[2] != ':')
                {
                    Console.WriteLine("Error: \"" + _inputTimestamp + "\" is not in the valid format \"hh:mm\"");
                    continue;
                }

                string[] timestamp = _inputTimestamp.Split(':');

                _inHour = timestamp[0];
                _inMinute = timestamp[1];

                _hours.TryGetValue(_inHour, out string oaut);
                if (int.Parse(_inHour) >= 0 && int.Parse(_inHour) < 12)
                    _amOrPm = "am";
                else
                    _amOrPm = "pm";



                if (_inMinute != "00")
                {
                    
                    _minuteTens = "" + _inMinute[0];
                    _minuteOnes = "" + _inMinute[1];

                    if (int.Parse(_inMinute) >= 10 && int.Parse(_inMinute) < 20)
                    {
                        _tensOfOnes.TryGetValue(_inMinute, out string minutes);
                        minutes2 = minutes;
                    }
                    else
                    {
                        _tens.TryGetValue(_minuteTens, out string minutes);
                        minutes2 = minutes;
                        _ones.TryGetValue(_minuteOnes, out string outOnes);
                        ones = outOnes;
                    }

                    Console.WriteLine("It's " + oaut + " " + minutes2 + " " + ones + " " + _amOrPm);
                }
                else
                {
                    Console.WriteLine("It's " + oaut + " " + _amOrPm);

                }


            }



        }

        static Dictionary<string,string> LoadJson()
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"/config.json"))
            {
                
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }   
    }
}
