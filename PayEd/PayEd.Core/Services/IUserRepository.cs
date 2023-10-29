using PayEd.Data.Dto;
using PayEd.Data.Dtos;
using PayEd.Infrastructure.Helpers;

namespace PayEd.Core.Services
{
    public interface IUserRepository
    {
        Task<ApiResponse> Login(LoginDto login);
        Task<ApiResponse> CreateUser(UserRegistrationDto userRequest);
        //Task<ApiResponse> CreateSchool(SchoolDto userRequest);
        Task<ApiResponse> SignOut();
    }
}