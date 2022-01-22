using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text.Style;
using Math = System.Math;

namespace Ir.XamarinDev.Android.ProgressButton {
    internal class DrawableSpan : ImageSpan {
        internal int PaddingStart { get; set; }
        internal int PaddingEnd { get; set; }
        internal bool UseTextAlpha { get; set; }

        public DrawableSpan(Drawable drawable, int paddingStart = 0, int paddingEnd = 0, bool useTextAlpha = false) : base(drawable) {
            PaddingStart = paddingStart;
            PaddingEnd = paddingEnd;
            UseTextAlpha = useTextAlpha;
        }

        public override int GetSize(Paint paint, Java.Lang.ICharSequence text, int start, int end, Paint.FontMetricsInt fm) {
            var rect = Drawable.Bounds;
            var fontMetrics = paint.GetFontMetricsInt();

            var lineHeight = fontMetrics.Bottom - fontMetrics.Top;
            var drHeight = Math.Max(lineHeight, rect.Bottom - rect.Top);
            var centerY = fontMetrics.Top + lineHeight / 2;
            if (fm != null) {
                fm.Ascent = centerY - drHeight / 2;
                fm.Descent = centerY + drHeight / 2;
                fm.Top = fm.Ascent;
                fm.Bottom = fm.Descent;
            }
            return rect.Width() + PaddingStart + PaddingEnd;
        }
        public override void Draw(Canvas canvas, Java.Lang.ICharSequence text, int start, int end, float x, int top, int y, int bottom, Paint paint) {
            canvas.Save();
            var fontMetrics = paint.GetFontMetricsInt();
            var lineHeight = fontMetrics.Descent - fontMetrics.Ascent;

            var centerY = y + fontMetrics.Descent - lineHeight / 2;
            var transY = centerY - Drawable.Bounds.Height() / 2;
            canvas.Translate(x + PaddingStart, transY);

            if (UseTextAlpha) {
                var colorAlpha = Color.GetAlphaComponent(paint.Color);
                Drawable.Alpha = colorAlpha;
            }

            Drawable.Draw(canvas);
            canvas.Restore();
        }
    }
}