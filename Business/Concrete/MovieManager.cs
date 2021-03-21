using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    [SecuredOperation]
    public class MovieManager : IMovieService
    {
        IMovieDal _movieDal;
        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;
        }

        public IResult Add(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Movie>> GetAll(int? pageNumber, int? pageSize)
        {
            try
            {
                return new SuccessDataResult<List<Movie>>(_movieDal.GetListWithPagination(pageNumber, pageSize).ToList());
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<Movie>>(e.Message);
            }
        }

        public IDataResult<Movie> GetById(int id)
        {
            try
            {
                return new SuccessDataResult<Movie>(_movieDal.Get(p => p.Id == id));
            }
            catch (Exception e)
            {
                return new ErrorDataResult<Movie>(Messages.MovieUpdated);
            }            
        }
        [ValidationAspect(typeof(MovieForUpdateDtoValidator), Priority = 1)]
        public IResult Update(MovieForUpdateDto movieForUpdateDto)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdExists(movieForUpdateDto.Id));

            if (result != null)
            {
                return result;
            }

            var movie = _movieDal.Get(p => p.Id == movieForUpdateDto.Id);

            movie.UserVote = movieForUpdateDto.Vote;
            movie.Note = movieForUpdateDto.Note;            

            _movieDal.Update(movie);
            return new SuccessResult(Messages.MovieUpdated);
        }

        private IResult CheckIfMovieIdExists(int id)
        {

            var result = _movieDal.Get(p => p.Id == id);
            if (result == null)
            {
                return new ErrorResult(Messages.MovieNotFound);
            }

            return new SuccessResult();
        }
    }
}
