using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Camera
{
    public class DetectCamera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return this.transform; }
        }
        public Vector2 Centre
        {
            get { return this.centre; }
        }

        private Vector2 centre;
        private Viewport viewport;

        public DetectCamera(Viewport newviewport)
        {
            this.viewport = newviewport;
        }

        public void Update(Vector2 position, int bgWidth, int bgHeight)
        {
            if (position.X < this.viewport.Width / 2)
                this.centre.X = this.viewport.Width / 2;
            else if (position.X > bgWidth - (this.viewport.Width / 2))
                this.centre.X = bgWidth - (this.viewport.Width / 2);
            else
                this.centre.X = position.X;

            if (position.Y < this.viewport.Height / 2)
                this.centre.Y = this.viewport.Height / 2;
            else if (position.Y > bgHeight - (this.viewport.Height / 2))
                this.centre.Y = bgHeight - (this.viewport.Height / 2);
            else
                this.centre.Y = position.Y;
            this.transform = Matrix.CreateTranslation(new Vector3(-this.centre.X + (this.viewport.Width / 2),
                                                                  -this.centre.Y + (this.viewport.Height / 2), 0));
        }
    }
}
