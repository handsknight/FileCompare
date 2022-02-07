using FileCompare.Application.Services.UserStories;
using FileCompare.Core.DTO;
using FileCompare.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileCompare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CompareController : ControllerBase
    {
        private IFileService _fileService;

        public CompareController(IFileService fileService)
        {
            _fileService = fileService;
        }

        //[Authorize]
        [HttpPost]
        [Route("CompareFile")]
        public async Task<IActionResult> CompareFile([Required] IFormFile firstStudentFile, [Required] IFormFile secondStudentFile, [Required] string firstStudentName, [Required] string secondStudentName)
        {
            if (!firstStudentFile.FileName.Contains(".txt") || !secondStudentFile.FileName.Contains(".txt"))
            {
                return BadRequest(new { message = "Invalid file extension: Files must be a text file!" });
            }


            if (firstStudentFile.FileName.Length <= 0 || secondStudentFile.FileName.Length <= 0)
            {
                return BadRequest(new { message = "One of the files is empty!" });
            }

            return Ok(await _fileService.CompareFile(firstStudentFile, secondStudentFile, firstStudentName, secondStudentName));

        }

        //[Authorize]
        [HttpGet]
        [Route("HistoryGetAll")]
        public async Task<IActionResult> HistoryGetAll()
        {
            var result = await _fileService.GetHistoryAllAsync();
            if (result == default(List<FileCompareHistoryEntity>) 
                || result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //[Authorize]
        [HttpGet]
        [Route("HistoryGetById")]
        public async Task<IActionResult> HistoryGetById([Required] Int64 id)
        {
            var result = await _fileService.GetHistoryByIdAsync(id);
            if (result == default(FileCompareHistoryEntity))
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
