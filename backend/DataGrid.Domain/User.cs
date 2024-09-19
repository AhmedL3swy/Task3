using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Domain
{
    public class User
    {

        public int Id { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Email { get; set; }

        public string Mobile { get; set; }

        public string MunicipalNo { get; set; }


        public string NationalId { get; set; }


        //public MaritalStatusEnum MaritalStatus { get; set; }

        public string Address { get; set; }

        // Navigation property to MaritalStatus
        [ForeignKey("MaritalStatus")]
        public int MaritalStatusId { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
    }
}
