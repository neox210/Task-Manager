#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace TaskManager.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
