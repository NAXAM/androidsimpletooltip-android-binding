using Android.Support.V4.App;

namespace Naxam.Busuu.Droid.Profile.Utils
{
    public interface IMyPagerAdapter
    {
        int Count { get; }

        Fragment GetItem(int position);
    }
}