using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.ModelsVM
{
    public class CultureVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResourceVM> Resources { get; set; }
        public CultureVM()
        {
            Resources = new List<ResourceVM>();
        }
    }
}
