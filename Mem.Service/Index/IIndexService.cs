using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mem.Core;
using Mem.Core.Domain.Catelog;
using Mem.Core.Domain.Settings;
namespace Mem.Service.Index
{
    public interface IIndexService :IDependency
    {
        string GetTestName();
        List<Category> GetTestTable();
    }
}
