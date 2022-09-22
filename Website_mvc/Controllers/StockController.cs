﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website_mvc.Controllers
{
    public class StockController : Controller
    {
        List<Models.otcTurnoverate> data = new List<Models.otcTurnoverate>();

        // GET: Stock
        public ActionResult Listed()
        {
            return View();
        }

        public ActionResult OTC()
        {
            cs.stock.Otc otc = new cs.stock.Otc();
            data = otc.ParseOTCTurnoverInfo("111/09/20");
            ViewBag.OTCturnoverate = data;
            return View();
        }

        public ActionResult GetOTCturnoverate()
        {
            return RedirectToAction("OTC");
        }
    }
}