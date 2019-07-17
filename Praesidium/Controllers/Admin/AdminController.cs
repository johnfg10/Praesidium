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
            var viewModel = new EditUserViewModel {User = user};
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(EditUserInputModel model, string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("userId was either null, empty or all whitespace");
/*            if (string.IsNullOrWhiteSpace(userId))
            {
                var vm = new ErrorViewModel();
                vm.Error = new ErrorMessage() { Error = "UserId was null, empty or all white space"};
                return View(vm);
            }*/

            model.User.Id = userId;
            var res = await _userManager.UpdateAsync(model.User);
/*            if (!res.Succeeded)
            {
                var vm = new ErrorViewModel();
                vm.Error = new ErrorMessage() { Error = res.Errors.ToJson()};
                return View(vm);
            }*/

            return RedirectToAction("EditUser", new {userId = userId});
        }
    }
}