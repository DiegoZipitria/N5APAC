using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Domain
{
    public class StudentClaim
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLogged { get; set; }
        public int Age { get; set; }

    }
}
