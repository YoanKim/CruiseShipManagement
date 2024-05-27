using DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using X.PagedList;

namespace CSMUI.Controllers
{
    public class TicketsController : Controller
    {
        // GET: TicketsController
        public ActionResult Index(int? page, string searchString, string searchby)
        {
            string url = "https://localhost:7154/api/Tickets";
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            IEnumerable<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json).ToPagedList(page ?? 1, 10);

            foreach (Ticket item in tickets)
            {
                string connUrl = "https://localhost:7154/api/People/" + item.PersonId;
                var connJson = "";

                using (ApiAuthenticationClient connClient = new())
                {
                    connJson = connClient.GetStringAsync(connUrl).Result;
                }

                item.Person = JsonConvert.DeserializeObject<Person>(connJson);
            }

            Ticket temporalCheck = new Ticket();
            var properties = temporalCheck.GetType().GetProperties();

            List<string> data = new List<string>();

            foreach (var item in properties)
            {
                data.Add(item.Name);
            }

            IEnumerable<SelectListItem> selectWorkItems = new SelectList(data);
            data.Remove("Id");
            data.Remove("PersonId");
            ViewData["datalist"] = selectWorkItems;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchby)
                {
                    case "Price":
                        tickets = tickets.Where(t => t.Price.ToString().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "Description":
                        tickets = tickets.Where(t => t.Description.ToLower().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "BuyDate":
                        tickets = tickets.Where(t => t.BuyDate.Year.ToString().Equals(searchString) || t.BuyDate.Month.ToString().Equals(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "ExpireDate":
                        tickets = tickets.Where(t => t.ExpireDate.Year.ToString().Equals(searchString) || t.ExpireDate.Month.ToString().Equals(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "Person":
                        tickets = tickets.Where(t => t.Person.FirstName.ToLower().Contains(searchString) || t.Person.LastName.ToLower().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    default:
                        break;
                }
            }

            return View(tickets);
        }

        // GET: TicketsController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            string url = "https://localhost:7154/api/Tickets/" + id;
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            Ticket tickets = JsonConvert.DeserializeObject<Ticket>(json);

            string connUrlPerson = "https://localhost:7154/api/People/" + tickets.PersonId;
            var connJsonPerson = "";

            using (ApiAuthenticationClient connClient = new())
            {
                connJsonPerson = connClient.GetStringAsync(connUrlPerson).Result;
            }

            tickets.Person = JsonConvert.DeserializeObject<Person>(connJsonPerson);

            return View(tickets);
        }

        // GET: TicketsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            string url = "https://localhost:7154/api/People/";
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            IEnumerable<Person> people = JsonConvert.DeserializeObject<List<Person>>(json);
            IEnumerable<SelectListItem> select = new SelectList(people, "Id", "LastName");
            ViewData["peopleInfo"] = select;
            return View();
        }

        // POST: TicketsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Create();
                }

                string url = "https://localhost:7154/api/Tickets";
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
                return RedirectToAction("Index");
            }
        }

        // GET: TicketsController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            string url = "https://localhost:7154/api/Tickets/" + id;
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);

            string connUrl = "https://localhost:7154/api/People/";
            var connJson = "";

            using (ApiAuthenticationClient client = new())
            {
                connJson = client.GetStringAsync(connUrl).Result;
            }

            IEnumerable<Person> people = JsonConvert.DeserializeObject<List<Person>>(connJson);
            IEnumerable<SelectListItem> select = new SelectList(people, "Id", "LastName");
            ViewData["peopleInfo"] = select;

            return View(ticket);
        }

        // POST: TicketsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Create();
                }

                string url = "https://localhost:7154/api/Tickets/" + model.Id;
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
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            string url = "https://localhost:7154/api/Tickets/" + id;

            using (var client = new ApiAuthenticationClient())
            {
                HttpResponseMessage resp = client.DeleteAsync(url).Result;
                Console.WriteLine(resp);
            }

            return RedirectToAction("Index");
        }
    }
}
