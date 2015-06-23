using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminMemory.Models.Catalog
{
    public class CategoryModel
    {
        public long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string TwoName { get; set; }
    }
}