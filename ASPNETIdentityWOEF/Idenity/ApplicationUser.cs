using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPNETIdentityWOEF.Idenity
{
    public class ApplicationUser : IUser
    {
        private string _password;

        public virtual int UserId { get; set; }

        public string Id { get { return UserId.ToString(); } }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int? CurrentClient { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long? Phone { get; set; }

        public byte LanguageID { get; set; }

        public string AuthProvider { get; set; }

        public bool EmailConfirmed { get; set; }

        public string ClientsToken { get; set; }

        public DateTimeOffset LockoutEndDate { get; internal set; }

        public int AccessFailedCount { get; internal set; }

        public object PhoneNumber { get; internal set; }

        public object PasswordHash { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            try
            {
                var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

                // Add custom user claims here            

                return userIdentity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal void SetPasswordHash(string password)
        {
            this._password = password;
            this.PasswordHash = password;
        }

        internal string GetPasswordHash()
        {
            return this._password;
        }
    }
}