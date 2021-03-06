﻿using System;
using System.Collections.Generic;

namespace _293_defusingTheBomb
{
    class Model : IModel
    {
        // Wire states
        const int UNMODIFIED = 0;
        const int CUT = 1;
        const int BLOCKED = 4;
        const int REQUIRED = 2;
        const int REQUIRED_OR = 3;

        // Wire colors
        const int WHITE = 0;
        const int BLACK = 1;
        const int PURPLE = 2;
        const int RED = 3;
        const int GREEN = 4;
        const int ORANGE = 5;

        private List<string> wires;
        private int[] wireState = { 0, 0, 0, 0, 0, 0 };
        private string[] wireStateString = { "", " (CUT)", " (REQUIRED)", " (ONE REQUIRED)", " (BLOCKED)"};

        private int triggerWire;
        public int Triggerwire
        {
            get
            {
                return triggerWire;
            }
            
        }


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
            if (WireIsCut(wire))
                return "";

            if (WireIsBlocked(wire))
                return "boom";

            CheckIfWireWasRequired(wire);

            if (wire == triggerWire)
                return "boom";

            BlockWire(wire); // if should be blocked
            wireState[wire] = CUT;

            return "";
        }

        private void CheckIfWireWasRequired(int wire)
        {

            if (wireState[WHITE] == REQUIRED_OR && wireState[ORANGE] == REQUIRED_OR)
            {
                wireState[WHITE] = UNMODIFIED;
                wireState[ORANGE] = UNMODIFIED;
            }

            if (wireState[RED] == REQUIRED_OR && wireState[BLACK] == REQUIRED_OR)
            {
                wireState[RED] = UNMODIFIED;
                wireState[BLACK] = UNMODIFIED;
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
                    

                if (wirestate == UNMODIFIED)
                {
                    return false;
                }
                
            }

            return true;
            
        }

        private void BlockWire(int wire)
        {
            if (WireIsBlocked(wire) || WireIsCut(wire))
                return;

            
            switch (wire)
            {
                case WHITE:
                    if (wireState[BLACK] == UNMODIFIED) wireState[BLACK] = BLOCKED;
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
                    break;

                case ORANGE:
                    if(wireState[RED] == UNMODIFIED && wireState[BLACK] == UNMODIFIED)
                    {
                        wireState[RED] = REQUIRED_OR;
                        wireState[BLACK] = REQUIRED_OR;
                    }
                    break;

                default:
                    throw new IndexOutOfRangeException();
                    
            };
        }

        private bool WireIsBlocked(int i)
        {
            return wireState[i].Equals(BLOCKED);
        }

        private bool WireIsCut(int i)
        {
            return wireState[i].Equals(CUT);
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
                
                
                        
                result += "\n";
                i++;
            }

            return result;
        }

        public string GetWiresFinished()
        {
            string result = "\n";
            int i = 0;
            foreach (string wire in wires)
            {
                result += (i + 1) + " " + wire + WireStateToString(i);
                if (i == triggerWire)
                {
                    
                    
                        result += " (TRIGGER WIRE)";
                    


                }


                result += "\n";
                i++;
            }

            return result;
        }



    }
        
    }
