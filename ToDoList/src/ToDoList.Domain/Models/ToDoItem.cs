using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Models
{
    public class ToDoItem
    {

        [Key]
        public int ToDoItemId { get; set; } //EF core hledá nějakou proměnou, která má název něco jako ID, nebo NameId
        [Length(1, 50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
