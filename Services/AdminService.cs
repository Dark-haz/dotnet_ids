using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using Solution.dotnet_ids.Models.Request;

namespace Solution.dotnet_ids.Services
{
    public class AdminService
    {
        private readonly IRepository<Admin> _dbAdmin;

        public AdminService(IRepository<Admin> dbAdmin , IConfiguration configuration)
        {
            _dbAdmin = dbAdmin;            
        }

        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            await _dbAdmin.CreateAsync(admin);
            return admin;
        }
        
        public async Task<Admin> DeleteAdminAsync(Admin admin)
        {
            await _dbAdmin.DeleteAsync(admin);
            return admin;
        }
        
        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            await _dbAdmin.UpdateAsync(admin);
            return admin;
        }

        public async Task<Admin ?> GetAdmin(Expression<Func<Admin,bool>>?  filter = null , bool tracked = true)
        {
            Admin ? admin = await _dbAdmin.GetAsync(filter , tracked);
            return admin;
        }
        
        public async Task<List<Admin>> GetAllAdmins(Expression<Func<Admin,bool>> ? filter = null)
        {
            List<Admin> admins = await _dbAdmin.GetAllAsync(filter);
            return admins;
        }
    }
}