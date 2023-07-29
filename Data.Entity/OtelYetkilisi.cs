namespace Data.Entity;
public class OtelYetkilisi
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string FirmaUnvan { get; set; }
    public int OtelId { get; set; }
    public Otel Otel { get; set; }
}
