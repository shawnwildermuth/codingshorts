namespace WrongAboutBlazor.Bechdel;

public partial class BechdelClient
{
  public BechdelClient(HttpClient client)
    : this("https://bechdel.azurewebsites.net", client)
  {

  }
}
