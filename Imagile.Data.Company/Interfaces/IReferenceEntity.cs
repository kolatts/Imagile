using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Data.Company.Interfaces;
public interface IReferenceEntity<TEntity>
{
    static abstract List<TEntity> GetSeedData();
}
