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
using Android.Support.V7.Widget;
using Naxam.Busuu.Learning.Model;
using Com.Bumptech.Glide;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Learning.Adapters
{
    public class SelectWordImageRecyclerViewAdapter : RecyclerView.Adapter
    {
        public event EventHandler<AnswerModel> ItemClicked;
        IList<AnswerModel> answers;
        Context context;
        int clickPosition;
        public SelectWordImageRecyclerViewAdapter(Context context, IList<AnswerModel> answers)
        {
            this.context = context;
            this.answers = answers;
        }
        public override int ItemCount => answers.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SelectWordImageRecyclerViewHolder viewHolder = (SelectWordImageRecyclerViewHolder)holder;
            viewHolder.txtAnswer.Text = answers[position].Text;
            Glide.With(context).Load(answers[position].Image).CenterCrop().Into(viewHolder.imgAnswer);
            viewHolder.imgResult.SetBackgroundColor(answers[position].Value?Color.ParseColor("#8074B825") : Color.ParseColor("#80EE6253"));
            viewHolder.imgResult.SetImageResource(answers[position].Value ? Resource.Drawable.v : Resource.Drawable.x);
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.select_word_with_image_item, parent, false);
            if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                view.Elevation = Util.Util.PxFromDp(context, 4);
                view.TranslationZ = Util.Util.PxFromDp(context, 1);
            }
            
            return new SelectWordImageRecyclerViewHolder(view);
        }
    }
    public class SelectWordImageRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public TextView txtAnswer;
        public ImageView imgAnswer;
        public ImageView imgResult;
        public SelectWordImageRecyclerViewHolder(View view) : base(view)
        {
            txtAnswer = view.FindViewById<TextView>(Resource.Id.txtAnswer);
            imgAnswer = view.FindViewById<ImageView>(Resource.Id.imgAnswer);
            imgResult = view.FindViewById<ImageView>(Resource.Id.imgResult);
        }
    }
}