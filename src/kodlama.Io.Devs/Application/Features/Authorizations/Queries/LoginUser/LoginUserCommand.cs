using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Queries.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
        {
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginUserCommandHandler(IMapper mapper, ITokenHelper tokenHelper, IUserRepository userRepository, AuthBusinessRules authBusinessRules)
            {
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await _authBusinessRules.UserControlCorrespondingToEmailAddress(request.Email);

                await _authBusinessRules.PasswordVerification(request.Password, user);

                var claims = _userRepository.GetClaims(user);

                var token = _tokenHelper.CreateToken(user, claims);

                return new() { Token = token.Token, Expiration = token.Expiration };

            }
        }
    }
}
