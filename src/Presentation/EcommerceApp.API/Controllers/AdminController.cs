using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Application.Services.AdminService;
using EcommerceApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
            for (int i = 0; i < length; i++)
            {

            }
        }

        [HttpGet("GetAdmins")]
        public async Task<ActionResult<List<ListOfAdminVM>>> GetAllAdmins()
        {
            var admins = await _adminService.GetAdmins();
            if (admins == null)
            {
                return NotFound();
            }
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UpdateAdminDTO>> GetAdmin([FromForm] Guid id)
        {
            var admin = await _adminService.GetAdmin(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UpdateAdminDTO>> DeleteManager([FromForm] Guid id)
        {
            await _adminService.DeleteAdmin(id);
            return Ok();
        }


        //CREATE VE UPDATE İÇİN BAKIP GELELİM !!!!! 


        [HttpPost("PostAdmin")]
        public async Task<ActionResult> CreateManager(IFormFile images, [FromForm] AddAdminDTO addAdminDTO)
        {
            addAdminDTO.UploadPath = images;
            if (ModelState.IsValid)
            {
                try
                {
                    await _adminService.CreateAdmin(addAdminDTO);
                }

                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return Ok(addAdminDTO);
        }

    }
}
