namespace PerdeCim.Models;

public class Satici
{
    
    public int SaticiId { get; set; }
    public string? SaticiAdi { get; set; }
    public string? SaticiCv { get; set; }
    public string? SaticiFoto { get; set; }

    
    public Satici(int saticiId, string? saticiAdi, string? saticiCv, string? saticiFoto)
    {
        this.SaticiId = saticiId;
        this.SaticiAdi = saticiAdi;
        this.SaticiCv = saticiCv;
        this.SaticiFoto = saticiFoto;
    }

    
    public Satici() { }
}