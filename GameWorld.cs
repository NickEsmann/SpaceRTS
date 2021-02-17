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
        private bool choosing = false;
        private Texture2D t;
        private Vector2 currentMousPosition = new Vector2(Cursor.Position.X, Cursor.Position.Y);
        private bool dropDownMenu = false;
        private List<Rectangle> rects = new List<Rectangle>();
        private SpriteFont font;
        private Vector2 textPos1;
        private Vector2 textPos2;
        private Vector2 textPos3;
        private Vector2 textPos4;
        private bool canPlace;
        private Vector2 buildPos;
        private bool HQPlaced = false;
        private Vector2 HQText;
        private SpriteFont headLine;
        public static List<GameObject> deleteObjects;
        public static bool HQClicked = false;
        private Vector2 HQPosition;
        private Texture2D collisionTexture;
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
            deleteObjects = new List<GameObject>();
            //miner.Add(new Mine(new Vector2(300, 100)));
            //miner.Add(new Mine(new Vector2(500, 800)));
            //miner.Add(new Mine(new Vector2(700, 200)));
            miner.Add(new Mine(new Vector2(1270, 400)));
            //miner.Add(new Mine(new Vector2(1400, 700)));
            gameObjects = new List<GameObject>();
            Building = new List<GameObject>();
            gameObjects.Add(worker);
            gameObjects.AddRange(miner);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            collisionTexture = Content.Load<Texture2D>("Pixel");
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
            foreach (GameObject gob in gameObjects)
            {
                gob.Update(gameTime);
                worker.CheckCollision(gob);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            gameObjects.AddRange(Building);
            gameObjects.AddRange(miner);
            Building.Clear();
            miner.Clear();

            if (!HQPlaced)
            {
                buildHQ();
            }
            if (HQPlaced)
            {
                buildBuilding();
            }

            foreach (var item in gameObjects)
            {
                item.CheckCollision(item);
                foreach (var go in deleteObjects)
                {
                    gameObjects.Remove(go);
                }
            }

            //Listen rydes.
            deleteObjects.Clear();

            base.Update(gameTime);
        }

        private void DrawCollisionBox(GameObject go)
        {
#if DEBUG
            //Der laves en streg med tykkelsen 1 for hver side af Collision.
            Rectangle topLine = new Rectangle(go.Collision.X, go.Collision.Y, go.Collision.Width, 1);
            Rectangle bottomLine = new Rectangle(go.Collision.X, go.Collision.Y + go.Collision.Height, go.Collision.Width, 1);
            Rectangle rightLine = new Rectangle(go.Collision.X + go.Collision.Width, go.Collision.Y, 1, go.Collision.Height);
            Rectangle leftLine = new Rectangle(go.Collision.X, go.Collision.Y, 1, go.Collision.Height);
            //Der tegnes en streg med tykkelsen 1 for hver side af Collision med collsionTexture med farven rød.
            _spriteBatch.Draw(collisionTexture, topLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, bottomLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, rightLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, leftLine, Color.Red);
#endif
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            map.Draw(_spriteBatch);
            foreach (GameObject go in gameObjects)
            {
                go.Draw(_spriteBatch);
                DrawCollisionBox(go);
            }

            Rectangle buildOption1 = new Rectangle((int)currentMousPosition.X, (int)currentMousPosition.Y, 200, 50);
            Rectangle buildOption2 = new Rectangle((int)currentMousPosition.X, (int)currentMousPosition.Y + 51, 200, 50);
            Rectangle buildOption3 = new Rectangle((int)currentMousPosition.X, (int)currentMousPosition.Y + 102, 200, 50);

            if (dropDownMenu)
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
            if (!HQPlaced)
            {
                _spriteBatch.DrawString(headLine, "Press mouse 1 to place your HQ where you desire", HQText, Color.Blue);
            }
            _spriteBatch.DrawString(font, $"Gold currency {Headquarter.CurrentGold}", new Vector2(1600, 20), Color.Yellow);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void Destroy(GameObject go)
        {
            deleteObjects.Add(go);
        }

        private void buildHQ()
        {
            HQText = new Vector2(600, 75);
            MouseState mouseHQClick = Mouse.GetState();
            if (mouseHQClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                for (int x = 0; x < 30; x++)
                {
                    for (int y = 0; y < 17; y++)
                    {
                        Rectangle rect = new Rectangle(x, y, 65, 65);
                        if (new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1).Intersects(new Rectangle(x * 65, y * 65, 65, 65)))
                        {
                            Building.Add(new Headquarter(new Vector2(x * 65, y * 65)));
                            HQPosition = new Vector2(x * 65, y * 65);
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

            if (mouseClick.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && choosing == false)
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

                            dropDownMenu = true;

                            choosing = true;

                            buildPos = new Vector2(x * 65, y * 65);
                        }
                    }
                }
            }
            if (canPlace)
            {
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D1))
                {
                    Building.Add(new Bank(new Vector2(buildPos.X, buildPos.Y)));
                    rects.Clear();
                    choosing = false;
                    canPlace = false;
                }
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D2))
                {
                    Building.Add(new Barack(new Vector2(buildPos.X, buildPos.Y)));
                    rects.Clear();
                    choosing = false;
                    canPlace = false;
                }
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D3))
                {
                    Building.Add(new Factory(new Vector2(buildPos.X, buildPos.Y)));
                    rects.Clear();
                    choosing = false;
                    canPlace = false;
                }
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D4))
                {
                    Building.Add(new Lab(new Vector2(buildPos.X, buildPos.Y)));
                    rects.Clear();
                    choosing = false;
                    canPlace = false;
                }
                HQClicked = false;
            }

            if (rects.Count > 4)
            {
                rects.RemoveRange(4, rects.Count - 4);
            }
            if (mouseClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1).Intersects(new Rectangle((int)HQPosition.X, (int)HQPosition.Y, 110, 120)))
                {
                    HQClicked = true;
                }
                rects.Clear();
                choosing = false;
                canPlace = false;
            }
        }
    }
}