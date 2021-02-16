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
        private Mine mine;
        private List<GameObject> miner;
        private List<GameObject> gameObjects;
        private List<GameObject> Building;
        public static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        private bool IsClicked = false;
        private bool Clicked = false;
        private Texture2D t;
        private Vector2 currentMousPosition = new Vector2(Cursor.Position.X, Cursor.Position.Y);
        private bool DropDownMenu = false;
        private List<Rectangle> rects = new List<Rectangle>();
        private SpriteFont font;
        private Vector2 textPos1;
        private Vector2 textPos2;
        private Vector2 textPos3;
        private Vector2 textPos4;
        private bool canPlace;
        private Vector2 buildPos;
        private bool HQPlaced = false;
        private Vector2 HGText;
        private SpriteFont headLine;
        public static bool HGClicked = false;
        private Vector2 HGPosition;

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
            worker = new Worker(1);
            miner = new List<GameObject>();
            miner.Add(new Mine(new Vector2(300, 100)));
            miner.Add(new Mine(new Vector2(500, 800)));
            miner.Add(new Mine(new Vector2(700, 200)));
            miner.Add(new Mine(new Vector2(1270, 400)));
            miner.Add(new Mine(new Vector2(1400, 700)));
            gameObjects = new List<GameObject>();
            Building = new List<GameObject>();
            gameObjects.Add(worker);
            gameObjects.AddRange(miner);
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
            sprites.Add("Barack", Content.Load<Texture2D>("Barack"));
            sprites.Add("Bank", Content.Load<Texture2D>("Bank"));
            sprites.Add("Factory", Content.Load<Texture2D>("Factory"));
            sprites.Add("Lab", Content.Load<Texture2D>("Lab"));
            font = Content.Load<SpriteFont>("font");
            headLine = Content.Load<SpriteFont>("HeadLine");
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
            gameObjects.AddRange(miner);
            Building.Clear();
            miner.Clear();

            if(!HQPlaced)
            {
                BuildHQ();
            }
            if(HQPlaced)
            {
                buildBuilding();
            }


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

            Rectangle buildOption1 = new Rectangle((int)currentMousPosition.X, (int)currentMousPosition.Y, 200, 50);
            Rectangle buildOption2 = new Rectangle((int)currentMousPosition.X, (int)currentMousPosition.Y + 51, 200, 50);
            Rectangle buildOption3 = new Rectangle((int)currentMousPosition.X, (int)currentMousPosition.Y + 102, 200, 50);

            if (IsClicked)
            {
                _spriteBatch.Draw(t, buildOption1, Color.Black);
                _spriteBatch.Draw(t, buildOption2, Color.Black);
                _spriteBatch.Draw(t, buildOption3, Color.Black);
            }

            if (DropDownMenu)
            {
                foreach (Rectangle r in rects)
                {
                    _spriteBatch.Draw(t, r, Color.Black);
                }
                if (rects.Count > 1)
                {
                    _spriteBatch.DrawString(font, "1. Bank", textPos1, Color.White);
                    _spriteBatch.DrawString(font, "2. Barack", textPos2, Color.White);
                    _spriteBatch.DrawString(font, "3. Factory", textPos3, Color.White);
                    _spriteBatch.DrawString(font, "4. Lab", textPos4, Color.White);
                }
            }
            if(!HQPlaced)
            {
                _spriteBatch.DrawString(headLine, "Press mouse 1 to place your HQ where you desire", HGText, Color.Blue);
            }
            _spriteBatch.DrawString(font, "Gold currency", new Vector2(1800, 20), Color.Yellow);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void BuildHQ()
        {
            HGText = new Vector2(600, 75);
            MouseState mouseHQClick = Mouse.GetState();
            if(mouseHQClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                for (int x = 0; x < 30; x++)
                {
                    for (int y = 0; y < 17; y++)
                    {
                        Rectangle rect = new Rectangle(x, y, 65, 65);
                        if (new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1).Intersects(new Rectangle(x * 65, y * 65, 65, 65)))
                        {
                            Building.Add(new Headquarter(new Vector2(x * 65, y * 65)));
                            HGPosition = new Vector2(x * 65, y * 65);
                            HQPlaced = true;
                        }
                        
                    }
                }
                        
            }
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
                            rects.Add(new Rectangle(x * 65, y * 65 + 150, 200, 50));

                            textPos1 = new Vector2(x * 65 + 75, y * 65 + 15);
                            textPos2 = new Vector2(x * 65 + 75, y * 65 + 65);
                            textPos3 = new Vector2(x * 65 + 75, y * 65 + 115);
                            textPos4 = new Vector2(x * 65 + 75, y * 65 + 165);

                            DropDownMenu = true;

                            Clicked = true;

                            buildPos = new Vector2(x * 65, y * 65);
                        }
                    }
                }
            }
            if (mouseClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                if (canPlace)
                {
                    if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D1))
                    {
                        Building.Add(new Bank(new Vector2(buildPos.X, buildPos.Y)));
                        rects.Clear();
                        Clicked = false;
                        canPlace = false;
                    }
                    if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D2))
                    {
                        Building.Add(new Barack(new Vector2(buildPos.X, buildPos.Y)));
                        rects.Clear();
                        Clicked = false;
                        canPlace = false;
                    }
                    if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D3))
                    {
                        Building.Add(new Factory(new Vector2(buildPos.X, buildPos.Y)));
                        rects.Clear();
                        Clicked = false;
                        canPlace = false;
                    }
                    if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D4))
                    {
                        Building.Add(new Lab(new Vector2(buildPos.X, buildPos.Y)));
                        rects.Clear();
                        Clicked = false;
                        canPlace = false;
                    }
                    HGClicked = false;
                }

            if (rects.Count > 4)
            {
                rects.RemoveRange(4, rects.Count - 4);
            }
            if (mouseClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1).Intersects(new Rectangle((int)HGPosition.X, (int)HGPosition.Y, 65, 65)))
                {
                    HGClicked = true;
                }
                rects.Clear();
                Clicked = false;
                canPlace = false;
            }
        }
    }
}