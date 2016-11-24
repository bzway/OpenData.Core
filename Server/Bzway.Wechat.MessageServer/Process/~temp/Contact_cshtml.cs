namespace AspNet.View
{
    using System.Threading.Tasks;

    public class Contact_cshtml : Bzway.Wechat.MessageServer.DynamicView
    {
        #line hidden
        public Contact_cshtml()
        {
        }

        #pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
#line 1 "Contact.cshtml"
  
    ViewData["Title"] = "Contact";

#line default
#line hidden

            WriteLiteral("<h2>");
#line 4 "Contact.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            WriteLiteral(".</h2>\r\n<h3>");
#line 5 "Contact.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            WriteLiteral(@"</h3>

<address>
    One Microsoft Way<br />
    Redmond, WA 98052-6399<br />
    <abbr title=""Phone"">P:</abbr>
    425.555.0100
</address>

<address>
    <strong>Support:</strong> <a href=""mailto:Support@example.com"">Support@example.com</a><br />
    <strong>Marketing:</strong> <a href=""mailto:Marketing@example.com"">Marketing@example.com</a>
</address>
");
        }
        #pragma warning restore 1998
    }
}
