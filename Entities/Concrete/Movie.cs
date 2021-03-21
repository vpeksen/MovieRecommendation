using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Movie : IEntity
    {
        public int Id { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string OriginalTitle { get; set; }
        public string OriginalLanguage { get; set; }
        public string Title { get; set; }
        public decimal Popularity { get; set; }
        public int VoteCount { get; set; }
        public decimal VoteAverage { get; set; }
        public int UserVote { get; set; }
        public string Note { get; set; }
        public int ExternalId { get; set; }

    }
}
