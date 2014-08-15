using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.Build.Construction;

namespace CodeOwls.PowerShell.MSBuild
{
    public class ProjectRootPathNode : PathNodeBase
    {
        private readonly string _filePath;
        private readonly ProjectRootElement _element;

        public ProjectRootPathNode( string filePath )
        {
            _filePath = filePath;
            _element = ProjectRootElement.Open(_filePath);
        }

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            var children = new List<IPathNode>();

            int i = 0;
            children.AddRange( _element.Children.ToList().ConvertAll(c => new ProjectElementPathNode(c, i++)) );

            return children;
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue( _element, Name );
        }

        public override string Name
        {
            get { return Path.GetFileNameWithoutExtension(_filePath); }
        }
    }
}