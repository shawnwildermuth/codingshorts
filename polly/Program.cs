using RestSharp;
using static System.Console;

var client = new RestClient("https://arest.me/api");

var request = new RestRequest("/data/flaky"); // Get By Default

var count = 0;

do
{
  var result = await client.ExecuteAsync<string>(request);

  count++;

  WriteLine(result.IsSuccessful ? "Succeeded" : "Failed");

} while (count < 10);


