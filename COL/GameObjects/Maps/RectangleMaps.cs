using COL.GameFramework.Rectangle;
using COL.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Maps
{
    public class RectangleMaps : GameObject
    {
        private List<RectangleData> rectangleData = new List<RectangleData>(); //List chứa các thông tin của Rectangle.

        public List<RectangleData> GetRectData //Hàm dùng để Get List Rectangle.
        {
            get { return this.rectangleData; }
        }

        public RectangleMaps(Game game, string xmlFileRectangle) //Sẽ chạy khi vừa khởi tạo Object bên Screen.
            : base(game)
        {
            this.rectangleData = DataHelpers.GetDataContent<List<RectangleData>>(xmlFileRectangle); //Lấy thông tin từ file XML và gán vào this.rectangleData.
        }
    }
}
