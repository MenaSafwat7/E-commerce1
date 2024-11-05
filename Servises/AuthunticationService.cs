using AutoMapper.Configuration.Annotations;
using Domain.Entities.Identity;
using Domain.Exeptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.ErrorModels;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servises
{
    internal class AuthunticationService(UserManager<User> userManager, IOptions<JWTOptions> options, IMapper mapper) : IAutionticationService
    {
        public async Task<bool> CheckEmailExist(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<AddressDTO> GetUserAddress(string Email)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == Email) ?? throw new UserNotFoundException(Email);

            return mapper.Map<AddressDTO>(user.Address);
            //return new AddressDTO
            //{
            //    city = user.Address.city,
            //    Country = user.Address.Country,
            //    FirstName = user.Address.FirstName,
            //    LastName = user.Address.LastName,
            //};
        }

        public async Task<UserResultDTO> GetUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);

            return new UserResultDTO(user.DisplayName, user.Email, await CreateTokenAsync(user));
        }

        public async Task<UserResultDTO> LoginAsync(LoginDTO loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.email);
            if (user is null) throw new UnauthorizedExeption("Email Doesn't Exist");

            var result = await userManager.CheckPasswordAsync(user, loginModel.password);
            if(!result) throw new UnauthorizedExeption();

            return new UserResultDTO(user.DisplayName, user.Email!, await CreateTokenAsync(user));

        }

        public async Task<UserResultDTO> RegesterAsync(UsserRegesterDTO regesterModel)
        {
            var user = new User()
            {
                Email = regesterModel.Email,
                DisplayName = regesterModel.DisplayName,
                PhoneNumber = regesterModel.PhoneNumber,
                UserName = regesterModel.UserName,
            };
            var result = await userManager.CreateAsync(user, regesterModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationExeption(errors);
            }

            return new UserResultDTO(user.DisplayName, user.Email!, await CreateTokenAsync(user));
        }


        public async Task<AddressDTO> UpdateUserAddress(string? email, AddressDTO address)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);

            // var Useraddress = mapper.Map<Address>(address);

            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LastName;
            user.Address.Street = address.Street;
            user.Address.city = address.city;
            user.Address.Country = address.Country;

            await userManager.UpdateAsync(user);

            return address;
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = options.Value;
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: jwtOptions.Audience,
                issuer: jwtOptions.Issure,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                claims: AuthClaims,
                signingCredentials: signingCreds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
