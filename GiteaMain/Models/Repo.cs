using System;
using System.ComponentModel.DataAnnotations;

namespace Gitea_1.GiteaMain.Models
{
    public class Repo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string RepoPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
