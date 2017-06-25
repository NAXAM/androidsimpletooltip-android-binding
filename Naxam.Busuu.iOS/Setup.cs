using System;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using Naxam.Busuu.iOS.Social;
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

		protected override IEnumerable<Assembly> GetViewAssemblies()
		{
			var assemblies = new List<Assembly>
			{
				typeof(DiscoverView).Assembly
			};

			assemblies.AddRange(base.GetViewAssemblies());
			return assemblies;
		}

		protected override IEnumerable<Assembly> GetViewModelAssemblies()
		{
			var assemblies = new List<Assembly>
			{
				typeof(DiscoverViewModel).Assembly
			};

			assemblies.AddRange(base.GetViewModelAssemblies());
			return assemblies;
		}
	}
}
