using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingPointsApplication
{
    interface IScoresDataWriter
    {
        string PostScores(string json);
    }
}
