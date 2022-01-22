using Android.Widget;
using AndroidX.Annotations;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary>
    /// Kept for fun
    /// </summary>
    class ProgressButtonUtils {
        /// <summary>
        /// Shows progress on the button with defined params.<br/>
        /// If params are not defined uses the default one.<br/><br/>
        /// Example:<br/>
        /// <c>ProgressButtonUtils.ShowProgress(button, new ProgressParams());</c><br/><br/>
        /// If you want to continue using your button after showing the progress,<br/>
        /// please hide the progress and clean up resources by calling:<br/>
        /// <see cref="HideProgress(TextView, string)"/><br/>
        /// <seealso cref="HideProgress(TextView, int)"/>
        /// </summary>
        /// <param name="textView">Button to show progress on</param>
        /// <param name="progressParams">The ProgressParams that will be used for this progress</param>
        public static void ShowProgress(TextView textView, ProgressParams progressParams) => textView.ShowProgress(progressParams);
        /// <param name="textView">Button to check if progress is active on</param>
        /// <returns>true if progress is currently showing and false if not</returns>
        public static bool IsProgressActive(TextView textView) => textView.IsProgressActive();
        /// <summary>
        /// Hides the progress and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide progress on</param>
        /// <param name="newText">String value to show on the button after hiding the progress</param>
        public static void HideProgress(TextView textView, string newText) => textView.HideProgress(newText);
        /// <summary>
        /// Hides the progress and cleans up internal references<br/>
        /// This method is required to call if you want to continue using your button
        /// </summary>
        /// <param name="textView">Button to hide progress on</param>
        /// <param name="newTextRes">String value to show on the button after hiding the progress</param>
        public static void HideProgress(TextView textView, [StringRes] int newTextRes) => textView.HideProgress(newTextRes);
    }
}