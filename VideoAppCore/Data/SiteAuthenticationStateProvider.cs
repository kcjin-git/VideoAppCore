using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VideoAppCore.Data
{
    public class SiteAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;

        public SiteAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            this._sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var emailAddr = await _sessionStorageService.GetItemAsync<string>("EMailAddr");
            var userName = await _sessionStorageService.GetItemAsync<string>("UserName");
            var orgnName = await _sessionStorageService.GetItemAsync<string>("OrgnName");

            ClaimsIdentity identity;

            if (emailAddr != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, emailAddr),
                    new Claim(ClaimTypes.GroupSid, orgnName)
                }, "apiauth_type");

            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        public void SetAuthenticationStateAsync(string email, string userName, string orgnName)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.GroupSid, orgnName)
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void LogOutAuthenticationStateAsync()
        {
            _sessionStorageService.RemoveItemAsync("EMailAddr");
            _sessionStorageService.RemoveItemAsync("UserName");
            _sessionStorageService.RemoveItemAsync("OrgnName");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

        }


    }
}
