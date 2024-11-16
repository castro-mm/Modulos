using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ModulosContext(DbContextOptions options) : DbContext(options)
{
}