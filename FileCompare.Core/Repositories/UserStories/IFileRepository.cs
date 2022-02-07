using FileCompare.Core.DTO;
using FileCompare.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Core.Repositories.UserStories
{
    public interface IFileRepository
    {
        Task<CompareFileResponseDTO> CompareFileAsync(FileDTO fileDTO);
        Task<List<FileCompareHistoryEntity>> GetHistoryAllAsync();
        Task<FileCompareHistoryEntity> GetHistoryByIdAsync(Int64 id);
    }
}
