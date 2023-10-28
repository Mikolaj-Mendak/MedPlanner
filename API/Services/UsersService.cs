using API.Authorization.Helpers;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UsersService(DataContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserAsync(Guid id, User updatedUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                if(user.FirstName != null)
                {
                    user.FirstName = updatedUser.FirstName;

                }
                if (user.LastName != null)
                {
                    user.LastName = updatedUser.LastName;
                }

                if (user.Email != null)
                {
                    user.Email = updatedUser.Email;

                }

                if (user.Pesel != null)
                {
                    user.Pesel = updatedUser.Pesel;

                }
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Where(u => !(u is Doctor)).ToListAsync();
            return users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserDetailsDto> GetUserDetailsByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null; 
            }

            UserDetailsDto userDetailsDto;

            if (user is Doctor doctor)
            {
                userDetailsDto = new UserDetailsDto
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Pesel = doctor.Pesel,
                    Email = doctor.Email,
                    DoctorNumber = doctor.DoctorNumber
                };
            }
            else
            {
                userDetailsDto = new UserDetailsDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Pesel = user.Pesel,
                    Email = user.Email
                };
            }

            return userDetailsDto;
        }

    }
}
