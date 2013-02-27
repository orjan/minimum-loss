using System;

namespace MinimumLoss
{
    [Serializable]
    public class Amount
    {
        public Amount(int value, int n)
        {
            Value = value;
            N = n;
        }

        public int N { get; set; }
        public int Value { get; set; }

        public int Total()
        {
            return N*Value;
        }
    }
}