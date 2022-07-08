using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bnotes_web_api.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<bnotes_web_api.Models.Friend>? Friends { get; set; }
    public DbSet<bnotes_web_api.Models.Note>? Notes { get; set; }
    public DbSet<bnotes_web_api.Models.Favourite>? Favourites { get; set; }
}
