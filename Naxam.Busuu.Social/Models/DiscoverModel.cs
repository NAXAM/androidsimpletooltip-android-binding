using System;

namespace Naxam.Busuu.Social.Models
{
    public class DiscoverModel
    {
        public uint Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageSpeakLanguage { get; set; }
        public string ImageLearn { get; set; }
		public string TextLearn { get; set; }
        public bool Speak { get; set; }
		public string Write { get; set; }
        public string PublicTime { get; set; }

        public DiscoverModel()
        {
            
        }
    }
}
