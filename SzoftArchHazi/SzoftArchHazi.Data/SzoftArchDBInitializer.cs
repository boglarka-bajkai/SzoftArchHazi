using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzoftArchHazi.Data
{
    public class SzoftArchDBInitializer : DropCreateDatabaseAlways<SzoftArchContext>
    {
        protected override void Seed(SzoftArchContext context)
        {
            EmployeeRepository.CreateEmployeesInDb(context);
            ProjectRepository.CreateProjectsInDb(context);
            OnDutyRepository.CreateDutiesInDb(context);
            OnDutyDateRepository.CreateOnDutyDaysInDb(context);

            base.Seed(context);
        }
    }
}
