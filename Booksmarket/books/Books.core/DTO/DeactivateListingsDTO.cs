using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.DTO
{
    public class DeactivateListingsDTO
    {
        public int ListingId { get; set; } /* מזהה מודעה*/
        public bool IsActiv { get; set; } /*אם המודעה פעילה*/
    }
}
