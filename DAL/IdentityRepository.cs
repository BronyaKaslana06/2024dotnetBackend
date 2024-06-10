using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Dynamic;
using EntityFramework.Context;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntityFramework.Models;

namespace DAL
{
    public interface IIdentityRepository
    {
        VehicleOwner GetOwnerByAccount(string accountSerial);
        Employee GetEmployeeByAccount(string accountSerial);
        Administrator GetAdministratorByAccount(string accountSerial);
        // 其他必要的方法
    }
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ModelContext _context;

        public IdentityRepository(ModelContext context)
        {
            _context = context;
        }

        public VehicleOwner GetOwnerByAccount(string accountSerial)
        {
            return _context.VehicleOwners
                           .FirstOrDefault(x => x.AccountSerial == accountSerial);
        }

        public Administrator GetAdministratorByAccount(string accountSerial)
        {
            return _context.Administrators
                           .FirstOrDefault(x => x.AccountSerial == accountSerial);
        }

        public Employee GetEmployeeByAccount(string accountSerial)
        {
            return _context.Employees
                        .Include(a => a.switchStation)
                        .Where(x => x.AccountSerial == accountSerial)
                        .DefaultIfEmpty().FirstOrDefault();
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
