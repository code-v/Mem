using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Mem.Core.Domain.Settings;

namespace Mem.Data.Mapping.Settings
{
    public class SiteMenuMap : EntityTypeConfiguration<SettingsSiteMenu>
    {
        public SiteMenuMap()
        {
            ToTable("SettingsSiteMenu");
            HasKey(c => c.Id).Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(c => c.Name).IsRequired().HasMaxLength(50);
            Property(c => c.Content).HasMaxLength(500);
            Property(c => c.ImageUrl).HasMaxLength(500);
        }
    }
}
