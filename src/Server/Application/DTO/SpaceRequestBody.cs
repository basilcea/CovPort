using System;

namespace Application.DTO
{
    public class SpaceRequestBody
    {
      public int RequesterId {get;set;}
      public string LocationName { get; set; }
      public string Date { get; set; }
      public int SpacesCreated{get;set;}
    }
}