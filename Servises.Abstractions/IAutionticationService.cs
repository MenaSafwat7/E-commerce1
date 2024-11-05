using Shared.ErrorModels;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Abstractions
{
    public interface IAutionticationService
    {
        public Task<UserResultDTO> LoginAsync(LoginDTO loginModel);
        public Task<UserResultDTO> RegesterAsync(UsserRegesterDTO regesterModel);

        public Task<UserResultDTO> GetUserByEmail(string email);
        public Task<bool> CheckEmailExist(string email);
        public Task<AddressDTO> GetUserAddress(string Email);
        public Task<AddressDTO> UpdateUserAddress(string? email, AddressDTO address);
    }
}
