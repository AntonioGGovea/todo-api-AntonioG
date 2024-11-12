using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

namespace Todo.Data.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoModel> Todo { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
