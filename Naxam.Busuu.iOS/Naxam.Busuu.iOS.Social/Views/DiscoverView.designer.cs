// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social
{
    [Register ("DiscoverView")]
    partial class DiscoverView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView DiscoverCollectionView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DiscoverCollectionView != null) {
                DiscoverCollectionView.Dispose ();
                DiscoverCollectionView = null;
            }
        }
    }
}