using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SaneServer.Server.Validators
{
    /// <summary>
    /// Provides an abstraction for validating passwords.
    /// This interface is a custom implementation of the asp.net core identity IPasswordValidator<TUser> interface
    /// https://github.com/aspnet/AspNetCore/blob/bfec2c14be1e65f7dd361a43950d4c848ad0cd35/src/Identity/Extensions.Core/src/IPasswordValidator.cs
    /// </summary>
    public interface ICustomPasswordValidator
    {
        /// <summary>
        /// Validates a password as an asynchronous operation.
        /// </summary>
        /// <param name="password">The password supplied for validation</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IdentityResult> ValidateAsync(string password);
    }
}