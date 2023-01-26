using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Application.Services.AdminService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceApp.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ManagerController : Controller
	{
		private readonly IManagerService _adminService;
		public ManagerController(IManagerService adminService)
		{
			_adminService = adminService;
		}
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> AddManager()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddManager(AddManagerDTO addManagerDTO)
		{

			//await _adminService.CreateManager(addManagerDTO);
			//return RedirectToAction(nameof(ListOfManagers));
			using (var client = new HttpClient())
			{
				//client.BaseAddress = new Uri("https://localhost:7186/");//APİ IN SENİN LOCALİNİNDE BULUNDUĞU YER
				//var responesTask = client.GetAsync("api/Manager/GetManagers");//apiye istek gösterdiğimiz verileri alması gereken yeri gösterdik 
				//responesTask.Wait();//Bu işlemin gerçekleşmesiini bekle
				//var resultTask = responesTask.Result;
				//if (responesTask.IsCompletedSuccessfully)
				//{
				//	var readTask = resultTask.Content.ReadAsStringAsync();
				//	readTask.Wait();
				//	var readData = JsonConvert.DeserializeObject<List<ListOfManagerVM>>(readTask.Result);
				//	return View(readData);
				//}
				//else
				//{
				//	ViewBag.EmpltyList = "List is not found";
				//	return View(new List<ListOfManagerVM>());
				//}
				client.BaseAddress = new Uri("https://localhost:7186/");
				var responseTask = client.PostAsJsonAsync<AddManagerDTO>("api/Manager/PostManager", addManagerDTO);
				responseTask.Wait();
				//bool returnValue = await responseTask..ReadAsAsync<bool>();
				var resultTask = responseTask.Result;
				if (responseTask.IsCompletedSuccessfully)
				{
					return RedirectToAction(nameof(ListOfManagers));
				}
				else
				{
					return BadRequest();
				}
			}
			//return View();

		}

		public async Task<IActionResult> ListOfManagers()
		{
			var managers = await _adminService.GetManagers();
			return View(managers);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateManager(Guid id)
		{
			var updateManager = await _adminService.GetManager(id);

			return View(updateManager);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateManager(UpdateManagerDTO updateManagerDTO)
		{
			if (ModelState.IsValid)
			{
				await _adminService.UpdateManager(updateManagerDTO);
				return RedirectToAction(nameof(ListOfManagers));
			}

			return View(updateManagerDTO);

		}

		public async Task<IActionResult> DeleteManager(Guid id)
		{
			await _adminService.DeleteManager(id);

			return RedirectToAction(nameof(ListOfManagers));

		}
	}
}
