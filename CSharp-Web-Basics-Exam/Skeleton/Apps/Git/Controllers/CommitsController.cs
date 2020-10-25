namespace Git.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Git.Services;
    using Git.ViewModels.Commits;
    
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repositoriesService;

        public CommitsController(ICommitsService commitsService, IRepositoriesService repositoriesService)
        {
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            //IEnumerable<CommitViewModel> commits;

            //if (!this.IsUserSignedIn())
            //{
            //    commits = this.commitsService.GetAllPublic();
            //}
            //else
            //{
            //    commits = this.commitsService.GetAllPerUser(this.GetUserId());
            //}

            var commits = this.commitsService.GetAllPerUser(this.GetUserId());


            return this.View(commits.ToList());
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var RepositoryName = this.repositoriesService.GetNameByID(id);
            if(RepositoryName == null)
            {
                return this.Error("Invalid repository id.");
            }

            var viewModel = new AddCommitViewModel
            {
                Id = id,
                Name = RepositoryName,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(AddCommitInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length < 5)
            {
                return this.Error("Description must be at least 5 symbols.");
            }

            var inputDTO = new AddCommitDTO
            {
                Description = input.Description,
                CreatedOn = DateTime.UtcNow,
                RepositoryId = input.Id,
                CreatorId = this.GetUserId(),
            };

            this.commitsService.Create(inputDTO);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var creatorId = this.commitsService.GetCreatorIdById(id);

            if(this.GetUserId() != creatorId)
            {
                return this.Error("You are not authorized for this operation!");
            }

            if (!this.commitsService.DeleteById(id))
            {
                return this.Error("Commit was not found.");
            }

            return this.Redirect("/Commits/All");// or  all repositories. => not sure
        }
    }
}
