using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Optimization;
using Elliptical.Mvc.Configuration;

namespace Elliptical.Mvc.Optimization
{
    internal static class BundleConfig
    {
        public static BundleSection Config = ConfigurationManager.GetSection("ellipticalBundles") as BundleSection;

        public static string GetFrameworkVirtualPath()
        {
            return Config.Framework.VirtualPath;
        }

        public static string GetAppVirtualPath()
        {
            return "~/bundles/app";
        }

        public static NameValueCollection GetFrameworkScripts()
        {
            var items = Config.Framework.Items.OfType<Add>();
            var nameValueColl = new NameValueCollection();
            foreach (var item in items)
            {
                nameValueColl.Add(item.Key, item.Value);
            }

            return nameValueColl;
        }

        public static string GetPlatformVirtualPath()
        {
            return Config.Platform.VirtualPath;
        }

        public static NameValueCollection GetPlatformScripts()
        {
            var items = Config.Platform.Items.OfType<Add>();
            var nameValueColl = new NameValueCollection();
            foreach (var item in items)
            {
                nameValueColl.Add(item.Key, item.Value);
            }

            return nameValueColl;
        }

        public static string GetCssVirtualPath()
        {
            return Config.Css.VirtualPath;
        }

        public static NameValueCollection GetCssFiles()
        {
            var items = Config.Css.Items.OfType<Add>();
            var nameValueColl = new NameValueCollection();
            foreach (var item in items)
            {
                nameValueColl.Add(item.Key, item.Value);
            }

            return nameValueColl;
        }
    }

    public class FrameworkBundle : Bundle
    {
        public FrameworkBundle(string virtualPath)
            : base(virtualPath, new JsMinify())
        {
            setPaths();
        }

        public FrameworkBundle()
            : base(BundleConfig.GetFrameworkVirtualPath(), new JsMinify())
        {
            setPaths();
        }

        private void setPaths()
        {
            var pathCollections = BundleConfig.GetFrameworkScripts();
            var paths = new List<string>();
            foreach (string key in pathCollections)
            {
                paths.Add(pathCollections[key]);
            }

            Include(paths.ToArray());
        }
    }

    public class PlatformBundle : Bundle
    {
        public PlatformBundle(string virtualPath)
            : base(virtualPath, new JsMinify())
        {
            setPaths();
        }

        public PlatformBundle()
            : base(BundleConfig.GetPlatformVirtualPath(), new JsMinify())
        {
            setPaths();
        }

        private void setPaths()
        {
            var pathCollections = BundleConfig.GetPlatformScripts();
            var paths = new List<string>();
            foreach (string key in pathCollections)
            {
                paths.Add(pathCollections[key]);
            }

            Include(paths.ToArray());
        }
    }

    public class AppBundle : Bundle
    {
        public AppBundle(string virtualPath)
            : base(virtualPath, new JsMinify())
        {
            setBundle();
        }

        public AppBundle()
            : base(BundleConfig.GetAppVirtualPath(), new JsMinify())
        {
            setBundle();
        }

        private string path { get; set; }

        private void setBundle()
        {
            IncludeDirectory("~/App_JS/middleware", "*.js", true)
                .Include("~/App_JS/app.js")
                .IncludeDirectory("~/App_JS/providers", "*.js", true)
                .IncludeDirectory("~/App_JS/services", "*.js", true)
                .IncludeDirectory("~/App_JS/modules", "*.js", true)
                .IncludeDirectory("~/App_JS/controllers", "*.js", true)
                .IncludeDirectory("~/App_JS/bindings", "*.js", true);
        }
    }

    public class CssBundle : Bundle
    {
        public CssBundle(string virtualPath)
            : base(virtualPath, new CssMinify())
        {
            setPaths();
        }

        public CssBundle()
            : base(BundleConfig.GetCssVirtualPath(), new CssMinify())
        {
            setPaths();
        }

        private void setPaths()
        {
            var pathCollections = BundleConfig.GetCssFiles();
            var cssPath = pathCollections[0];

            Include(cssPath, new CssRewriteUrlTransform());
        }
    }
}