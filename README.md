# Progress Button for Xamarin.Android - Ported from [this](https://github.com/razir/ProgressButton) library
[![Release](https://img.shields.io/github/v/release/XamarinDeveloper/ProgressButton?color=FC0&display_name=tag&label=Release)](https://github.com/XamarinDeveloper/ProgressButton/releases)
[![NuGet](https://img.shields.io/nuget/v/Ir.XamarinDev.Android.ProgressButton?label=NuGet)](https://nuget.org/packages/Ir.XamarinDev.Android.ProgressButton/)

[![basic progress button example](https://raw.githubusercontent.com/XamarinDeveloper/ProgressButton/main/Assets/ProgressEnd.gif)](#)

[![progress cebter button example](https://raw.githubusercontent.com/XamarinDeveloper/ProgressButton/main/Assets/ProgressCenter.gif)](#)

[![mixed progress button example](https://raw.githubusercontent.com/XamarinDeveloper/ProgressButton/main/Assets/MixedBehaviour.gif)](#)

#### Article on ProAndroidDev.com explaining how it works (in kotlin): [here](https://proandroiddev.com/replace-progressdialog-with-a-progress-button-in-your-app-14ed1d50b44)


#### Add progress animation to any button by adding a few lines of code without layout changes

### Main features: 
  - No layout changes required
  - Few lines of code to add
  - Easily configurable
  - Customizable
  - Built in fade animations

## NuGet package 
```
Install-Package Ir.XamarinDev.Android.ProgressButton
```

## How to use

### Basic example

```C#
protected override void OnCreate(Bundle savedInstanceState) {
    base.OnCreate(savedInstanceState);
    SetContentView(Resource.Layout.activity_main);
    
    var myButton = FindViewById<MaterialButton>(Resource.Id.myButton);
    
    // bind your button to activity lifecycle
    this.BindProgressButton(myButton);

    // (Optional) Enable fade in/out animations 
    myButton.AttachTextChangeAnimator();

    // Show progress with "Loading" text
    myButton.ShowProgress((progressParams) => {
        progressParams.ButtonTextRes = Resource.String.loading;
        progressParams.ProgressColor = Color.White;
    });

    // Hide progress and show "Submit" text instead
    myButton.HideProgress(Resource.String.submit);
}
```

### Showing AnimatedDrawable

[![animated drawable button example](https://raw.githubusercontent.com/XamarinDeveloper/ProgressButton/main/Assets/AnimatedDrawable.gif)](#)

```C#
var animatedDrawable = ContextCompat.GetDrawable(this, Resource.Drawable.animated_check);
// Defined bounds are required for your drawable  
animatedDrawable.Bounds = new Rect(0, 0, 40, 40);
  
button.ShowDrawable(animatedDrawable, (drawableParams) => {
    buttonTextRes = Resource.String.saved;
});
```

### Detailed doc: [here](DetailedDoc.md)

### Avoiding memory leaks
To avoid memory leaks you always need to bind your button to a LifecycleOwner (usually Activity, or Fragment):

```C#
[ILifecycleOwner].BindProgressButton(button);
```

### Author
XamarinDev

### Credits
Anton Hadutski - [GitHub](https://github.com/razir)
