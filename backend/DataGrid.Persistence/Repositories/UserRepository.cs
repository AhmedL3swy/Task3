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
    public class UserRepository : GenericRepositoryAsync<User>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMaritalStatusService _maritalStatusService;

        public UserRepository(AppDbContext appDbContext, IMaritalStatusService maritalStatusService) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _maritalStatusService = maritalStatusService;
        }

    }
}

