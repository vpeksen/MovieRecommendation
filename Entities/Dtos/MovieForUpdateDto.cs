using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class MovieForUpdateDto:IDto
    {
        public int Id { get; set; }
        public int Vote { get; set; }
        public string Note { get; set; }
    }
}
