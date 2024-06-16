using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame
{
    public class Obstacle : Object
    {
        public Obstacle(Color color, float posX, float posY, float width, float height) : base(color, posX, posY, width, height) 
        {
           
        }
 
        public override void Draw()
        {
            SplashKit.FillRectangle(Color, PosX, PosY, Width, Height);
        }
    }
}
