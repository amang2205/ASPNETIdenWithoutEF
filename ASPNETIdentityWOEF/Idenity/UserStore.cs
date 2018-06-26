using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPNETIdentityWOEF.Idenity
{
    public class UserStore<T> : IUserStore<T>,
        IUserPasswordStore<T>,
        IUserClaimStore<T>,
        IUserEmailStore<T>,
        IUserLoginStore<T>,
        IUserLockoutStore<T, string> where T : ApplicationUser, new()
    {
        private MockUserService _userService;

        public UserStore(MockUserService userService)
        {
            _userService = userService;
        }

        public Task AddClaimAsync(T user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(T user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(T user)
        {
            await _userService.CreateAsync(user);
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<T> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByIdAsync(string userId)
        {
            var user = await _userService.FindUserByIdAsync(userId);
            return (user as T);
        }

        public async Task<T> FindByNameAsync(string userName)
        {
            var user = await _userService.FindUserByUsernameAsync(userName);
            return (user as T);
        }

        public async Task<IList<Claim>> GetClaimsAsync(T user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("userid", user.Id),
            };
            foreach (var claim in (await _userService.GetAdditionalClaimsAsync(user)))
                claims.Add(new Claim(claim.Key, claim.Value));
            return (claims as IList<Claim>);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(T user)
        {
            var passwordHash = await _userService.GetPasswordHashAsync(user.UserId);
            user.SetPasswordHash(passwordHash);
            return user.GetPasswordHash();
        }

        public Task<bool> HasPasswordAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(T user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(T user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(T user, string passwordHash)
        {
            user.SetPasswordHash(passwordHash);
            return Task.FromResult(0);
        }

        public async Task UpdateAsync(T user)
        {
            await _userService.UpdateAsync(user);
        }

        public Task SetEmailAsync(T user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(T user)
        {
            return Task.FromResult(user.Email);
            //throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(T user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<T> FindByEmailAsync(string email)
        {
            var user = await _userService.FindUserByUsernameAsync(email);
            return (user as T);
        }

        public async Task<DateTimeOffset> GetLockoutEndDateAsync(T user)
        {
            return await _userService.GetLockoutEndDateAsync(user.Id);
        }

        public async Task SetLockoutEndDateAsync(T user, DateTimeOffset lockoutEnd)
        {
            await _userService.SetLockoutEndDateAsync(user.Id, lockoutEnd);
        }

        public async Task<int> IncrementAccessFailedCountAsync(T user)
        {
            return await _userService.IncrementAccessFailedCountAsync(user.Id);
        }

        public async Task ResetAccessFailedCountAsync(T user)
        {
            await _userService.ResetAccessFailedCountAsync(user.Id);
        }

        public async Task<int> GetAccessFailedCountAsync(T user)
        {
            return await _userService.GetAccessFailedCountAsync(user.Id);
        }

        public Task<bool> GetLockoutEnabledAsync(T user)
        {
            return Task.FromResult(true);
        }

        public Task SetLockoutEnabledAsync(T user, bool enabled)
        {
            return Task.FromResult(0);
        }
    }
}