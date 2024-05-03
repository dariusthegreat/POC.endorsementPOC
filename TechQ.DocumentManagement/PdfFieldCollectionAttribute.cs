namespace TechQ.DocumentManagement;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class PdfFieldCollectionAttribute   : Attribute { }
