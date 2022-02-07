using FileCompare.Core.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Application.Adapters.UserStories
{
    public class FileAdapter : IFileAdapter
    {
        public FileDTO Adapt(IFormFile firstStudentFile, IFormFile secondStudentFile, string firstStudentName, string secondStudentName)
        {
            return new FileDTO()
            {
                FirstStudentName = firstStudentName,
                FirstStudentFile = firstStudentFile,
                SecondStudentName = secondStudentName,
                SecondStudentFile = secondStudentFile
            };
        }
    }
}
