﻿using System.Windows;
using Kutny.WpfInfra;
using PitchDetector;
using UtagoeGui.Models;

namespace UtagoeGui.ViewModels
{
    public class NoteBlockViewModel : ViewModel2
    {
        private readonly NoteBlockModel _model;
        private readonly IAppStore _appStore;

        public NoteBlockViewModel(NoteBlockModel model, IAppStore appStore)
        {
            this._model = model;
            this._appStore = appStore;

            this.EnableAutoPropertyChangedEvent(appStore);
        }

        public string Text => VowelClassifier.VowelTypeToString(this._model.VowelType);

        public int RowIndex => Logics.MaximumNoteNumber - this._model.NoteNumber;

        [ChangeValueWhenModelPropertyChange(nameof(IAppStore.ScoreScale))]
        public Thickness Margin => new Thickness(this._model.Start * MainWindowViewModel.UnitWidth * this._appStore.ScoreScale, 0, 0, 0);

        [ChangeValueWhenModelPropertyChange(nameof(IAppStore.ScoreScale))]
        public double Width => this._model.Span * MainWindowViewModel.UnitWidth * this._appStore.ScoreScale;
    }
}
