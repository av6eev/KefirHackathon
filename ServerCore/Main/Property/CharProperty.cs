namespace ServerCore.Main.Property
{
    public class CharProperty : Property<double>
    {
        public CharProperty(string key, char defaultValue) : base(key, defaultValue) {}
    }
}