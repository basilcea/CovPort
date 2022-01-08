using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public string Id {get;set;}
        public string email {get;set;}
        public string Firstname {get;set;}
        public string Lastname {get;set;}
        public string UserRole {get;set;}
        public DateTime DateCreated { get; set; }
        
    }
}