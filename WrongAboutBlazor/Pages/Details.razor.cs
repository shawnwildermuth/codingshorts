using Microsoft.AspNetCore.Components;
using WrongAboutBlazor.Bechdel;
using WrongAboutBlazor.State;

namespace WrongAboutBlazor.Pages;

public class DetailsPage : ComponentBase
{
  [Inject]
  public required AppState State { get; set; }

  [Inject]
  public required NavigationManager Router { get; set; }

  [Parameter]
  public required string ImdbId { get; set; }

  public Film? Film { get; set; }

  protected override async Task OnInitializedAsync()
  {
    if (!State.Films.Any()) await State.LoadYear();
    else Film = State.Films.First(w => w.ImdbId == ImdbId);
  }

}
