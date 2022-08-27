using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniversityManagementWebApp.Models;

namespace UniversityManagementWebApp.Controllers
{
    [Authorize(Policy = "OperatorPage")]
    public class OperaterController : Controller
    {
        public readonly IWebHostEnvironment _webHostEnvironment;

        Uri baseAddress = new Uri("https://localhost:7073/api");
        HttpClient client;

        public OperaterController(IWebHostEnvironment webHostEnvironment)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _webHostEnvironment = webHostEnvironment;
        }
        public static List<University>? universityList = new List<University>();
        public ActionResult Index()
        {

            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/Universities/GetUniversities").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                universityList = JsonConvert.DeserializeObject<List<University>>(data);
                var orderedList = universityList.OrderBy(x => x.EstablishedYear);
                return View("Index", orderedList);
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View("Index");
        }
     
        public string CheckCollegeNameDuplicacy(string name)
        {
            var data =  universityList.FirstOrDefault(x => x.Name == name);
            if(data == null)
            {
                return "0";
            }
            return "1";
        }

        public ActionResult AddUniversity()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUniversity(University university)
        {       
            var postTask = client.PostAsJsonAsync<University>(baseAddress + "/Universities/PostUniversity", university);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View("university");
        }

        [HttpGet]
        public ActionResult UpdateUniversity(int id)
        {
            University? university = new University();

            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/Universities/GetUniversity/" + id.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                university = JsonConvert.DeserializeObject<University>(data);
            }
            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUniversity(University university)
        {
            var putTask = client.PutAsJsonAsync<University>(baseAddress + "/Universities/PutUniversity/" + university.Id.ToString(), university);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(university);
        }

        public ActionResult DeleteUniversity(int id)
        {

            //HTTP DELETE
            var deleteTask = client.DeleteAsync(baseAddress + "/Universities/DeleteUniversity/" + id);
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
