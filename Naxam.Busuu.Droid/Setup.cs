using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using System.Reflection;
using Naxam.Busuu.Droid.Profile.Views;
using Naxam.Busuu.Profile.ViewModel;
using Naxam.Busuu.Core.Converter;
using System.Collections;
using Naxam.Busuu.Droid.Learning.Views;
using Naxam.Busuu.Learning.ViewModel;
using MvvmCross.Binding.Bindings.Target.Construction;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.ViewModel;
using Naxam.Busuu.Droid.Learning.Views;
using MvvmCross.Binding.Bindings.Target.Construction;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Droid.Learning.TargetBinding;
using Com.Github.Lzyzsd.Circleprogress;
using MvvmCross.Binding.Droid.Views;
namespace Naxam.Busuu.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance(); 
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies as IList;
                toReturn.Add(typeof(IsMatchPatternBase64Converter).Assembly);
                return (IEnumerable<Assembly>)toReturn;

            }
        }
        protected override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewModelAssemblies());
            list.Add(typeof(ForgotPasswordViewModel).Assembly);
            list.Add(typeof(LearnViewModel).Assembly);
            return list.ToArray();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                return new List<Assembly>(base.AndroidViewAssemblies)
                {
                    typeof(MvvmCross.Binding.Droid.Views.MvxImageView).Assembly
                };
            }
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);
            registry.RegisterCustomBindingFactory<View>("BorderColor", view => new BorderTargetBinding(view));
            registry.RegisterCustomBindingFactory<CircleProgress>("Progress", view => new PercentTargetBinding(view));
            registry.RegisterCustomBindingFactory<CircleProgress>("FinishColor", view => new FinishColorTargetBinding(view));
            registry.RegisterCustomBindingFactory<LessonHeaderBackground>("BackgroundColor", view => new LessonHeaderTargetBinding(view));
            registry.RegisterCustomBindingFactory<View>("BackgroundColor", view => new BackgroundColorTargetBinding(view));
            registry.RegisterCustomBindingFactory<ImageView>("Source", view => new ImageSourceTargetBinding(view));
        }

        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewAssemblies());
            list.Add(typeof(ForgotPasswordView).Assembly);
            list.Add(typeof(LearnView).Assembly);
            return list.ToArray();
        }

    }
}