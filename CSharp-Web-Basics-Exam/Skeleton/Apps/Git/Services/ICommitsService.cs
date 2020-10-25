namespace Git.Services
{
    using Git.ViewModels.Commits;
    
    using System.Collections.Generic;

    public interface ICommitsService
    {
        void Create(AddCommitDTO inputDTO);

        string GetCreatorIdById(string id);

        bool DeleteById(string id);

        //IEnumerable<CommitViewModel> GetAllPublic();

        IEnumerable<CommitViewModel> GetAllPerUser(string id);
    }
}
