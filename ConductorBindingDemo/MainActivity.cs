using Android.App;
using Android.Widget;
using Android.OS;
using Com.Bluelinelabs.Conductor;
using Android.Views;
using System;
using Com.Bluelinelabs.Conductor.Changehandler;

namespace ConductorBindingDemo
{
    [Activity(Label = "ConductorBindingDemo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        private Router router;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			ViewGroup container = (ViewGroup)FindViewById(Resource.Id.controller_container);

			router = Conductor.AttachRouter(this, container, savedInstanceState);
			if (!router.HasRootController)
			    router.SetRoot(RouterTransaction.With(new FirstController()));
        }

        public override void OnBackPressed()
        {
            if(!router.HandleBack())
                base.OnBackPressed();
        }
    }

    public class FirstController : Controller
    {
        protected override View OnCreateView(LayoutInflater inflater, ViewGroup container)
        {
            View view = inflater.Inflate(Resource.Layout.FirstController, container, false);
            var nextButton = view.FindViewById(Resource.Id.btn_gotosecond);
            nextButton.Click += (sender, e) => {
                Router.
                      PushController(RouterTransaction.With(new SecondController())
                                     .PushChangeHandler(new HorizontalChangeHandler())
                                     .PopChangeHandler(new VerticalChangeHandler()));
			};
			return view;
        }
    }

	public class SecondController : Controller
	{
		protected override View OnCreateView(LayoutInflater inflater, ViewGroup container)
		{
            View view = inflater.Inflate(Resource.Layout.SecondController, container, false);
			return view;
		}
	}
}

