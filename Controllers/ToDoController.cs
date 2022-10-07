using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;



namespace ToDo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly AppDbContext _context;
    public ToDoController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemDTO>> GetToDoItems()
    {
        return _context.ToDoItems.Select(x => ItemToDTO(x)).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<ToDoItemDTO> GetToDoItem(int id)
    {
        var item = _context.ToDoItems.Find(id);

        if(item is null)  
            return NotFound();

        return ItemToDTO(item);
    }


    [HttpPost]
    public async Task<ActionResult<ToDoItemDTO>> PostToDoItem(ToDoItemDTO toDoItem)
    {
        ToDoItem item = new ToDoItem
        {
            Id = toDoItem.Id,
            Name = toDoItem.Name,
            IsComplete = toDoItem.IsComplete,
            CreatedAt = DateTime.Now
        };

        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetToDoItem), new {id = toDoItem.Id}, ItemToDTO(item));
    }

    [HttpPut("{id}")]
    public ActionResult PutToDoItem(int id, ToDoItemDTO toDoItemDTO)
    {
        if(id != toDoItemDTO.Id)
            return BadRequest();


        var toDoItem = _context.ToDoItems.Find(id);

        if(toDoItem is null)
        {
            return NotFound();
        }

        toDoItem.Name = toDoItemDTO.Name;
        toDoItem.IsComplete = toDoItemDTO.IsComplete;

        try{
            _context.SaveChanges();
        }
        catch(DbUpdateConcurrencyException) when(!ToDoItemExist(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteToDoItem(int id)
    {
        var item = _context.ToDoItems.Find(id);

        if(item is null)
            return NotFound();

        _context.ToDoItems.Remove(item);
        _context.SaveChanges();

        return NoContent();
    }



    #region Helpers:
    private bool ToDoItemExist(int id)
    {
        return _context.ToDoItems.Any(i => i.Id == id);
    }

    private ToDoItemDTO ItemToDTO(ToDoItem toDoItem) =>
        new ToDoItemDTO
        {
            Id = toDoItem.Id,
            Name = toDoItem.Name,
            IsComplete = toDoItem.IsComplete
        };
    
    #endregion

}