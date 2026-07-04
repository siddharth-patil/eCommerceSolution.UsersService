using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.Entities;
using AutoMapper;


namespace eCommerce.Core.Services;

internal class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(loginRequest.Email,
            loginRequest.Password);

        if (user == null)
        {
            return null;
        }
        //return new AuthenticationResponse
        //(
        //    user.UserId,
        //    user.Email,
        //    user.PersonName,
        //    user.Gender,
        //    "dummy-token",
        //    Success: true
        //);

        return _mapper.Map<AuthenticationResponse>(user) with
        { Success = true, Token = "dummy-token" };

    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        //Create a new ApplicationUser object from RegisterRequest
        ApplicationUser user = new ApplicationUser
        {
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            PersonName = registerRequest.PersonName,
            Gender = registerRequest.Gender.ToString()
        };

        //ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);

        ApplicationUser? registeredUser = await _userRepository.AddUser(user);
        if (registeredUser == null)
        {
            return null;
        }

        //return new AuthenticationResponse
        //(
        //    registeredUser.UserId,
        //    registeredUser.Email,
        //    registeredUser.PersonName,
        //    registeredUser.Gender,
        //    "dummy-token",
        //    Success: true
        //);
        return _mapper.Map<AuthenticationResponse>(registeredUser) with
        { Success = true, Token = "dummy-token" };
    }
}
