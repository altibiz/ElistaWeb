using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace ElistTheme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            //_manifest
            //    .DefineStyle("ElistTheme-bootstrap")
            //    .SetUrl("~/ElistTheme/vendor/css/bootstrap.min.css", "~/ElistTheme/vendor/css/bootstrap.css")
            //    .SetVersion("1.0.0");
            //// fonts / stylesheet.css
            _manifest
                .DefineStyle("ElistTheme-FontStyle")
                .SetUrl("~/ElistTheme/fonts/stylesheet.css")
                .SetVersion("1.0.0");
            _manifest
                .DefineStyle("ElistTheme-Swiper")
                .SetUrl("~/ElistTheme/vendor/swiper/swiper-bundle.min.css", "~/ElistTheme/vendor/swiper/swiper-bundle.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("ElistTheme-Aos")
                .SetUrl("~/ElistTheme/vendor/aos/aos.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistTheme-MalihuScrollbar")
                .SetUrl("~/ElistTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistTheme-Default")
                .SetUrl("~/ElistTheme/css/style.default.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistTheme-Custom")
                .SetUrl("~/ElistTheme/css/custom.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistTheme-FontAwesome")
                .SetUrl("https://use.fontawesome.com/releases/v5.10.0/css/all.css")
                .SetVersion("1.0.0");





            _manifest
                .DefineScript("ElistTheme-Bootstrap")
                .SetCdn("~/ElistTheme/vendor/bootstrap/js/bootstrap.bundle.min.js", "~/ElistTheme/vendor/bootstrap/js/bootstrap.bundle.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-Jquery")
                .SetCdn("~/ElistTheme/vendor/jquery/jquery.min.js", "~/ElistTheme/vendor/jquery/jquery.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-Swiper")
                .SetCdn("~/ElistTheme/vendor/swiper/swiper-bundle.min.js", "~/ElistTheme/vendor/swiper/swiper-bundle.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-BootstrapSelect")
                .SetCdn("~/ElistTheme/vendor/bootstrap-select/js/bootstrap-select.min.js", "~/ElistTheme/vendor/bootstrap-select/js/bootstrap-select.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-Aos")
                .SetCdn("~/ElistTheme/vendor/aos/aos.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-mCustomScrollbar")
                .SetCdn("~/ElistTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js", "~/ElistTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-CustomScrollbar")
                .SetCdn("~/ElistTheme/js/custom-scrollbar-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-PolyFills")
                .SetCdn("~/ElistTheme/vendor/smooth-scroll/smooth-scroll.polyfills.min.js", "~/ElistTheme/vendor/smooth-scroll/smooth-scroll.polyfills.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-Ofi")
                .SetCdn("~/ElistTheme/vendor/object-fit-images/ofi.min.js", "~/ElistTheme/vendor/object-fit-images/ofi.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-Countdown")
                .SetCdn("~/ElistTheme/js/countdown.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-Sliders")
                .SetCdn("~/ElistTheme/js/sliders-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistTheme-Theme")
                .SetCdn("~/ElistTheme/js/theme.js")
                .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
