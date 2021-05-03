﻿using Faregosoft.NewApi.Data.Entities;
using Faregosoft.NewApi.Enums;
using Faregosoft.NewApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Faregosoft.NewApi.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Juan", "Zuluaga", "juan@yopmail.com", "322 311 4620");
            await SeedProductsAync();
            await SeedCustomersAync();
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, "Admin");
                await _userHelper.AddUserToRoleAsync(user, "User");
            }

            return user;
        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task SeedProductsAync()
        {
            if (!_context.Products.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                Random random = new Random();
                for (int i = 0; i < 100; i++)
                {
                    _context.Products.Add(new Product { User = user, Name = $"Producto: {i}", Description = $"Producto: {i}", Price = random.Next(1, 100), Inventory = random.Next(1, 100), IsActive = true });
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedCustomersAync()
        {
            if (!_context.Customers.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                _context.Customers.Add(new Customer { User = user, FirstName = "Juan", LastName = "Reyes", Email = "juan@yopmail.com", Phonenumber = "303030", Address = "Calle Luna Calle Sol", IsActive = true });
                _context.Customers.Add(new Customer { User = user, FirstName = "Fausto", LastName = "Reyes", Email = "fausto@yopmail.com", Phonenumber = "404040", Address = "Calle Luna Calle Sol", IsActive = true });
                _context.Customers.Add(new Customer { User = user, FirstName = "Juan", LastName = "Zuluaga", Email = "zulu@yopmail.com", Phonenumber = "505050", Address = "Calle Luna Calle Sol", IsActive = true });
                await _context.SaveChangesAsync();
            }
        }
    }
}