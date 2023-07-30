namespace Data.Entity;

public class Rapor
{
    public int Id { get; set; }
    public DateTime TalepEdildigiTarih { get; set; }
    public string? Durum { get; set; }
    public int? OtelId { get; set; }
    public Otel? Otel { get; set; }
    public OtelYetkilisi? OtelYetkilisi { get; set; }
}
