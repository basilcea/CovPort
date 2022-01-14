using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public int Id {get;set;}
        public string Email {get;set;}
        public string Name {get;set;}
        public string UserRole {get;set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        
    }
}