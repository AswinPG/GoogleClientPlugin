#if ANDROID
using Android.Content.PM;
using Android.Content;
using Java.Security;
#endif

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
            //GoogleClientManager.Initialize(Platform.CurrentActivity);
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
                    d.OnCreate((activity, bundle) =>
                    {
                        GoogleClientManager.Initialize(Platform.CurrentActivity, null, "808874530445-7q8nubv1u67qpp97pku6ho2lllcdhvqj.apps.googleusercontent.com");
                        //GoogleClientManager.Initialize(Platform.CurrentActivity);
                        PrintHashKey(activity.BaseContext);
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
        /// 
#if ANDROID
        public static void PrintHashKey(Context pContext)
        {

            try
            {
                PackageInfo info = Android.App.Application.Context.PackageManager.GetPackageInfo(Android.App.Application.Context.PackageName, PackageInfoFlags.Signatures);
                foreach (var signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA");
                    md.Update(signature.ToByteArray());

                    System.Diagnostics.Debug.WriteLine(BitConverter.ToString(md.Digest()).Replace("-", ":"));
                }
            }
            catch (NoSuchAlgorithmException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

        }
#endif

    }
}
