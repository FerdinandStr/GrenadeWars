using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GrenadeWars {

    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GraphicsDevice device;
        Texture2D backgroundTexture;
        Texture2D grenadierTexture;

        Player[] players;
        int numberOfPlayers = 4;
        int currentPlayer = 0;

        int screenWidth;
        int screenHeight;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        //    LOAD CONTENT    //
        protected override void LoadContent() {
            device = graphics.GraphicsDevice;

            spriteBatch = new SpriteBatch(device);

            backgroundTexture = Content.Load<Texture2D>("Background");
            grenadierTexture = Content.Load<Texture2D>("Grenadier");

            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            SetUpPlayers();
        }


        protected override void Initialize() {

            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "GrenadeWars";

            base.Initialize();
        }

        protected override void UnloadContent() {
        }


        protected override void Update(GameTime gameTime) {
            ProcessKeyboard();
            base.Update(gameTime);
            
        }

        public void ProcessKeyboard() {
            KeyboardState boardState = Keyboard.GetState();
            if (boardState.IsKeyDown(Keys.Left)) {
                players[currentPlayer].Angle -= 0.01f;
            }
            if (boardState.IsKeyDown(Keys.Right)) {
                players[currentPlayer].Angle += 0.01f;
            }
        }

        // -------------------------------------------------------- //

        
        protected override void Draw(GameTime gameTime) {

            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawScenery();
            DrawPlayers();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawScenery() {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }

        public void DrawPlayers() {
            foreach (Player player in players) {
                if (player.IsAlive) {
                    spriteBatch.Draw(grenadierTexture,
                        player.Position,
                        null, //part of the immage
                        player.Color,//color modulation
                        0,//rotation
                        new Vector2(0, grenadierTexture.Height),//positioning bottom left
                        player.playerScaling, //scaling
                        SpriteEffects.None, //mirroring imgage
                        0); //layer of the image
                }
            }
        }




        private void SetUpPlayers() {

            Color[] playerColors = new Color[10];
            playerColors[0] = Color.Red;
            playerColors[1] = Color.Green;
            playerColors[2] = Color.Blue;
            playerColors[3] = Color.Purple;
            playerColors[4] = Color.Orange;
            playerColors[5] = Color.Indigo;
            playerColors[6] = Color.Yellow;
            playerColors[7] = Color.SaddleBrown;
            playerColors[8] = Color.Tomato;
            playerColors[9] = Color.Turquoise;

            players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++) {

                players[i] = new Player(true, playerColors[i], MathHelper.ToRadians(90), 100);
            }

            players[0].Position = new Vector2(100, 100);
            players[1].Position = new Vector2(400, 100);
            players[2].Position = new Vector2(100, 400);
            players[3].Position = new Vector2(400, 400);

        }
    }
}
