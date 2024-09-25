using DataGrid.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Application.Features.Users.Query
{
    public class UserResultDto
    {
        public int Id { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Email { get; set; }

        public string Mobile { get; set; }

        public string MunicipalNo { get; set; }


        public string NationalId { get; set; }



        public string Address { get; set; }

        public string MaritalStatus { get; set; }

        public DateOnly? BirthDate { get; set; }
    }
}
