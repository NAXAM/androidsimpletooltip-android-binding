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
                if (_color != value)
                {
                    _color = value; 
                }
            }
        }


        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value; 
                }
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value; 
                }
            }
        }

        private int _percent;

        public int Percent
        {
            get { return _percent; }
            set
            {
                if (_percent != value)
                {
                    _percent = value; 
                }
            }
        }
 
         
    }
}
