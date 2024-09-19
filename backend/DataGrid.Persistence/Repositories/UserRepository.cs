using DataGrid.Application.Contracts;
using DataGrid.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Persistence.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMaritalStatusService _maritalStatusService;

        public UserRepository(AppDbContext appDbContext, IMaritalStatusService maritalStatusService)
        {
            _appDbContext = appDbContext;
            _maritalStatusService = maritalStatusService;
        }


        #region Add User

        public async Task AddUser(User user)
        {

            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();


        }
        #endregion

        #region Edit User

        public async Task EditUser(User user)
        {
            var userFromDb = await _appDbContext.Users.FindAsync(user.Id);
            if (userFromDb == null)
            {
                throw new Exception("User not found");
            }
            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.Email = user.Email;
            userFromDb.Mobile = user.Mobile;
            userFromDb.MunicipalNo = user.MunicipalNo;
            userFromDb.NationalId = user.NationalId;
            userFromDb.Address = user.Address;
            userFromDb.MaritalStatusId = user.MaritalStatusId;

            await _appDbContext.SaveChangesAsync();


        }

        #endregion

        #region Get User By Id

        public async Task<User> GetUserById(int id)
        {
            return await _appDbContext.Users.FindAsync(id);
        }

        #endregion

        #region Unique Validators

        public async Task<bool> IsEmailUniqueAsync(User user)
        {
            if (user.Id == 0)
            {
                return !await _appDbContext.Users.AnyAsync(u => u.Email == user.Email);
            }
            else
            {
                return !await _appDbContext.Users.AnyAsync(u => u.Email == user.Email && u.Id != user.Id);
            }

        }

        public async Task<bool> IsMobileUniqueAsync(User user)
        {
            if (user.Id == 0)
            {
                return !await _appDbContext.Users.AnyAsync(u => u.Mobile == user.Mobile);
            }
            else
            {
                return !await _appDbContext.Users.AnyAsync(u => u.Mobile == user.Mobile && u.Id != user.Id);
            }
        }

        public async Task<bool> IsMunicipalNoUniqueAsync(User user)
        {
            if (user.Id == 0)
            {
                return !await _appDbContext.Users.AnyAsync(u => u.MunicipalNo == user.MunicipalNo);
            }
            else
            {
                return !await _appDbContext.Users.AnyAsync(u => u.MunicipalNo == user.MunicipalNo && u.Id != user.Id);
            }
        }

        public async Task<bool> IsNationalIdUniqueAsync(User user)
        {
            if (user.Id == 0)
            {
                return !await _appDbContext.Users.AnyAsync(u => u.NationalId == user.NationalId);
            }
            else
            {
                return !await _appDbContext.Users.AnyAsync(u => u.NationalId == user.NationalId && u.Id != user.Id);
            }

        }

        public async Task<bool> IsMaritalStatusValidAsync(int maritalStatusId)
        {
            return await _appDbContext.MaritalStatuses.AnyAsync(m => m.Id == maritalStatusId);
        }
        #endregion
    }
}
