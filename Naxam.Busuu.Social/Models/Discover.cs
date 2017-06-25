using System;
namespace Naxam.Busuu.Social.Models
{
    public class Discover
    {
        public uint Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Learn { get; set; }
        public string Write { get; set; }
        public bool Speak { get; set; } 
        public string PublicTime { get; set; }

        public Discover (uint _id, string _avatar, string _name, string _country, string _language, string _learn, string _write, bool _speak, string _publictime)
        {
            this.Id = _id;
            this.Avatar = _avatar;
            this.Name = _name;
            this.Country = _country;
            this.Language = _language;
            this.Learn = _learn;
            this.Write = _write;
			this.Speak = _speak;
            this.PublicTime = _publictime;
        }
    }
}
