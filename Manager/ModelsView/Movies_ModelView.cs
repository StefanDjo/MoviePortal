using Data_Access_Layer.MoviesPortal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ModelsView
{
    public class Movies_ModelView
    {
        public Movies_ModelView()
        {
            listaMovieRent = new List<Movie>();
            listaMovieUnrent = new List<Movie>();
        }
        public List<Movie> listaMovieRent { get; set; }
        public List<Movie> listaMovieUnrent { get; set; }
    }
}
