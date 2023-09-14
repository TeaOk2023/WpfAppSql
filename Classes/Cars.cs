using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace WpfAppSql1
{
    public class Company
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString() => $"{Title}";
        public List<Car> Cars { get; set; } = new(); // сотрудники компании
    }
    public class Car  //user
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public int year { get; set; }
        public int probeg { get; set; }


        public int CompanyId { get; set; }

        public Company Company { get; set; }

    }

 
}
