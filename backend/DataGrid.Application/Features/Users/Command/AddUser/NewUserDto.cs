﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Application.Features.Users.Command.AddUser
{
    public class NewUserDto
    {

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Email { get; set; }

        public string Mobile { get; set; }

        public string MunicipalNo { get; set; }


        public string NationalId { get; set; }

        public string Address { get; set; }

        public int MaritalStatusId { get; set; }
        public string BirthDate { get; set; }

    }
}
