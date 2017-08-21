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
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using System.Threading.Tasks;
using System.Threading;
using Android.Text;
using IO.Github.Douglasjunior.AndroidSimpleTooltip;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Social.Views
{
    [Activity(Label = "ReplyView", MainLauncher = true)]
    public class ReplyView : AppCompatActivity, View.IOnLongClickListener, View.IOnTouchListener
    {
        // fixing an error

        private Dialog dialog;
        private int progress = 0;
        private TextView txtSwipe;
        private Button btnClick;
        private ImageView imgBack;
        private ProgressBar progressBar;
        private bool IsSend = false;
        private EditText edtMessage;
        private FloatingActionButton btnSend;
        private bool SendButtonLongPressed = false;

        public bool OnLongClick(View v)
        {
            if (IsSend == false)
            {
                txtSwipe.Visibility = ViewStates.Visible;
                progressBar.Visibility = ViewStates.Visible;
                Task.Run(async () =>
                {
                    while (true)
                    {
                        progressBar.Progress = progress;
                        await Task.Delay(100);
                        progress += 1;
                        if (progress == 100)
                        {

                            break;
                        }
                    }
                    txtSwipe.Visibility = ViewStates.Invisible;
                    progressBar.Visibility = ViewStates.Gone;
                    progress = 0;
                });
            }

            SendButtonLongPressed = true;
            return true;
       
    }

        public bool OnTouch(View pView, MotionEvent pEvent)
        {
            int THRESHOLD = 30;
            int initX = 0;
            int initY = 0;
            int initXtxt = 0;
            int initYtxt = 0;
            pView.OnTouchEvent(pEvent);

            if (pEvent.Action == MotionEventActions.Up)
            {

               

                if (SendButtonLongPressed)
                {
                    // Do something when the button is released.
                    txtSwipe.SetX(initXtxt);
                    txtSwipe.SetY(initXtxt);
                    txtSwipe.Visibility = ViewStates.Gone;
                    progressBar.Visibility = ViewStates.Invisible;
                    progress = 0;
                    SendButtonLongPressed = false;
                }
            }
            else if (pEvent.Action == MotionEventActions.Move)
            {
                if (((int)pEvent.GetX() - initX) < -THRESHOLD)
                {

                    int a = (int)pEvent.GetX() - initX;
                    txtSwipe.TranslationX = (initXtxt + a);
                    if (a < -150)
                    {
                        txtSwipe.Visibility = ViewStates.Gone;
                        progressBar.Visibility = ViewStates.Invisible;
                        progress = 0;
                    }
                }
            }
            else if (pEvent.Action == MotionEventActions.Down)
            {
                initXtxt = (int)txtSwipe.GetX();
                initYtxt = (int)txtSwipe.GetY();
                //
                initX = (int)pEvent.GetX();
                initY = (int)pEvent.GetY();
            }
           

            return false;
    }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TestReply);
            init();
            btnClick = FindViewById<Button>(Resource.Id.btnClick);
            btnClick.Click += (s, e) =>
            {
                dialog.Show();
            };

        }
        private void init()
        {
            dialog = new Dialog(this, Resource.Style.Theme_AppCompat);
            dialog.SetContentView(Resource.Layout.activity_reply);
            dialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            txtSwipe = (TextView)dialog.FindViewById(Resource.Id.txtSwipe);
            String str = "< swipe to cancle";
            txtSwipe.Text = str;
            imgBack = (ImageView)dialog.FindViewById(Resource.Id.imgBack);
            edtMessage = (EditText)dialog.FindViewById(Resource.Id.edtMessage);
            btnSend = (FloatingActionButton)dialog.FindViewById(Resource.Id.btnSend);
            progressBar = (ProgressBar)dialog.FindViewById(Resource.Id.mProgressBar);
            progressBar.Progress = 0;
            progressBar.Max = 100;
            //
         
            btnSend.SetOnLongClickListener(this);
            btnSend.SetOnTouchListener(this);
            //
            btnSend.Click += (s, e) =>
            {
                if (IsSend == true)
                {
                    // do something when clicking the send button
                    new SimpleTooltip.Builder(dialog.Context)
                            .AnchorView(btnSend)
                            .Text("Tap and hold to record")
                            .ArrowColor(Color.ParseColor("#000000"))
                            .Gravity((int)GravityFlags.Top)
                            .BackgroundColor(Color.ParseColor("#000000"))
                            .TextColor(Color.ParseColor("#FFFFFF"))
                            .DismissOnOutsideTouch(false)
                            .DismissOnInsideTouch(true)
                            .Build()
                            .Show();
                }

            };
            imgBack.Click += (s, e) =>
            {

                dialog.Dismiss();
            };
            edtMessage.TextChanged += (s, e) =>
            {
                if (e.Text.Count() == 0)
                {
                    btnSend.SetImageResource(Resource.Drawable.ic_mic);
                    IsSend = false;
                }
                else
                {
                    btnSend.SetImageResource(Resource.Drawable.ic_send_social);
                    IsSend = true;
                }
            };

        }




        }
    }
