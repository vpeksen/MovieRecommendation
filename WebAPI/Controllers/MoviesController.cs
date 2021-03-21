using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private IMovieService _movieService;
        private IMailService _mailService;
        public MoviesController(IMovieService movieService, IMailService mailService)
        {
            _movieService = movieService;
            _mailService = mailService;
        }

        [HttpPost("update")]
        public ActionResult Update(MovieForUpdateDto movieForUpdateDto)
        {
            var result = _movieService.Update(movieForUpdateDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public ActionResult GetById(int id)
        {
            var result = _movieService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getall")]
        public ActionResult GetAll(int? pageNumber, int? pageSize)
        {
            var result = _movieService.GetAll(pageNumber, pageSize);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("advice")]
        public ActionResult Advice(int movieId, string to)
        {
            string badRequestMessage = "";
            var movie = _movieService.GetById(movieId);
            
            if (movie.Success)
            {
                var result = _mailService.SendMail(movie.Data.Title, to);
                if (result.Success)
                {
                    return Ok(result.Message);
                }
                else
                {
                    badRequestMessage = result.Message;
                }
            }
            else
            {
                badRequestMessage = movie.Message;
            }
            

            return BadRequest(badRequestMessage);
        }
    }
}
