using JF_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JF_BugTracker.Controllers
{
    public class GraphingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public class MorrisBarData
        {
            public string label { get; set; }
            public int value { get; set; }
        }

        public class MorrisDonutData
        {
            public string label { get; set; }
            public int value { get; set; }
        }

        // GET: Graphing
        public JsonResult ProduceChart1Data()
        {
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            foreach (var priority in db.TicketPriorities.ToList())
            {
                data = new MorrisBarData();
                data.label = priority.PriorityName;
                data.value = db.Tickets.Where(t => t.TicketPriority.PriorityName == priority.PriorityName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceChart2Data()
        {
            var myData = new List<MorrisDonutData>();
            MorrisDonutData data = null;
            foreach (var priority in db.TicketPriorities.ToList())
            {
                data = new MorrisDonutData();
                data.label = priority.PriorityName;
                data.value = db.Tickets.Where(t => t.TicketPriority.PriorityName == priority.PriorityName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceChart3Data()
        {
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            foreach (var priority in db.TicketStatuses.ToList())
            {
                data = new MorrisBarData();
                data.label = priority.StatusName;
                data.value = db.Tickets.Where(t => t.TicketStatus.StatusName == priority.StatusName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceChart4Data()
        {
            var myData = new List<MorrisDonutData>();
            MorrisDonutData data = null;
            foreach (var priority in db.TicketStatuses.ToList())
            {
                data = new MorrisDonutData();
                data.label = priority.StatusName;
                data.value = db.Tickets.Where(t => t.TicketStatus.StatusName == priority.StatusName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceChart5Data()
        {
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            foreach (var priority in db.TicketTypes.ToList())
            {
                data = new MorrisBarData();
                data.label = priority.TypeName;
                data.value = db.Tickets.Where(t => t.TicketType.TypeName == priority.TypeName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceChart6Data()
        {
            var myData = new List<MorrisDonutData>();
            MorrisDonutData data = null;
            foreach (var priority in db.TicketTypes.ToList())
            {
                data = new MorrisDonutData();
                data.label = priority.TypeName;
                data.value = db.Tickets.Where(t => t.TicketType.TypeName == priority.TypeName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }


    }
}