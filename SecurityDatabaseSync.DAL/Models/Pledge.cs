using System;

namespace SecurityDatabaseSync.DAL.Models
{
    /// <summary>
    /// Залог.
    /// </summary>
    public class Pledge
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Prizn { get; set; }
        public string ID { get; set; }
        public int? Client { get; set; }
        public string Otd { get; set; }
        public double? Oper { get; set; }
        public DateTime? DateOld { get; set; }
        public DateTime? Zdate { get; set; }
        public short? Numofday { get; set; }
        public DateTime? Vdate { get; set; }
        public double? PerSsud { get; set; }
        public double? PerStrah { get; set; }
        public double? PerShtraf { get; set; }
        public double? PriceOcen { get; set; }
        public double? Ssud { get; set; }
        public double? Vikup { get; set; }
        public DateTime? DateFact { get; set; }
        public byte? Tved { get; set; }
        public byte? Categ { get; set; }
        public string Def1 { get; set; }
        public string Def2 { get; set; }
        public string Izd { get; set; }
        public string Probe { get; set; }
        public double? Nweight { get; set; }
        public double? Bweight { get; set; }
        public double? Serv { get; set; }
        public DateTime? TimeZal { get; set; }
        public short? OperNum { get; set; }
        public string Act { get; set; }
        public double? PriceGr { get; set; }
        public byte? Prz { get; set; }
        public DateTime? TimeVik { get; set; }
        public double? AddPSsud { get; set; }
        public double? AddPStrah { get; set; }
        public int? CutDays { get; set; }
        public DateTime? DateZalog { get; set; }
        public int? numbso { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
