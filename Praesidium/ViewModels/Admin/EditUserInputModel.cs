using System;
using Praesidium.Models;

namespace Praesidium.ViewModels.Admin
{
    public class EditUserInputModel
    {
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        public DateTimeOffset LockoutEnd { get; set; }
        
        public bool LockoutEnabled { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; }
        
        public int AccessFailedCount { get; set; }
    }
}