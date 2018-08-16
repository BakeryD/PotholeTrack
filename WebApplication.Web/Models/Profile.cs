﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models.Account;

namespace WebApplication.Web.Models
{
    public class Profile
    {
		public User user { get; set; }
		public IList<Report> reports { get; set; }
		
    }
}