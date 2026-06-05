using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static bakimonarim.Data.AppDbContext;

namespace bakimonarim.Data
{
    public class Kategori
    {
        [Key]
        public int KategorId {get;set;}

        public string? Ad { get; set; }
        public string? Seo{get;set;}
        public byte Sira { get; set; }
        
        [ValidateNever]
        public virtual required List<Urun> Urunler { get; set; }
        
    }
}