using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        // Essa propriedade representa a tabela Notes no banco de dados
        public DbSet<Note> Notes { get; set; }
    }
}
