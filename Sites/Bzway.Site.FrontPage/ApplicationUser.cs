namespace Bzway.Site.FrontPage.Controllers
{
    internal class ApplicationUser
    {
        public string Email { get; set; }
        public object PasswordHash { get; internal set; }
        public string UserName { get; set; }
    }
}