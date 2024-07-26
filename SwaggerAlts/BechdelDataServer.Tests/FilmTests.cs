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
  public class FilmTests : BaseFilmTests
  {


    [Fact]
    public async Task CanGetFilms() => await TestFilms("/api/films");

    [Fact]
    public async Task CanGetFilmsMax() => await TestFilmsMax("/api/films");

    [Fact]
    public async Task CanGetFilmsPassed() => await TestFilms("/api/films/passed");

    [Fact]
    public async Task CanGetFilmsPassedMax() => await TestFilmsMax("/api/films/passed");

    [Fact]
    public async Task CanGetFilmsFailed() => await TestFilms("/api/films/failed");

    [Fact]
    public async Task CanGetFilmsFailedMax() => await TestFilmsMax("/api/films/failed");

    [Fact]
    public async Task CanGetFilmsPassedByYear() => await TestFilms("/api/films/passed/2013");

    [Fact]
    public async Task CanGetFilmsPassedByYearMax() => await TestFilmsMax("/api/films/passed/2013");

    [Fact]
    public async Task CanGetFilmsFailedByYear() => await TestFilms("/api/films/failed/2013");

    [Fact]
    public async Task CanGetFilmsFailedByYearMax() => await TestFilmsMax("/api/films/failed/2013");


    [Fact]
    public async Task CanGetFilmsByYear() => await TestFilms("/api/films/2013");

    [Fact]
    public async Task CanGetFilmsByYearMax() => await TestFilmsMax("/api/films/2013");

    [Fact]
    public async Task CanGetYears()
    {
      HttpResponseMessage response = await _client.GetAsync("/api/years");

      Assert.True(response.IsSuccessStatusCode);
      
      var result = await response.Content.ReadFromJsonAsync<int[]>();

      Assert.NotNull(result);
      Assert.True(result?.Any());
    }

  }
}
