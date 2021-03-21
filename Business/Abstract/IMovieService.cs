using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IMovieService
    {
        IResult Add(Movie movie);
        IResult Update(MovieForUpdateDto movieForUpdateDto);
        IDataResult<Movie> GetById(int id);
        IDataResult<List<Movie>> GetAll(int? pageNumber, int? pageSize);

    }
}
