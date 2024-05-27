using DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using X.PagedList;

namespace CSMUI.Controllers
{
    public class PeopleController : Controller
    {
        // GET: PeopleController
        public ActionResult Index(int? page, string searchString, string searchby)
        {
            string url = "https://localhost:7154/api/People";
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            IEnumerable<Person> people = JsonConvert.DeserializeObject<List<Person>>(json).ToPagedList(page ?? 1, 10);

            Person temporalCheck = new Person();
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
                    case "FirstName":
                        people = people.Where(p => p.FirstName.ToLower().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "LastName":
                        people = people.Where(p => p.LastName.ToLower().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "DateOfBirth":
                        people = people.Where(p => p.DateOfBirth.Year.ToString().Equals(searchString) || p.DateOfBirth.Month.ToString().Equals(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "Phone":
                        people = people.Where(p => p.Phone.Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "Email":
                        people = people.Where(p => p.Email.ToLower().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "Address":
                        people = people.Where(p => p.Address.ToLower().Contains(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    default:
                        break;
                }
            }

            return View(people);
        }

        // GET: PeopleController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            string url = "https://localhost:7154/api/People/" + id;
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            Person people = JsonConvert.DeserializeObject<Person>(json);

            return View(people);
        }

        // GET: PeopleController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string url = "https://localhost:7154/api/People";
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

        // GET: PeopleController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            string url = "https://localhost:7154/api/People/" + id;
            var json = "";

            using (ApiAuthenticationClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }

            Person person = JsonConvert.DeserializeObject<Person>(json);

            return View(person);
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string url = "https://localhost:7154/api/People/" + model.Id;
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
            string url = "https://localhost:7154/api/People/" + id;

            using (var client = new ApiAuthenticationClient())
            {
                HttpResponseMessage resp = client.DeleteAsync(url).Result;
                Console.WriteLine(resp);
            }

            return RedirectToAction("Index");
        }
    }
}
