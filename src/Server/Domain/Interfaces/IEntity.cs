using System;

namespace Domain.Interfaces
{
    public interface IEntity
    {
        string Id {get;set;}
        DateTime DateCreated { get; set; }
    }
}