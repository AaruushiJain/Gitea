using System.ComponentModel.DataAnnotations;

namespace Gitea_1.GiteaMain.Models
{
    public class FileRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public required string Hash { get; set; }

        [Required]
        public required string Content { get; set; }   // File data (text or base64)

        public long Size { get; set; }
    }
}
