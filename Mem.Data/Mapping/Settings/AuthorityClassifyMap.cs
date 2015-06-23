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
    public class AuthorityClassifyMap : EntityTypeConfiguration<SettingsAuthorityClassify>
    {
        public AuthorityClassifyMap()
        {
            ToTable("SettingsAuthorityClassify");
            HasKey(c => c.Id).Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
