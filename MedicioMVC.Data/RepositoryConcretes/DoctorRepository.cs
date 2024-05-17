using MedicioMVC.Core.Models;
using MedicioMVC.Core.RepositoryAbstract;
using MedicioMVC.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicioMVC.Data.RepositoryConcretes
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
