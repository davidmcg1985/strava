using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace TestApplicationRestsharp.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
        }

        public HomeViewModel(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
        }

        public bool IsAuthenticated { get; set; }
        public IList<ActivityViewModel> Activities { get; set; } =  new ObservableCollection<ActivityViewModel>();
        public AthleteViewModel Athlete { get; set; }
    }
}