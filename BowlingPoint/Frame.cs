using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingPoint
{
    public class Frame
    {
        public int ballOne { get; private set; }
        public int ballTwo { get; private set; }
        public bool isStrike { get; private set; }
        public bool isSpare { get; private set; } 

        public Frame(int bOne, int bTwo)
        {
            ballOne = bOne;
            ballTwo = bTwo;
            isStrike = bOne == 10;
            isSpare = bOne + bTwo == 10;
        }
    }
}
