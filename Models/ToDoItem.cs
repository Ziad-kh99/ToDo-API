using System.ComponentModel.DataAnnotations;

namespace ToDo.Models;

public class ToDoItem
{
    public int Id { get; set; }

    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    public bool IsComplete { get; set; }

    public DateTime CreatedAt { get; set; }
}