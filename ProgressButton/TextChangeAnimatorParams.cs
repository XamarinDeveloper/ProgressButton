using Android.Content.Res;
using Android.Widget;
using AndroidX.Annotations;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary>
    /// Used to customize progress/drawable button show/hiding animations<br/>
    /// <see cref="ButtonTextAnimatorExtensions.AttachTextChangeAnimator(TextView, TextChangeAnimatorParams)"/><br/>
    /// <seealso cref="ButtonTextAnimatorExtensions.AttachTextChangeAnimator(TextView, System.Action{TextChangeAnimatorParams})"/>
    /// </summary>
    public class TextChangeAnimatorParams {
        /// <summary>
        /// Fade in/out using current Color/ColorStateList
        /// </summary>
        public bool UseCurrentTextColor = true;

        /// <summary>
        /// Color value for fade in/out
        /// </summary>
        [ColorInt]
        public int TextColor = 0;

        /// <summary>
        /// ColorStateList for fade in/out
        /// </summary>
        public ColorStateList TextColorList = null;

        /// <summary>
        /// Color resource for fade in/out
        /// </summary>
        [ColorRes]
        public int? TextColorRes = null;

        /// <summary>
        /// Fade in animation time in milliseconds
        /// </summary>
        public long FadeInMills = 150L;

        /// <summary>
        /// Fade out animation time in milliseconds
        /// </summary>
        public long FadeOutMills = 150L;
    }
}