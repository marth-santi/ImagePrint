using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagePrint.Models.ViewModels
{
    public class PrintViewModel
    {
        public Order UserOrder { get; set; }
        public OrderDetail UploadedDetail { get; set; }
        public List<ImageDetail> ImageDetailList { get; set; }
    }
}