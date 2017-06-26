using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naxam.Busuu.Social.Models;

namespace Naxam.Busuu.Social.Serveices
{
    public class DataDiscover : IDataDiscover
    {
        public Task<List<Discover>> GetAllDiscover()
        {
            var list = new List<Discover>()
            {
                new Discover()
                {
                    Id = 0,
                    Avatar = "res:user_avatar_placeholder",
                    Name = "Nguyen Nhu Son",
                    Country = "Vietnam",
                    ImageSpeakLanguage = "res:profile_flag_vn",
                    ImageLearn = "res:flag_small_english",
                    TextLearn = "ENGLISH",
                    Speak = false,
                    Write = "Hello Naxam!\nI am a new member.",
                    PublicTime = "29/5/2017"
                },
				new Discover()
				{
					Id = 1,
					Avatar = "res:user_avatar_placeholder",
					Name = "Nguyen Nhu Son",
					Country = "Vietnam",
					ImageSpeakLanguage = "res:profile_flag_vn",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
                    Speak = true,
					Write = "Hello Naxam!\nI am a new member.",
					PublicTime = "29/5/2017"
                },
				new Discover()
                {
                    Id = 2,
                    Avatar = "res:user_avatar_placeholder",
                    Name = "Anabela Rodrigues",
                    Country = "Portugal",
                    ImageSpeakLanguage = "res:flag_small_portuguese",
                    ImageLearn = "res:flag_small_english",
                    TextLearn = "ENGLISH",
                    Speak = false,
					Write = "Hello Naxam!",
					PublicTime = "28/5/2017"
				},
				new Discover()
				{
					Id = 3,
					Avatar = "res:user_avatar_placeholder",
					Name = "Kaiser",
					Country = "Mexico",
					ImageSpeakLanguage = "res:flag_small_spanish",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!",
					PublicTime = "21/5/2017"
				},
				new Discover()
				{
					Id = 4,
					Avatar = "res:user_avatar_placeholder",
					Name = "Iyp",
					Country = "China",
					ImageSpeakLanguage = "res:flag_small_chinese",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!",
					PublicTime = "21/5/2017"
				},
				new Discover()
				{
					Id = 5,
					Avatar = "res:user_avatar_placeholder",
					Name = "Juan Pablo Cervantes",
					Country = "Colombia",
					ImageSpeakLanguage = "res:flag_small_spanish",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!",
					PublicTime = "21/5/2017"
				},
				new Discover()
				{
					Id = 6,
					Avatar = "res:user_avatar_placeholder",
					Name = "Mohamed",
					Country = "Agypt",
					ImageSpeakLanguage = "res:flag_small_arabic",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
                    Speak = true,
					Write = "Hello Naxam!",
					PublicTime = "19/5/2017"
				},
			    new Discover()
				{
					Id = 7,
					Avatar = "res:user_avatar_placeholder",
					Name = "Mauricio Percara",
					Country = "Argentina",
					ImageSpeakLanguage = "res:flag_small_spanish",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Hello Naxam!",
					PublicTime = "19/5/2017"
				},
				new Discover()
				{
					Id = 8,
					Avatar = "res:user_avatar_placeholder",
					Name = "Leandra",
					Country = "Swiss",
					ImageSpeakLanguage = "res:flag_small_german",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Hello Naxam!",
					PublicTime = "19/5/2017"
				},
				new Discover()
				{
					Id = 9,
					Avatar = "res:user_avatar_placeholder",
					Name = "Nguyen Nhu Son",
					Country = "Vietnam",
					ImageSpeakLanguage = "res:profile_flag_vn",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!\nI am a new member.",
					PublicTime = "29/5/2017"
				},
				new Discover()
				{
					Id = 10,
					Avatar = "res:user_avatar_placeholder",
					Name = "Nguyen Nhu Son",
					Country = "Vietnam",
					ImageSpeakLanguage = "res:profile_flag_vn",
					ImageLearn = "res:flag_small_english",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Hello Naxam!\nI am a new member.",
					PublicTime = "29/5/2017"
				},
            };
            return Task.FromResult(list);
        }
    }
}
