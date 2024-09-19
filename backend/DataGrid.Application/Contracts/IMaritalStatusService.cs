using DataGrid.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Application.Contracts
{
    public interface IMaritalStatusService
    {
        MaritalStatus GetMaritalStatusById(int id);
        IEnumerable<MaritalStatus> GetAllMaritalStatuses();
    }
}
