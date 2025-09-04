using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Internship> Internships { get; set; } = null!;
        public DbSet<StudentProfile> StudentProfiles { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<StudentSkill> StudentSkills { get; set; } = null!;
        public DbSet<InternshipSkill> InternshipSkills { get; set; } = null!;
        public DbSet<Application> Applications { get; set; } = null!;
        public DbSet<Recruiter> Recruiters { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // StudentSkill composite key
            builder.Entity<StudentSkill>()
                .HasKey(ss => new { ss.StudentId, ss.SkillId });

            builder.Entity<StudentSkill>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSkills)
                .HasForeignKey(ss => ss.StudentId);

            builder.Entity<StudentSkill>()
                .HasOne(ss => ss.Skill)
                .WithMany()
                .HasForeignKey(ss => ss.SkillId);

            // InternshipSkill composite key
            builder.Entity<InternshipSkill>()
                .HasKey(its => new { its.InternshipId, its.SkillId });

            builder.Entity<InternshipSkill>()
                .HasOne(its => its.Internship)
                .WithMany(i => i.InternshipSkills)
                .HasForeignKey(its => its.InternshipId);

            builder.Entity<InternshipSkill>()
                .HasOne(its => its.Skill)
                .WithMany()
                .HasForeignKey(its => its.SkillId);
        }

    }
}