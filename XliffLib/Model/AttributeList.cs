using System;
using System.Collections.Generic;
using System.Linq;
using Localization.Xliff.OM.Core;
using Localization.Xliff.OM.Modules.Metadata;

namespace XliffLib.Model
{
    public class AttributeList : Dictionary<string, string>
    {
        public AttributeList()
        {
        }

        public MetadataContainer ToXliffMetadata()
        {
            var metadata = new MetadataContainer();

            var defaultGroup = new MetaGroup()
            {
                Id = "XliffLib"
            };

            foreach (var attribute in this)
            {
                var metadataItem = new Meta(attribute.Key, attribute.Value);
                defaultGroup.Containers.Add((metadataItem));
            }

            metadata.Groups.Add(defaultGroup);
            return metadata;
        }

        public void FromXliff(TranslationContainer container)
        {
            if (container.Metadata != null && container.Metadata.HasGroups)
            {
                var metaGroup = container.Metadata.Groups.Where(x => x.Id == "XliffLib")
                    .FirstOrDefault();
                if (metaGroup != null)
                {
                    foreach (var metaItem in metaGroup.Containers)
                    {
                        var meta = metaItem as Meta;
                        this.Add(meta.Type, meta.NonTranslatableText);
                    }
                }
            }
        }
    }
}
