using System.Linq.Expressions;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models;

namespace Solution.dotnet_ids.Services
{
    public class AdminService
    {
        private readonly IRepository<Admin> _dbAdmin;

        public AdminService(IRepository<Admin> dbAdmin)
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