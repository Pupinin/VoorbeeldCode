using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.DomainClasses;

namespace Aon18.data.Interfaces
{
   public interface ISkeletDbRepository
    {
        IEnumerable<Skelet> GetAllSkelets();
        Skelet AddSkelet(Skelet skelet);
        void DeleteById(int id);
        Skelet GetById(int id);
    }
}
