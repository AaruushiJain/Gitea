using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Gitea_1.GiteaMain.Data;
using Gitea_1.GiteaMain.Models;
using Microsoft.AspNetCore.Http;

namespace Gitea_1.Services
{
    public class ObjectService : IObjectService
    {
        private readonly GiteaContext _context;
        private readonly string _objectsDir = ".git_gud/objects";

        public ObjectService(GiteaContext context)
        {
            _context = context;
        }

        public string HashAndStoreObject(IFormFile file)
        {
            try
            {
                // Ensure objects directory exists
                if (!Directory.Exists(_objectsDir))
                    Directory.CreateDirectory(_objectsDir);

                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var bytes = ms.ToArray();

                // Compute SHA1 hash
                using var sha1 = SHA1.Create();
                var hashBytes = sha1.ComputeHash(bytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Save file to .git_gud/objects/
                var path = Path.Combine(_objectsDir, hash);
                File.WriteAllBytes(path, bytes);

                // Save metadata to database
                var gitObj = new GitObject
                {
                    Hash = hash,
                    FilePath = path,
                    CreatedAt = DateTime.UtcNow
                };
                _context.GitObjects.Add(gitObj);

                var fileRec = new FileRecord
                {
                    Hash = hash,
                    Content = Convert.ToBase64String(bytes),
                    Size = bytes.Length
                };
                _context.FileRecords.Add(fileRec);

                _context.SaveChanges();

                return hash;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ObjectService] Error: {ex.Message}");
                return "Error hashing file";
            }
        }
    }
}
