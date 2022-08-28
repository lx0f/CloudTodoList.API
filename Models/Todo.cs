using System.ComponentModel.DataAnnotations;

namespace CloudTodoList.API.Models;

public enum Status
{
    Open,
    Closed
}

public class Todo
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; } = Status.Open;
}

public class TodoDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; }
}
