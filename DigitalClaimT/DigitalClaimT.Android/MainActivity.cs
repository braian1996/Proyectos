using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.Media;
using Android.Support.V4.App;
using Android;
using Android.Util;
using Android.Support.Design.Widget;
using Android.Graphics;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Content;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "DigitalClaimT", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //EditText t1;
        //EditText t2;
        //ImageView imgLogi;
       // private ProgressDialog progressDialog;

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;


                base.OnCreate(bundle);

                //await CrossMedia.Current.Initialize();

                CrossCurrentActivity.Current.Activity = this;

                global::Xamarin.Forms.Forms.Init(this, bundle);

                Xamarin.FormsMaps.Init(this, bundle);
                // LoadApplication(new App());
                //SetContentView(Resource.Layout.login);

                //progressDialog = new ProgressDialog(this);

                //progressDialog.SetMessage("DigitalClaim se esta iniciando, Por favor espere!!");
                ////muestras el ProgressDialog
                //progressDialog.Indeterminate = true;
                //progressDialog.SetCancelable(false);
                //progressDialog.Show();


                StartActivity(typeof(ActivityLogin));
                //Finish();

                //new Handler().PostDelayed(() =>
                //{
                //    progressDialog.Dismiss();
                //}, 4000);

                
            }
            catch (Exception ex)
            {

               
            }
       
        }
        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

