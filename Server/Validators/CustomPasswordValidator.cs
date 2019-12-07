using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SaneServer.Server.Validators
{   
    /// <summary>
    /// Provides the custom password policy for the application
    /// This class is a custom implementation of the asp.net core identity PasswordValidator<TUser> class
    /// https://github.com/aspnet/AspNetCore/blob/bfec2c14be1e65f7dd361a43950d4c848ad0cd35/src/Identity/Extensions.Core/src/PasswordValidator.cs
    /// </summary>
    public class CustomPasswordValidator : ICustomPasswordValidator
    {
        public PasswordOptions passwordOptions { get; private set; }
        
        /// <summary>
        /// Gets the <see cref="IdentityErrorDescriber"/> used to supply error text.
        /// </summary>
        /// <value>The <see cref="IdentityErrorDescriber"/> used to supply error text.</value>
        public IdentityErrorDescriber Describer { get; private set; }

        /// <summary>
        /// Constructions a new instance of <see cref="CustomPasswordValidator"/>.
        /// </summary>
        /// <param name="options">The <see cref="PasswordOptions"/> to specify options for password requirements</param>
        /// <param name="errors">The <see cref="IdentityErrorDescriber"/> to retrieve error text from.</param>
        public CustomPasswordValidator(PasswordOptions options = null, IdentityErrorDescriber errors = null)
        {
            passwordOptions = options ?? new PasswordOptions() {
                RequireDigit   = true,
                RequiredLength = 8,
                RequireUppercase = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true
            };
            Describer = errors ?? new IdentityErrorDescriber();
        }

        /// <summary>
        /// Validates a password as an asynchronous operation.
        /// </summary>
        /// <param name="password">The password supplied for validation</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public virtual Task<IdentityResult> ValidateAsync(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            var errors = new List<IdentityError>();

            if (string.IsNullOrWhiteSpace(password) || password.Length < passwordOptions.RequiredLength)
            {
                errors.Add(Describer.PasswordTooShort(passwordOptions.RequiredLength));
            }
            if (passwordOptions.RequireNonAlphanumeric && password.All(IsLetterOrDigit))
            {
                errors.Add(Describer.PasswordRequiresNonAlphanumeric());
            }
            if (passwordOptions.RequireDigit && !password.Any(IsDigit))
            {
                errors.Add(Describer.PasswordRequiresDigit());
            }
            if (passwordOptions.RequireLowercase && !password.Any(IsLower))
            {
                errors.Add(Describer.PasswordRequiresLower());
            }
            if (passwordOptions.RequireUppercase && !password.Any(IsUpper))
            {
                errors.Add(Describer.PasswordRequiresUpper());
            }
            if (passwordOptions.RequiredUniqueChars >= 1 && password.Distinct().Count() < passwordOptions.RequiredUniqueChars)
            {
                errors.Add(Describer.PasswordRequiresUniqueChars(passwordOptions.RequiredUniqueChars));
            }

            return
                Task.FromResult(errors.Count == 0
                    ? IdentityResult.Success
                    : IdentityResult.Failed(errors.ToArray()));
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is a digit.
        /// </summary>
        /// <param name="c">The character to check if it is a digit.</param>
        /// <returns>True if the character is a digit, otherwise false.</returns>
        public virtual bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is a lower case ASCII letter.
        /// </summary>
        /// <param name="c">The character to check if it is a lower case ASCII letter.</param>
        /// <returns>True if the character is a lower case ASCII letter, otherwise false.</returns>
        public virtual bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is an upper case ASCII letter.
        /// </summary>
        /// <param name="c">The character to check if it is an upper case ASCII letter.</param>
        /// <returns>True if the character is an upper case ASCII letter, otherwise false.</returns>
        public virtual bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is an ASCII letter or digit.
        /// </summary>
        /// <param name="c">The character to check if it is an ASCII letter or digit.</param>
        /// <returns>True if the character is an ASCII letter or digit, otherwise false.</returns>
        public virtual bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }
}