using Android.App;
using Feedback.Core.ViewModels.Places;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Feedback.Droid.Views.Places
{
    [Activity(Label = "@string/app_name"
            , Icon = "@drawable/icon"
            , Theme = "@style/MainTheme")]
    public class PlacesActivity : MvxAppCompatActivity<IPlacesViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.PlacesView);
        }
    }
}
