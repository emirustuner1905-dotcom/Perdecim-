namespace PerdeCim.Models;

public class Perde
{
    public int PerdeId { get; set; }
    public string PerdeAdi { get; set; } = null!;
    public string PerdeModeli { get; set; } = null!; 
    public string PerdeGorsel { get; set; } = null!;
    public string PerdeAciklama { get; set; } = null!;
    public string KumasTuru { get; set; } = null!; 
    
    public decimal PerdeFiyati { get; set; } 
    public int? IndirimOrani { get; set; } 
    public int StokAdedi { get; set; }
    
}