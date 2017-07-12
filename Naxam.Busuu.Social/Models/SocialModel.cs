namespace Naxam.Busuu.Social.Models
{
    public class SocialModel
    {
		public int Id { get; set; }
		public string Avatar { get; set; }
		public string Name { get; set; }
		public string Country { get; set; }
		public string ImageSpeakLanguage { get; set; }
		public string ImageLearn { get; set; }
		public string TextLearn { get; set; }
		public bool Speak { get; set; }
		public string Write { get; set; }
		public string PublicTime { get; set; }
        public double Star { get; set; }
        public bool Friends { get; set; }
        public string ImgQuestion { get; set; }
        public string TextQuestion { get; set; }
    }
}
