namespace Git.Services
{
    using Git.ViewModels.Commits;
    using System.Collections.Generic;



    public interface ICommitsService
    {
        IEnumerable<CommitViewModel> GetAll(string userId);

        string GetRepoNameById(string repoId);

        void CreateCommit(string userId, string repoId, string description);

        void Delete(string id);
    }
}
