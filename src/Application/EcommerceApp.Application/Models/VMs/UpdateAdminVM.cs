using EcommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Models.VMs
{
    public class UpdateAdminVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public Roles Roles => Roles.Admin;
        public string? ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
    }
}
