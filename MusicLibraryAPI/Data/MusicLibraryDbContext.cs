using MusicLibraryAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Data
{
    public class MusicLibraryDbContext: DbContext
    {
        public DbSet<ArtistEntity> Artists { get; set; }
        public DbSet<AlbumEntity> Albums { get; set; }
        public DbSet<SongEntity> Songs { get; set; }

        public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ArtistEntity>().ToTable("Artists");
            modelBuilder.Entity<ArtistEntity>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ArtistEntity>().HasMany(a => a.Albums).WithOne(a => a.Artist);

            modelBuilder.Entity<AlbumEntity>().ToTable("Albums");
            modelBuilder.Entity<AlbumEntity>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AlbumEntity>().HasOne(a => a.Artist).WithMany(a => a.Albums);
            modelBuilder.Entity<AlbumEntity>().HasMany(a => a.Songs).WithOne(a => a.Album);

            modelBuilder.Entity<SongEntity>().ToTable("Songs");
            modelBuilder.Entity<SongEntity>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SongEntity>().HasOne(s => s.Album).WithMany(s => s.Songs);
            
            /*
            modelBuilder.Entity<TeamEntity>().ToTable("Teams");
            modelBuilder.Entity<TeamEntity>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TeamEntity>().HasMany(t => t.Players).WithOne(p => p.Team);

            modelBuilder.Entity<PlayerEntity>().ToTable("Players");
            modelBuilder.Entity<PlayerEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PlayerEntity>().HasOne(p => p.Team).WithMany(t => t.Players);
            */
            //dotnet tool install --global dotnet-ef
            //dotnet ef migrations add InitialCreate
            //dotnet ef database update
        }
    }
}
