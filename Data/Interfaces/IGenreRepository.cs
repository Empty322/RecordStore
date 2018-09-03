using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IGenreRepository : IRepository<Genre, string>
    {
    }
}
