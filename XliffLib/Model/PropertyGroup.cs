using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Localization.Xliff.OM.Core;
using Localization.Xliff.OM.Modules.Metadata;

namespace XliffLib.Model
{
    public class PropertyGroup : PropertyContainer
    {
        public PropertyGroup(string name) : base(name)
        {
            Containers = new List<PropertyContainer>();
        }
        public IList<PropertyContainer> Containers { get; private set; }

        public static new PropertyContainer FromXliff(TranslationContainer xliffGroup, bool loadFromSource = false)
        {
            var group = xliffGroup as Group;
            var propertyGroup = new PropertyGroup(xliffGroup.Name);

            propertyGroup.Attributes.FromXliff(xliffGroup);

            foreach (var container in group.Containers)
            {
                propertyGroup.Containers.Add(PropertyContainer.FromXliff(container, loadFromSource));
            }

            return propertyGroup;
        }

        public override TranslationContainer ToXliff(IdCounter counter)
        {
            var id = "g" + (counter.GetNextGroupId());
            var xliffGroup = new Group(id)
            {
                Name = this.Name
            };

            if (this.Attributes.Count > 0)
            {
                xliffGroup.Metadata = this.Attributes.ToXliffMetadata();
            }

            foreach (var container in Containers)
            {
                var xliffContainer = container.ToXliff(counter);
                xliffGroup.Containers.Add(xliffContainer);
            }
            return xliffGroup;
        }
    }
}
