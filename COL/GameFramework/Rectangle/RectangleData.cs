using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework.Rectangle
{
    public class RectangleData
    {
        //Các yếu tố cần có trong file Rectangle XML.
        public int id { get; set; } //ID của Rectangle
        public string properties { get; set; } //Thuộc tính khi va chạm, vd: die = chết,...
        public int x { get; set; } //Tọa độ X của Rectangle.
        public int y { get; set; } //Tọa độ Y của Rectangle.
        public int width { get; set; } //Chiều rộng của Rectangle.
        public int height { get; set; } //Chiều cao của Rectangle.
    }
}
