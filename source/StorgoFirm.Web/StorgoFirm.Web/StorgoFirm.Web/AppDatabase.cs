using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorgoFirm.Web
{
    public class AppDatabase : DbContext
    {
        public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<SportEvent> Events { get; set; }
    }

    public class Sport
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<League> Leagues { get; set; }
    }

    public class League
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long SportId { get; set; }

        [ForeignKey(nameof(SportId))]
        public Sport Sport { get; set; }
    }

    public class SportEvent
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public DateTime DateUtc { get; set; }

        [ForeignKey(nameof(SportId))]
        public Sport Sport { get; set; }
        public long SportId { get; set; }

        [ForeignKey(nameof(LeagueId))]
        public League League { get; set; }
        public long LeagueId { get; set; }

        public decimal HomeTeamScore { get; set; }
        public decimal AwayTeamScore { get; set; }
        public double HomeTeamOdds { get; set; }
        public double AwayTeamOdds { get; set; }
        public double DrawOdds { get; set; }
    }
}
