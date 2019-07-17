using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;

namespace Praesidium.Models
{
    public class AuctorUser
    {
        public bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string ToString()
        {
            return base.ToString();
        }

        [BsonIgnoreIfNull]
        public string Id { get; set; }
        
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public List<Claim> UserClaims { get; set; } = new List<Claim>();
        
        public List<UserLoginInfo> UserLoginInfos { get; set; } = new List<UserLoginInfo>();

        public List<string> UserRoles { get; set; } = new List<string>();
    }
}