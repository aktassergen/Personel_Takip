using SA.PTM.Entity.Abstruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
