using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumLoss
{
    public class LossCalculator
    {
        private readonly int expected;
        private readonly Stack<Match> matchesInProgress = new Stack<Match>(); 
        private readonly Stack<Match> completedMatches = new Stack<Match>();
        private readonly Stack<int> units;

        public LossCalculator(int[] unit, int expected)
        {
            this.expected = expected;
            this.units = CreateBaseStack(unit);
        }

        private Stack<int> CreateBaseStack(int[] ids)
        {
            var stack = new Stack<int>(ids.Length);
            foreach (var i in ids.OrderBy(i => i))
            {
                stack.Push(i);
            }
            return stack;
        } 

        // TODO: remove best match from this class and use calculatematches instead
        public Match BestMatch()
        {
            return CalculateMatches().OrderByDescending(x=>x.Remaining).First();
        }
        
        public IEnumerable<Match> CalculateMatches()
        {
            matchesInProgress.Push(new Match(expected, units));

            while (matchesInProgress.Count > 0)
            {
                CalculateMatch(matchesInProgress.Pop());
            }

            return completedMatches;
        } 

        private void CalculateMatch(Match match)
        {
            if (match.Units.Count == 0)
            {
                return;
            }

            var unit = match.Units.Pop();

            var n = match.Remaining / unit;

            for (int i = 0; i <= n + 1; i++)
            {
                var newMatch = Match.DeepClone(match);
                newMatch.AddAmount(unit, i);

                if (newMatch.IsCompleted)
                {
                    completedMatches.Push(newMatch);
                }
                else
                {
                    matchesInProgress.Push(newMatch);
                }
            }

        }
    }
}
