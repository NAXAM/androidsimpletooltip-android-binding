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
using MvvmCross.Binding.Droid.Target;
using Naxam.Busuu.Core.Models;
using Android.Text;
using static Android.Widget.TextView;
using Android.Text.Style;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Core.TargetBinding
{
    public class TextNotifyTargetBinding : MvxAndroidTargetBinding
    {
        public TextNotifyTargetBinding(object target) : base(target)
        {
            // do something here later
        }

        public override Type TargetType => typeof(TextView);

        protected override void SetValueImpl(object target, object value)
        {
            TextView textview = (TextView)target;
            NotificationModel mValue = (NotificationModel)value;
            string name = mValue.NameUser;
            ViewType type = (ViewType)mValue.TypeView;
            //
            string display = "nothing to show";
           
            if (type == ViewType.Accpect)
            {
                display= "has accepted your friend request";

            }
            else if (type == ViewType.Reply)
            {
                display = "replied";
            }
            if (type == ViewType.Request)
            {
                display = "Request ";

            }
            else if (type == ViewType.Like)
            {
                display = "liked your comment";

            }
            if (type == ViewType.Correct)
            {
                display = "corrected your excise";

            }
           
            SpannableStringBuilder ssbt = new SpannableStringBuilder(name+" "+display);
            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), 0, name.Length, SpanTypes.InclusiveInclusive);
            textview.SetText(ssbt, BufferType.Normal);
        }
    }
}