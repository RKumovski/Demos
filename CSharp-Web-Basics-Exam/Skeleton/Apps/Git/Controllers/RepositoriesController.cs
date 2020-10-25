namespace Git.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    using Git.Services;
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            IEnumerable<RepositoryViewModel> repositories;

            if (!this.IsUserSignedIn())
            {
                repositories = this.repositoriesService.GetAllPublic();
            }
            else
            {
                repositories = this.repositoriesService.GetAllPerUser(this.GetUserId());
            }


            return this.View(repositories.ToList());
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AddRepositoryInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.Name) || input.Name.Length < 3 || input.Name.Length > 10)
            {
                return this.Error("Name must be between 3 and 10 symbols.");
            }

            if (input.RepositoryType.ToLower() != "public" && input.RepositoryType.ToLower() != "private")
            {
                return this.Error("Don't try to cheat!");
            }

            var inputDTO = new AddRepositoryDTO
            {
                Name = input.Name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = input.RepositoryType.ToLower() == "public",
                OwnerId = this.GetUserId(),
            };

            this.repositoriesService.Create(inputDTO);

            return this.Redirect("/Repositories/All");
        }
    }
}
