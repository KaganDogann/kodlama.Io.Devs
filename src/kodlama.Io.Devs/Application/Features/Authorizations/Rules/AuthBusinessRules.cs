using Application.Features.Authorizations.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;


        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> UserControlCorrespondingToEmailAddress(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);

            if (user == null)
                throw new BusinessException("Email or password is incorrect!");

            return user;
        }

        public async Task EmailAddressCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> user = await _userRepository.GetListAsync(u => u.Email == email);

            if (user.Items.Any())
                throw new BusinessException("The email address is already registered. Please login");
        }



        public Task PasswordVerification(string password, User user)
        {
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException("Email or password is incorrect!");

            return Task.CompletedTask;
        }


        public void RegisteredUserCanNotBeNull(User user)
        {
            if (user == null)
            {
                throw new BusinessException(BusinessConstants.CreatedUserCanNotBeNull);
            }
        }

        public async void IsUserExists(string email)
        {
            var checkUser = await _userRepository.GetAsync(u => u.Email == email);

            if (checkUser == null)
            {
                throw new BusinessException(BusinessConstants.UserNotExists);
            }
        }
      
    }
}
