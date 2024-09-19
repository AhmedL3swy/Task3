using DataGrid.Application.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Application.Features.Users.Query
{
    public class UserQueryDto : SearchQuery
    {
        public string Name
        {
            set
            {
                SearchByKeyword.Add(new SearchByKeyword { Fields = "firstname,lastname", Keyword = value });
            }
        }
        public string Email
        {
            set
            {
                SearchByKeyword.Add(new SearchByKeyword { Fields = "email", Keyword = value });
            }
        }
        public string Mobile
        {
            set
            {
                SearchByKeyword.Add(new SearchByKeyword { Fields = "mobile", Keyword = value });
            }
        }
        public string NationalId
        {
            set
            {
                SearchByKeyword.Add(new SearchByKeyword { Fields = "nationalid", Keyword = value });
            }
        }
        public string MunicipalNo
        {
            set
            {
                SearchByKeyword.Add(new SearchByKeyword { Fields = "municipalno", Keyword = value });
            }
        }
        public string MaritalStatus
        {
            set
            {
                SearchByKeyword.Add(new SearchByKeyword { Fields = "maritalstatus.name", Keyword = value });
            }
        }
    }
}
