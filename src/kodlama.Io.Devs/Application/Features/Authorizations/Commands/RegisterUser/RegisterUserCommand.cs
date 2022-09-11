using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Commands.RegisterUser
{
    public class RegisterUserCommand:IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public class RegisterUserCommandHandler:IRequestHandler<RegisterUserCommand, RegisteredDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            private readonly AuthBusinessRules _authBusinessRules;

            public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<RegisteredDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                _authBusinessRules.IsUserExists(request.UserForRegisterDto.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
                User user = new User { Email = request.UserForRegisterDto.Email, FirstName = request.UserForRegisterDto.FirstName, LastName = request.UserForRegisterDto.LastName, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Status = true };

                User registeredUser = _userRepository.Add(user);

                RegisteredDto registeredDto = _mapper.Map<RegisteredDto>(registeredUser);

                return registeredDto;
                //var userCheck = await _userRepository.GetAsync(u => u.Email == request.UserForRegisterDto.Email);
            }
        }
    }
}
