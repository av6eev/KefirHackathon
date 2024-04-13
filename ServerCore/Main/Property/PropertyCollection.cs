using System.Collections.Generic;

namespace ServerCore.Main.Property
{
    public class PropertyCollection
    {
        public readonly List<IProperty> Properties = new();

        public IntProperty CreateIntProperty(string id, int defaultValue)
        {
            var property = new IntProperty(id, defaultValue);
            Properties.Add(property);
            return property;
        }

        public FloatProperty CreateFloatProperty(string id, float defaultValue)
        {
            var property = new FloatProperty(id, defaultValue);
            Properties.Add(property);
            return property;
        }

        public DoubleProperty CreateDoubleProperty(string id, double defaultValue)
        {
            var property = new DoubleProperty(id, defaultValue);
            Properties.Add(property);
            return property;
        }

        public CharProperty CreateCharProperty(string id, char defaultValue)
        {
            var property = new CharProperty(id, defaultValue);
            Properties.Add(property);
            return property;
        }

        public StringProperty CreateStringProperty(string id, string defaultValue)
        {
            var property = new StringProperty(id, defaultValue);
            Properties.Add(property);
            return property;
        }

        public BoolProperty CreateBoolProperty(string id, bool defaultValue)
        {
            var property = new BoolProperty(id, defaultValue);
            Properties.Add(property);
            return property;
        }
    }
}