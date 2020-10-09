using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagePrint.Models.ViewModels
{
    public class PrintViewModel
    {
        public Order UserOrder { get; set; }
        public Image UploadedImage { get; set; }
        public OrderDetail UploadedDetail { get; set; }
        public List<OrderDetail> OrderDetailList { get; set; }
    }
}