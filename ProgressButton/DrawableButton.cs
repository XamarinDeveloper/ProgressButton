namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary></summary>
    public class DrawableButton {
        /// <summary>
        /// Defines the default value for a param if it's possible
        /// </summary>
        public const int Default = -1;

        /// <summary></summary>
        public enum Gravity {
            /// <summary>
            /// Show drawable at the start of the text
            /// </summary>
            TextStart = 0,
            /// <summary>
            /// Show drawable at the end of the text
            /// </summary>
            TextEnd = 1,
            /// <summary>
            /// Show drawable on the center, Use only if your text is null or empty
            /// </summary>
            Center = 2
        }
    }
}