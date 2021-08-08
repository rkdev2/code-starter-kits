using System.Collections.Generic;
using System.Linq;

namespace BowlingBall
{
    public class Game
    {
        private readonly List<Frame> _frames = new List<Frame>();

        private const int MaxFrameCount = 10;
        private const int MaxPinCount = 10;

        public void Roll(int pins)
        {
            if (!_frames.Any() || _frames.Last().IsFrameClosed)
            {
                var isFinalFrame = _frames.Count == MaxFrameCount - 1;
                _frames.Add(new Frame(isFinalFrame));
            }

            _frames.Last().RegisterRoll(pins);
        }

        public int GetScore()
        {
            var score = 0;
            for (var frameIndex = 0; frameIndex < _frames.Count; frameIndex++)
            {
                var frame = _frames[frameIndex];
                var frameScore = 0;
                var bonusScore = 0;
                var isStrike = false;

                var maxRollIndex = frame.RolledPins.Count < 2 ? frame.RolledPins.Count : 2;

                for (var rollIndex = 0; rollIndex < maxRollIndex; rollIndex++)
                {
                    var result = frame.RolledPins[rollIndex];
                    frameScore += result;

                    if (result != MaxPinCount) continue;
                    isStrike = true;

                    bonusScore += CalculateBonusPoints(frameIndex, rollIndex, 2);
                    break;
                }

                if (!isStrike && frameScore == MaxPinCount)
                {
                    bonusScore += CalculateBonusPoints(frameIndex, maxRollIndex - 1, 1);
                }

                score += frameScore + bonusScore;
            }

            return score;
        }
        private int CalculateBonusPoints(int frameIndex, int rollIndex, int rollCount)
        {
            if (rollCount == 0)
            {
                return 0;
            }

            var bonusPoints = 0;

            if (_frames[frameIndex].RolledPins.Count > rollIndex + 1)
            {
                bonusPoints += _frames[frameIndex].RolledPins[rollIndex + 1];
                bonusPoints += CalculateBonusPoints(frameIndex, rollIndex + 1, rollCount - 1);
            }
            else
            {
                if (_frames.Count <= frameIndex + 1) return bonusPoints;

                bonusPoints += _frames[frameIndex + 1].RolledPins[0];
                bonusPoints += CalculateBonusPoints(frameIndex + 1, 0, rollCount - 1);
            }

            return bonusPoints;
        }
    }
}