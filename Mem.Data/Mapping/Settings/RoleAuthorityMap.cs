using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

using Mem.Core.Domain.Settings;

namespace Mem.Data.Mapping.Settings
{
    public class RoleAuthorityMap : EntityTypeConfiguration<SettingsRoleAuthority>
    {
    }
}
