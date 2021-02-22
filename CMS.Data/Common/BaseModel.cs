using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Common
{
    public class BaseModel
    {
        public bool Deleted { get; set; }
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }

}
