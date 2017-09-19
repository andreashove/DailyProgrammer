using System;
using System.Collections.Generic;


namespace MVC_console
{
    //Model: Business logic

    public class Model : IModel
    {
        private Dictionary<string, string> _ones = new Dictionary<string, string>{
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

        private Dictionary<string, string> _tens = new Dictionary<string, string>{
                { "0", "oh" },
                { "1", "useHourForThis" },
                { "2","twenty"},
                { "3","thirty"},
                { "4","fourty"},
                { "5","fifty"}

            };

        private Dictionary<string, string> _tensOfOnes = new Dictionary<string, string>{
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

        private Dictionary<string, string> _hours = new Dictionary<string, string>
        {
                {"00", "twelve"},
                {"01", "one"},
                {"02", "two"},
                {"03", "three"},
                {"04", "four"},
                {"05", "five"},
                {"06", "six"},
                {"07", "seven"},
                {"08", "eight"},
                {"09", "nine"},
                {"10", "ten"},
                {"11", "eleven"},
                {"12", "twelve"},
                {"13", "one"},
                {"14", "two"},
                {"15", "three"},
                {"16", "four"},
                {"17", "five"},
                {"18", "six"},
                {"19", "seven"},
                {"20", "eight"},
                {"21", "nine"},
                {"22", "ten"},
                {"23", "eleven"},
                {"24", "twelve"}
        };

        
        public string HandleUserInput(string userInput)
        {
            // quit command
            if (userInput == "q")
                Environment.Exit(0);

            // validation of input format
            if (!InputIsValid(userInput))            
                return "Error: \"" + userInput + "\" is not valid (format: \"hh:mm\")";
         
            var time = VerifyInputFormat(userInput);


            // conversion
            return Convert(time);
            

        }

        private string Convert(string time)
        {
            var hour = ConvertHour(time);
            var minute = ConvertMinute(time);
            var amOrPm = AmOrPm(time);
            
            return "It's " + hour + " " + minute + " " + amOrPm;
        }

        private string VerifyInputFormat(string userInput)
        {
            if (String.IsNullOrEmpty(userInput))
                return DateTime.Now.Hour.ToString("00.##") + ":" + DateTime.Now.Minute.ToString("00.##"); //00.## ensures that 5:30 is returned as 05:30

            else
                return userInput;



        }

        private bool InputIsValid(string userInput)
        {
            if (String.IsNullOrEmpty(userInput))            
                return true;

            else if (userInput.Length == 5 && userInput[2] == ':')           // if input is of correct format "HH:mm" ...       
                return TimeSpan.TryParse(userInput, out var unusedOutput);   // verify that the time is between 00:00 and 23:59   

            else
                return false;
        }

        private string ConvertHour(string time)
        {
            // from "12:34" take out "12"
            var hour = time.Split(':')[0];
            _hours.TryGetValue(hour, out string h);
            return h;
           
        }

        private string ConvertMinute(string time)
        {
            // from "12:34" take out "34"
            string minute = time.Split(':')[1];

            var minutes = "";

            if (minute != "00")
            {

                var intMinute = int.Parse(minute);
                if (intMinute >= 10 && intMinute < 20)
                {
                    _tensOfOnes.TryGetValue(minute, out string outMinutes);
                    minutes = outMinutes;
                }
                else
                {
                    minutes = SplitMinuteAndConvertIndividually(minute);
                }

            }

            return minutes;
        }

        private string SplitMinuteAndConvertIndividually(string minute)
        {
            string _minuteTens = "" + minute[0];
            string _minuteOnes = "" + minute[1];
            _tens.TryGetValue(_minuteTens, out string outMinutes);

            _ones.TryGetValue(_minuteOnes, out string outOnes);

            return outMinutes + " " + outOnes;
        }

        private string AmOrPm(string time)
        {
            // from "21:34" take out "21"
            string hour = time.Split(':')[0];
            
            var intHour = int.Parse(hour);

            if (intHour >= 0 && intHour < 12)
                return "a.m.";
            else
                return "p.m.";
            
            
        }

    }
}
