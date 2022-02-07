using FileCompare.Core.DTO;
using FileCompare.Core.Entities;
using FileCompare.Core.Repositories.UserStories;
using FileCompare.Infastructure.Persistence.DBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Infastructure.Repositories.UserStories

{
    public class FileRepository : IFileRepository
    {
        private IHostingEnvironment _hostingEnvironment;
        private ApplicationDBContext _applicationDBContext;

        public FileRepository(IHostingEnvironment hostingEnvironment, ApplicationDBContext applicationDBContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _applicationDBContext = applicationDBContext;
        }

        public async Task<CompareFileResponseDTO> CompareFileAsync(FileDTO fileDTO)
        {
            CompareFileResponseDTO resp = new CompareFileResponseDTO();

            await SaveFileToDiskAsync(fileDTO);
            resp = await CompareAsync(fileDTO);

            return resp;
        }
        private async Task SaveFileToDiskAsync(FileDTO fileDTO)
        {
            var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files");
            Directory.CreateDirectory(target);

            // var filePath = Path.Combine(target, fileDTO.FirstStudentFile.FileName);
            using (var stream = new FileStream(Path.Combine(target, fileDTO.FirstStudentFile.FileName), FileMode.Create))
            {
                await fileDTO.FirstStudentFile.CopyToAsync(stream);
            }

            //filePath = Path.Combine(target, fileDTO.SecondStudentFile.FileName);
            using (var stream = new FileStream(Path.Combine(target, fileDTO.SecondStudentFile.FileName), FileMode.Create))
            {
                await fileDTO.SecondStudentFile.CopyToAsync(stream);
            }
        }
        private async Task<CompareFileResponseDTO> CompareAsync(FileDTO fileDTO)
        {
            var f1 = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files", fileDTO.FirstStudentFile.FileName);
            var f2 = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files", fileDTO.SecondStudentFile.FileName);
            List<byte> lsByte = new List<byte>();
            string fresult = string.Empty;
            int totalWordCount = 0;
            int found = 0;

            // initialize returned entity
            FileCompareHistoryEntity entity = new FileCompareHistoryEntity();
            entity.FirstStudentName = fileDTO.FirstStudentName;
            entity.FirstStudentFileName = fileDTO.FirstStudentFile.FileName;
            entity.SecondStudentName = fileDTO.SecondStudentName;
            entity.SecondStudentFileName = fileDTO.SecondStudentFile.FileName;
            entity.DateCompare = DateTime.Now;

            var areEquals = System.IO.File.ReadLines(f1).SequenceEqual(System.IO.File.ReadLines(f2), StringComparer.OrdinalIgnoreCase);

            if (System.IO.File.ReadLines(f1).SequenceEqual(System.IO.File.ReadLines(f2)) == true)
            {
                var sbFirstFile = new StringBuilder();
                using (var reader = new StreamReader(fileDTO.FirstStudentFile.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        sbFirstFile.AppendLine(await reader.ReadLineAsync());
                }

                entity.SimilarityContent = sbFirstFile.ToString();
                entity.SimilarityPercentage = "100%";

                // save to database
                await SaveToSQLDatabaseAsync(entity);

                //TODO:  call save repository to save the result here
                return new CompareFileResponseDTO
                {
                    PercentageRate = "100%",
                    SimilarContent = sbFirstFile.ToString()
                };
            }
            else
            {
                CompareByString(ref f1, ref f2, ref fresult, ref totalWordCount, ref found);

                decimal per = Math.Round((((decimal)found / (decimal)totalWordCount) * 100), 2);
                entity.SimilarityContent = fresult;
                entity.SimilarityPercentage = per.ToString() + "%";

                // save to database
                await SaveToSQLDatabaseAsync(entity);

                return new CompareFileResponseDTO
                {
                    PercentageRate = per.ToString() + "%",
                    SimilarContent = fresult
                };
            }
        }
        private async Task SaveToSQLDatabaseAsync(FileCompareHistoryEntity entity)
        {
            await _applicationDBContext.FileCompareHistory.AddAsync(entity);
            await _applicationDBContext.SaveChangesAsync();
        }
        public async Task<List<FileCompareHistoryEntity>> GetHistoryAllAsync()
        {
            return await _applicationDBContext.Set<FileCompareHistoryEntity>().ToListAsync();
        }
        public async Task<FileCompareHistoryEntity> GetHistoryByIdAsync(Int64 id)
        {
            return await _applicationDBContext.Set<FileCompareHistoryEntity>().FindAsync(id);
        }
        
        private void CompareByString(ref string f1, ref string f2, ref string fresult, ref int totalWordCount, ref int found)
        {
            string[] aryFile1 = File.ReadAllText(f1).Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace(",", " ").Replace(".", " ").Split(' ');
            string[] aryFile2 = File.ReadAllText(f2).Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace(",", " ").Replace(".", " ").Split(' ');
            
            List<string> lsresult = new List<string>();

            foreach (string e in aryFile1)
            {
                if (e.Trim() == "") continue;

                foreach (string e2 in aryFile2)
                {
                    if (e2.Trim() == "") continue;

                    totalWordCount = totalWordCount + 1;
                    if (e.Equals(e2, StringComparison.OrdinalIgnoreCase))
                    {
                        found = found + 1;
                        lsresult.Add(e);
                    }
                    else
                    {
                        if (areAnagram(e, e2))
                        {
                            found = found + 1;
                            lsresult.Add(e);
                        }
                    }
                }
            }

            if (lsresult.Count > 0)
            {
                fresult = String.Join(" ", lsresult.ToArray());
            }

        }
        private bool areAnagram(string firstString, string secondString)
        {
            if (firstString.Length != secondString.Length)
            {
                return false;
            }
            //Convert string to character array  
            char[] firstCharsArray = firstString.ToLower().ToCharArray();
            char[] secondCharsArray = secondString.ToLower().ToCharArray();
            //Sort array  
            Array.Sort(firstCharsArray);
            Array.Sort(secondCharsArray);
            //Check each character and position.  
            for (int i = 0; i < firstCharsArray.Length; i++)
            {
                if (firstCharsArray[i].ToString() != secondCharsArray[i].ToString())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
