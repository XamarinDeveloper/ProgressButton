using AndroidX.Annotations;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary>
    /// Used to customize progress drawable or other animated drawables
    /// </summary>
    public class DrawableParams {
        /// <summary>
        /// String resource to show along with progress/drawable
        /// </summary>
        [StringRes]
        public int? ButtonTextRes = null;

        /// <summary>
        /// String value to show along with progress/drawable
        /// </summary>
        public string ButtonText = null;

        /// <summary>
        /// progress/drawable gravity.<br/><br/>
        /// Default value: 
        /// <see cref="DrawableButton.Gravity.TextEnd"/>
        /// </summary>
        public DrawableButton.Gravity Gravity = DrawableButton.Gravity.TextEnd;

        /// <summary>
        /// Dimension resource for the margin between text and progress/drawable
        /// </summary>
        [DimenRes]
        public int? TextMarginRes = null;

        /// <summary>
        /// Integer value for the margin between text and progress/drawable in pixels
        /// </summary>
        public int TextMarginPx = DrawableButton.Default;
    }
}