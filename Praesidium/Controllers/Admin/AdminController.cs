using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Praesidium.ViewModels;
using IdentityServer4.Models;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Praesidium.Models;
using Praesidium.ViewModels.Admin;

namespace Praesidium.Controllers.Admin
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AdminController : Controller
    {
        
        private readonly UserManager<AuctorUser> _userManager;
        private readonly SignInManager<AuctorUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AdminController(
            UserManager<AuctorUser> userManager,
            SignInManager<AuctorUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserList()
        {
            var viewModel = new EditUserListViewModel();
            viewModel.Users.AddRange(_userManager.Users.Take(25));
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("userId was either null, empty or all whitespace");
            
            var user = _userManager.Users.FirstOrDefault(it => it.Id == userId);

            if (user == null)
                return NotFound();
            
            var viewModel = new EditUserViewModel
            {
                Username = user.UserName,
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
            
            if (user.LockoutEnd != null)
                viewModel.LockoutEnd = (DateTimeOffset) user.LockoutEnd;
            
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(EditUserInputModel model, string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("userId was either null, empty or all whitespace");
            
            var user = _userManager.Users.FirstOrDefault(it => it.Id == userId);
            
            if (user == null)
                return NotFound();
            
            if(!string.IsNullOrWhiteSpace(model.Username))
                user.UserName = model.Username;
            
            if(!string.IsNullOrWhiteSpace(model.Email))
                user.Email = model.Email;

            if(!string.IsNullOrWhiteSpace(model.PhoneNumber))
                user.PhoneNumber = model.PhoneNumber;

            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
            
            if(model.LockoutEnd > DateTimeOffset.Now)
                user.LockoutEnd = model.LockoutEnd;
            
            user.LockoutEnabled = model.LockoutEnabled;
            user.AccessFailedCount = model.AccessFailedCount;

            await _userManager.UpdateAsync(user);
            
            return RedirectToAction("EditUser", new {userId = userId});
        }
    }
}