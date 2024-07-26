using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BechdelDataServer.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BechdelDataServer.Tests
{
  public abstract class BaseFilmTests : IDisposable
  {

    protected readonly WebApplicationFactory<Program> _app;
    protected readonly HttpClient _client;

    public BaseFilmTests()
    {
      _app = new WebApplicationFactory<Program>();
      _client = _app.CreateClient();

    }

    protected async Task TestFilms(string url)
    {
      var response = await GetFilmResults(url);
      var result = await response.Content.ReadFromJsonAsync<FilmResult>();

      Assert.NotNull(result);
      Assert.True(result?.Count > 0);
      Assert.True(result?.PageCount > 0);
      Assert.True(result?.CurrentPage > 0);
      Assert.True(result?.Results?.Any());

    }

    protected async Task TestFilmsMax(string url)
    {
      var response = await GetFilmResults(url);

      var result = await response.Content.ReadFromJsonAsync<FilmResult>();

      response = await _client.GetAsync($"{url}?page={result?.CurrentPage}");

      var lastResult = await response.Content.ReadFromJsonAsync<FilmResult>();

      Assert.True(lastResult?.Results?.Any());
    }

    protected async Task<HttpResponseMessage> GetFilmResults(string url)
    {
      HttpResponseMessage response = await _client.GetAsync(url);

      Assert.True(response.IsSuccessStatusCode);

      return response;
    }

    public void Dispose()
    {
      _client.Dispose();
      _app.Dispose();
    }
  }
}
