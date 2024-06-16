using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
namespace DemoGame
{
    public abstract class Object
    {
        private float _posX;
        private float _posY;
        private float _width;
        private float _height;
        protected Color _color;


        
        public Object(Color color , float posX, float posY, float width, float height) 
        {
            _color = color;
            _posX=posX;
            _posY=posY;
            _width=width;
            _height=height;
        }
        public Object(Color color ,float width, float height) : this(color,0,0,width,height)
        {
            
        }
 
        public Color Color { get { return _color; } set { _color = value; } }
        public float PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }
        public float PosY { get { return _posY; } set { _posY = value; } }

        public float Width { get { return _width; } set { _width = value; } }
        public float Height { get { return _height; } set { _height = value; } }

        public abstract void Draw();
        
    }
}
