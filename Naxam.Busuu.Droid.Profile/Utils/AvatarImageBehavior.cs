using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Content.Res;
using Java.Lang;

namespace Naxam.Busuu.Droid.Profile.Utils
{
    [Register("Naxam.Busuu.Droid.Profile.Utils.AvatarImageBehavior")]
    public class AvatarImageBehavior: CoordinatorLayout.Behavior
    {
        private  static float MIN_AVATAR_PERCENTAGE_SIZE = 0.3f;
        private  static int EXTRA_FINAL_AVATAR_PADDING = 80;

        private  static string TAG = "behavior";
        private Context mContext;

        private float mCustomFinalYPosition;
        private float mCustomStartXPosition;
        private float mCustomStartToolbarPosition;
        private float mCustomStartHeight;
        private float mCustomFinalHeight;

        private float mAvatarMaxSize;
        private float mFinalLeftAvatarPadding;
        private float mStartPosition;
        private int mStartXPosition;
        private float mStartToolbarPosition;
        private int mStartYPosition;
        private int mFinalYPosition;
        private int mStartHeight;
        private int mFinalXPosition;
        private float mChangeBehaviorPoint;
        
        public AvatarImageBehavior(Context context, IAttributeSet attrs)
        {
            mContext = context;

            if (attrs != null)
            {
                TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.AvatarImageBehavior);
                mCustomFinalYPosition = a.GetDimension(Resource.Styleable.AvatarImageBehavior_finalYPosition, 0);
                mCustomStartXPosition = a.GetDimension(Resource.Styleable.AvatarImageBehavior_startXPosition, 0);
                mCustomStartToolbarPosition = a.GetDimension(Resource.Styleable.AvatarImageBehavior_startToolbarPosition, 0);
                mCustomStartHeight = a.GetDimension(Resource.Styleable.AvatarImageBehavior_startHeight, 0);
                mCustomFinalHeight = a.GetDimension(Resource.Styleable.AvatarImageBehavior_finalHeight, 0);

                a.Recycle();
            }

            init();

            mFinalLeftAvatarPadding = context.Resources.GetDimension(
                Resource.Dimension.spacing_normal);
        }


        public AvatarImageBehavior(Context context)
        {
            mContext = context;
            init();

            mFinalLeftAvatarPadding = context.Resources.GetDimension(
                Resource.Dimension.spacing_normal);
        }
        private void init()
        {
            bindDimensions();
        }
        private void bindDimensions()
        {
            mAvatarMaxSize = 120;
        }
        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            return dependency.GetType()==typeof(  Toolbar);
        }
        //
        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
           ImageView image = (ImageView)child;
            maybeInitProperties((ImageView)image, dependency);

             int maxScrollDistance = (int)(mStartToolbarPosition);
            float expandedPercentageFactor = dependency.GetY() / maxScrollDistance;

            if (expandedPercentageFactor < mChangeBehaviorPoint)
            {
                float heightFactor = (mChangeBehaviorPoint - expandedPercentageFactor) / mChangeBehaviorPoint;

                float distanceXToSubtract = ((mStartXPosition - mFinalXPosition)
                        * heightFactor) + (image.Height / 2);
                float distanceYToSubtract = ((mStartYPosition - mFinalYPosition)
                        * (1f - expandedPercentageFactor)) + (image.Height / 2);

                image.SetX(mStartXPosition - distanceXToSubtract);
                image.SetY(mStartYPosition - distanceYToSubtract);

                float heightToSubtract = ((mStartHeight - mCustomFinalHeight) * heightFactor);

                CoordinatorLayout.LayoutParams lp = (CoordinatorLayout.LayoutParams)image.LayoutParameters;
                lp.Width = (int)(mStartHeight - heightToSubtract);
                lp.Height = (int)(mStartHeight - heightToSubtract);
                image.LayoutParameters=lp;
            }
            else
            {

                float distanceYToSubtract = ((mStartYPosition - mFinalYPosition)
                        * (1f - expandedPercentageFactor)) + (mStartHeight / 2);

                image.SetX(mStartXPosition - image.Width/ 2);
                image.SetY(mStartYPosition - distanceYToSubtract);

                CoordinatorLayout.LayoutParams lp = (CoordinatorLayout.LayoutParams)image.LayoutParameters;
                lp.Width = (int)(mStartHeight);
                lp.Height = (int)(mStartHeight);
                image.LayoutParameters=lp;
            }
            return true;
        }
        private void maybeInitProperties(ImageView child, View dependency)
        {
            if (mStartYPosition == 0)
                mStartYPosition = (int)(dependency.GetY());

            if (mFinalYPosition == 0)
                mFinalYPosition = (dependency.Height / 2);

            if (mStartHeight == 0)
                mStartHeight = child.Height;

            if (mStartXPosition == 0)
                mStartXPosition = (int)(child.GetX() + (child.Width / 2));

            if (mFinalXPosition == 0)
                mFinalXPosition = mContext.Resources.GetDimensionPixelOffset(Resource.Dimension.abc_action_bar_content_inset_material) + ((int)mCustomFinalHeight / 2);

            if (mStartToolbarPosition == 0)
                mStartToolbarPosition = dependency.GetY();

            if (mChangeBehaviorPoint == 0)
            {
                mChangeBehaviorPoint = (child.Height - mCustomFinalHeight) / (2f * (mStartYPosition - mFinalYPosition));
            }
        }


        public int getStatusBarHeight()
        {
            int result = 0;
            int resourceId = mContext.Resources.GetIdentifier("status_bar_height", "dimen", "android");

            if (resourceId > 0)
            {
                result = mContext.Resources.GetDimensionPixelSize(resourceId);
            }
            return result;
        }

    }
}