using AndroidX.Annotations;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary>
    /// Used to customize progress drawable<br/>
    /// The final progress drawable size will be <c>(radius + stroke) * 2</c>
    /// </summary>
    public class ProgressParams : DrawableParams {
        /// <summary>
        /// Dimension resource for the progress radius
        /// </summary>
        [DimenRes]
        public int? ProgressRadiusRes = null;

        /// <summary>
        /// Integer value for the progress radius in pixels
        /// </summary>
        public int ProgressRadiusPx = DrawableButton.Default;

        /// <summary>
        /// Dimension resource for the progress stroke
        /// </summary>
        [DimenRes]
        public int? ProgressStrokeRes = null;

        /// <summary>
        /// Integer value for the progress stroke in pixels
        /// </summary>
        public int ProgressStrokePx = DrawableButton.Default;

        /// <summary>
        /// Color value for the progress
        /// </summary>
        [ColorInt]
        public int? ProgressColor = null;

        /// <summary>
        /// Color resource for the progress
        /// </summary>
        [ColorRes]
        public int? ProgressColorRes = null;

        /// <summary>
        /// List of color values for the progress
        /// </summary>
        public int[] ProgressColors = null;
    }
}