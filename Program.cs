using ConsoleApp3.API_Methods;
using ConsoleApp3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var data = await APICallsMethods.GetData("http://simplelive-api.azurewebsites.net/api/TestCandidate/GetData//");
            var cards = await APICallsMethods.DrawCards("http://simplelive-api.azurewebsites.net/api/TestCandidate/CardsDraw", 20);
            string color = CardManipulationMethods.GetCardColorWithHighestSumOfCardValues(cards, data.card_colors);
            var groups = CardManipulationMethods.CalculateNumberOfGroups(cards);
            int winnig_points = CardManipulationMethods.CalculateTotalPointsOfWinnigSets(cards, data.winning_Sets);
            Results results = new Results { most_cards = color, groups = groups, winning_points = winnig_points };
            await APICallsMethods.PostResults("http://simplelive-api.azurewebsites.net/api/TestCandidate/Results", results);
        }
    }
    
}
