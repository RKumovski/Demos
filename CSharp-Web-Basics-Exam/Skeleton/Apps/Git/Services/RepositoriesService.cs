namespace Git.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Git.Data;
    using Git.ViewModels.Repositories;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext dbContext;

        public RepositoriesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(AddRepositoryDTO repositoryDTO)
        {
            var repository = new Repository
            {
                Name = repositoryDTO.Name,
                CreatedOn = repositoryDTO.CreatedOn,
                IsPublic = repositoryDTO.IsPublic,
                OwnerId = repositoryDTO.OwnerId,
            };

            dbContext.Repositories.Add(repository);
            dbContext.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAllPublic()
        {
            var repositories = this.dbContext.Repositories.Where(r => r.IsPublic).Select(r => new RepositoryViewModel
            {
                Id = r.Id,
                Name = r.Name,
                CreatedOn = r.CreatedOn,
                Owner = r.Owner.Username,
                CommitsCount = r.Commits.Count,
            }).ToList();

            return repositories;
        }

        public IEnumerable<RepositoryViewModel> GetAllPerUser(string id)
        {
            var repositories = this.dbContext.Repositories.Where(r => r.IsPublic || r.OwnerId == id).Select(r => new RepositoryViewModel
            {
                Id = r.Id,
                Name = r.Name,
                CreatedOn = r.CreatedOn,
                Owner = r.Owner.Username,
                CommitsCount = r.Commits.Count,
            }).ToList();

            return repositories;
        }

        public string GetNameByID(string id)
        {
            return this.dbContext.Repositories.FirstOrDefault(r => r.Id == id)?.Name;
        }
    }
}
