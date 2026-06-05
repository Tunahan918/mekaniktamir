namespace bakimonarim.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Yoneticiler")]
    public partial class Yonetici
    {
        [Key]
        public int YoneticiId { get; set; }

        [StringLength(50)]
        [Display(Name ="Kullanıcı Adı")]
        public string? KullaniciAdi { get; set; }

        [StringLength(50)]
        [Display(Name = "Şifre")]
        public string? Sifre { get; set; }

        [StringLength(50)]
        [Display(Name = "Görevi")]
        public string? Gorevi { get; set; }

        [StringLength(150)]
        [Display(Name = "Ad Soyad")]
        public string? AdiSoyadi { get; set; }
    }
}
