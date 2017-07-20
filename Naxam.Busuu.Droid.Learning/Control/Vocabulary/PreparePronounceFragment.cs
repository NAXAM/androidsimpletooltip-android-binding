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
using static Android.Resource;
using Android.Views.Animations;
using Naxam.Busuu.Learning.Model;

namespace Naxam.Busuu.Droid.Learning.Control.Vocabulary
{
    public class PreparePronounceFragment : VocabularyFragmentBase
    {
        private bool isClickStar;
        ImageView imgPlayBtn, imgStarBtn;

        public PreparePronounceFragment(UnitModel Item)
        {
            this.Item = Item;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.PreparePronounce, container, false);
            Init(view);
            view.Tag = "1";
            return view;
        }
         

        private void Init(View view)
        {
            Context context = view.Context;

            imgStarBtn = view.FindViewById<ImageView>(Resource.Id.imgStar);
            imgStarBtn.SetBackgroundResource(Resource.Drawable.star_white);


            imgStarBtn.Click += (s, e) =>
            {
                if (isClickStar == false)
                {
                    imgStarBtn.SetBackgroundResource(Resource.Drawable.ic_yellow_star);
                }
                else
                {
                    imgStarBtn.SetBackgroundResource(Resource.Drawable.star_white);
                }
                isClickStar = !isClickStar;

            };

        }
    }
}