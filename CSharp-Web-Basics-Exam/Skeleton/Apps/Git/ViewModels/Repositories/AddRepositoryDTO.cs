namespace Git.ViewModels.Repositories
{
    using System;

    public class AddRepositoryDTO
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }
    }
}
