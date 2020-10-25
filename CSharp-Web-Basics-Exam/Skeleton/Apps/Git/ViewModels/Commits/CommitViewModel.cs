namespace Git.ViewModels.Commits
{
    using System;

    public class CommitViewModel
    {
        public string CommitId { get; set; }

        public string RepositoryName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsRepositoryDeletable { get; set; }
    }
}
