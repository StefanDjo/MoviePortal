using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Layer.MoviesPortal
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Author { get; set; }
        public bool? Available { get; set; }
        public string UserId { get; set; }
    }
}
