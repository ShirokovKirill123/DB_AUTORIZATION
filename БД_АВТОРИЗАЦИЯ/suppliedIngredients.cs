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
    
    public partial class suppliedIngredients
    {
        public int id { get; set; }
        public Nullable<int> supplier_id { get; set; }
        public Nullable<int> ingredient_id { get; set; }
        public Nullable<int> ingredient_quntity { get; set; }
        public Nullable<decimal> price { get; set; }
    
        public virtual Ingredients Ingredients { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}
