using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class Brand:IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string?  Description { get; set; }// that's not required.
        public string? Logo { get; set; } // prefer nullable for Logo area
        [Display(Name = "Aktif?")]
        public bool IsActive {  get; set; } // you can do active or passive for users
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }  // ıt's create date for brand
        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }
        public IList<Product> Products { get; set; } // a brand can have more than one product
    }
}
