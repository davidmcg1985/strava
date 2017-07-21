using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace TestApplicationRestsharp.Models
{
    public class AthleteViewModel
    {
        public AthleteViewModel()
        {
        }

        public AthleteViewModel(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
        }

        public bool IsAuthenticated { get; set; }

        public int FollowerCount { get; set; }

        public int FriendCount { get; set; }

        public int MutualFriendCount { get; set; }

        public string AthleteType { get; set; }

        public string DatePreference { get; set; }

        public string MeasurementPreference { get; set; }

        public string Email { get; set; }

        public int Ftp { get; set; }

        public double Weight { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string FullName { get; set; }

        public string ProfileMedium { get; set; }

        public string Profile { get; set; }

        public string City { get; set; }

        public object State { get; set; }

        public object Country { get; set; }

        public string Sex { get; set; }

        public object Friend { get; set; }

        public object Follower { get; set; }

        public bool Premium { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public int Id { get; set; }

        public int ResourceState { get; set; }

    }

}