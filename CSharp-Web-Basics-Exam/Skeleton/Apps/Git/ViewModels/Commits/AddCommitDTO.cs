namespace Git.ViewModels.Commits
{
    using System;

    public class AddCommitDTO
    {
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public string RepositoryId { get; set; }
    }
}
