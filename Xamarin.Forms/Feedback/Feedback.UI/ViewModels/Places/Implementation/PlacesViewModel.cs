﻿using System.Collections.ObjectModel;
using Feedback.Core.Entities;
using Feedback.UI.ViewModels.Base.Implementation;
using PropertyChanged;

namespace Feedback.UI.ViewModels.Places.Implementation
{
    [ImplementPropertyChanged]
    internal class PlacesViewModel : BaseLoadableViewModel, IPlacesViewModel
    {
        public PlacesViewModel(PlacesFactory factory)
        {
            LoadCommand = factory.GetLoadPlacesCommand(this);
        }

        public ObservableCollection<Place> Places { get; internal set; }
    }
}