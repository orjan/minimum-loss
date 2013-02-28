using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumLoss
{
    public class LossCalculator
    {
        private readonly int expected;
        private readonly Stack<Match> matchesInProgress = new Stack<Match>();
        private readonly Stack<int> units;
        private readonly IBestMatchFinder heavyIsBetterThanSmall;


        public LossCalculator(int[] unit, int expected)
        {
            this.expected = expected;
            units = CreateBaseStack(unit);
            heavyIsBetterThanSmall = new HeavyIsBetterThanSmall();
        }

        private Stack<int> CreateBaseStack(int[] ids)
        {
            var stack = new Stack<int>(ids.Length);
            foreach (int i in ids.OrderBy(i => i))
            {
                stack.Push(i);
            }
            return stack;
        }

        // TODO: remove best match from this class and use calculatematches instead
        public Match BestMatch()
        {
            return heavyIsBetterThanSmall.BestMatch(CalculateMatches());
        }

        public IEnumerable<Match> CalculateMatches()
        {
            matchesInProgress.Push(new Match(expected, units));

            while (matchesInProgress.Count > 0)
            {
                var match = matchesInProgress.Pop();
                {
                    if (match.Units.Count == 0)
                    {
                        throw new Exception("This should not happend");
                    }

                    int unit = match.Units.Pop();

                    int n = 0;
                    if (match.Units.Count == 0)
                    {
                        n = match.Remaining / unit;
                        if (match.Remaining % unit != 0)
                        {
                            n++;
                        }
                    }

                    for (int i = n; ; i++)
                    {
                        Match newMatch = Match.DeepClone(match);
                        newMatch.AddAmount(unit, i);

                        if (newMatch.IsCompleted)
                        {
                            Console.WriteLine(newMatch);
                            yield return newMatch;
                            break;
                        }

                        matchesInProgress.Push(newMatch);
                    }
                }
            }
        }
    }

    public class HeavyIsBetterThanSmall : IBestMatchFinder
    {
        private readonly Stack<Match> completedMatches = new Stack<Match>();

        public Match BestMatch(IEnumerable<Match> calculateMatches)
        {
            foreach (var calculateMatch in calculateMatches)
            {
                if (calculateMatch.Remaining == 0)
                {
                    return calculateMatch;
                }
            }
            return calculateMatches.OrderByDescending(x => x.Remaining).First();
        }
    }

    public interface IBestMatchFinder
    {
        Match BestMatch(IEnumerable<Match> calculateMatches);
    }
}