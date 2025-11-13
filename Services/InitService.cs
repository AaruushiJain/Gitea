using System;
using System.IO;
using System.Linq;
using Gitea_1.GiteaMain.Data;
using Gitea_1.GiteaMain.Models;

namespace Gitea_1.Services
{
    public class InitService : IInitService
    {
        private readonly GiteaContext _context;
        private readonly string _repoPath = ".git_gud";
        private readonly string _objectsDir = ".git_gud/objects";

        public InitService(GiteaContext context)
        {
            _context = context;
        }

        public bool InitializeRepository()
        {
            try
            {
                // Create main repo directory
                if (!Directory.Exists(_repoPath))
                    Directory.CreateDirectory(_repoPath);

                // Create objects directory
                if (!Directory.Exists(_objectsDir))
                    Directory.CreateDirectory(_objectsDir);

                // Check if already exists in DB
                if (_context.Repositories.Any(r => r.RepoPath == _repoPath))
                    return false;

                // Save to database
                var repo = new Repo { RepoPath = _repoPath };
                _context.Repositories.Add(repo);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InitService] Error: {ex.Message}");
                return false;
            }
        }
    }
}
