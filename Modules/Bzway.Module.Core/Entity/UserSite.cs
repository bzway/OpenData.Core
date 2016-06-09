namespace Bzway.Module.Core
{
    public class UserSite : EntityBase, IUserSite
    {
        public string Name { get; set; }
        public string[] Host { get; set; }

    }


    public interface IUserSite
    {
        string Name { get; set; }
        string[] Host { get; set; }
    }
}