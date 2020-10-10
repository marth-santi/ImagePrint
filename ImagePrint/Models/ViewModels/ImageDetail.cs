using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagePrint.Models.ViewModels
{
    public class ImageDetail
    {
        public OrderDetail OrderDetail { get; set; }
        public Image Image { get; set; }
        public Size Size { get; set; }
    }
}