using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;

namespace ProgressExample {
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var openProgressButtons = FindViewById<MaterialButton>(Resource.Id.openProgressButtons);
            var openDrawableButtons = FindViewById<MaterialButton>(Resource.Id.openDrawableButtons);
            var openRecyclerView = FindViewById<MaterialButton>(Resource.Id.openRecyclerView);

            openProgressButtons.Click += delegate {
                StartActivity(typeof(ProgressButtonsActivity));
            };

            openDrawableButtons.Click += delegate {
                StartActivity(typeof(DrawableButtonsActivity));
            };

            openRecyclerView.Click += delegate {
                StartActivity(typeof(RecyclerViewActivity));
            };
        }
    }
}