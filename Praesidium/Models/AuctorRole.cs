using System;
using Microsoft.AspNetCore.Identity;

namespace Praesidium.Models
{
    public class AuctorRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}