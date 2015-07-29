namespace mdl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Catalog()
        //{
        //    Catalogs1 = new HashSet<Catalog>();
        //    Products = new HashSet<Product>();
        //}

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentId { get; set; }


                //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Catalog> Catalogs1 { get; set; }

        public virtual Catalog Catalog1 { get; set; }

     //   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Catalog> ChildCatalog { get; set; }
    }
}
