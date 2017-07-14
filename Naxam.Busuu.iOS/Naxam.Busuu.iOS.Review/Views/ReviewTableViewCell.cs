using System;
using System.Globalization;
using AVFoundation;
using FFImageLoading;
using FFImageLoading.Work;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Platform.Converters;
using Naxam.Busuu.Review.Models;
using UIKit;


namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewTableViewCell : MvxTableViewCell
    {
        private ReviewModel _item;
		public ReviewModel Item { get => _item; set => _item = value; }
		private AVAudioPlayer _ringtoneAudioPlayer;
        public bool isPlaying = ReviewAllView.isPlayingAudio;

        public void SetupCell()
        {
            lbTitle.Text = Item.Title;
            lbSubtitle.Text = Item.SubTitle;
            switch (Item.StrengthLevel)
            {
                case 0: imgStrength.Image = UIImage.FromBundle("strength_0"); break;
                case 1: imgStrength.Image = UIImage.FromBundle("strength_1"); break;
                case 2: imgStrength.Image = UIImage.FromBundle("strength_2"); break;
                case 3: imgStrength.Image = UIImage.FromBundle("strength_3"); break;
                case 4: imgStrength.Image = UIImage.FromBundle("strength_4"); break;
                default:
                    imgStrength.Image = UIImage.FromBundle("strength_0"); break;
            }
            if(Item.IsFavorite)
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_gold"), UIControlState.Normal);
            }else
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_grey"), UIControlState.Normal);
            }

            ImageService.Instance.LoadUrl(Item.ImgWord).
                        ErrorPlaceholder("image_placeholder.png",ImageSource.ApplicationBundle).
                        LoadingPlaceholder("placeholder", ImageSource.CompiledResource).
                        Into(imgWord);

			_ringtoneAudioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(Item.SoundUrl));
			_ringtoneAudioPlayer.NumberOfLoops = -1;
            if(isPlaying)
            {
                PlayRingtone();
            }else
            {
                StopRingtone();
            }
        }

        protected ReviewTableViewCell(IntPtr handle) : base(handle)
        {
			// Note: this .ctor should not contain any initialization logic.
        }

        partial void btnStar_TouchUpInside(NSObject sender)
        {
            Item.IsFavorite = !Item.IsFavorite;
			if (Item.IsFavorite)
			{
				btnStar.SetImage(UIImage.FromBundle("rating_star_gold"), UIControlState.Normal);
			}
			else
			{
				btnStar.SetImage(UIImage.FromBundle("rating_star_grey"), UIControlState.Normal);
			}
        }

        partial void btnPlay_TouchUpInside(NSObject sender)
        {
            isPlaying = !isPlaying;
            if(isPlaying) 
            {
                PlayRingtone();
            }else
            {
                StopRingtone();
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            imgWord.Layer.CornerRadius = 4;
        }

		public void PlayRingtone()
		{
			if (_ringtoneAudioPlayer != null)
			{
				_ringtoneAudioPlayer.Stop();
			}
            btnPlay.SetImage(UIImage.FromBundle("conversation_speaking_stop_button"), UIControlState.Normal);
			_ringtoneAudioPlayer.Play();
		}

		public void StopRingtone()
		{
			if (_ringtoneAudioPlayer != null)
			{
                btnPlay.SetImage(UIImage.FromBundle("conversation_speaking_play_button"), UIControlState.Normal);
				_ringtoneAudioPlayer.Stop();
			}
		}
    }
}
