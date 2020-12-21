namespace Git.Controllers
{
    using Git.Services;
    using Git.ViewModels.Repositories;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var model = this.repositoriesService.GetAll();

            return this.View(model);
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
        public HttpResponse Create(CreateInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.Name)
                || input.Name.Length < 3
                || input.Name.Length > 10)
            {
                return this.Redirect("/Repositories/Create");
            }

            var userId = this.GetUserId();

            this.repositoriesService.CreateRepository(userId, input.Name, input.repositoryType);

            return this.Redirect("/Repositories/All");
        }
    }
}
