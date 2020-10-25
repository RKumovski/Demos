namespace Git.Services
{
    using System.Collections.Generic;

    using Git.ViewModels.Repositories;

    public interface IRepositoriesService
    {
        void Create(AddRepositoryDTO repositoryDTO);

        IEnumerable<RepositoryViewModel> GetAllPublic();

        IEnumerable<RepositoryViewModel> GetAllPerUser(string id);

        string GetNameByID(string id);
    }
}
