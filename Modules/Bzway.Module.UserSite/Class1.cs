namespace Bzway.Module.UserSite
{


    //public interface IApplicationTenant

    //{

    //    string ApplicationName { get; }


    //    IFeatureRegistry EnabledFeatures { get; }


    //    IEnumerable<string> UrlPaths { get; }


    //    IContainer DependencyContainer { get; }


    //    IViewEngine ViewEngine { get; }

    //}
    //public interface IFeature

    //{

    //    string FeatureName { get; }

    //}


    //public interface IComplexFeature : IFeature

    //{

    //    IEnumerable<IFeature> SubFeatures { get; }

    //}


    //public interface IFeatureRegistry

    //{

    //    IEnumerable<IFeature> Features { get; }


    //    bool IsEnabled(IEnumerable<string> featurePath);

    //}
    //public interface ITenantSelector

    //{

    //    IEnumerable<IApplicationTenant> Tenants { get; }


    //    IApplicationTenant Select(RequestContext context);

    //}


    //public class DefaultTenantSelector : ITenantSelector

    //{

    //    public DefaultTenantSelector(IEnumerable<IApplicationTenant> tenants)

    //    {

    //        Ensure.Argument.NotNull(tenants, "tenants");

    //        this.Tenants = tenants;

    //    }


    //    public IEnumerable<IApplicationTenant> Tenants { get; private set; }


    //    public IApplicationTenant Select(RequestContext context)

    //    {

    //        Ensure.Argument.NotNull(context, "context");


    //        string baseurl = context.HttpContext.BaseUrl().TrimEnd('/');


    //        var valid = from tenant in this.Tenants

    //                    from path in tenant.UrlPaths

    //                    where path.Trim().TrimEnd('/').Equals(baseurl, StringComparison.OrdinalIgnoreCase)

    //                    select tenant;


    //        if (!valid.Any())

    //            throw new TenantNotFoundException();

    //        return valid.First();

    //    }

    //}
}