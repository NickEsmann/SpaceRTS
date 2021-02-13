using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Forms;

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
        private bool IsClicked = false;
        bool Clicked = false;
        Texture2D t;
        Vector2 currentMousPosition = new Vector2(Cursor.Position.X, Cursor.Position.Y);

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
                _spriteBatch.Draw(t,buildOption1, Color.Black);
                _spriteBatch.Draw(t, buildOption2, Color.Black);
                _spriteBatch.Draw(t, buildOption3, Color.Black);
            }
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void buildBuilding()
        {
            MouseState mouseClick = Mouse.GetState();

            if (mouseClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && Clicked == false)
            {
                for (int x = 0; x < 30; x++)
                {
                    for (int y = 0; y < 17; y++)
                    {
                        Rectangle rect = new Rectangle(x, y, 65, 65);
                        if (new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1).Intersects(new Rectangle(x * 65, y * 65, 65, 65)))
                        {
                            
                            //IsClicked = true;
                            Building.Add(new Headquarter(new Vector2(x * 65, y * 65)));
                        }
                    }
                }
                Clicked = true;
            }
            if(mouseClick.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                Clicked = false;
            }

        }
    }
}