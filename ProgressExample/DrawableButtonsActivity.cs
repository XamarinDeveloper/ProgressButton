using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using Google.Android.Material.Button;
using Ir.XamarinDev.Android.ProgressButton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressExample {
    [Activity(Label = "DrawableButtonsActivity")]
    public class DrawableButtonsActivity : AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_drawable_buttons);

            var buttonAnimatedDrawable = FindViewById<MaterialButton>(Resource.Id.buttonAnimatedDrawable);
            var buttonProgressMixed = FindViewById<MaterialButton>(Resource.Id.buttonProgressMixed);

            this.BindProgressButton(buttonAnimatedDrawable);
            this.BindProgressButton(buttonProgressMixed);

            buttonAnimatedDrawable.AttachTextChangeAnimator();
            buttonProgressMixed.AttachTextChangeAnimator();

            buttonAnimatedDrawable.Click += ShowAnimatedDrawable;
            buttonProgressMixed.Click += ShowMixed;
        }
        private void ShowAnimatedDrawable(object sender, EventArgs e) {
            var button = sender as Button;
            var animatedDrawable = ContextCompat.GetDrawable(this, Resource.Drawable.animated_check);
            // Defined bounds are required for your drawable
            var drawableSize = Resources.GetDimensionPixelSize(Resource.Dimension.doneSize);
            animatedDrawable.Bounds = new Rect(0, 0, drawableSize, drawableSize);

            button.Enabled = false;

            button.ShowDrawable(animatedDrawable, (drawableParams) => {
                drawableParams.ButtonTextRes = Resource.String.saved;
                drawableParams.TextMarginRes = Resource.Dimension.drawableTextMargin;
            });
            
            new Handler(Looper.MainLooper).PostDelayed(() => {
                button.Enabled = true;
            
                button.HideDrawable(Resource.String.animatedDrawable);
            }, 3000);
        }

        private void ShowMixed(object sender, EventArgs e) {
            var button = sender as Button;
            var animatedDrawable = ContextCompat.GetDrawable(this, Resource.Drawable.animated_check);
            // Defined bounds are required for your drawable
            var drawableSize = Resources.GetDimensionPixelSize(Resource.Dimension.doneSize);
            animatedDrawable.Bounds = new Rect(0, 0, drawableSize, drawableSize);

            button.Enabled = false;

            button.ShowProgress((progressParams) => {
                progressParams.ButtonTextRes = Resource.String.loading;
                progressParams.ProgressColor = Color.White;
            });

            new Handler(Looper.MainLooper).PostDelayed(() => {
                button.Enabled = true;

                button.ShowDrawable(animatedDrawable, (drawableParams) => {
                    drawableParams.ButtonTextRes = Resource.String.saved;
                });

                new Handler(Looper.MainLooper).PostDelayed(() => {
                    button.HideDrawable(Resource.String.mixedBehaviour);
                }, 2000);
            }, 3000);
        }

    }
}