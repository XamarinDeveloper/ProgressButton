using Android.Animation;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.Lifecycle;
using System;
using System.Collections.Generic;
using static Ir.XamarinDev.Android.ProgressButton.DrawableButtonExtensions;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary></summary>
    public static class ProgressButtonHolder {

        internal static Dictionary<TextView, TextChangeAnimatorParams> AttachedViews = new Dictionary<TextView, TextChangeAnimatorParams>();
        internal static Dictionary<TextView, List<Animator>> ActiveAnimations = new Dictionary<TextView, List<Animator>>();
        internal static Dictionary<TextView, DrawableViewData> ActiveViews = new Dictionary<TextView, DrawableViewData>();

        /// <summary>
        /// Binds your buttons to component lifecycle for the correct data recycling<br/>
        /// This method is required for all buttons that show progress/drawable
        /// </summary>
        /// <param name="lifecycleOwner">lifecycle owner to which to bind to (eg. Activity, Fragment or other)</param>
        /// <param name="button">button instance to bind</param>
        public static void BindProgressButton(this ILifecycleOwner lifecycleOwner, TextView button) {
            lifecycleOwner.Lifecycle.AddObserver(new ProgressButtonLifecycleHandler(new WeakReference<TextView>(button)));
        }
        /// <summary>
        /// Stops the animation and cleans up the drawable if it's active
        /// </summary>
        /// <param name="textView">Button to clean up the drawable of</param>
        public static void CleanUpDrawable(this TextView textView) {
            if (ActiveViews.ContainsKey(textView)) {
                var drawable = ActiveViews[textView]?.Drawable;
                if (drawable != null) {
                    if (textView is IAnimatable animatable) {
                        animatable.Stop();
                    }
                    drawable.Callback = null;
                }
                ActiveViews.Remove(textView);
            }
        }

        private class ProgressButtonLifecycleHandler : Java.Lang.Object, ILifecycleEventObserver {
            private readonly WeakReference<TextView> textView;

            public ProgressButtonLifecycleHandler(WeakReference<TextView> textView) {
                this.textView = textView;
            }

            public void OnStateChanged(ILifecycleOwner source, Lifecycle.Event @event) {
                if (@event == Lifecycle.Event.OnDestroy) {
                    if (textView.TryGetTarget(out var it)) {
                        it.CancelAnimations();
                        it.CleanUpDrawable();
                        it.RemoveTextAnimationAttachViewListener();
                        it.RemoveDrawableAttachViewListener();
                        AttachedViews.Remove(it);
                    }
                }
            }
        }

        internal static void AddTextAnimationAttachViewListener(this TextView textView) {
            textView.AddOnAttachStateChangeListener(new TextAnimationsAttachListener());
        }

        internal static void RemoveTextAnimationAttachViewListener(this TextView textView) {
            textView.RemoveOnAttachStateChangeListener(new TextAnimationsAttachListener());
        }

        internal static void AddDrawableAttachViewListener(this TextView textView) {
            textView.AddOnAttachStateChangeListener(new DrawablesAttachListener());
        }

        internal static void RemoveDrawableAttachViewListener(this TextView textView) {
            textView.RemoveOnAttachStateChangeListener(new DrawablesAttachListener());
        }

        private class TextAnimationsAttachListener : Java.Lang.Object, View.IOnAttachStateChangeListener {
            public void OnViewAttachedToWindow(View attachedView) {
            }

            public void OnViewDetachedFromWindow(View detachedView) {
                if (AttachedViews.ContainsKey((TextView)detachedView)) {
                    (detachedView as TextView).CancelAnimations();
                }
            }
        }

        private class DrawablesAttachListener : Java.Lang.Object, View.IOnAttachStateChangeListener {
            public void OnViewAttachedToWindow(View attachedView) {
                if (ActiveViews.ContainsKey((TextView)attachedView)) {
                    var drawable = ActiveViews[(TextView)attachedView]?.Drawable;
                    if (drawable != null) {
                        if (drawable is IAnimatable animatable) {
                            animatable.Start();
                        }
                    }
                }
            }

            public void OnViewDetachedFromWindow(View detachedView) {
                if (ActiveViews.ContainsKey((TextView)detachedView)) {
                    var drawable = ActiveViews[(TextView)detachedView]?.Drawable;
                    if (drawable != null) {
                        if (drawable is IAnimatable animatable) {
                            animatable.Stop();
                        }
                    }
                }
            }
        }
    }
}