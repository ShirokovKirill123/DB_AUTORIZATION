//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bakery_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.OrderedProducts = new HashSet<OrderedProducts>();
            this.ProductСomposition = new HashSet<ProductСomposition>();
        }
    
        public int id { get; set; }
        public string pName { get; set; }
        public string ptype { get; set; }
        public Nullable<decimal> pricePerUnit { get; set; }
        public Nullable<int> outputVolume { get; set; }
        public Nullable<System.DateTime> shelflife { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductСomposition> ProductСomposition { get; set; }
    }
}
