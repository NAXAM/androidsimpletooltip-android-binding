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

namespace Naxam.Busuu.Droid.Social.Views
{
    [Activity(Label = "ReplyView")]
    public class ReplyView : AppCompatActivity
    {
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
            imgBack = (ImageView)dialog.FindViewById(Resource.Id.imgBack);
            edtMessage = (EditText)dialog.FindViewById(Resource.Id.edtMessage);
            btnSend = (FloatingActionButton)dialog.FindViewById(Resource.Id.btnSend);
            progressBar = (ProgressBar)dialog.FindViewById(Resource.Id.mProgressBar);
            progressBar.Progress = 0;
            progressBar.Max = 100;
            //
            btnSend.SetOnLongClickListener(new MLongClick(ref progressBar, ref SendButtonLongPressed, ref progress, ref IsSend, ref txtSwipe));
            btnSend.SetOnTouchListener(new mOnTouch(ref SendButtonLongPressed, ref txtSwipe, ref progressBar, ref progress));
            //
            btnSend.Click += (s, e) =>
            {
                if (IsSend == true)
                {
                    // do something when clicking the send button
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

        private class MLongClick : Java.Lang.Object, View.IOnLongClickListener
        {
            private int progress;
            private bool SendButtonLongPressed;
            private bool IsSend = false;
            private ProgressBar progressBar;
            private TextView txtSwipe;


            public MLongClick(ref ProgressBar progressBar, ref bool SendButtonLongPressed, ref int progress, ref bool IsSend, ref TextView txtSwipe)
            {
                this.txtSwipe = txtSwipe;
                this.progress = progress;
                this.progressBar = progressBar;
                this.SendButtonLongPressed = SendButtonLongPressed;
                this.IsSend = IsSend;
            }


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
                        progress = 0;
                    });
                }

                SendButtonLongPressed = true;
                return true;
            }
        }

        private class mOnTouch : Java.Lang.Object, View.IOnTouchListener
        {
            private int progress;
            private TextView txtSwipe;
            private bool SendButtonLongPressed;
            private ProgressBar progressBar;

            public mOnTouch(ref bool SendButtonLongPressed, ref TextView txtSwipe, ref ProgressBar progressBar, ref int progress)
            {
                this.progress = progress;
                this.progressBar = progressBar;
                this.SendButtonLongPressed = SendButtonLongPressed;
                this.txtSwipe = txtSwipe;
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
                    // Do something when the button is released.
                    txtSwipe.SetX(initXtxt);
                    txtSwipe.SetY(initXtxt);
                    txtSwipe.Visibility = ViewStates.Gone;
                    progressBar.Visibility = ViewStates.Invisible;
                    progress = 0;
                    SendButtonLongPressed = false;

                    //if (SendButtonLongPressed)
                    //{
                    //    // Do something when the button is released.
                    //    txtSwipe.SetX(initXtxt);
                    //    txtSwipe.SetY(initXtxt);
                    //    txtSwipe.Visibility = ViewStates.Gone;
                    //    progressBar.Visibility = ViewStates.Invisible;
                    //    progress = 0;
                    //    SendButtonLongPressed = false;
                    //}
                }
                else if (pEvent.Action == MotionEventActions.Move)
                {


                    if (((int)pEvent.GetY() - initX) < -THRESHOLD)
                    {
                        //int a = (int)pEvent.GetX() - initX;
                        //txtSwipe.TranslationX = initXtxt + a;
                        ////(int) pEvent.getX() - initX) > THRESHOLD
                        //// move right

                        //if (THRESHOLD > 20)
                        //{
                        //    txtSwipe.Visibility = ViewStates.Gone;
                        //    progressBar.Visibility = ViewStates.Invisible;
                        //    progress = 0;

                        //}
                        if (((int)pEvent.GetX() - initX) < -THRESHOLD)
                        {
                            //(int) pEvent.getX() - initX) > THRESHOLD
                            // move right
                            int a = (int)pEvent.GetX() - initX;
                            txtSwipe.TranslationX = (initXtxt + a);
                            //                    if (THRESHOLD>20){
                            //                        txtSwipe.setVisibility(View.GONE);
                            //                        progressBar.setVisibility(View.INVISIBLE);
                            //                        progress = 0;
                            //
                            //                    }
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
            }


        }
    }
