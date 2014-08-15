using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Text;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.Build.Construction;

namespace CodeOwls.PowerShell.MSBuild
{
    public class ProjectElementPathNode : PathNodeBase
    {
        private readonly ProjectElement _element;
        private readonly int _index;

        public ProjectElementPathNode( ProjectElement element, int index )
        {
            _element = element;
            _index = index;
        }

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            var children = new List<IPathNode>();
            var container = _element as ProjectElementContainer;
            if (null != container)
            {
                int i = 0;
                children.AddRange(container.Children.ToList().ConvertAll(c => new ProjectElementPathNode(c, i++)));
            }

            return children;
        }

        public override string ItemMode
        {
            get { return EncodedItemMode; }
        }

        public override IPathValue GetNodeValue()
        {
            return new PathValue( _element, Name, IsContainer );
        }

        public override string Name
        {
            get { return _index.ToString( CultureInfo.InvariantCulture); }
        }

        bool IsContainer
        {
            get { return _element is ProjectElementContainer; }
        }

    }
}
