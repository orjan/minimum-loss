using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace MinimumLoss
{
    [Serializable] 
    public class Match
    {
        private readonly int target;
        private readonly Stack<int> units;
        private readonly IList<Amount> amounts = new List<Amount>();

        public Match(int target, Stack<int> units)
        {
            this.target = target;
            this.units = units;
        }

        public int Remaining
        {
            get { return target - TotalValue; }
        }

        public bool IsCompleted
        {
            get { return Remaining <= 0; }
        }

        public Stack<int> Units {get { return units; }} 

        public int TotalValue
        {
            get { return amounts.Sum(x=>x.Total()); }
        }

        public IEnumerable<Amount> Amounts
        {
            get { return amounts; }
        }

        public void AddAmount(int value, int n)
        {
            amounts.Add(new Amount(value, n));
        }

        public static Match DeepClone(Match obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (Match)formatter.Deserialize(ms);
            }
        }
    }
}