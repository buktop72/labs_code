using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TvStore.Models

{
    public interface IRepository
    {
        IEnumerable<Tv> GetAll();
        Tv Get(int id);
        void Create(Tv tv);
    }
}
