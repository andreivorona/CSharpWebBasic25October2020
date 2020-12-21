namespace Git.Services
{
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;

    public interface IRepositoriesService
    {
        void CreateRepository(string userId, string name, string repositoryType);

        IEnumerable<RepositoryViewModel> GetAll();
    }
}
