#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace TaskManager.Models
{
    public class Priority
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
