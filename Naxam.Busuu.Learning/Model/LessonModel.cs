using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Naxam.Busuu.Learning.Model
{
    public class LessonModel : MvxObservableCollection<TopicModel>
    {
        public LessonModel(IList<TopicModel> collection) : base(collection)
        {

        }

        public event EventHandler<LessonModel> DownloadHandle;

        private int _Id;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value; 
                }
            }
        }

        private string _color;

        public string Color
        {
            get { return _color; }
            set
            {
                _color = value; 
            }
        }

        private string _lessonNumber;

        public string LessonNumber
        {
            get { return _lessonNumber; }
            set
            {
                if (_lessonNumber != value)
                {
                    _lessonNumber = value; 
                }
            }
        }

        private string _lessonName;

        public string LessonName
        {
            get { return _lessonName; }
            set
            {
                if (_lessonName != value)
                {
                    _lessonName = value; 
                }
            }
        }


        public int Percent { get; set; }

        public string Icon { get; set; } 

        
        //private IList<TopicModel> _topics;

        //public IList<TopicModel> Topics
        //{
        //    get { return _topics; }
        //    set
        //    {
        //        if (_topics != value)
        //        {
        //            _topics = value; 
        //        }
        //    }
        //}
         
    }
}
