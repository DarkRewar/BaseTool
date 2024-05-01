using System;
using System.Collections;
using System.Collections.Generic;

namespace BaseTool
{
    public static class RangeExtensions
    {
        public static RangeEnumerator GetEnumerator(this Range range)
        {
            if (range.Start.IsFromEnd || range.End.IsFromEnd)
            {
                throw new ArgumentException(nameof(range));
            }

            return new RangeEnumerator(range.Start.Value, range.End.Value);
        }

        public struct RangeEnumerator : IEnumerator<int>
        {
            private readonly int _end;

            private int _current;

            public RangeEnumerator(int start, int end)
            {
                _current = start - 1; // - 1 fixes a bug in the original code
                _end = end;
            }

            public int Current => _current;

            object IEnumerator.Current => Current;

            public bool MoveNext() => ++_current < _end;

            public void Dispose() { }

            public void Reset() => throw new NotImplementedException();
        }
    }
}