using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETIdentityWOEF.Idenity
{
    public class MockUserService
    {
        private Dictionary<int, ApplicationUser> users = new Dictionary<int, ApplicationUser>();

        public Task CreateAsync<T>(T user) where T : ApplicationUser, new()
        {
            user.UserId = users.Count + 1;
            users.Add(user.UserId, user);
            return Task.FromResult(0);
        }

        internal Task<ApplicationUser> FindUserByUsernameAsync(string userName)
        {
            var foundUser = (from user in users
                             where user.Value.UserName == userName
                             select user.Value).FirstOrDefault();
            return Task.FromResult<ApplicationUser>(foundUser);
        }

        internal Task<ApplicationUser> FindUserByIdAsync(string userId)
        {
            var foundUser = (from user in users
                             where user.Value.Id == userId
                             select user.Value).FirstOrDefault();
            return Task.FromResult<ApplicationUser>(foundUser);
        }

        internal async Task UpdateAsync(ApplicationUser userId)
        {
            // TODO
        }

        internal Task<string> GetPasswordHashAsync(int userId)
        {
            string retVal = string.Empty;
            var foundUser = (from user in users
                             where user.Value.Id == userId.ToString()
                             select user.Value).FirstOrDefault();
            if (foundUser != null)
                retVal = (foundUser as ApplicationUser).GetPasswordHash();
            return Task.FromResult(retVal);
        }

        internal Task<Dictionary<string, string>> GetAdditionalClaimsAsync(ApplicationUser user)
        {
            return Task.FromResult(new Dictionary<string, string>());
        }

        internal Task<DateTimeOffset> GetLockoutEndDateAsync(string userId)
        {
            var retVal = DateTimeOffset.MinValue;
            var foundUser = (from user in users
                             where user.Value.Id == userId
                             select user.Value).FirstOrDefault();
            if (foundUser != null)
                retVal = (foundUser as ApplicationUser).LockoutEndDate;
            return Task.FromResult(retVal);
        }

        internal Task SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd)
        {
            var foundUser = (from user in users
                             where user.Value.Id == userId
                             select user.Value).FirstOrDefault();
            if (foundUser != null)
                (foundUser as ApplicationUser).LockoutEndDate = lockoutEnd;
            return Task.FromResult<bool>(true);
        }

        internal Task<int> IncrementAccessFailedCountAsync(string userId)
        {
            var retVal = 0;
            var foundUser = (from user in users
                             where user.Value.Id == userId
                             select user.Value).FirstOrDefault();
            if (foundUser != null)
            {
                retVal = (foundUser as ApplicationUser).AccessFailedCount + 1;
                (foundUser as ApplicationUser).AccessFailedCount = retVal;
            }
            return Task.FromResult<int>(retVal);
        }

        internal Task ResetAccessFailedCountAsync(string userId)
        {
            var foundUser = (from user in users
                             where user.Value.Id == userId
                             select user.Value).FirstOrDefault();
            if (foundUser != null)
                (foundUser as ApplicationUser).AccessFailedCount = 0;
            return Task.FromResult<bool>(true);
        }

        internal Task<int> GetAccessFailedCountAsync(string userId)
        {
            var retVal = 0;
            var foundUser = (from user in users
                             where user.Value.Id == userId
                             select user.Value).FirstOrDefault();
            if (foundUser != null)
                retVal = (foundUser as ApplicationUser).AccessFailedCount;
            return Task.FromResult<int>(retVal);
        }
    }
}