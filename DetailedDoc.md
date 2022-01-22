## Detailed documentation for ProgressButton library

Quick setup can be found [here](Readme.md)

## Showing Progress

![basic progress button example](https://raw.githubusercontent.com/XamarinDeveloper/ProgressButton/main/Assets/ProgressEnd.gif)

```C#
protected override void OnCreate(Bundle savedInstanceState) {
    base.OnCreate(savedInstanceState);
    SetContentView(Resource.Layout.activity_main);
    
    var myButton = FindViewById<MaterialButton>(Resource.Id.myButton);
    
    // It is mandatory to bind your button to activity or fragment lifecycle
    this.BindProgressButton(myButton);

    // (Optional) Enable fade in/out animations
    // All parameters are Optional (the Action itself is optional too)
    myButton.AttachTextChangeAnimator((textChangeAnimatorParams) => {
       textChangeAnimatorParams.FadeOutMills = 150; // current text fade out time in milliseconds. default 150
       textChangeAnimatorParams.FadeInMills = 150; // current text fade in time in milliseconds. default 150
    
       textChangeAnimatorParams.UseCurrentTextColor = false; // by default is true. handling text color based on the current color settings
       
       textChangeAnimatorParams.TextColor = Color.White; // override button text color with single color
       textChangeAnimatorParams.TextColorRes = Resource.Color.white; // override button text color with single color resource
       textChangeAnimatorParams.TextColorList = new ColorStateList(...); // override button text color with stateful color
    });

    // Show progress with "Loading" text. The final progress size will be (radius + stroke) * 2
    myButton.ShowProgress((progressParams) => {
        progressParams.ButtonText = "Loading"; // string value to show next to progress
        progressParams.ButtonTextRes = Resource.String.loading; // text resource to show next to progress
        
        // progress drawable gravity relative to button text
        // possible values TextStart, TextEnd and Center
        progressParams.Gravity = DrawableButton.Gravity.TextEnd; // default value is Gravity.TextEnd
       
        progressParams.TextMarginRes = Resource.Dimension.progressMargin; // margin between text and progress. default 10dp
        progressParams.TextMarginPx = 30; // margin between text and progress in pixels. default 10dp
       
        progressParams.ProgressColor = Color.White; // progress color int
        progressParams.ProgressColorRes = Resource.Color.white; // progress color resource
        progressParams.ProgressColors = new int[]{ Color.White, Color.Black }; // progress colors list
        
        
        progressParams.ProgressRadiusRes = Resource.Dimension.smallRadius; // progress radius dimension resource. default 7.5dp
        progressParams.ProgressRadiusPx = 50; // progress radius in pixels default 7.5dp
        
        progressParams.ProgressStrokeRes = Resource.Dimension.stroke3; // progress stroke dimension resource. default 2.5dp
        progressParams.ProgressStrokePx = 50; // progress stroke in pixels. default 2.5dp
    });

    // Hide progress and show "Submit" text instead
    myButton.HideProgress(Resource.String.submit);
}
```

## Showing AnimatedDrawable

![animated drawable button example](https://raw.githubusercontent.com/XamarinDeveloper/ProgressButton/main/Assets/AnimatedDrawable.gif)

```C#
protected override void OnCreate(Bundle savedInstanceState) {
    base.OnCreate(savedInstanceState);
    SetContentView(Resource.Layout.activity_main);
    
    var myButton = FindViewById<MaterialButton>(Resource.Id.myButton);
    
    // It is mandatory to bind your button to activity or fragment lifecycle
    this.BindProgressButton(myButton);

    // (Optional) Enable fade in/out animations
    // All parameters are Optional (the Action itself is optional too)
    myButton.AttachTextChangeAnimator((textChangeAnimatorParams) => {
        // same as Showing Progress above
    });
    
    // setup bounds is required to use AnimatedDrawable with library
    var animatedDrawable = ContextCompat.GetDrawable(this, Resource.Drawable.animated_check);
    animatedDrawable.Bounds = new Rect(0, 0, 50, 50);

    myButton.ShowDrawable(animatedDrawable, (drawableParams) => {
        drawableParams.ButtonText = "Done"; // string value to show next to animated drawable
        drawableParams.ButtonTextRes = Resource.String.done; // text resource to show next to animated drawable
        
        // drawable gravity relative to button text
        // possible values TextStart, TextEnd and Center
        drawableParams.Gravity = DrawableButton.Gravity.TextEnd; // default value is Gravity.TextEnd
        
        drawableParams.TextMarginRes = Resource.Dimension.progressMargin; // margin between text and drawable. default 10dp
        drawableParams.TextMarginPx = 30; // margin between text and drawable in pixels. default 10dp
    });

    // Hide drawable and show "Save" text instead
    myButton.HideDrawable(Resource.String.save);
}
```