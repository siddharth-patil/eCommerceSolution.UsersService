using eCommerce.Core.DTO;

namespace eCommerce.Core.ServiceContracts;
/// <summary>
/// Contract for users service that contains use cases for users.
/// </summary>
public interface IUsersService
{
    /// <summary>
    /// Method to handle user login use case na dreturns an AuthenticationResponse object 
    /// containing user details and authentication token and status of login.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);

    /// <summary>
    /// Method to handle user registration use case and returns an AuthenticationResponse object
    /// that represents the status of user registration.
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);

}
