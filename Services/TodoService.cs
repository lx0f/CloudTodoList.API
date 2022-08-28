using Microsoft.EntityFrameworkCore;
using CloudTodoList.API.Models;
using CloudTodoList.API.Data;

namespace CloudTodoList.API.Services;

public interface ITodoService
{
    public Task<IEnumerable<Todo>> GetAll();

    public Task<Todo?> GetById(int id);

    public Task<bool> Create(TodoDto todoDto);

    public Task<bool> Update(int id, TodoDto todoDto);

    public Task<bool> Delete(int id);
}

public class TodoService : ITodoService
{
    private readonly DataContext _context;

    public TodoService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Todo>> GetAll()
    {
        return await _context.Todos
        .ToListAsync();
    }

    public async Task<Todo?> GetById(int id)
    {
        return await _context.Todos
        .FindAsync(id);
    }

    public async Task<bool> Create(TodoDto todoDto)
    {
        Todo? existTitle = await _context.Todos
            .SingleOrDefaultAsync(t => t.Title == todoDto.Title);

        if (existTitle != null)
            return false;

        Todo todo = new()
        {
            Title = todoDto.Title,
            Description = todoDto.Description,
            Status = todoDto.Status
        };

        await _context.AddAsync(todo);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(int id, TodoDto todoDto)
    {
        Todo? todo = await _context.Todos.FindAsync(id);

        if (todo == null)
            return false;

        todo.Title = todoDto.Title;
        todo.Description = todoDto.Description;
        todo.Status = todoDto.Status;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        Todo? todo = await _context.Todos.FindAsync(id);

        if (todo == null)
            return false;

        _context.Remove(todo);
        await _context.SaveChangesAsync();

        return true;
    }
}