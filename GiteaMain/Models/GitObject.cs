using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gitea_1.GiteaMain.Models
{
    public class GitObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public required string Hash { get; set; }   // SHA1 or SHA256

        [Required]
        public required string FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
