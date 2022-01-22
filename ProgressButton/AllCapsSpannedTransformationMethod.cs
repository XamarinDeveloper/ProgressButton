using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Java.Lang;
using Java.Util;

namespace Ir.XamarinDev.Android.ProgressButton {
    internal class AllCapsSpannedTransformationMethod : Java.Lang.Object, ITransformationMethod {
        private readonly Context context;
        private Locale Locale => context.Resources.Configuration.Locales.Get(0);
        public AllCapsSpannedTransformationMethod(Context context) {
            this.context = context;
        }

        public ICharSequence GetTransformationFormatted(ICharSequence source, View view) {
            if (source == null) {
                return null;
            }
            var upperCaseText = new Java.Lang.String(source.ToString()).ToUpperCase(Locale ?? Locale.GetDefault(Locale.Category.Display));
            if (source is ISpanned) {
                var spannable = new SpannableString(upperCaseText);
                TextUtils.CopySpansFrom((ISpannable)source, 0, source.Length(), null, spannable, 0);
                return spannable;
            }
            else {
                return new Java.Lang.String(upperCaseText);
            }
        }

        public void OnFocusChanged(View view, ICharSequence sourceText, bool focused, [GeneratedEnum] FocusSearchDirection direction, Rect previouslyFocusedRect) { }
    }
}
