using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Transversal.Common;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<Response<string>> AuthenticateUser(string username, string password);
        Task<Response<IEnumerable<UserDto>>> GetAllUsers();
        Task<Response<UserDto>> GetUserById(int id);
        Task<Response<UserDto>> CreateUser(UserDto dto);
        Task<Response<UserDto>> UpdateUser(int id, UserDto dto);
        Task<Response<bool>> DeleteUser(int id);
    }
}
