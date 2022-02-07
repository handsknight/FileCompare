using FileCompare.Application.Adapters.UserStories;
using FileCompare.Core.DTO;
using FileCompare.Core.Entities;
using FileCompare.Core.Repositories.UserStories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Application.Services.UserStories
{
    public class FileService: IFileService
    {
        private IFileRepository _fileRepository;
        private IFileAdapter _fileAdapter;

        public FileService(IFileRepository fileRepository, IFileAdapter fileAdapter)
        {
            _fileRepository = fileRepository;
            _fileAdapter = fileAdapter;
        }

        public async Task<CompareFileResponseDTO> CompareFile(IFormFile firstStudentFile, IFormFile secondStudentFile, string firstStudentName, string secondStudentName)
        {
            FileDTO fileDTO = new FileDTO();
            fileDTO = _fileAdapter.Adapt(firstStudentFile, secondStudentFile, firstStudentName, secondStudentName);
            
            return await _fileRepository.CompareFileAsync(fileDTO);
        }

        public async Task<List<FileCompareHistoryEntity>> GetHistoryAllAsync()
        {
            return await _fileRepository.GetHistoryAllAsync();
        }

        public async Task<FileCompareHistoryEntity> GetHistoryByIdAsync(Int64 id)
        {
            return await _fileRepository.GetHistoryByIdAsync(id);
        }
    }
}
