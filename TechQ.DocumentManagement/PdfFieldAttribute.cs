namespace TechQ.DocumentManagement;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class PdfFieldAttribute : Attribute
{
    public PdfFieldAttribute(string fieldName) => FieldName = fieldName;
    public string FieldName { get; }
}