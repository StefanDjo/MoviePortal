using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ModelsView
{
    public class DetaljiMovie_ModelView
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Author { get; set; }
        public bool? Available { get; set; }
        public string UserId { get; set; }
    }
}
