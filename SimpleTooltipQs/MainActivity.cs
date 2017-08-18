using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Views;
using System;
using IO.Github.Douglasjunior.AndroidSimpleTooltip;

namespace SimpleTooltipQs
{
    [Activity(Label = "SimpleTooltipQs", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, View.IOnClickListener, SimpleTooltip.IOnDismissListener, SimpleTooltip.IOnShowListener
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);
            fab.SetOnClickListener(this);

            FindViewById(Resource.Id.btn_simple).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_animated).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_overlay).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_maxwidth).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_outside).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_inside).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_inside_modal).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_modal_custom).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_no_arrow).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_custom_arrow).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_dialog).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_center).SetOnClickListener(this);
            FindViewById(Resource.Id.btn_overlay_rect).SetOnClickListener(this);

        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.fab)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text("Floating Action Button")
                        .Gravity(1)

                    .Build()
                    .Show();
            }
            else if (v.Id == Resource.Id.btn_simple)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text(Resource.String.btn_simple)
                        .Gravity((int)GravityFlags.End)
                        .Build()
                        .Show();

            }
            else if (v.Id == Resource.Id.btn_animated)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text(Resource.String.btn_animated)
                        .Gravity((int)GravityFlags.Top)
                        .Animated(true)
                        .Build()
                        .Show();

            }
            else if (v.Id == Resource.Id.btn_overlay)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text(Resource.String.btn_overlay)
                        .Gravity((int)GravityFlags.Top)
                        .Animated(true)
                        .TransparentOverlay(false)
                        .Build()
                        .Show();

            }
            else if (v.Id == Resource.Id.btn_maxwidth)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text(GetString(Resource.String.btn_maxwidth) + GetString(Resource.String.btn_maxwidth) + GetString(Resource.String.btn_maxwidth) + GetString(Resource.String.btn_maxwidth) + GetString(Resource.String.btn_maxwidth))
                        .Gravity((int)GravityFlags.End)
                        .MaxWidth(Resource.Dimension.simpletooltip_max_width)
                        .Build()
                        .Show();

            }
            else if (v.Id == Resource.Id.btn_outside)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text(Resource.String.btn_outside)
                        .Gravity((int)GravityFlags.Bottom)
                        .DismissOnOutsideTouch(true)
                        .DismissOnInsideTouch(false)
                        .Build()
                        .Show();

            }
            else if (v.Id == Resource.Id.btn_inside)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text(Resource.String.btn_inside)
                        .Gravity((int)GravityFlags.Start)
                        .DismissOnOutsideTouch(false)
                        .DismissOnInsideTouch(true)
                        .Build()
                        .Show();

            }
            else if (v.Id == Resource.Id.btn_inside_modal)
            {
                new SimpleTooltip.Builder(this)
                        .AnchorView(v)
                        .Text(Resource.String.btn_inside_modal)
                        .Gravity((int)GravityFlags.End)
                        .DismissOnOutsideTouch(false)
                        .DismissOnInsideTouch(true)
                        .Modal(true)
                        .Build()
                        .Show();

            }

        }

        public void OnDismiss(SimpleTooltip p0)
        {
            // do ur stuff here
            throw new NotImplementedException();
        }

        public void OnShow(SimpleTooltip p0)
        {
            // do ur stuff here
            throw new NotImplementedException();
        }
    }
}



