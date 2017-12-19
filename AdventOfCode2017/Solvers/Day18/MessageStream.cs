using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers.Day18
{
    internal class MessageStream
    {
        private Queue<long> _internalStream = new Queue<long>();

        public int TotalMessageCount { get; private set; }

        public bool HasMessage()
        {
            return _internalStream.Count > 0;
        }

        public void Send(long value)
        {
            _internalStream.Enqueue(value);
            TotalMessageCount++;
        }

        public long Receive()
        {
            return _internalStream.Dequeue();
        }
    }
}