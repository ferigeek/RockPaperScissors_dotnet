using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    internal class BotDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = @"./data/botDB.sqlite";
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }


    [Table("Users")]
    public class User
    {
        [Required]
        public long UserID { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
    }
}
