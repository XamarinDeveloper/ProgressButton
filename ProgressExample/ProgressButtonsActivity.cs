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
    [Activity(Label = "ProgressButtonsActivity")]
    public class ProgressButtonsActivity : AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_progress_buttons);

            var buttonProgressRightText = FindViewById<MaterialButton>(Resource.Id.buttonProgressRightText);
            var buttonProgressLeftText = FindViewById<MaterialButton>(Resource.Id.buttonProgressLeftText);
            var buttonProgressCenter = FindViewById<MaterialButton>(Resource.Id.buttonProgressCenter);
            var buttonProgressCustomStyle = FindViewById<MaterialButton>(Resource.Id.buttonProgressCustomStyle); 

            this.BindProgressButton(buttonProgressRightText);
            this.BindProgressButton(buttonProgressLeftText);
            this.BindProgressButton(buttonProgressCenter);
            this.BindProgressButton(buttonProgressCustomStyle);

            buttonProgressRightText.AttachTextChangeAnimator();
            buttonProgressLeftText.AttachTextChangeAnimator();
            buttonProgressCenter.AttachTextChangeAnimator();
            buttonProgressCustomStyle.AttachTextChangeAnimator();

            buttonProgressRightText.Click += ShowProgressRight;
            buttonProgressLeftText.Click += ShowProgressLeft;
            buttonProgressCenter.Click += ShowProgressCenter;
            buttonProgressCustomStyle.Click += ShowProgressCustom;
        }

        private void ShowProgressRight(object sender, EventArgs e) {
            var button = sender as Button;

            button.Enabled = false;

            button.ShowProgress((progressParams) => {
                progressParams.ButtonTextRes = Resource.String.loading;
                progressParams.ProgressColor = Color.White;
            });

            new Handler(Looper.MainLooper).PostDelayed(() => {
                button.Enabled = true;

                button.HideProgress(Resource.String.progressRight);
            }, 3000);
        }

        private void ShowProgressLeft(object sender, EventArgs e) {
            var button = sender as Button;

            button.Enabled = false;

            button.ShowProgress((progressParams) => {
                progressParams.ButtonTextRes = Resource.String.loading;
                progressParams.ProgressColor = Color.White;
                progressParams.Gravity = DrawableButton.Gravity.TextStart;
            });

            new Handler(Looper.MainLooper).PostDelayed(() => {
                button.Enabled = true;

                button.HideProgress(Resource.String.progressLeft);
            }, 3000);
        }

        private void ShowProgressCenter(object sender, EventArgs e) {
            var button = sender as Button;

            button.Enabled = false;

            button.ShowProgress((progressParams) => {
                progressParams.ProgressColor = Color.White;
                progressParams.Gravity = DrawableButton.Gravity.Center;
            });

            new Handler(Looper.MainLooper).PostDelayed(() => {
                button.Enabled = true;

                button.HideProgress(Resource.String.progressCenter);
            }, 3000);
        }

        private void ShowProgressCustom(object sender, EventArgs e) {
            var button = sender as Button;

            button.Enabled = false;

            button.ShowProgress((progressParams) => {
                progressParams.ButtonTextRes = Resource.String.loading;
                progressParams.ProgressColors = new int[] { Color.White, Color.Magenta, Color.Green };
                progressParams.Gravity = DrawableButton.Gravity.TextEnd;
                progressParams.ProgressRadiusRes = Resource.Dimension.progressRadius;
                progressParams.ProgressStrokeRes = Resource.Dimension.progressStroke;
                progressParams.TextMarginRes = Resource.Dimension.textMarginStyled;
            });

            new Handler(Looper.MainLooper).PostDelayed(() => {
                button.Enabled = true;

                button.HideProgress(Resource.String.progressCustomStyle);
            }, 5000);
        }
    }
}