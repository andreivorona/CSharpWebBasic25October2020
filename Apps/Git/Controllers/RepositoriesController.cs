namespace Git.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class RepositoriesController : Controller
    {
        public HttpResponse All()
        {
            return this.View();
        }
    }
}
