using DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using X.PagedList;

namespace CSMUI.Controllers
{
    public class CruisesController : Controller
    {
        // GET: CruisesController
        public ActionResult Index(int? page, string searchString, string searchby)
        {
            string url = "https://localhost:7154/api/Cruises";
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            IEnumerable<Cruise> cruises = JsonConvert.DeserializeObject<List<Cruise>>(json).ToPagedList(page ?? 1, 10);

            Cruise temporalCheck = new Cruise();
            var properties = temporalCheck.GetType().GetProperties();

            List<string> data = new List<string>();

            foreach (var item in properties)
            {
                data.Add(item.Name);
            }

            IEnumerable<SelectListItem> selectWorkItems = new SelectList(data);
            data.Remove("Id");
            ViewData["datalist"] = selectWorkItems;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchby)
                {
                    case "Name":
                        cruises = cruises.Where(c => c.Name.ToLower().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "StartDate":
                        cruises = cruises.Where(p => p.StartDate.Year.ToString().Equals(searchString) || p.StartDate.Month.ToString().Equals(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "EndDate":
                        cruises = cruises.Where(p => p.EndDate.Year.ToString().Equals(searchString) || p.EndDate.Month.ToString().Equals(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "StarRating":
                        cruises = cruises.Where(p => p.StarRating.ToString().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "Seating":
                        cruises = cruises.Where(p => p.Seating.ToString().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    default:
                        break;
                }
            }

            return View(cruises);
        }

        // GET: CruisesController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            string url = "https://localhost:7154/api/Cruises/" + id;
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            Cruise cruises = JsonConvert.DeserializeObject<Cruise>(json);

            return View(cruises);
        }

        // GET: CruisesController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CruisesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cruise model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string url = "https://localhost:7154/api/Cruises";
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new(json, Encoding.UTF8, "application/json");

                Console.WriteLine(content);

                using (var client = new ApiAuthenticationClient())
                {
                    HttpResponseMessage resp = client.PostAsync(url, content).Result;
                    Console.WriteLine(resp);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CruisesController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            string url = "https://localhost:7154/api/Cruises/" + id;
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            Cruise cruise = JsonConvert.DeserializeObject<Cruise>(json);

            return View(cruise);
        }

        // POST: CruisesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cruise model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string url = "https://localhost:7154/api/Cruises/" + model.Id;
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new(json, Encoding.UTF8, "application/json");

                Console.WriteLine(content);

                using (var client = new ApiAuthenticationClient())
                {
                    HttpResponseMessage resp = client.PutAsync(url, content).Result;
                    Console.WriteLine(resp);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            string url = "https://localhost:7154/api/Cruises/" + id;

            using (var client = new ApiAuthenticationClient())
            {
                HttpResponseMessage resp = client.DeleteAsync(url).Result;
                Console.WriteLine(resp);
            }

            return RedirectToAction("Index");
        }
    }
}
