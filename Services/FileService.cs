using System;
using System.IO;
using System.Linq;
using Gitea_1.GiteaMain.Data;
using Gitea_1.GiteaMain.Models;

namespace Gitea_1.Services
{
    public class FileService : IFileService
    {
        private readonly GiteaContext _context;

        public FileService(GiteaContext context)
        {
            _context = context;
        }

        public string? GetFileByHash(string hash)
        {
            try
            {
                var record = _context.FileRecords.FirstOrDefault(f => f.Hash == hash);
                if (record == null)
                    return null;

                // Decode from Base64
                var bytes = Convert.FromBase64String(record.Content);
                var content = System.Text.Encoding.UTF8.GetString(bytes);

                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService] Error: {ex.Message}");
                return null;
            }
        }
    }
}
