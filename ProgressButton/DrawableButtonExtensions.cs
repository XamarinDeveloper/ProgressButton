using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Util;
using Android.Widget;
using AndroidX.Annotations;
using AndroidX.AppCompat.Text;
using AndroidX.Core.Content;
using AndroidX.SwipeRefreshLayout.Widget;
using Java.Lang;
using System;
using System.Linq;
using static Ir.XamarinDev.Android.ProgressButton.ProgressButtonHolder;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary></summary>
    public static class DrawableButtonExtensions {
        /// <summary>
        /// Shows progress on the button with defined params.<br/>
        /// If params are not defined uses the default one.<br/><br/>
        /// Example:<br/>
        /// <c>button.ShowProgress((progressParams) => { progressParams.ButtonText = "Loading"; progressParams.ProgressColor = Color.White });</c><br/><br/>
        /// If you want to continue using your button after showing the progress,<br/>
        /// please hide the progress and clean up resources by calling:<br/>
        /// <see cref="HideProgress(TextView, string)"/><br/>
        /// <seealso cref="HideProgress(TextView,int)"/>
        /// </summary>
        /// <param name="textView">Button to show progress on</param>
        /// <param name="action">Action to apply on the ProgressParams that will be used for this progress</param>
        public static void ShowProgress(this TextView textView, Action<ProgressParams> action = null) {
            var paramValues = new ProgressParams();
            action?.Invoke(paramValues);
            textView.ShowProgress(paramValues);
        }
        /// <summary>
        /// Shows your animated drawable on the button with defined params.<br/>
        /// Important: drawable bounds should be defined already (eg. drawable.Bounds)<br/>
        /// If params are not defined uses the default one.<br/><br/>
        /// Example:<br/>
        /// <c>button.ShowDrawable(yourDrawable, (drawableParams) => { drawableParams.ButtonText = "Done"; });</c><br/><br/>
        /// If you want to continue using your button after showing the drawable,<br/>
        /// please hide the drawable and clean up resources by calling:<br/>
        /// <see cref="HideDrawable(TextView, string)"/><br/>
        /// <seealso cref="HideDrawable(TextView,int)"/>
        /// </summary>
        /// <param name="textView">Button to show drawable on</param>
        /// <param name="drawable">Your animated drawable, Will be played automatically</param>
        /// <param name="action">Action to apply on the DrawableParams that will be used for this drawable</param>
        public static void ShowDrawable(this TextView textView, Drawable drawable, Action<DrawableParams> action = null) {
            var paramValues = new DrawableParams();
            action?.Invoke(paramValues);
            textView.ShowDrawable(drawable, paramValues);
        }
        /// <param name="textView">Button to check if progress is active on</param>
        /// <returns>true if progress is currently showing and false if not</returns>
        public static bool IsProgressActive(this TextView textView) => textView.IsDrawableActive();
        /// <param name="textView">Button to check if drawable is active on</param>
        /// <returns>true if drawable is currently showing and false if not</returns>
        public static bool IsDrawableActive(this TextView textView) => ActiveViews.ContainsKey(textView);

        /// <summary>
        /// Hides the progress and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide progress on</param>
        /// <param name="newText">String value to show on the button after hiding the progress</param>
        public static void HideProgress(this TextView textView, string newText = null) => textView.HideDrawable(newText);
        /// <summary>
        /// Hides the progress and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide progress on</param>
        /// <param name="newTextRes">String resource to show on the button after hiding the progress</param>
        public static void HideProgress(this TextView textView, [StringRes] int newTextRes) => textView.HideDrawable(newTextRes);
        /// <summary>
        /// Hides the drawable and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide drawable on</param>
        /// <param name="newText">String value to show on the button after hiding the drawable</param>
        public static void HideDrawable(this TextView textView, string newText = null) {
            textView.CleanUpDrawable();
            if (textView.IsAnimatorAttached()) {
                textView.AnimateTextChange(newText);
            }
            else {
                textView.Text = newText;
            }
        }
        /// <summary>
        /// Hides the drawable and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide drawable on</param>
        /// <param name="newTextRes">String value to show on the button after hiding the drawable</param>
        public static void HideDrawable(this TextView textView, [StringRes] int newTextRes) => textView.HideDrawable(textView.Context.GetString(newTextRes));

        internal static void ShowProgress(this TextView textView, ProgressParams progressParams) {
            var res = textView.Context.Resources;
            var progressStrokeValue = progressParams.ProgressStrokePx;
            if (progressParams.ProgressStrokeRes != null) {
                progressStrokeValue = res.GetDimensionPixelSize((int)progressParams.ProgressStrokeRes);
            }
            var progressRadiusValue = progressParams.ProgressRadiusPx;
            if (progressParams.ProgressRadiusRes != null) {
                progressStrokeValue = res.GetDimensionPixelSize((int)progressParams.ProgressRadiusRes);
            }
            var colors = new int[] { };
            if (progressParams.ProgressColorRes != null) {
                colors = new int[] { ContextCompat.GetColor(textView.Context, (int)progressParams.ProgressColorRes) };
            }
            if (progressParams.ProgressColor != null) {
                colors = new int[] { (int)progressParams.ProgressColor };
            }
            if (progressParams.ProgressColors != null) {
                colors = progressParams.ProgressColors;
            }
            var progressDrawable = new GenerateProgressDrawable(textView.Context, colors, progressRadiusValue, progressStrokeValue);
            textView.ShowDrawable(progressDrawable, progressParams);

        }
        internal static void ShowDrawable(this TextView textView, Drawable drawable, DrawableParams @params) {
            var res = textView.Context.Resources;
            var buttonTextValue = @params.ButtonText;
            if (@params.ButtonTextRes != null) {
                buttonTextValue = res.GetString((int)@params.ButtonTextRes);
            }
            var textMarginValue = @params.TextMarginPx;
            if (@params.TextMarginRes != null) {
                textMarginValue = res.GetDimensionPixelSize((int)@params.TextMarginRes);
            }
            textView.ShowDrawable(drawable, buttonTextValue, @params.Gravity, textMarginValue);
        }
        private static void ShowDrawable(this TextView textView, Drawable drawable, string text, DrawableButton.Gravity gravity, int textMarginPx) {
            if (textView.IsDrawableActive()) {
                textView.CleanUpDrawable();
            }
            // Workaround to check if textAllCaps==true on any android api version
            if (textView.TransformationMethod?.GetType()?.Name == "Android.Text.Method.AllCapsTransformationMethod" || textView.TransformationMethod is AllCapsTransformationMethod) {
                textView.TransformationMethod = new AllCapsSpannedTransformationMethod(textView.Context);
            }

            var drawableMargin = textMarginPx == DrawableButton.Default ? textView.Context.DpToPixels(DefaultDrawableMarginDp) : textMarginPx;
            var animatorAttached = textView.IsAnimatorAttached();
            var newText = GetDrawableSpannable(drawable, text, gravity, drawableMargin, animatorAttached);
            if (animatorAttached) {
                textView.AnimateTextChange(newText);
            }
            else {
                textView.TextFormatted = newText;
            }

            textView.AddDrawableAttachViewListener();
            SetupDrawableCallback(textView, drawable);
            if (drawable is IAnimatable animatable) {
                animatable.Start();
            }
        }

        private static void SetupDrawableCallback(this TextView textView, Drawable drawable) {
            var callback = new InvalidatorCallback(textView);
            ActiveViews[textView] = new DrawableViewData(drawable, callback);
            drawable.Callback = callback;
        }

        private class InvalidatorCallback : Java.Lang.Object, Drawable.ICallback {
            private readonly TextView textView;

            public InvalidatorCallback(TextView textView) {
                this.textView = textView;
            }

            public void InvalidateDrawable(Drawable who) {
                textView.Invalidate();
            }

            public void ScheduleDrawable(Drawable who, Java.Lang.IRunnable what, long when) {
            }

            public void UnscheduleDrawable(Drawable who, Java.Lang.IRunnable what) {
            }
        }

        private class GenerateProgressDrawable : CircularProgressDrawable {
            public GenerateProgressDrawable(Context context, int[] progressColors, int progressRadiusPx, int progressStrokePx) : base(context) {
                SetStyle(Default);
                if (progressColors.Count() != 0) {
                    SetColorSchemeColors(progressColors);
                }
                if (progressRadiusPx != DrawableButton.Default) {
                    CenterRadius = progressRadiusPx;
                }
                if (progressStrokePx != DrawableButton.Default) {
                    StrokeWidth = progressStrokePx;
                }
                var size = (int)(CenterRadius + StrokeWidth) * 2;
                SetBounds(0, 0, size, size);
            }
        }

        private static SpannableString GetDrawableSpannable(Drawable drawable, string text, DrawableButton.Gravity gravity, int drawableMarginPx, bool useTextAlpha) {
            var drawableSpan = new DrawableSpan(drawable, useTextAlpha: useTextAlpha);
            if (gravity == DrawableButton.Gravity.TextStart) {
                drawableSpan.PaddingEnd = drawableMarginPx;
                var newText = new SpannableString($" {text ?? ""}");
                newText.SetSpan(drawableSpan, 0, 1, SpanTypes.ExclusiveExclusive);
                return newText;
            }
            if (gravity == DrawableButton.Gravity.TextEnd) {
                drawableSpan.PaddingStart = drawableMarginPx;
                var newText = new SpannableString($"{text ?? ""} ");
                newText.SetSpan(drawableSpan, newText.Length() - 1, newText.Length(), SpanTypes.ExclusiveExclusive);
                return newText;
            }
            if (gravity == DrawableButton.Gravity.Center) {
                var newText = new SpannableString(" ");
                newText.SetSpan(drawableSpan, 0, 1, SpanTypes.ExclusiveExclusive);
                return newText;
            }
            throw new IllegalArgumentException("Please set the correct gravity");
        }

        internal class DrawableViewData {
            public Drawable Drawable;
            public Drawable.ICallback Callback;
            public DrawableViewData(Drawable drawable, Drawable.ICallback callback) {
                Drawable = drawable;
                Callback = callback;
            }
        }

        private const float DefaultDrawableMarginDp = 10f;
        private static int DpToPixels(this Context context, float dpValue) => (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dpValue, context.Resources.DisplayMetrics);
    }
}