using System;
using System.Security.Cryptography;
using System.Text;
using SplashKitSDK;

namespace DemoGame
{
    static class Screen
    {
        public static int WIDTH = 1000;
        public static int HEIGHT = 700;
        public static float GRAVITY = 10;
    }
    
    public class Program
    {
        
        public static bool CheckCollision(Object a, Object b)
        {
            if(a == null | b == null)
            {
                return false;
            }else
            if (
                a.PosY + a.Height >= b.PosY &&
                a.PosX <= b.PosX + b.Width &&
                a.PosX + a.Width >= b.PosX &&
                a.PosY <= b.PosY + b.Height
                )
            {return true;}
            else
            {return false;}   
        }
        public static bool CheckCollision1(Player a, Object b)
        {
            if (a == null | b == null)

            {
                return false;
            }
            else
            if (
                a.PosY + a.Height >= b.PosY 
                //a.PosX <= b.PosX + b.Width &&
                //a.PosX + a.Width >= b.PosX &&
                //a.PosY <= b.PosY + b.Height
                )
            { return true; }
            else
            { return false; }
        }
        public static Obstacle CollideWith(Player p, Map m)
        {
            for(int i = 0; i < m.Obstacles.Count(); i++) { 
                if (CheckCollision(p, m.Obstacles[i]))
                {
                    return m.Obstacles[i];
                }
            

            }
            return null;
        }
        public static void Gravity(Object d, Player p)
        {
            if (d == null && !p.IsJumped)
            {
                p.PosY += Screen.GRAVITY;
                return;
            }
            if (CheckCollision(d, p) && p.PosY + p.Height >= d.PosY)
            {
                p.PosY -= (p.PosY + p.Height -d.PosY);
            }
            if ( !CheckCollision(d,p) && !p.IsJumped)
            {
                    p.PosY += Screen.GRAVITY;
              
            }
            else
            {
                if (p.IsJumped)
                {
                    p.Jump();
                }

            }
        }
        public static void Main()
        {
            
            Window myWindow = new Window("Demo", Screen.WIDTH, Screen.HEIGHT);
            Map map1 = new Map();

            Obstacle Dirt = new Obstacle(Color.DarkKhaki, 0, Screen.HEIGHT-180, Screen.WIDTH, 180);
            Obstacle Dirt2 = new Obstacle(Color.Black, 0, 379, 200, 10);
            Obstacle Dirt3 = new Obstacle(Color.Black, Screen.WIDTH - 200,379, 200,10);
            Obstacle Dirt4 = new Obstacle(Color.Black, Screen.WIDTH / 2 - 205, 220, 410, 10);
            map1.AddObstacle(Dirt);
            map1.AddObstacle(Dirt2);
            map1.AddObstacle(Dirt3);
            map1.AddObstacle(Dirt4);

            Player p1 = new Player();
            p1.IsCollided = Dirt;

            Player p2 = new Player(Color.Red,Screen.WIDTH -50,50);
            p2.IsCollided=Dirt;
            bool GameOver = false;

            do
            {
                
                SplashKit.ProcessEvents();
                p1.IsCollided = CollideWith(p1, map1);
                for (int i = 0; i < map1.Obstacles.Count(); i++)
                {

                    if (CheckCollision(p2, map1.Obstacles[i]))
                    {
                        p2.IsCollided = map1.Obstacles[i];
                        break;
                    }
                    else
                    {
                        p2.IsCollided = null;
                    }
                }

                Gravity(p1.IsCollided, p1);
                Gravity(p2.IsCollided, p2);

                p1.Shoot(p2);
                p2.Shoot(p1);

                
                if (SplashKit.KeyTyped(KeyCode.DKey) )
                {
                    p1.LeftSide = false;
                }
                
                if (SplashKit.KeyDown(KeyCode.DKey) )
                {
                    p1.RunRight();
                }
                if (SplashKit.KeyDown(KeyCode.AKey) )
                {
                    p1.RunLeft();
                }
                if (SplashKit.KeyTyped(KeyCode.AKey))
                {
                    p1.LeftSide = true;
                }
                if (SplashKit.KeyTyped(KeyCode.WKey ) && CheckCollision(p1.IsCollided,p1))
                {

                    p1.IsJumped = true;
                
                }
                if (SplashKit.KeyTyped(KeyCode.JKey))
                {
                  
                    p1.Charge();
                }

                if (SplashKit.KeyTyped(KeyCode.RightKey))
                {
                    p2.LeftSide = false;
                }
                if (SplashKit.KeyDown(KeyCode.RightKey))
                {
                    p2.RunRight();
                }
                if (SplashKit.KeyTyped(KeyCode.LeftKey))
                {
                    p2.LeftSide= true;
                }
                if (SplashKit.KeyDown(KeyCode.LeftKey))
                {
                    p2.RunLeft();
                }
                if (SplashKit.KeyTyped(KeyCode.UpKey) && CheckCollision(p2, p2.IsCollided))
                {

                    p2.IsJumped = true;

                }
                if (SplashKit.KeyTyped(KeyCode.MKey))
                {
                    p2.Charge();
                }

                map1.DisplayMap();
                p1.Draw();
                p2.Draw();

                if(p2.Health == 0 || p1.Health == 0)
                {
                    GameOver = true;
                }
                if (GameOver == true)
                {
                    do
                    {

                        string win = (p1.Health == 0)? "p2 Win" : "p1 Win";
                        SplashKit.ProcessEvents();
                        SplashKit.RefreshScreen(60);
                        SplashKit.DrawRectangle(Color.Black,Screen.WIDTH/2-200/2, Screen.HEIGHT/2,200,50);
                        SplashKit.DrawText(win, Color.Black, myWindow.Width / 2-10, myWindow.Height/2 + 25 );
                        if (SplashKit.KeyTyped(KeyCode.QKey))
                        {
                            myWindow.Close();
                        }
                        if (SplashKit.KeyTyped(KeyCode.RKey))
                        {
                            Console.WriteLine(GameOver);
                            GameOver = false;
                            p1.ResetHealth();
                            p1.PosX = 50;
                            p1.PosY = 50;
                            p2.ResetHealth();
                            p2.PosX = Screen.WIDTH - 50;
                            p2.PosY = 50;
                            SplashKit.ClearScreen();
                            break;
                        }

                    } while (!myWindow.CloseRequested);
                }

                SplashKit.RefreshScreen(60);
                SplashKit.ClearScreen();

            } while (!myWindow.CloseRequested);
        }
    }
}
