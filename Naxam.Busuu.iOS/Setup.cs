using System;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using Naxam.Busuu.iOS.Review.Views;
using Naxam.Busuu.Review.ViewModels;
using UIKit;
using Naxam.Busuu.iOS.Social.Views;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.iOS
{
    public class Setup : MvxIosSetup
    {
		public Setup(MvxApplicationDelegate appDelegate, IMvxIosViewPresenter presenter) : base(appDelegate, presenter)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}

        protected override System.Collections.Generic.IEnumerable<System.Reflection.Assembly> GetViewAssemblies()
        {
            var assemblies = new List<Assembly>
            {
                typeof(ReviewAllView).Assembly,
                typeof(SocialView).Assembly,
            };

            assemblies.AddRange(base.GetViewAssemblies());
            return assemblies;
        }

        protected override IEnumerable<Assembly> GetViewModelAssemblies()
        {
			var assemblies = new List<Assembly>
			{
                typeof(ReviewAllViewModel).Assembly,
                typeof(SocialViewModel).Assembly,
			};

			assemblies.AddRange(base.GetViewModelAssemblies());
			return assemblies;
        }

        protected override void FillBindingNames(MvvmCross.Binding.BindingContext.IMvxBindingNameRegistry obj)
        {
            base.FillBindingNames(obj);
        }

        protected override void FillTargetFactories(MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
        {
            
        }
    }
}
