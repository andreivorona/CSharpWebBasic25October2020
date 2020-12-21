namespace Git.Services
{
    using Git.Data;
    using Git.ViewModels.Commits;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CommitViewModel> GetAll(string userId)
        {
            var commits = this.db.Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Description = x.Description,
                    RepositoryName = x.Repository.Name,
                }).ToList();

            return commits;
        }

        public void CreateCommit(string userId, string repoId, string description)
        {
            var commit = new Commit
            {
                Description = description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                RepositoryId = repoId,
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var result = this.db.Commits
                .Find(id);

            this.db.Commits.Remove(result);
            this.db.SaveChanges();
        }

        public string GetRepoNameById(string repoId)
        {
            var result = this.db.Repositories
                .Where(x => x.Id == repoId)
                .Select(x => x.Name)
                .FirstOrDefault();

            return result;
        }
    }
}
