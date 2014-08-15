using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.PowerShell.MSBuild
{
    public class MSBuildPathResolver : CodeOwls.PowerShell.Paths.Processors.PathResolverBase
    {
        public override IEnumerable<IPathNode> ResolvePath(PowerShell.Provider.PathNodeProcessors.IProviderContext context, string path)
        {
            context.WriteDebug(String.Format("Resolving path [{0}] drive [{1}]", path, context.Drive));
            string filePath = Regex.Replace(path, @"^[^::]+::", String.Empty);
            if (null != context.Drive && !String.IsNullOrEmpty(context.Drive.Root))
            {
                var re = new Regex("^.*(" + Regex.Escape(context.Drive.Root) + ")(.*)$", RegexOptions.IgnoreCase);
                var matches = re.Match(path);
                filePath = matches.Groups[1].Value;
                path = matches.Groups[2].Value; ;
            }
            _rootPathNode = new ProjectRootPathNode( filePath );

            return base.ResolvePath(context, path);
        }

        IPathNode _rootPathNode;
        protected override IPathNode Root
        {
            get
            {
                return _rootPathNode;
            }
        }
    }
}