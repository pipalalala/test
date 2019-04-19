using System.Web.Optimization;

namespace Lab08.MVC
{
    public class BundleConfig
    {
        protected BundleConfig()
        { }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Styles").Include(
                        "~/Content/Styles/Books.css",
                        "~/Content/Styles/Delete.css",
                        "~/Content/Styles/Contacts.css",
                        "~/Content/Styles/Home.css",
                        "~/Content/Styles/Editor.css",
                        "~/Content/Styles/Layout.css",
                        "~/Content/Styles/UserIndex.css",
                        "~/Content/Styles/UserLogin.css",
                        "~/Content/Styles/UserRegister.css"));
        }
    }
}
