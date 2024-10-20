using Microsoft.AspNetCore.Components;
using WrongAboutBlazor.State;

namespace WrongAboutBlazor.Pages;

public class HomePage : ComponentBase
{
  [Inject]
  public required AppState State { get; set; }

  public string? ErrorMessage { get; set; }

  protected override async Task OnInitializedAsync()
  {
    var success = await State.LoadYears();

    if (!success) ErrorMessage = "Failed call API";
    else if (State.CurrentYear > 0) await LoadYear();
  }

  protected async Task LoadYear()
  {
    if (State.CurrentYear != default)
    {
      var success = await State.LoadYear();

      if (!success) ErrorMessage = "Failed to load films";
    }
  } 
}
