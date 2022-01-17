using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenersMultitool.Domain.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
