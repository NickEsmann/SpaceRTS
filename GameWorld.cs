using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace SpaceRTS
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map map;
        private Worker worker;
        private List<GameObject> gameObjects;
        private List<GameObject> Building;
        public static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        private bool DropDownMenu = false;
        List<Rectangle> rects = new List<Rectangle>();
        bool Clicked = false;
        Texture2D t;
        Vector2 currentMousPosition = new Vector2(Cursor.Position.X, Cursor.Position.Y);
        SpriteFont font;
        Vector2 textPos1;
        Vector2 textPos2;
        Vector2 textPos3;
        bool canPlace;
        Vector2 buildPos;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            worker = new Worker();
            gameObjects = new List<GameObject>();
            Building = new List<GameObject>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(this.Content);
            }
            map.LoadContent(Content);
            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White });
            sprites.Add("HQ", Content.Load<Texture2D>("HQ"));
            sprites.Add("Mine", Content.Load<Texture2D>("Mine"));
            font = Content.Load<SpriteFont>("font");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            gameObjects.AddRange(Building);
            Building.Clear();
            buildBuilding();

            Debug.WriteLine(canPlace);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            map.Draw(_spriteBatch);
            foreach (GameObject go in gameObjects)
            {
                go.Draw(_spriteBatch);
            }

            
            
            if (DropDownMenu)
            {
                foreach(Rectangle r in rects)
                {
                    _spriteBatch.Draw(t, r, Color.Black);
                }
                if(rects.Count > 1)
                {
                    _spriteBatch.DrawString(font, "1. HeadQuarter", textPos1, Color.White);
                    _spriteBatch.DrawString(font, "2. Mine", textPos2, Color.White);
                    _spriteBatch.DrawString(font, "3. Barack", textPos3, Color.White);
                }
                
            }
            

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void buildBuilding()
        {
            MouseState mouseClick = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            if (mouseClick.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && Clicked == false)
            {
                canPlace = true;
                for (int x = 0; x < 30; x++)
                {
                    for (int y = 0; y < 17; y++)
                    {
                        Rectangle rect = new Rectangle(x, y, 65, 65);

                        if (new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1).Intersects(new Rectangle(x * 65, y * 65, 65, 65)))
                        {
                            rects.Add(new Rectangle(x * 65, y * 65, 200, 50));
                            rects.Add(new Rectangle(x * 65, y * 65 + 50, 200, 50));
                            rects.Add(new Rectangle(x * 65, y * 65 + 100, 200, 50));

                            textPos1 = new Vector2(x * 65 + 75, y * 65 + 15);
                            textPos2 = new Vector2(x * 65 + 75, y * 65 + 65);
                            textPos3 = new Vector2(x * 65 + 75, y * 65 + 115);
                            
                            DropDownMenu = true;

                            Clicked = true;

                            buildPos = new Vector2(x * 65, y * 65);

                        }
                    }
                }
            }
            if (canPlace)
            {
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D1))
                {
                    Building.Add(new Headquarter(new Vector2(buildPos.X, buildPos.Y)));
                    rects.Clear();
                    Clicked = false;
                }
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D2))
                {
                    //Building.Add(new Headquarter(new Vector2(buildPos.X, buildPos.Y)));
                    rects.Clear();
                    Clicked = false;
                }
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D3))
                {
                    //Building.Add(new Headquarter(new Vector2(buildPos.X, buildPos.Y)));
                    rects.Clear();
                    Clicked = false;
                }
            }

            if (rects.Count > 3)
            {
                rects.RemoveRange(3, rects.Count - 3);
            }
            if(mouseClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                rects.Clear();
                Clicked = false;
                canPlace = false;
            }
        }
    }
}