using Foundation;
using System;
using UIKit;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Social.ViewModels;
using CoreGraphics;
using CoreAnimation;

namespace Naxam.Busuu.iOS.Social
{
	[MvxFromStoryboard(StoryboardName = "Social")]
    public partial class DiscoverView : MvxViewController<DiscoverViewModel>, IUICollectionViewDataSource, IUICollectionViewDelegate, IUICollectionViewDelegateFlowLayout
    {
       
        public DiscoverView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			nfloat viewCV_TB = (UIScreen.MainScreen.Bounds.Size.Height - 20) / 6;
            nfloat viewCV_LR = ((UIScreen.MainScreen.Bounds.Size.Width - 88) * 0.16f) - 10;

            CollectionViewLineLayout myFlow = new CollectionViewLineLayout();
            DiscoverCollectionView.SetCollectionViewLayout(myFlow, true);
			DiscoverCollectionView.BackgroundColor = null;
            DiscoverCollectionView.ContentInset = new UIEdgeInsets(viewCV_TB, viewCV_LR, viewCV_TB, viewCV_LR);
            DiscoverCollectionView.WeakDataSource = this;
            DiscoverCollectionView.WeakDelegate = this;
        }

		public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell =  (UICollectionViewCell)collectionView.DequeueReusableCell("DiscoverCell", indexPath);
            return cell;
		}		

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return ViewModel.Discovers.Count;
        }
    }

	public class CollectionViewLineLayout : UICollectionViewFlowLayout
	{
        public const float ZOOM_FACTOR = 0.16f;
		public float ACTIVE_DISTANCE;

        public CollectionViewLineLayout()
		{
            float ITEM_SIZE = (float)UIScreen.MainScreen.Bounds.Size.Width - 88 - ((float)UIScreen.MainScreen.Bounds.Size.Width - 88) * ZOOM_FACTOR;
			ACTIVE_DISTANCE = ITEM_SIZE;
            nfloat insetsTB = (UIScreen.MainScreen.Bounds.Size.Height - 20) / 6;
			nfloat insetsLR = ((UIScreen.MainScreen.Bounds.Size.Width - 88) * ZOOM_FACTOR) / 2 + 1.5f;           

			ItemSize = new CGSize(ITEM_SIZE, ITEM_SIZE);		
			SectionInset = new UIEdgeInsets(insetsTB, insetsLR, insetsTB, insetsLR);
            ScrollDirection = UICollectionViewScrollDirection.Horizontal;
			MinimumLineSpacing = 40.0f;
		}

		public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds)
		{
			return true;
		}

		public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
		{
			var array = base.LayoutAttributesForElementsInRect(rect);
			var visibleRect = new CGRect(CollectionView.ContentOffset, CollectionView.Bounds.Size);

			foreach (var attributes in array)
			{
				if (attributes.Frame.IntersectsWith(rect))
				{
					float distance = (float)(visibleRect.GetMidX() - attributes.Center.X);
					float normalizedDistance = distance / ACTIVE_DISTANCE;
					if (Math.Abs(distance) < ACTIVE_DISTANCE)
					{
						float zoom = 1 + ZOOM_FACTOR * (1 - Math.Abs(normalizedDistance));
						attributes.Transform3D = CATransform3D.MakeScale(zoom, zoom, 1.0f);
						attributes.ZIndex = 1;
					}
				}
			}
			return array;
		}

		public override CGPoint TargetContentOffset(CGPoint proposedContentOffset, CGPoint scrollingVelocity)
		{
			float offSetAdjustment = float.MaxValue;
			float horizontalCenter = (float)(proposedContentOffset.X + (this.CollectionView.Bounds.Size.Width / 2.0));
			CGRect targetRect = new CGRect(proposedContentOffset.X, 0.0f, this.CollectionView.Bounds.Size.Width, this.CollectionView.Bounds.Size.Height);
			var array = base.LayoutAttributesForElementsInRect(targetRect);
			foreach (var layoutAttributes in array)
			{
				float itemHorizontalCenter = (float)layoutAttributes.Center.X;
				if (Math.Abs(itemHorizontalCenter - horizontalCenter) < Math.Abs(offSetAdjustment))
				{
					offSetAdjustment = itemHorizontalCenter - horizontalCenter;
				}
			}
			return new CGPoint(proposedContentOffset.X + offSetAdjustment, proposedContentOffset.Y);
		}

	}
}