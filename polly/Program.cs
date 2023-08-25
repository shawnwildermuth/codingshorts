using Polly;
using RestSharp;
using static System.Console;

var client = new RestClient("https://arest.me/api");

var request = new RestRequest("/data/flaky"); // Get By Default

var count = 0;

var retryPolicy = Policy
  .HandleResult<RestResponse<string>>(r => !r.IsSuccessful)
  .WaitAndRetryAsync(5,
    retryCount => TimeSpan.FromSeconds(1),
    (_, _, retryNumber, _) => WriteLine($"Retrying: {retryNumber}"));

var breakerPolicy = Policy
  .HandleResult<RestResponse<string>>(r => !r.IsSuccessful)
  .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10),
                  (_, _, _) => WriteLine("Breaker Hit"),
                  _ => { });

var policy = Policy.WrapAsync(retryPolicy, breakerPolicy);
do
{
  var result = await policy
    .ExecuteAsync(async () => await client.ExecuteAsync<string>(request));

  count++;

  WriteLine(result.IsSuccessful ? "Succeeded" : "Failed");

} while (count < 10);


