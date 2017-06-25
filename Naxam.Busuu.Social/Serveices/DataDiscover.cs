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
            var list = new List<Discover>(){
				new Discover( 0, "user_avatar_placeholder", "Nguyen Nhu Son", "Vietnam", "profile_flag_vn", "flag_small_english", "Hello Naxam!\nI am a new member.", false, "29/5/2017"),
                new Discover( 1, "user_avatar_placeholder", "Kaiser", "Mexico", "flag_small_spanish", "flag_small_english", "Hello Naxam.", false, "21/6/2017"),
                new Discover( 2, "user_avatar_placeholder", "Iyp", "China", "flag_small_english", "flag_small_english", "Hello Naxam.", true, "22/6/2017"),
				new Discover( 3, "user_avatar_placeholder", "Juan Pablo Cervantes", "Colombia", "flag_small_spanish", "flag_small_english", "Hello Naxam.", false, "21/6/2017"),
                new Discover( 4, "user_avatar_placeholder", "Wang", "China", "flag_small_chinese", "flag_small_english", "Hello Naxam.", true, "19/6/2017"),
				new Discover( 5, "user_avatar_placeholder", "Mohamed", "Agypt", "flag_small_arabic", "flag_small_english", "Hello Naxam.", false, "21/6/2017"),
			    new Discover( 6, "user_avatar_placeholder", "Mauricio Percara", "Argentina", "flag_small_spanish", "flag_small_english", "Hello Naxam.", false, "21/6/2017"),
                new Discover( 7, "user_avatar_placeholder", "Leandra", "Swiss", "flag_small_german", "flag_small_english", "Hello Naxam.", true, "21/6/2017"),
                new Discover( 8, "user_avatar_placeholder", "Talon", "Mexico", "flag_small_spanish", "flag_small_english", "Hello Naxam.", true, "21/6/2017"),
				new Discover( 9, "user_avatar_placeholder", "Kathus", "Mexico", "flag_small_spanish", "flag_small_english", "Hello Naxam.", false, "21/6/2017"),
				new Discover( 10, "user_avatar_placeholder", "Vladimir", "Mexico", "flag_small_spanish", "flag_small_english", "Hello Naxam.", false, "21/6/2017"),
			};

            return Task.FromResult(list);
        }
    }
}
