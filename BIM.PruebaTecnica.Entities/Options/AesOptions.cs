namespace BIM.PruebaTecnica.Entities.Options;
public class AesOptions
{
    public const string SectionKey = nameof(AesOptions);
    public string Clave { get; set; }
    public string AesIV { get; set; }
}
