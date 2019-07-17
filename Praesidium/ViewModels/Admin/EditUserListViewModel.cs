using System.Collections.Generic;
using Praesidium.Models;

namespace Praesidium.ViewModels.Admin
{
    public class EditUserListViewModel : EditUserListInputModel
    {
        public List<AuctorUser> Users { get; set; } = new List<AuctorUser>();
    }
}