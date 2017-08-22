// This file has been autogenerated from a class added in the UI designer.

using System;

using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.iOS.Notification.Cells
{
	public partial class NotificationCell : MvxTableViewCell
	{
        readonly MvxImageViewLoader _loaderImageUser;

		public NotificationCell (IntPtr handle) : base(handle)
		{
            _loaderImageUser = new MvxImageViewLoader(() => this.imgUser);

			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<NotificationCell, NotificationModel>();
				setBinding.Bind(_loaderImageUser).To(n => n.ImgUser).WithConversion("ImageUriValueConverter");
                setBinding.Bind(lblDetail).For("FormattedText").To(n => n.Details).WithConversion("NotificationTextConverter");
				setBinding.Bind(lblTime).To(n => n.Time).WithConversion("NotificationDatetimeConverter");
				setBinding.Bind(ContentView).For(n => n.BackgroundColor).To(n => n.Check).WithConversion("NotificationColorConverter");
				setBinding.Apply();
			});
		}

        public override void AwakeFromNib()
        {
			base.AwakeFromNib();	

            imgUser.Layer.CornerRadius = imgUser.Frame.Width / 2;
        }
	}
}