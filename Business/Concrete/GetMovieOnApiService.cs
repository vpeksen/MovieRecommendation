using Core.ScheduledJobs;
using DataAccess.Abstract;
using Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GetMovieOnApiService : HostedService
    {
        HttpClient restClient;
        IMovieDal _movieDal;
        public GetMovieOnApiService(IMovieDal movieDal)
        {
            _movieDal = movieDal;
            restClient = new HttpClient();
        }
        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                for (int i = 1; i <= 50; i++)
                {
                    var baseAddress = new Uri("http://api.themoviedb.org/3/discover/movie?api_key=a2c427f8d25c015f4910a6ce282c6ec9&language=tr-TR&page="+i);
                    var response = await restClient.GetAsync(baseAddress);
                    if (response.IsSuccessStatusCode)
                    {
                        var fact = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<RootObject>(fact);

                        foreach (var result in model.results)
                        {
                            Movie movie = new Movie
                            {
                                Adult = result.adult,
                                OriginalTitle = result.original_title,
                                ReleaseDate = result.release_date,
                                Popularity = result.popularity,
                                Title = result.title,
                                VoteAverage = result.vote_average,
                                VoteCount = result.vote_count,
                                ExternalId = result.id,
                                Overview = result.overview,
                                OriginalLanguage = result.original_language
                            };

                            var checkMovie = _movieDal.Get(p => p.ExternalId == result.id);
                            if (checkMovie == null)
                            {
                                _movieDal.Add(movie);
                            }
                        }

                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(10), cToken);
            }
        }
        public class Result
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public int id { get; set; }
            public string overview { get; set; }
            public string original_title { get; set; }
            public string original_language { get; set; }
            public string release_date { get; set; }
            public string poster_path { get; set; }
            public decimal popularity { get; set; }
            public string title { get; set; }
            public bool video { get; set; }
            public decimal vote_average { get; set; }
            public int vote_count { get; set; }
        }

        public class RootObject
        {
            public int page { get; set; }
            public List<Result> results { get; set; }
            public int total_pages { get; set; }
            public int total_results { get; set; }
        }
    }
}
