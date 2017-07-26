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
using Naxam.Busuu.Learning.Model;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class BaseFragment : Android.Support.V4.App.Fragment
    { 
        public UnitModel Item;
        public IList<UnitModel> Items;
        public virtual event EventHandler<int> NextClicked;
    }
}