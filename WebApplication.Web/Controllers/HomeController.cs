﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.Models;
using WebApplication.Web.DAL;

namespace WebApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserDAL uDal;
        private IPotholeDAL pDal;
        private IClaimDAL cDal;

        public HomeController(IUserDAL u, IPotholeDAL p, IClaimDAL c)
        {
            this.uDal = u;
            this.pDal = p;
            this.cDal = c;
        }

        /// <summary>
        /// Displays the home page with a map showing all of the currently unfixed potholes
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var reports = pDal.GetAllReports();
            return View(reports);
        }

        public IActionResult AddRecord()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

		public IActionResult Employee()
		{
			var reports = pDal.GetAllReports();
			return View(reports);
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
