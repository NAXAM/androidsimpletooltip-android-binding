﻿using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.Linq;
using CoreAnimation;
using System.Collections.Generic;
using FBKVOControllerNS;

namespace Naxam.Busuu.iOS.Core.CustomControls
{
	class BadgeLabel : UILabel
	{
		public BadgeLabel() : base(CGRect.Empty)
		{
			BackgroundColor = UIColor.Red;
			Layer.MasksToBounds = true;
			TextColor = UIColor.White;
			Font = UIFont.SystemFontOfSize(13);
			TextAlignment = UITextAlignment.Center;
			Hidden = true;
		}

		public override CGRect Bounds
		{
			get
			{
				return base.Bounds;
			}
			set
			{
				base.Bounds = value;
				Layer.CornerRadius = value.Height / 2;
			}
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				Hidden = value == null;
			}
		}
	}

	public interface IMaterialTabBarDelegate
	{
		void SelectedIndex(MaterialTabBar tabbar, int index);
	}

	public class MaterialTabBar : UIView
	{
		#region Private properties
		private UIColor _SelectedColor;
		private UIColor _UnselectedColor;
		private CGSize _IconSize = new CGSize(24, 24);
		private nfloat _LabelHeight = 16;
		private CAShapeLayer _RippleLayer = new CAShapeLayer()
		{
			AnchorPoint = new CGPoint(0.5f, 0.5f)
		};
		private static int MAIN_STACK_TAG = 1001;
		private List<BadgeLabel> _Badges = new List<BadgeLabel>();

		private UIColor _RippleColor;

		UIView _RippleContainer = new UIView()
		{
			BackgroundColor = UIColor.Clear,
			ClipsToBounds = true
		};

		FBKVOController _KVOController;
		#endregion

		#region Public properties

		public UIColor RippleColor
		{
			get => _RippleColor;
			set
			{
				_RippleColor = value;
				_RippleLayer.FillColor = (value ?? UIColor.Clear).CGColor;
			}
		}


		public IMaterialTabBarDelegate Delegate;

		public int SelectedIndex
		{
			get; private set;
		}
		#endregion

		public MaterialTabBar(UITabBar tabbar, UIColor selectedColor = null, UIColor unselectedColor = null) : base(CGRect.Empty)
		{
			if (tabbar == null) throw new Exception("Tabbar cannot be null");

			_SelectedColor = selectedColor ?? UIColor.FromRGB(14.0f / 255.0f, 122.0f / 255.0f, 254.0f / 255.0f);
			_UnselectedColor = unselectedColor ?? UIColor.Gray;
			_RippleColor = _SelectedColor.ColorWithAlpha(0.2f);

			if (tabbar.Subviews.Length > 1)
			{
				_IconSize = tabbar.Subviews[1].Subviews[0].Frame.Size;
				_LabelHeight = tabbar.Subviews[1].Subviews[1].Frame.Height;
			}

			BackgroundColor = UIColor.White;
			Layer.ShadowColor = UIColor.Black.CGColor;
			Layer.ShadowOpacity = 0.2f;
			Layer.ShadowOffset = new CGSize(0, -2);

			tabbar.Superview.Add(this);
			TranslatesAutoresizingMaskIntoConstraints = false;
			TopAnchor.ConstraintEqualTo(tabbar.TopAnchor).Active = true;
			LeadingAnchor.ConstraintEqualTo(tabbar.LeadingAnchor).Active = true;
			BottomAnchor.ConstraintEqualTo(tabbar.BottomAnchor).Active = true;
			TrailingAnchor.ConstraintEqualTo(tabbar.TrailingAnchor).Active = true;

			_RippleContainer.ClipsToBounds = true;
			AddSubview(_RippleContainer);
			_RippleContainer.Layer.AddSublayer(_RippleLayer);

			var mainStack = new UIStackView()
			{
				Axis = UILayoutConstraintAxis.Horizontal,
				Distribution = UIStackViewDistribution.FillEqually,
				Alignment = UIStackViewAlignment.Fill,
				ClipsToBounds = true,
				Tag = MAIN_STACK_TAG
			};
			mainStack.Layer.MasksToBounds = true;

			AddSubview(mainStack);
			mainStack.TranslatesAutoresizingMaskIntoConstraints = false;
			mainStack.TopAnchor.ConstraintEqualTo(LayoutMarginsGuide.TopAnchor, -5).Active = true;
			mainStack.LeadingAnchor.ConstraintEqualTo(LayoutMarginsGuide.LeadingAnchor, -8).Active = true;
			mainStack.BottomAnchor.ConstraintEqualTo(LayoutMarginsGuide.BottomAnchor, 6).Active = true;
			mainStack.TrailingAnchor.ConstraintEqualTo(LayoutMarginsGuide.TrailingAnchor, 8).Active = true;

			_RippleLayer.Path = UIBezierPath.FromOval(new CGRect(0, 0, 1, 1)).CGPath;
			_RippleLayer.FillColor = _RippleColor.CGColor;

			if (tabbar.SelectedItem != null)
			{
				SelectedIndex = tabbar.Items.ToList().IndexOf(tabbar.SelectedItem);
			}
			else
			{
				SelectedIndex = 0;
			}

			UserInteractionEnabled = true;

			_KVOController = new FBKVOController(this);

			UpdateItems(tabbar.Items);
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			var touch = touches.First() as UITouch;
			var touchedPoint = touch.LocationInView(this);
			var itemSize = Bounds.Size.Width / ViewWithTag(MAIN_STACK_TAG).Subviews.Length;
			var index = Math.Floor(touchedPoint.X / itemSize);
			SetSelectedIndex((int)index, true);
			Delegate?.SelectedIndex(this, (int)index);
			base.TouchesEnded(touches, evt);
		}

		public void UpdateItems(UITabBarItem[] items)
		{
			_KVOController.UnobserveAll();
			var mainStack = ViewWithTag(MAIN_STACK_TAG) as UIStackView;
			foreach (UIView sview in mainStack.Subviews)
			{
				sview.RemoveFromSuperview();
			}
			foreach (BadgeLabel label in _Badges)
			{
				label.RemoveFromSuperview();
			}
			_Badges.Clear();

			var tag = 1;
			foreach (UITabBarItem item in items)
			{
				var stack = new UIStackView()
				{
					Axis = UILayoutConstraintAxis.Vertical,
					Distribution = UIStackViewDistribution.FillProportionally,
					Alignment = UIStackViewAlignment.Center,
					Tag = tag
				};

				var isSelected = tag == SelectedIndex + 1;

				var iv = new UIImageView(new CGRect(CGPoint.Empty, _IconSize))
				{
					TintColor = isSelected ? _SelectedColor : _UnselectedColor,
					ContentMode = UIViewContentMode.ScaleAspectFit,
					Tag = 201
				};

				UIImage image = null;
				UIImage highlightedImage = null;
				if (isSelected)
				{
					image = item.SelectedImage ?? item.Image;
					highlightedImage = item.Image ?? item.SelectedImage;
				}
				else
				{
					image = item.Image ?? item.SelectedImage;
					highlightedImage = item.SelectedImage ?? item.Image;
				}

				iv.Image = image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
				iv.HighlightedImage = highlightedImage?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
				stack.AddArrangedSubview(iv);
				iv.TranslatesAutoresizingMaskIntoConstraints = false;
				iv.HeightAnchor.ConstraintGreaterThanOrEqualTo(_IconSize.Height).Active = true;

				var label = new UILabel
				{
					Text = item.Title,
					TextAlignment = UITextAlignment.Center,
					Font = UIFont.SystemFontOfSize(10),
					AdjustsFontSizeToFitWidth = true,
					MinimumScaleFactor = 0.5f,
					TextColor = isSelected ? _SelectedColor : _UnselectedColor,
					Tag = 202,
					Hidden = !isSelected
				};
				stack.AddArrangedSubview(label);

				mainStack.AddArrangedSubview(stack);

				var badge = new BadgeLabel();
				AddSubview(badge);
				badge.TranslatesAutoresizingMaskIntoConstraints = false;
				badge.CenterXAnchor.ConstraintEqualTo(iv.RightAnchor).Active = true;
				badge.TopAnchor.ConstraintEqualTo(LayoutMarginsGuide.TopAnchor, -5).Active = true;
				badge.WidthAnchor.ConstraintGreaterThanOrEqualTo(badge.HeightAnchor).Active = true;
				badge.Text = item.BadgeValue;
				_Badges.Add(badge);

				item.Tag = tag;

				_KVOController.Observe(item, "badgeValue", NSKeyValueObservingOptions.New, UpdateBadgeForItem);

				tag++;
			}
		}

		private void UpdateBadgeForItem(NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2)
		{
			if (arg1 is UITabBarItem item)
			{
				UpdateBadge((int)item.Tag - 1, item.BadgeValue);
			}
		}

		public void SetSelectedIndex(int index, bool animated)
		{
			var mainStack = ViewWithTag(MAIN_STACK_TAG);

			if (index == SelectedIndex || index < 0 || index >= mainStack.Subviews.Length)
			{
				return;
			}
			var oldIV = mainStack.ViewWithTag(SelectedIndex + 1).ViewWithTag(201) as UIImageView;
			var oldLabel = mainStack.ViewWithTag(SelectedIndex + 1).ViewWithTag(202) as UILabel;
			var newStack = mainStack.ViewWithTag(index + 1);
			var newIV = newStack.ViewWithTag(201) as UIImageView;
			var newLabel = newStack.ViewWithTag(202) as UILabel;
			var width = Bounds.Size.Width / mainStack.Subviews.Length;
			var oldCache = oldIV.Image;
			var newCache = newIV.Image;
			Action finalState = () =>
			{
				oldIV.TintColor = _UnselectedColor;
				oldLabel.TextColor = _UnselectedColor;
				oldLabel.Hidden = true;
				oldIV.Image = oldIV.HighlightedImage;
				oldIV.HighlightedImage = oldCache;

				newIV.TintColor = _SelectedColor;
				newLabel.TextColor = _SelectedColor;
				newLabel.Hidden = false;
				newIV.Image = newIV.HighlightedImage;
				newIV.HighlightedImage = newCache;

				if (animated)
				{
					_RippleLayer.AffineTransform = CGAffineTransform.MakeScale(width, width);
				}
			};

			if (animated)
			{
				_RippleContainer.Frame = new CGRect(width * index, 0, width, Bounds.Height);
				_RippleLayer.Frame = new CGRect(_RippleContainer.Bounds.Size.Width / 2, _RippleContainer.Bounds.Size.Height / 2, 1, 1);
				_RippleContainer.Hidden = false;

				UIView.AnimateNotify(0.3, finalState, (finished) =>
				{
					SelectedIndex = index;
					_RippleContainer.Hidden = true;
					_RippleLayer.AffineTransform = CGAffineTransform.MakeIdentity();
				});
			}
			else
			{
				finalState.Invoke();
				SelectedIndex = index;
			}
		}

		public void UpdateBadge(int index, string badgeValue)
		{
			if (index < 0 || index >= _Badges.Count) return;
			_Badges[index].Text = badgeValue;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (_KVOController != null)
			{
				_KVOController.UnobserveAll();
				_KVOController.Dispose();
				_KVOController = null;
			}
		}
	}
}
