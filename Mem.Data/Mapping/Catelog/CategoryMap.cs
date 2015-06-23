using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Mem.Core.Domain.Catelog;
namespace Mem.Data.Mapping
{
    public class CategoryMap :EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Category");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(50);
        }
    }
}
