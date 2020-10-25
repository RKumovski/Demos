namespace Git.Services
{
    using Git.Data;
    using Git.ViewModels.Commits;
    using System.Collections.Generic;
    using System.Linq;

    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext dbContext;

        public CommitsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(AddCommitDTO inputDTO)
        {
            var commit = new Commit
            {
                Description = inputDTO.Description,
                CreatedOn = inputDTO.CreatedOn,
                RepositoryId = inputDTO.RepositoryId,
                CreatorId = inputDTO.CreatorId,
            };

            this.dbContext.Commits.Add(commit);
            this.dbContext.SaveChanges();
        }

        public bool DeleteById(string id)
        {
            var commit = this.dbContext.Commits.FirstOrDefault(c => c.Id == id);
            if (commit == null)
            {
                return false;
            }

            this.dbContext.Commits.Remove(commit);
            this.dbContext.SaveChanges();
            
            return true;
        }

        //public IEnumerable<CommitViewModel> GetAllPublic()
        //{
        //    var commits = this.dbContext.Commits.Where(c => c.Repository.IsPublic).Select(c => new CommitViewModel
        //    {
        //        CommitId = c.Id,
        //        Description = c.Description,
        //        CreatedOn = c.CreatedOn,
        //        RepositoryName = c.Repository.Name,
        //    }).ToList();

        //    return commits;
        //}

        public IEnumerable<CommitViewModel> GetAllPerUser(string id)
        {
            var commits = this.dbContext.Commits.Where(c => c.CreatorId == id).Select(c => new CommitViewModel
            {
                CommitId = c.Id,
                Description = c.Description,
                CreatedOn = c.CreatedOn,
                RepositoryName = c.Repository.Name,
                IsRepositoryDeletable = c.CreatorId == id,
            }).ToList();

            return commits;
        }

        public string GetCreatorIdById(string id)
        {
            return this.dbContext.Commits.Where(c => c.Id == id).Select(c => c.CreatorId).FirstOrDefault();
        }
    }
}
