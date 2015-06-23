using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mem.Core.Domain.Catelog
{
    public class Category : BaseEntity<int>
    {
        public virtual string Name { get; set; }
        public virtual string TwoName { get; set; }
        public virtual string TestName { get; set; }
        public virtual string ThreeName { get; set; }
    }
}
