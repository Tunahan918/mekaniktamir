using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bakimonarim.Data
{
    [Table("Urunler")]
    public class Urun
    {
        [Key]
        public int UrunId { get; set; }

        [StringLength(500)]
        public string? Adi { get; set; }

        [StringLength(250)]
        public string? vitrinresim { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Aciklama { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true),Required]
        public DateTime Tarih { get; set; }

        [Required]
        public bool Onay { get; set; }

        public int Sira { get; set; }

        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        [ValidateNever]
        public virtual Kategori Kategori { get; set; }
        public Urun()
        {
            Onay = true;
            Tarih = DateTime.Now;
            Sira = 0;
        }

    }

}