//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImagePrint.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public short ImageId { get; set; }
        public int NumberOfPrints { get; set; }
        public int SizeId { get; set; }
    
        public virtual Image Image { get; set; }
        public virtual Order Order { get; set; }
        public virtual Size Size { get; set; }
    }
}
