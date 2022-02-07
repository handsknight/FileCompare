using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Core.DTO
{
    public class FileDTO
    {
        public string FirstStudentName { get; set; }
        public IFormFile FirstStudentFile { get; set; }
        public string SecondStudentName { get; set; }
        public IFormFile SecondStudentFile { get; set; }
    }
}
