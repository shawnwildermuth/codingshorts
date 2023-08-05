namespace WhatIsPolly;

public class Site
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public int YearInscribed { get; set; }
  public string? Url { get; set; }
  public string? ImageUrl { get; set; }
  public string? DescriptionMarkup { get; set; }
  public string? States { get; set; }
  public Location? Location { get; set; }
  public string? CategoryName { get; set; }
  public string? RegionName { get; set; }
}