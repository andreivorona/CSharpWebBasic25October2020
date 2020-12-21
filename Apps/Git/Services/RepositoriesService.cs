namespace Git.Services
{
    using Git.Data;
    using Git.ViewModels.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateRepository(string userId, string name, string repositoryType)
        {
            bool isPublic = false;

            if (repositoryType == "Public")
            {
                isPublic = true;
            }

            var repo = new Repository
            {
                OwnerId = userId,
                Name = name,
                IsPublic = isPublic,
                CreatedOn = DateTime.UtcNow,
            };

            this.db.Repositories.Add(repo);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            var repo = this.db.Repositories
                .Where(x => x.IsPublic == true)
                .Select(x => new RepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    Owner = x.Owner.Username,
                    CommitsCount = this.db.Commits.Count(),
                }).ToList();

            return repo;
        }
    }
}
