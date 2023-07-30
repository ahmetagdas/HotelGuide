namespace Data.Entity;

public class IletisimBilgisi
{
    public int Id { get; set; }
    public string? BilgiTipi { get; set; }
    public string? BilgiIcerigi { get; set; }
    public string? Adres { get; set; }
    public string? Telefon { get; set; }
    public int? OtelId { get; set; }
    public Otel? Otel { get; set; }
}
