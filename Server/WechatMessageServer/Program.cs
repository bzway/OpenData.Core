namespace Bzway.Wechat.MessageServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServer server = new WebServer();
            server.UseMvc();
            server.UseKeyWord();
            server.Run();
        }
    }
}