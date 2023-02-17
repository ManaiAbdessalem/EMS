using EMC_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace EMC_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = configuration["EMSAPIBackendUrl"];
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Employee");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        employees = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                    }
                }
                return View(employees);
            }
        }


        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string keyword)
        {
            List<Employee> employees = new List<Employee>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Employee/search/"+ keyword);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        employees = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                    }
                }
                return View(employees);
            }
            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id,Name,Email,DateOfBirth,Department")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl + "api/employee");

                    var createEmployee = client.PostAsJsonAsync<Employee>("employee", employee);
                    createEmployee.Wait();

                    HttpResponseMessage result = createEmployee.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "One or more fields are not valids!");
                        return View(employee);
                    }
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator!");
                }
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = await GetEmployeeAsync(id.Value);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Edit: Employees
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,DateOfBirth,Department")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<Employee>("api/Employee/" + id, employee);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "One or more fields are not valids!");
                        return View(employee);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = await GetEmployeeAsync(id.Value);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Employee/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }

        private async Task<Employee> GetEmployeeAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Employee/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        Employee employee = JsonConvert.DeserializeObject<Employee>(EmpResponse);
                        return employee;
                    }
                }
                return null;
            }
        }
    }
}