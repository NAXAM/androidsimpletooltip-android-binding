// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Start.Model;
using UIKit;

namespace Naxam.Busuu.iOS.Start.Cells
{
	public partial class LanguageTableViewCell : MvxTableViewCell
	{
        readonly MvxImageViewLoader _loaderImgLanaguage;

		public LanguageTableViewCell (IntPtr handle) : base (handle)
		{
            _loaderImgLanaguage = new MvxImageViewLoader(() => this.imgLanguage);

			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<LanguageTableViewCell, LanguageModel>();
                setBinding.Bind(_loaderImgLanaguage).To(n => n.Flag).WithConversion("ImageUriValueConverter");
                setBinding.Bind(nameLanguage).To(n => n.Language);
				setBinding.Apply();
			});
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

            imgLanguage.Layer.CornerRadius = imgLanguage.Frame.Width / 2;
		}
	}
}