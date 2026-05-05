using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace IcfTheme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineStyle("IcfTheme-FontStyle")
                .SetUrl("~/IcfTheme/fonts/stylesheet.css")
                .SetVersion("1.0.0");
            _manifest
                .DefineStyle("IcfTheme-Swiper")
                .SetUrl("~/IcfTheme/vendor/swiper/swiper-bundle.min.css", "~/IcfTheme/vendor/swiper/swiper-bundle.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("IcfTheme-Aos")
                .SetUrl("~/IcfTheme/vendor/aos/aos.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("IcfTheme-MalihuScrollbar")
                .SetUrl("~/IcfTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("IcfTheme-Default")
                .SetUrl("~/IcfTheme/css/style.default.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("IcfTheme-Custom")
                .SetUrl("~/IcfTheme/css/custom.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("IcfTheme-Icf")
                .SetUrl("~/IcfTheme/css/icf.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("IcfTheme-FontAwesome")
                .SetUrl("https://use.fontawesome.com/releases/v5.10.0/css/all.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-Bootstrap")
                .SetCdn("~/IcfTheme/vendor/bootstrap/js/bootstrap.bundle.min.js", "~/IcfTheme/vendor/bootstrap/js/bootstrap.bundle.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-Jquery")
                .SetCdn("~/IcfTheme/vendor/jquery/jquery.min.js", "~/IcfTheme/vendor/jquery/jquery.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-Swiper")
                .SetCdn("~/IcfTheme/vendor/swiper/swiper-bundle.min.js", "~/IcfTheme/vendor/swiper/swiper-bundle.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-BootstrapSelect")
                .SetCdn("~/IcfTheme/vendor/bootstrap-select/js/bootstrap-select.min.js", "~/IcfTheme/vendor/bootstrap-select/js/bootstrap-select.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-Aos")
                .SetCdn("~/IcfTheme/vendor/aos/aos.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-mCustomScrollbar")
                .SetCdn("~/IcfTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js", "~/IcfTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-CustomScrollbar")
                .SetCdn("~/IcfTheme/js/custom-scrollbar-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-PolyFills")
                .SetCdn("~/IcfTheme/vendor/smooth-scroll/smooth-scroll.polyfills.min.js", "~/IcfTheme/vendor/smooth-scroll/smooth-scroll.polyfills.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-Ofi")
                .SetCdn("~/IcfTheme/vendor/object-fit-images/ofi.min.js", "~/IcfTheme/vendor/object-fit-images/ofi.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-Sliders")
                .SetDependencies("IcfTheme-Swiper")
                .SetCdn("~/IcfTheme/js/sliders-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("IcfTheme-Theme")
                .SetCdn("~/IcfTheme/js/theme.js")
                .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
