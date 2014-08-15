using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;
using CodeOwls.PowerShell.Paths.Processors;
using CodeOwls.PowerShell.Provider;

namespace CodeOwls.PowerShell.MSBuild
{
    [CmdletProvider("MSBuild", ProviderCapabilities.ShouldProcess)]
    public class MSBuildProvider : CodeOwls.PowerShell.Provider.Provider
    {
        protected override IPathResolver PathResolver
        {
            get { return new MSBuildPathResolver(); }
        }
    }
}
