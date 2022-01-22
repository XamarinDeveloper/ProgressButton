using Android.Animation;
using Android.Content.Res;
using Android.Graphics;
using Android.Text;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.Core.Graphics;
using Java.Lang;
using System;
using System.Collections.Generic;
using static Android.Animation.Animator;
using static Ir.XamarinDev.Android.ProgressButton.ProgressButtonHolder;

namespace Ir.XamarinDev.Android.ProgressButton {
    /// <summary></summary>
    public static class ButtonTextAnimatorExtensions {
        /// <summary>
        /// Adds fade in/out animations on drawable/progress showing<br/><br/>
        /// Example:<br/>
        /// <c>button.AttachTextChangeAnimator((textChangeAnimatorParams) => { textChangeAnimatorParams.FadeInMills = 200; });</c>
        /// </summary>
        /// <param name="textView">Button to add animation to</param>
        /// <param name="action">Action to apply on the TextChangeAnimatorParams that will be used for these animations</param>
        public static void AttachTextChangeAnimator(this TextView textView, Action<TextChangeAnimatorParams> action = null) {
            var paramValues = new TextChangeAnimatorParams();
            action?.Invoke(paramValues);
            textView.AttachTextChangeAnimator(paramValues);
        }

#nullable enable
        /// <summary>
        /// Adds fade in/out animations on drawable/progress showing
        /// </summary>
        /// <param name="textView">TextView to add animation to</param>
        /// <param name="textChangeAnimatorParams">The TextChangeAnimatorParams that will be used for these animations</param>
        public static void AttachTextChangeAnimator(this TextView textView, TextChangeAnimatorParams? textChangeAnimatorParams) {
            var animParams = textChangeAnimatorParams ?? new TextChangeAnimatorParams();
            if (animParams.UseCurrentTextColor) {
                animParams.TextColorList = textView.TextColors;
            }
            else {
                if (animParams.TextColorRes != null) {
                    animParams.TextColor = ContextCompat.GetColor(textView.Context, (int)animParams.TextColorRes);
                }
            }
            textView.AddTextAnimationAttachViewListener();
            AttachedViews[textView] = textChangeAnimatorParams;
        }
#nullable disable

        /// <summary>
        /// Removes fade in/out animations on drawable/progress showing
        /// </summary>
        /// <param name="textView">TextView to remove animation from</param>
        public static void DetachTextChangeAnimator(this TextView textView) {
            if (AttachedViews.ContainsKey(textView)) {
                textView.CancelAnimations();
                AttachedViews.Remove(textView);
                textView.RemoveTextAnimationAttachViewListener();
            }
        }

        /// <summary>
        /// Checks if animations handler is currently active for the given button
        /// </summary>
        /// <param name="textView">TextView to remove animation from</param>
        public static bool IsAnimatorAttached(this TextView textView) {
            return AttachedViews.ContainsKey(textView);
        }
        internal static void AnimateTextChange(this TextView textView, string newText) {

            textView.AnimateTextChange(newText == null ? null : new SpannableString(newText));
        }
        internal static void AnimateTextChange(this TextView textView, SpannableString newText) {
            textView.CancelAnimations();
            var @params = AttachedViews[textView];
            var textColor = textView.GetAnimateTextColor();

            if (@params is null) {
                throw new NullPointerException();
            }

            var fadeInAnimator = ObjectAnimator.OfInt(textView, "textColor", ColorUtils.SetAlphaComponent(textColor, 0), textColor);
            fadeInAnimator.SetDuration(@params.FadeInMills);
            fadeInAnimator.SetEvaluator(new ArgbEvaluator());
            fadeInAnimator.AddListener(new FadeInAnimatorListener(textView));
            fadeInAnimator.Start();

            var fadeOutAnimator = ObjectAnimator.OfInt(textView, "textColor", textColor, ColorUtils.SetAlphaComponent(textColor, 0));
            fadeOutAnimator.SetDuration(@params.FadeOutMills);
            fadeOutAnimator.SetEvaluator(new ArgbEvaluator());
            fadeOutAnimator.AddListener(new FadeOutAnimatorListener(textView, newText, fadeInAnimator));
            fadeOutAnimator.Start();
        }

