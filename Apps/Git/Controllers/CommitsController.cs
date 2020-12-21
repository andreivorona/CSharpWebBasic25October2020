namespace Git.Controllers
{
    using Git.Services;
    using Git.ViewModels.Commits;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            var model = this.commitsService.GetAll(userId);

            return this.View(model);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var name = this.commitsService.GetRepoNameById(id);

            var model = new CommitViewModel
            {
                Id = id,
                RepositoryName = name,
            };

            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(string id, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(description)
                || description.Length < 5)
            {
                return this.Redirect("/Commist/Create");
            }

            var userId = this.GetUserId();

            this.commitsService.CreateCommit(userId, id, description);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.commitsService.Delete(id);

            return this.Redirect("/Commits/All");
        }
    }
}
