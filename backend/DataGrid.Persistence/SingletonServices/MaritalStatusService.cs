using DataGrid.Application.Contracts;
using DataGrid.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Persistence.SingletonServices
{
    public class MaritalStatusService : IMaritalStatusService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<int, MaritalStatus> _cache;
        private readonly AppDbContext _context;

        public MaritalStatusService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            _cache = new ConcurrentDictionary<int, MaritalStatus>();

            // Seed cache on initialization
            LoadAllMaritalStatuses();
        }

        public MaritalStatus GetMaritalStatusById(int id)
        {
            return _cache.TryGetValue(id, out var status) ? status : null;
        }

        public IEnumerable<MaritalStatus> GetAllMaritalStatuses()
        {
            return _cache.Values;
        }

        private void LoadAllMaritalStatuses()
        {
            var statuses = _context.MaritalStatuses.ToList();
            foreach (var status in statuses)
            {
                _cache[status.Id] = status;
            }
        }
    }

}
