using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_3_Mission8.Models
{
    public class TaskFormResponse
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string Task { get; set; } // Task Name
        public DateTime DueDate { get; set; }
        [Required]
        public int Quadrant { get; set; }
        public bool Completed { get; set; }

        // Build FK Relationship
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
