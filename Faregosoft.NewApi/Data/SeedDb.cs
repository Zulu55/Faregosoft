using Faregosoft.NewApi.Data.Entities;
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
            await CheckUserAsync("Juan", "Reyes", "euclides@yopmail.com", "322 311 4620");
            await CheckUserAsync("Fausto", "Reyes", "fausto@yopmail.com", "322 311 4620");
            await SeedProductsAync();
            await SeedCustomersAync();
            await SeedSellersAync();
            await SeedProvidersAync();
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
                for (int i = 0; i < 345; i++)
                {
                    _context.Products.Add(new Product { User = user, Name = $"Producto: {i}", Description = $"Producto: {i}", Price = random.Next(1, 100), Inventory = random.Next(1, 100), IsActive = true });
                }
                await _context.SaveChangesAsync();
            }
        }


        private async Task SeedProvidersAync()
        {
            if (!_context.Providers.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                Random random = new Random();
                for (int i = 0; i < 25; i++)
                {
                    _context.Providers.Add(new Providers 
                    {  
                        User = user,  FirstName = $"Firstname: {i}",
                        LastName = $"LastName: {i}",
                        Code= $"Code: {i}",
                        Category= $"Category: {i}",
                        Type = $"TYPE: {i}",
                        Contact = $"Category: {i}",
                        Address = $"Dirección: {i}",
                        Country = $"País: {i}",
                        City = $"Ciudad: {i}",
                        CreditLimit = random.Next(1, 100000),
                        PaymentCodition = 30,
                        Observation = $"Observatión: {i}",
                        IsActive = true
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedCustomersAync()
        {
            if (!_context.Customers.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 0; i < 256; i++)
                {
                    _context.Customers.Add(new Customer 
                    { 
                        User = user, 
                        FirstName = $"Nombres {i}", 
                        LastName = $"Apellidos  {i}", 
                        Email = $"cliente{i}@yopmail.com", 
                        Phonenumber = $" {i}{i}", 
                        Address = $"Dirección {i}", 
                        IsActive = true 
                    });
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }            
            }
        }

        private async Task SeedSellersAync()
        {
            if (!_context.Sellers.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                _context.Sellers.Add(new Seller { User = user, FirstName = "Pedro", LastName = "Ruiz", Comision = 10, Address = "Calle Sol", IsActive = true });
                _context.Sellers.Add(new Seller { User = user, FirstName = "Carlos", LastName = "Peralta", Comision = 5, Address = "Calle Restauracion", IsActive = true });
                _context.Sellers.Add(new Seller { User = user, FirstName = "Julio", LastName = "Almonte", Comision = 8, Address = "Calle San Luis", IsActive = true });
                await _context.SaveChangesAsync();
            }
        }
    }
}
