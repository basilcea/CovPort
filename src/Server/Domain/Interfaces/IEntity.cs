using System;

namespace Domain.Interfaces
{
    public interface IEntity
    {
        int Id {get;set;}
        DateTime DateCreated { get; set;}
        DateTime DateUpdated {get;set;}
    }
}