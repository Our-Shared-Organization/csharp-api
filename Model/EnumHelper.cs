namespace whatever_api.Model;

[AttributeUsage(AttributeTargets.Property,AllowMultiple=true)]
public class EnumHelper : Attribute
{
    public Type EnumType;
    public EnumHelper(Type enumType)
    {
        EnumType = enumType;
    }
}