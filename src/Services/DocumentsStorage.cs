using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Muon.Models;

namespace Muon.Services
{
    public class DocumentsStorage : DbContext
    {

        string DbPath;
        public DbSet<Document> Documents;

        public DocumentsStorage(string path = "./documents.db")
        {
            DbPath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().ToTable("Documents");
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public async Task<Document> AddDocument()
        {
            var doc = new Document() { Title = "New document" };
            await AddAsync(doc);
            await SaveChangesAsync();
            return doc;
        }

        public void RemoveDocument(Document doc)
        {
            Remove(doc);
        }
    }
}