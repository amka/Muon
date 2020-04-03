using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Muon.Models;

namespace Muon.Services
{
    public class StorageContext : DbContext
    {
        public string DbPath { get; }
        public DbSet<Document> Documents;

        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {
        }

        void EnsureFolderCreated(string appConfigPath)
        {
            if (!Directory.Exists(appConfigPath))
            {
                Directory.CreateDirectory(appConfigPath);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}