using FileCompare.Core.DTO;
using FileCompare.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Application.Services.UserStories
{
    public interface IFileService
    {
        Task<CompareFileResponseDTO> CompareFile(IFormFile firstStudentFile, IFormFile secondStudentFile, string firstStudentName, string secondStudentName);
        Task<List<FileCompareHistoryEntity>> GetHistoryAllAsync();
        Task<FileCompareHistoryEntity> GetHistoryByIdAsync(Int64 id);
    }
}
