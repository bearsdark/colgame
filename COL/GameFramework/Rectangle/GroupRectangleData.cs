using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework.Rectangle
{
    public class GroupRectangleData
    {
        //Các yếu tố có trong file GroupRectangle XML.
        public int id { get; set; } //ID của Group.
        public string properties { get; set; } //Thuộc tính của Group. Ví dụ: va chạm vào Player sẽ mất máu thì thuộc tính là "die".
    }
}
