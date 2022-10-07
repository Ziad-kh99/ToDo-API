using System.ComponentModel.DataAnnotations;

namespace ToDo.Models;

public class ToDoItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
}