using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileCompare.Core.Entities
{
    public class FileCompareHistoryEntity
    {
        
        public Int64 Id { get; set; }
        public string FirstStudentName { get; set; }
        public string SecondStudentName { get; set; }
        public string FirstStudentFileName { get; set; }
        public string SecondStudentFileName { get; set; }
        public string SimilarityPercentage { get; set; }
        public string SimilarityContent { get; set; }
        public DateTime DateCompare { get; set; }
        
    }
}
