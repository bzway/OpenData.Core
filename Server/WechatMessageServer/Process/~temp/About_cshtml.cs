namespace AspNet.View
{
    using System.Threading.Tasks;

    public class About_cshtml : Bzway.Wechat.MessageServer.DynamicView
    {
        #line hidden
        public About_cshtml()
        {
        }

        #pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
#line 1 "About.cshtml"
  
    if (this.Model==null)
    {

#line default
#line hidden

            WriteLiteral("        <a>true</a>\r\n");
#line 5 "About.cshtml"
    }
    else
    {

#line default
#line hidden

            WriteLiteral("        <a>false</a>\r\n");
#line 9 "About.cshtml"
    }

#line default
#line hidden

        }
        #pragma warning restore 1998
    }
}
