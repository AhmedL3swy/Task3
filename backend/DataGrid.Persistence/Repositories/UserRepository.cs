using DataGrid.Application.Contracts;
using DataGrid.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        #region Generic Unique Validator

        // Generic method to check uniqueness based on a field and its value using Expressions
        public async Task<bool> IsUniqueAsync<T, TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression, TProperty value) where T : class
        {
            var entityType = typeof(T);
            var entityEntry = _appDbContext.Entry(entity);
            var entityId = entityEntry.Property("Id").CurrentValue;

            // Build expression to check uniqueness: entity => entity.Property == value && entity.Id != entityId
            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = Expression.Property(parameter, ((MemberExpression)propertyExpression.Body).Member.Name);
            var valueExpression = Expression.Constant(value);
            var idExpression = Expression.Property(parameter, "Id");
            var notEqualId = Expression.NotEqual(idExpression, Expression.Constant(entityId));

            var equality = Expression.Equal(property, valueExpression);
            var combined = Expression.AndAlso(equality, notEqualId);

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

            // Check the uniqueness in the database
            return !await _appDbContext.Set<T>().AnyAsync(lambda);
        }

        #endregion

        #region Specific Unique Validators
        public async Task<bool> IsEmailUniqueAsync(User user)
        {
            return await IsUniqueAsync(user, u => u.Email, user.Email);
        }

        public async Task<bool> IsMobileUniqueAsync(User user)
        {
            return await IsUniqueAsync(user, u => u.Mobile, user.Mobile);
        }

        public async Task<bool> IsMunicipalNoUniqueAsync(User user)
        {
            return await IsUniqueAsync(user, u => u.MunicipalNo, user.MunicipalNo);
        }

        public async Task<bool> IsNationalIdUniqueAsync(User user)
        {
            return await IsUniqueAsync(user, u => u.NationalId, user.NationalId);
        }

        public async Task<bool> IsMaritalStatusValidAsync(int maritalStatusId)
        {
            return await _appDbContext.MaritalStatuses.AnyAsync(m => m.Id == maritalStatusId);
        }

        #endregion
    }
    #endregion
}

