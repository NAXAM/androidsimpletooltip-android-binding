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
using Java.Lang;
using Naxam.Busuu.Droid.Core.Adapter;
using Naxam.Busuu.Review.Models;
using Com.Bumptech.Glide;

namespace Naxam.Busuu.Droid.Review.Adapter
{
    public class ReviewListAdapter : SectionAdapter
    {
        public event EventHandler<ReviewModel> SelectReview;
        Context context;
        IList<ReviewModel> reviews;
        List<IGrouping<char, ReviewModel>> ListSection;

        public ReviewListAdapter(Context context, IList<ReviewModel> reviews)
        {
            this.context = context;
            this.reviews = new List<ReviewModel>(reviews);
            ListSection = new List<IGrouping<char, ReviewModel>>();
            ListSection = reviews.OrderBy(d => d.Title).GroupBy((d) => d.Title[0]).ToList();
        }

        public override Java.Lang.Object GetRowItem(int section, int row)
        {
            return null;
        }



        public override int NumberOfRows(int section)
        {
            return ListSection.ElementAt(section < 0 ? 0 : section).Count<ReviewModel>();
        }

        public override int NumberOfSections()
        {
            return ListSection.Count;
        }


        public override bool HasSectionHeaderView(int section)
        {
            return true;
        }


        public override View GetRowView(int section, int row, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.review_item, null);
            }
            TextView txtTitle = convertView.FindViewById<TextView>(Resource.Id.txtTitle);
            TextView txtSubTitle = convertView.FindViewById<TextView>(Resource.Id.txtSubTitle);
            ImageView imgCover = convertView.FindViewById<ImageView>(Resource.Id.imgCover);
            ImageView imgStrength = convertView.FindViewById<ImageView>(Resource.Id.imgStrength);
            ImageView btnPlay = convertView.FindViewById<ImageView>(Resource.Id.btnPlay);
            ImageView btnFavorite = convertView.FindViewById<ImageView>(Resource.Id.btnFavorite);


            ReviewModel review = RowItem(section, row);
            txtTitle.Text = review.Title;
            txtSubTitle.Text = review.SubTitle;
            Glide.With(context).Load(review.ImgWord).Into(imgCover);
            switch (review.StrengthLevel)
            {
                case 0:
                    imgStrength.SetImageResource(Resource.Drawable.entity_strength_0);
                    break;
                case 1:
                    imgStrength.SetImageResource(Resource.Drawable.entity_strength_1);
                    break;
                case 2:
                    imgStrength.SetImageResource(Resource.Drawable.entity_strength_2);
                    break;
                case 3:
                    imgStrength.SetImageResource(Resource.Drawable.entity_strength_3);
                    break;
                case 4:
                    imgStrength.SetImageResource(Resource.Drawable.entity_strength_4);
                    break;
            }


            return convertView;
        }

        private ReviewModel RowItem(int section, int row)
        {
            return ListSection.ElementAt(section).ElementAt(row);
        }

        public override int GetSectionHeaderViewTypeCount()
        {
            return 2;
        }


        public override int GetSectionHeaderItemViewType(int section)
        {
            return section % 2;
        }

        public override void OnRowItemClick(AdapterView parent, View view, int section, int row, long id)
        {
            base.OnRowItemClick(parent, view, section, row, id);
            SelectReview?.Invoke(this, RowItem(section, row));
        }

        public override View GetSectionHeaderView(int section, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.review_header_item, null);
            }
            TextView txtHeader = convertView.FindViewById<TextView>(Resource.Id.txtHeader);
            txtHeader.Text = ListSection.ElementAt(section).Key + "";
            return convertView;
        }
    }
}