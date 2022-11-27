using Microsoft.Maui.LifecycleEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Maui.GoogleClient
{
    public static class AppHostBuilderExtensions
    {
        /// <summary>
        /// Automatically sets up lifecycle events and Maui Handlers
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static MauiAppBuilder UseGoogleLogin(this MauiAppBuilder builder)
        {
#if ANDROID
            GoogleClientManager.Initialize(Platform.CurrentActivity);
#elif IOS
            GoogleClientManager.Initialize();
#endif

            builder.ConfigureLifecycleEvents(lifecycle =>
            {
#if ANDROID
                lifecycle.AddAndroid(d =>
                {
                    d.OnActivityResult((activity, requestCode, resultCode, data) =>
                    {
                        GoogleClientManager.OnAuthCompleted(requestCode, resultCode, data);
                    });
                });
#elif IOS
                lifecycle.AddiOS(d =>
                {
                    d.OpenUrl((app, url, options) =>
                    {
                        return GoogleClientManager.OnOpenUrl(app, url, options);
                    });
                });
#endif

            });



            return builder;
        }

        /// <summary>
        /// Automatically sets up lifecycle events and maui handlers, with the additional option to have additional back press logic
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="backPressHandler"></param>
        /// <returns></returns>

    }
}
