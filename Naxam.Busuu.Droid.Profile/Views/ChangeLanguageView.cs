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
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Support.V7.Widget;
using static Android.Support.V7.Widget.GridLayoutManager;
using Android.Support.V4.View;
using Java.Util;
using Android.Support.V4.Text;
using Android.Graphics;
using Android.Util;
using Android.Content.Res;
using Naxam.Busuu.Droid.Learning.Views;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Languages", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, ParentActivity = typeof(MainView))]
    public class ChangeLanguageView : MvxAppCompatActivity
    {
        MvxRecyclerView LanguageListview;
        GridSpacingItemDecoration ItemDecoration;
        int count;
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.ChangeLanguageActivity);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            LanguageListview = FindViewById<MvxRecyclerView>(Resource.Id.LanguageListview);
            count = LanguageListview.Adapter.ItemsSource.Cast<object>().Count();
            SetLayoutManager(2);
        }

        void SetLayoutManager(int column)
        {
            if (ItemDecoration != null)
            {
                LanguageListview.RemoveItemDecoration(ItemDecoration);
            }
            DisplayMetrics displayMetrics = ApplicationContext.Resources.DisplayMetrics;
            float dpHeight = displayMetrics.HeightPixels / displayMetrics.Density;
            float dpWidth = displayMetrics.WidthPixels / displayMetrics.Density;

            ItemDecoration = new GridSpacingItemDecoration(column, (int)(dpWidth - 136 * column) / column, false);
            LanguageListview.AddItemDecoration(ItemDecoration);
            StaggeredGridLayoutManager grid2 = new StaggeredGridLayoutManager(column, 1);

            GridLayoutManager grid = new GridLayoutManager(this, column);
            grid.SetSpanSizeLookup(new LanguageSpanSizeLookup(column, count));
            LanguageListview.SetLayoutManager(grid);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                SetLayoutManager(3);
            }
            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                SetLayoutManager(2);
            }

        }

        public int ToPixel(float dp)
        {
            float px = TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Resources.DisplayMetrics);
            return (int)Math.Round(px);
        }
        class GridSpacingItemDecoration : RecyclerView.ItemDecoration
        {
            private int spanCount;
            private int spacing;
            private bool includeEdge;
            // private bool isRtl = TextUtilsCompat.GetLayoutDirectionFromLocale(Locale.Default) == ViewCompat.ScrollAxisHorizontal;

            public GridSpacingItemDecoration(int spanCount, int spacing, bool includeEdge)
            {
                this.spanCount = spanCount;
                this.spacing = spacing;

                this.includeEdge = includeEdge;
            }

            public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
            {
                int position = parent.GetChildAdapterPosition(view); // item position 

                int column = (position - 1) % spanCount; // item column

                if (position == 0)
                {
                    return;
                }
                else
                {
                    if (includeEdge)
                    {
                        outRect.Left = spacing - column * spacing / spanCount; // spacing - column * ((1f / spanCount) * spacing)
                        outRect.Right = (column + 1) * spacing / spanCount; // (column + 1) * ((1f / spanCount) * spacing)

                        outRect.Top = spacing;
                        outRect.Bottom = spacing;
                    }
                    else
                    {
                        outRect.Right = column * spacing / spanCount; // column * ((1f / spanCount) * spacing)
                        outRect.Left = spacing - (column + 1) * spacing / spanCount; // spacing - (column + 1) * ((1f /    spanCount) * spacing)
                        outRect.Top = -48;
                        outRect.Bottom = spacing;
                    }
                }

            }


        }
        class LanguageSpanSizeLookup : SpanSizeLookup
        {
            int column;
            int count;

            public LanguageSpanSizeLookup(int column, int count)
            {
                this.column = column;
                this.count = count;
            }

            public override int GetSpanSize(int position)
            {
                if (position == 0)
                {
                    return column;
                }
                int col = (count - 1) % column;

                if (position >= count - col)
                {
                    return column - col + 1;
                }

                return 1;
            }
        }

    }
}