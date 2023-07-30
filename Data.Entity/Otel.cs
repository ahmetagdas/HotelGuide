namespace Data.Entity;

public class Otel
{
    public int Id { get; set; }
    public string? Ad { get; set; }
    public string? Adres { get; set; }
    public string? Sehir { get; set; }
    public string? Ulke { get; set; }
    public string? Telefon { get; set; }
    public string? Eposta { get; set; }
    public string? WebSitesi { get; set; }
    public string? Aciklama { get; set; }
    public string?OdaSayisi { get; set; }

    public ICollection<IletisimBilgisi>? IletisimBilgileri { get; set; }
    public ICollection<OtelYetkilisi>? Yetkililer { get; set; }
    public ICollection<Rapor>? Raporlar { get; set; }
}
