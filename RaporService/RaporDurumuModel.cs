namespace RaporService
{
    public class RaporDurumuModel
    {
        public int Id { get; set; }
        public int RaporId { get; set; }
        public RaporDurumu Durum { get; set; }
    }

    public enum RaporDurumu
    {
        Hazirlaniyor,
        Tamamlandi
    }

}
