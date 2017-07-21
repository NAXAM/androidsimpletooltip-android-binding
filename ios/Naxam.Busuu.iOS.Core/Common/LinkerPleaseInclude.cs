using System;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Common
{
    public class LinkerPleaseInclude
    {
		public void Include(UIBarButtonItem button)
		{
			button.Clicked += (sender, e) => { button.Title = button.Title + ""; };
		}
	}
}
