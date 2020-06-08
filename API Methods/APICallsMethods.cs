using ConsoleApp3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp3.API_Methods
{
    public class APICallsMethods
    {
        public static async Task<GetDataCallDTO> GetData(string linkUri)
        {
            var client = new HttpClient();
            var uri = new Uri(linkUri);
            Stream respStream = await client.GetStreamAsync(uri);
            var result = await JsonSerializer.DeserializeAsync<GetDataCallDTO>(respStream);

            return result;
        }

        public static async Task<List<Card>> DrawCards(string linkUri, int amount)
        {
            var client = new HttpClient();
            var uri = new Uri(linkUri + $"?numberOfCards={amount}");
            Stream respStream = await client.GetStreamAsync(uri);
            var result = await JsonSerializer.DeserializeAsync<List<Card>>(respStream);

            return result;
        }

        public static async Task PostResults(string linkUri, Results results)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(linkUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = $"{{\"most_cards\":\"{results.most_cards}\"," +
                  $"\"winning_points\":{results.winning_points}," + 
                  $"\"groups\":{results.groups}}}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse) await httpWebRequest.GetResponseAsync();
            Console.WriteLine(httpResponse.StatusCode);

        }
    }
}
