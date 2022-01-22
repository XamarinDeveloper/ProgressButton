using Android.Graphics.Drawables;
using Android.Widget;
using AndroidX.Annotations;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary>
    /// Kept for fun
    /// </summary>
    class DrawableButtonUtils {
        /// <summary>
        /// Shows your animated drawable on the button with defined params.<br/>
        /// Important: drawable bounds should be defined already (eg. drawable.Bounds)<br/>
        /// If params are not defined uses the default one.<br/><br/>
        /// Example:<br/>
        /// <c>DrawableButtonUtils.ShowDrawable(button, yourDrawable, new DrawableParams());</c><br/><br/>
        /// If you want to continue using your button after showing the drawable,<br/>
        /// please hide the drawable and clean up resources by calling:<br/>
        /// <see cref="HideDrawable(TextView, string)"/><br/>
        /// <seealso cref="HideDrawable(TextView, int)"/>
        /// </summary>
        /// <param name="textView">Button to show drawable on</param>
        /// <param name="drawable">Your animated drawable, Will be played automatically</param>
        /// <param name="drawableParams">The DrawableParams that will be used for this drawable</param>
        public static void ShowDrawable(TextView textView, Drawable drawable, DrawableParams drawableParams) => textView.ShowDrawable(drawable, drawableParams);
        /// <param name="textView">Button to check if drawable is active on</param>
        /// <returns>true if drawable is currently showing and false if not</returns>
        public static bool IsDrawableActive(TextView textView) => textView.IsDrawableActive();
        /// <summary>
        /// Hides the drawable and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide drawable on</param>
        /// <param name="newText">String value to show on the button after hiding the drawable</param>
        public static void HideDrawable(TextView textView, string newText) => textView.HideDrawable(newText);
        /// <summary>
        /// Hides the drawable and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide drawable on</param>
        /// <param name="newTextRes">String value to show on the button after hiding the drawable</param>
        public static void HideDrawable(TextView textView, [StringRes] int newTextRes) => textView.HideDrawable(newTextRes);
    }
}