﻿using System.Threading.Tasks;
using System.Linq;
using Naxam.Busuu.Social.Models;
using System.Collections.Generic;
using System;

namespace Naxam.Busuu.Social.Serveices
{
    public class DataSocial : IDataSocial
    {
        public async Task<SocialModel[]> GetAllSocial()
        {
            var list = new List<SocialModel>()
			{
				new SocialModel()
                {
                    Id = 0,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Nguyen Nhu Son",
                    Country = "Vietnam",
                    ImageSpeakLanguage = "profile_flag_vn.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = false,
                    Write = "Hello Naxam!\nI am a new member.",
                    PublicTime = "29/5/2017",
                    Star = 5,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 1,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Nguyen Nhu Son",
                    Country = "Vietnam",
                    ImageSpeakLanguage = "profile_flag_vn.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = true,
                    Write = "Lumia-RingTone-Nokia-Remix-Nokia-DJ.mp3",
                    PublicTime = "29/5/2017",
                    Star = 4.5,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 2,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Anabela Rodrigues",
                    Country = "Portugal",
                    ImageSpeakLanguage = "flag_small_portuguese.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = false,
                    Write = "Hello Naxam!",
                    PublicTime = "28/5/2017",
                    Star = 4.5,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 3,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Kaiser",
                    Country = "Mexico",
                    ImageSpeakLanguage = "flag_small_spanish.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = false,
                    Write = "Hello Naxam!\nI am a new member.\nI am a new member.\nI am a new member.",
                    PublicTime = "21/5/2017",
                    Star = 4,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 4,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Iyp",
                    Country = "China",
                    ImageSpeakLanguage = "flag_small_chinese.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = true,
                    Write = "Nokia-tune-Nokia-tune.mp3",
                    PublicTime = "21/5/2017",
                    Star = 3.5,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 5,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Juan Pablo Cervantes",
                    Country = "Colombia",
                    ImageSpeakLanguage = "flag_small_spanish.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = false,
                    Write = "Hello Naxam!",
                    PublicTime = "21/5/2017",
                    Star = 3,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 6,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Mohamed",
                    Country = "Agypt",
                    ImageSpeakLanguage = "flag_small_arabic.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = true,
                    Write = "Nokia-tune-Nokia-tune.mp3",
                    PublicTime = "19/5/2017",
                    Star = 2.5,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 7,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Mauricio Percara",
                    Country = "Argentina",
                    ImageSpeakLanguage = "flag_small_spanish.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = false,
                    Write = "Hello Naxam!",
                    PublicTime = "19/5/2017",
                    Star = 2,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 8,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Leandra",
                    Country = "Swiss",
                    ImageSpeakLanguage = "flag_small_german.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = true,
                    Write = "Lumia-RingTone-Nokia-Remix-Nokia-DJ.mp3",
                    PublicTime = "19/5/2017",
                    Star = 0,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 9,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Nguyen Nhu Son",
                    Country = "Vietnam",
                    ImageSpeakLanguage = "profile_flag_vn.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = false,
                    Write = "Hello Naxam!\nI am a new member.",
                    PublicTime = "29/5/2017",
                    Star = 5,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
                {
                    Id = 10,
                    Avatar = "user_avatar_placeholder.png",
                    Name = "Nguyen Nhu Son",
                    Country = "Vietnam",
                    ImageSpeakLanguage = "profile_flag_vn.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Speak = true,
                    Write = "Nokia-tune-Nokia-tune.mp3",
                    PublicTime = "29/5/2017",
                    Star = 4.5,
                    Friends = false,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 11,
					Avatar = "user_avatar_placeholder.png",
					Name = "Nguyen Nhu Son",
					Country = "Vietnam",
					ImageSpeakLanguage = "profile_flag_vn.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!\nI am a new member.",
					PublicTime = "29/5/2017",
					Star = 5,
                    Friends = true,
                    ImgQuestion = "Naxam_logo_socialdetail.png",
                    TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 12,
					Avatar = "user_avatar_placeholder.png",
					Name = "Nguyen Nhu Son",
					Country = "Vietnam",
					ImageSpeakLanguage = "profile_flag_vn.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Lumia-RingTone-Nokia-Remix-Nokia-DJ.mp3",
					PublicTime = "29/5/2017",
					Star = 4.5,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 13,
					Avatar = "user_avatar_placeholder.png",
					Name = "Anabela Rodrigues",
					Country = "Portugal",
					ImageSpeakLanguage = "flag_small_portuguese.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!",
					PublicTime = "28/5/2017",
					Star = 4.5,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 14,
					Avatar = "user_avatar_placeholder.png",
					Name = "Kaiser",
					Country = "Mexico",
					ImageSpeakLanguage = "flag_small_spanish.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!\nI am a new member.\nI am a new member.\nI am a new member.",
					PublicTime = "21/5/2017",
					Star = 4,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 15,
					Avatar = "user_avatar_placeholder.png",
					Name = "Iyp",
					Country = "China",
					ImageSpeakLanguage = "flag_small_chinese.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Nokia-tune-Nokia-tune.mp3",
					PublicTime = "21/5/2017",
					Star = 3.5,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 16,
					Avatar = "user_avatar_placeholder.png",
					Name = "Juan Pablo Cervantes",
					Country = "Colombia",
					ImageSpeakLanguage = "flag_small_spanish.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!",
					PublicTime = "21/5/2017",
					Star = 3,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 17,
					Avatar = "user_avatar_placeholder.png",
					Name = "Mohamed",
					Country = "Agypt",
					ImageSpeakLanguage = "flag_small_arabic.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Nokia-tune-Nokia-tune.mp3",
					PublicTime = "19/5/2017",
					Star = 2.5,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 18,
					Avatar = "user_avatar_placeholder.png",
					Name = "Mauricio Percara",
					Country = "Argentina",
					ImageSpeakLanguage = "flag_small_spanish.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!",
					PublicTime = "19/5/2017",
					Star = 2,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 19,
					Avatar = "user_avatar_placeholder.png",
					Name = "Leandra",
					Country = "Swiss",
					ImageSpeakLanguage = "flag_small_german.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Lumia-RingTone-Nokia-Remix-Nokia-DJ.mp3",
					PublicTime = "19/5/2017",
					Star = 0,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
			    new SocialModel()
				{
					Id = 20,
					Avatar = "user_avatar_placeholder.png",
					Name = "Nguyen Nhu Son",
					Country = "Vietnam",
					ImageSpeakLanguage = "profile_flag_vn.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = false,
					Write = "Hello Naxam!\nI am a new member.",
					PublicTime = "29/5/2017",
					Star = 5,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				},
				new SocialModel()
				{
					Id = 21,
					Avatar = "user_avatar_placeholder.png",
					Name = "Nguyen Nhu Son",
					Country = "Vietnam",
					ImageSpeakLanguage = "profile_flag_vn.png",
					ImageLearn = "flag_small_english.png",
					TextLearn = "ENGLISH",
					Speak = true,
					Write = "Nokia-tune-Nokia-tune.mp3",
					PublicTime = "29/5/2017",
					Star = 4.5,
					Friends = true,
					ImgQuestion = "Naxam_logo_socialdetail.png",
					TextQuestion = "Say hello Naxam!"
				}
			};
            return list.ToArray();
        }

        public async Task<SocialModel[]> GetDiscoverSocial()
        {
            return (await GetAllSocial()).Where(d => !d.Friends).ToArray();
        }

        public async Task<SocialModel[]> GetFriendSocial()
        {
           return (await GetAllSocial()).Where(d => d.Friends).ToArray();
        }

        public async Task<SocialModel> GetSocialById(int id)
        {
            return (await GetAllSocial()).Where(d => d.Id == id).FirstOrDefault();
        }
    }
}
