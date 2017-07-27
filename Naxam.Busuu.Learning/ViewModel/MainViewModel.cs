using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Model;
using Naxam.Busuu.Learning.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naxam.Busuu.Core.ViewModels;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class MainViewModel : MvxViewModel
    {
        readonly ILearningService learningService;

        public MainViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        public async override void ViewAppeared()
        {
            base.ViewAppeared();
            Lessons = new MvxObservableCollection<LessonModel>(await learningService.GetAllLesson());

            LessonAndSubLessions = new MvxObservableCollection<object>();
            foreach (var lesson in Lessons)
            {
                LessonAndSubLessions.Add(lesson);
                foreach (var topic in lesson)
                {
                    LessonAndSubLessions.Add(topic);
                }
            }
        }

        #region Property
        private MvxObservableCollection<LessonModel> _lessons;

        public MvxObservableCollection<LessonModel> Lessons
        {
            get { return _lessons; }
            set
			{
				SetProperty(ref _lessons, value);
            }
        }

        private MvxObservableCollection<object> _lessonAndSubLessions;

		public MvxObservableCollection<object> LessonAndSubLessions
		{
			get { return _lessonAndSubLessions; }
			set
			{
                SetProperty(ref _lessonAndSubLessions, value);
            }
		}

        #endregion Property

        #region Command

        private IMvxCommand _GoPremiumCommand;

        public IMvxCommand GoPremiumCommand
        {
            get { return _GoPremiumCommand = _GoPremiumCommand ?? new MvxCommand(RunGoPremiumCommand); }

        }

        void RunGoPremiumCommand()
        {
            ShowViewModel<PremiumViewModel>();
        }

        private IMvxCommand _ChangeLanguageCommand;

        public IMvxCommand ChangeLanguageCommand
        {
            get { return _ChangeLanguageCommand = _ChangeLanguageCommand ?? new MvxCommand(RunChangeLanguageCommand); }

        }

        void RunChangeLanguageCommand()
        {
            ShowViewModel<ChangeLanguageViewModel>();
        }


        private IMvxCommand _DownloadCommand;

        public IMvxCommand DownloadCommand
        {
            get { return _DownloadCommand = _DownloadCommand ?? new MvxCommand(RunDownloadCommand); }

        }

        void RunDownloadCommand()
        {

        }

        private IMvxCommand _ExerciseClickCommand;

        public IMvxCommand ExerciseClickCommand
        {
            get { return _ExerciseClickCommand = _ExerciseClickCommand ?? new MvxCommand<ExerciseClickEventArg>(RunExerciseClickCommand); }

        }

        void RunExerciseClickCommand(ExerciseClickEventArg e)
        {
            ShowViewModel<VocabularyViewModel>(e.Exercise);
        }

        #endregion Command
    }
}
