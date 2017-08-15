using System;
using Localization.Xliff.OM.Core;

namespace XliffLib.Model
{
    public abstract class PropertyContainer : ContentElement
    {
        public PropertyContainer(string name) : base()
        {
            Name = name;
        }
        public string Name { get; set; }

        public abstract TranslationContainer ToXliff(IdCounter counter);


        public static PropertyContainer FromXliff(TranslationContainer container, bool loadFromSource = false)
        {
            if (container is Group)
                return PropertyGroup.FromXliff(container, loadFromSource);
            if (container is Unit)
                return Property.FromXliff(container, loadFromSource);
            return null;
        }
    }
}