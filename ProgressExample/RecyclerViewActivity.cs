using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.Button;
using Ir.XamarinDev.Android.ProgressButton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressExample {
    [Activity(Label = "RecyclerViewActivity")]
    public class RecyclerViewActivity : AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rv);

            var rv = FindViewById<RecyclerView>(Resource.Id.rv);

            rv.SetLayoutManager(new LinearLayoutManager(this));
            rv.SetAdapter(new ButtonsAdapter(this));
        }
    }

    class ButtonsAdapter : RecyclerView.Adapter {
        private readonly ILifecycleOwner lifecycleOwner;
        private readonly ISet<int> inProgress = new HashSet<int>();

        public ButtonsAdapter(ILifecycleOwner lifecycleOwner) {
            this.lifecycleOwner = lifecycleOwner;
        }
        public override int ItemCount => 100;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            return new Holder(this, LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_button, parent, false));
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            ((Holder)holder).Bind(position);
        }

        class Holder : RecyclerView.ViewHolder {
            private readonly ButtonsAdapter adapter;
            private readonly MaterialButton buttonProgress;
            private readonly TextView number;
            public Holder(ButtonsAdapter adapter, View itemView) : base(itemView) {
                this.adapter = adapter;
                buttonProgress = itemView.FindViewById<MaterialButton>(Resource.Id.buttonProgress);
                number = itemView.FindViewById<TextView>(Resource.Id.number);
                buttonProgress.AttachTextChangeAnimator();
                adapter.lifecycleOwner.BindProgressButton(buttonProgress);
                buttonProgress.Click += delegate {
                    adapter.inProgress.Add(BindingAdapterPosition);
                    buttonProgress.ShowProgress((progressParams) => {
                        progressParams.ProgressColor = Color.White;
                    });
                };
            }
            public void Bind(int position) {
                number.Text = $"position #{position}";
                buttonProgress.CleanUpDrawable();
                if (!adapter.inProgress.Contains(position)) {
                    buttonProgress.SetText(Resource.String.submit);
                }
                else {
                    buttonProgress.ShowProgress((progressParams) => {
                        progressParams.ProgressColor = Color.White;
                    });
                }
            }
        }
    }
}