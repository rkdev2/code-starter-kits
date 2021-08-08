using System.Collections.Generic;

namespace BowlingBall
{
    public class Frame
    {
        private int _standingPins = 10;
        private readonly bool _isFinalFrame;

        public List<int> RolledPins { get; } = new List<int>();
        public bool IsFrameClosed => !_isFinalFrame && _standingPins == 0 ||
                                     !_isFinalFrame && RolledPins.Count == 2 ||
                                     RolledPins.Count == 3;

        public Frame(bool isFinalFrame = false)
        {
            _isFinalFrame = isFinalFrame;
        }

        public void RegisterRoll(int pins)
        {
            RolledPins.Add(pins);
            _standingPins -= pins;
        }
    }
}