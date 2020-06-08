using ConsoleApp3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp3.API_Methods
{
    public class CardManipulationMethods
    {
        public static string GetCardColorWithHighestSumOfCardValues(List<Card> cards, List<CardColor> cardColors)
        {
            var cardsGroupedByColor = cards.GroupBy(card => card.color_id);
            var groupsWithSummedValues = cardsGroupedByColor.Select(gcards => new Card { color_id = gcards.Key, value = gcards.Sum(c => c.value) } );
            var result = groupsWithSummedValues.Select(g => new Card { color_id = g.color_id, value = g.value }).Where(g => g.value == groupsWithSummedValues.Max(c => c.value)).ToList();

            string color = cardColors.Where(cc => cc.id == result.ElementAt(0).color_id).First().color;

            return color;
        }

        public static int CalculateTotalPointsOfWinnigSets(List<Card> cards, List<WinningSet> winnigSets)
        {
            int totalPoints = 0;
            StringBuilder cardSequence = new StringBuilder("");

            cards.ForEach(c => cardSequence.Append(c.color_id));

            string cardSequenceString = cardSequence.ToString();

            foreach (var set in winnigSets)
            {
                StringBuilder currentWinnigSet = new StringBuilder("");
                set.set.ForEach(e => currentWinnigSet.Append(e));
                string currentWinnigSetString = currentWinnigSet.ToString();
                int currentIndex = 0;
                int incrementor = currentWinnigSet.Length;

                while ((currentIndex < cardSequence.Length) && (currentIndex + incrementor < cardSequence.Length))
                {
                    string subString = cardSequenceString.Substring(currentIndex, incrementor);
                    if (subString == currentWinnigSetString)
                    {
                        totalPoints += set.points;
                        currentIndex += incrementor;
                        continue;
                    }
                    else
                    {
                        currentIndex += 1;
                        continue;
                    }
                }
            }

            return totalPoints;
        }

        public static int CalculateNumberOfGroups(List<Card> cards)
        {
            cards = cards.OrderBy(c => c.value).ToList();
            int result = 0;
            int curSum = 0;
            int startingPointer = 0;
            int endingPointer = cards.Count - 1;

            while (true)
            {
                if (startingPointer == endingPointer)
                {
                    result++;
                    break;
                }
                else if(startingPointer > endingPointer)
                {
                    break;
                }

                if (curSum + cards.ElementAt(startingPointer).value + cards.ElementAt(endingPointer).value < 10)
                {
                    curSum += cards.ElementAt(startingPointer).value;
                    startingPointer++;
                    continue;
                }
                else if (curSum + cards.ElementAt(startingPointer).value + cards.ElementAt(endingPointer).value > 10)
                {
                    endingPointer--;
                    result++;
                    curSum = 0;
                    continue;
                }
                else if (curSum + cards.ElementAt(startingPointer).value + cards.ElementAt(endingPointer).value == 10)
                {
                    endingPointer--;
                    startingPointer++;
                    result++;
                    curSum = 0;
                    continue;
                }
            }

            return result;
        }
    }
}