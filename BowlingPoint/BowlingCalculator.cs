using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BowlingPoint
{
    public class BowlingCalculator
    {
        private int normalGameFrames = Int32.Parse(ConfigurationManager.AppSettings["normalGameFrames"]);
        private IList<Frame> frames = new List<Frame>();
        public IList<int> currentScore { get; private set; }

        public BowlingCalculator()
        {
            currentScore = new List<int>();
        }

        public void CalculateScore()
        {
            int frameSum = 0;
            for (int i = 0; i < frames.Count && i < normalGameFrames; i++)
            {
                int bonus = 0;
                if (frames[i].isStrike || frames[i].isSpare) {
                    bonus = frames[i].isStrike ?
                        GetStrikeBonus(frames, i, i == normalGameFrames - 1) : GetSpareBonus(frames, i);
                    if (bonus == -1)
                        continue;
                }

                frameSum += frames[i].ballOne + frames[i].ballTwo + bonus;
                currentScore.Add(frameSum);
            }
        }

        /// <summary>
        /// Calculates the bonus for a spare
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="currentFrameNr"></param>
        /// <returns>the bonus points or -1 if the bonus can not be calculated yet, meaning
        /// need to wait for one more ball to be able to calculate</returns>
        private int GetSpareBonus(IList<Frame> frames, int currentFrameNr)
        {
            if (frames.Count - 1 != currentFrameNr)
                return frames[currentFrameNr + 1].ballOne;
            return -1;
        }

        /// <summary>
        /// Calculate the bonus of a strike
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="currentFrameNr"></param>
        /// <param name="isLastNormalBall">shows that the next one is a bonus frame</param>
        /// <returns>the bonus points or -1 if the bonus can not be calculated yet, meaning
        /// need to wait for one more ball to be able to calculate</returns>
        private int GetStrikeBonus(IList<Frame> frames, int currentFrameNr, bool isLastNormalBall)
        {
            if (frames.Count - 1 != currentFrameNr)
            {
                if (!frames[currentFrameNr + 1].isStrike || isLastNormalBall)
                    return frames[currentFrameNr + 1].ballOne + frames[currentFrameNr + 1].ballTwo;
                else if (frames.Count - 2 != currentFrameNr && frames[currentFrameNr + 1].isStrike)
                    return frames[currentFrameNr + 1].ballOne + frames[currentFrameNr + 2].ballOne;
            }
            return -1;
        }

        public void AddFrame(Frame frame){
            frames.Add(frame);
        }
    }
}
