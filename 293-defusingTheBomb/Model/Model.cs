using System;
using System.Collections.Generic;

namespace _293_defusingTheBomb
{
    class Model : IModel
    {
        const int BLOCKED = 9;
        const int CUTTED = 5;
        const int REQUIRED = 2;
        const int REQUIRED_OR = 3;

        private List<string> wires;
        private int[] wireState = { 0, 0, 0, 0, 0, 0 };
        private string[] wireStateString = { "", "", " (REQUIRED)", " (ONE REQUIRED)", "", " (CUTTED)", "", "", "", " (BLOCKED)" };

        
        private int triggerWire;
        public int Triggerwire
        {
            get
            {
                return triggerWire;
            }
            
        }

        private bool difficulty = false;

        const int UNMODIFIED = 0;
        const int WHITE = 0;
        const int BLACK = 1;
        const int PURPLE = 2;
        const int RED = 3;
        const int GREEN = 4;
        const int ORANGE = 5;

        public Model()
        {
            InitializeWires();
            


        }

        public void InitializeWires()
        {
            Random rnd = new Random();
            triggerWire = rnd.Next(0, 6);

            wires = new List<string>(){
                 "White ",
                 "Black ",
                 "Purple",
                 "Red   ",
                 "Green ",
                 "Orange"
             };

            wireState = new int[] { 0, 0, 0, 0, 0, 0 };
        }

        

        public string CutWire(int wire)
        {
            if (WireIsCutted(wire))
                return "";

            if (WireIsBlocked(wire))
                return "boom";

            CheckIfWireWasRequired(wire);

            
            if (wire == triggerWire)
                return "boom";

            BlockWire(wire); // if should be blocked

            

            wireState[wire] = CUTTED;

            //if (GameIsWon())
             //   return "won";


            return "";
        }

        private void CheckIfWireWasRequired(int wire)
        {
            int c = 0;
            if (wire == WHITE || wire == ORANGE)
            {
                foreach (int wirestate in wireState)
                {
                    if (wirestate == REQUIRED_OR)
                    {
                        wireState[c] = UNMODIFIED;
                    }
                    c++;

            }
            }
            
        }

        public bool GameIsWon()
        {
            int i = -1;
            foreach(int wirestate in wireState)
            {
                i++;

                if (i == triggerWire)
                {
                    continue;
                }
                    

                if (wirestate == 0)
                {
                    return false;
                }
                
            }

            return true;
            
        }

        private void BlockWire(int wire)
        {
            if (WireIsBlocked(wire) || WireIsCutted(wire))
                return;

            
            switch (wire)
            {
                case WHITE:
                    wireState[BLACK] = wireState[BLACK] == UNMODIFIED ? BLOCKED : wireState[BLACK];
                    //if (wireState[BLACK] == UNMODIFIED) wireState[BLACK] = BLOCKED;
                    break;

                case BLACK:
                case PURPLE:
                    if (wireState[WHITE] == UNMODIFIED) wireState[WHITE] = BLOCKED;
                    if (wireState[GREEN] == UNMODIFIED) wireState[GREEN] = BLOCKED;
                    if (wireState[ORANGE] == UNMODIFIED) wireState[ORANGE] = BLOCKED;
                    break;

                case RED:
                    if (wireState[GREEN] == UNMODIFIED) wireState[GREEN] = REQUIRED;
                    break;

                case GREEN:
                    if (wireState[WHITE] == UNMODIFIED && wireState[ORANGE] == UNMODIFIED)
                    {
                        wireState[WHITE] = REQUIRED_OR;
                        wireState[ORANGE] = REQUIRED_OR;
                    }
                        //if (wireState[ORANGE] == UNMODIFIED) wireState[ORANGE] = REQUIRED_OR;
                    break;

                case ORANGE:
                    break;

                default:
                    throw new IndexOutOfRangeException();
                    
            };
                

            
            
        }
        public void UpdateDifficultySetting(bool d)
        {
            difficulty = d;
        }

        private bool WireIsBlocked(int i)
        {
            return wireState[i].Equals(BLOCKED);
        }

        private bool WireIsCutted(int i)
        {
            return wireState[i].Equals(CUTTED);
        }

        private string WireStateToString(int i)
        {
            

            return wireStateString[wireState[i]];
        }

        

        public string GetWires()
        {
            string result = "\n";
            int i = 0;
            foreach (string wire in wires)
            {
                result += (i + 1) + " " + wire + WireStateToString(i);
                if (i == triggerWire)
                { 
                    if (GameIsWon())
                    {
                        result += " (TRIGGER WIRE)";
                    }
                    

                }
                
                        
                result += "\n";
                i++;
            }

            return result;
        }

        private void Explode()
        {
            throw new NotImplementedException();
        }

    }
        
    }