        private class FadeInAnimatorListener : Java.Lang.Object, IAnimatorListener {
            private readonly TextView textView;

            public FadeInAnimatorListener(TextView textView) {
                this.textView = textView;
            }

            public void OnAnimationCancel(Animator animation) {
                textView.ResetColor();
                textView.RemoveAnimator(animation);
            }

            public void OnAnimationEnd(Animator animation) {
                textView.RemoveAnimator(animation);
                textView.ResetColor();
            }

            public void OnAnimationRepeat(Animator animation) {
            }

            public void OnAnimationStart(Animator animation) {
                textView.AddAnimator(animation);
            }
        }

        private class FadeOutAnimatorListener : Java.Lang.Object, IAnimatorListener {
            private readonly TextView textView;
            private readonly SpannableString newText;
            private readonly ObjectAnimator fadeInAnimator;

            public FadeOutAnimatorListener(TextView textView, SpannableString newText, ObjectAnimator fadeInAnimator) {
                this.textView = textView;
                this.newText = newText;
                this.fadeInAnimator = fadeInAnimator;
            }

            public void OnAnimationCancel(Animator animation) {
                textView.TextFormatted = newText;
                textView.ResetColor();
                textView.RemoveAnimator(animation);
            }

            public void OnAnimationEnd(Animator animation) {
                textView.TextFormatted = newText;
                fadeInAnimator.Start();
                textView.RemoveAnimator(animation);
            }

            public void OnAnimationRepeat(Animator animation) {
            }

            public void OnAnimationStart(Animator animation) {
                textView.AddAnimator(animation);
            }
        }

        private static void AddAnimator(this TextView textView, Animator animator) {
            if (ActiveAnimations.ContainsKey(textView)) {
                var animations = ActiveAnimations[textView];
                animations?.Add(animator);
            }
            else {
                ActiveAnimations[textView] = new List<Animator> { animator };
            }
        }
        private static void RemoveAnimator(this TextView textView, Animator animator) {
            if (ActiveAnimations.ContainsKey(textView)) {
                var animations = ActiveAnimations[textView];
                if (animations is null) {
                    throw new NullPointerException();
                }
                animations.Remove(animator);
                if (animations.Count == 0) {
                    ActiveAnimations.Remove(textView);
                }
            }
        }
        private static void ResetColor(this TextView textView) {
            if (textView.IsAnimatorAttached()) {
                var @params = AttachedViews[textView];
                if (@params is null) {
                    throw new NullPointerException();
                }

                if (@params.TextColorList != null) {
                    textView.SetTextColor(@params.TextColorList);
                }
                else {
                    textView.SetTextColor($"#{@params.TextColor:X8}");
                }
            }
        }
        internal static void CancelAnimations(this TextView textView) {
            if (ActiveAnimations.ContainsKey(textView)) {
                var animations = ActiveAnimations[textView];
                if (animations is null) {
                    throw new NullPointerException();
                }
                var copy = new List<Animator>(animations);
                copy.ForEach(it => {
                    it.Cancel();
                });
                ActiveAnimations.Remove(textView);
            }
        }
        private static int GetAnimateTextColor(this TextView textView) {
            var @params = AttachedViews[textView];
            if (@params is null) {
                throw new NullPointerException();
            }
            if (@params.TextColorList != null) {
                var viewState = textView.GetDrawableState();
                return @params.TextColorList.GetColorForState(viewState, Color.Black);
            }
            else {
                return @params.TextColor;
            }
        }

        private static void SetTextColor(this TextView textView, string color) {
            textView.SetTextColor(ColorStateList.ValueOf(Color.ParseColor(color)));
        }
    }
}
