using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml;
// -----------------------------------------------------------------------
// <copyright file="$safeitemrootname$.cs" company="$registeredorganization$">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MyGame3D_0912100
{
    public class MainMenu : VisibleGameEntity
    {
        List<MyXNAButton> _ButtonList;
        int _nButton;
        int _focusButton;
        private bool _fros = false;

        private KeyboardState previousKbs;

        private bool isFirstPress = true;

        private void setFocusButton(int i)
        {
            if (i >= 0 && i < _nButton)
            {
                _ButtonList[_focusButton].EnableAnimation(false);
                _ButtonList[i].EnableAnimation(true);
                _focusButton = i;
            }
        }
        public MainMenu(ContentManager content, string menuFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(menuFile);
            }
            catch (Exception e)
            {
                return;
            }
            //Load Background
            XmlElement xmlElement = (XmlElement)xmlDoc.SelectSingleNode(@"//BackgroundSprite");
            string[] strBgTextures = null;
            Vector2 topLeft = Vector2.Zero;
            Vector2 size = Vector2.Zero;
            float backgroundDelay = 16f;
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                XmlNode xmlNode = xmlNodeList[i];
                switch (xmlNode.Name)
                {
                    case "Delay":
                        backgroundDelay = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "x":
                        topLeft.X = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "y":
                        topLeft.Y = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "height":
                        size.Y = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "width":
                        size.X = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "textures":
                        {
                            XmlNodeList nodeList = xmlElement.ChildNodes[i].ChildNodes;
                            strBgTextures = new string[nodeList.Count];
                            for (int j = 0; j < nodeList.Count; j++)
                            {
                                strBgTextures[j] = nodeList[j].InnerText;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            //Load Title
            xmlElement = (XmlElement)xmlDoc.SelectSingleNode(@"//TitleSprite");
            string[] strTitleTextures = null;
            Vector2 position = Vector2.Zero;
            float titleDelay = 16f;
            //Vector2 size = Vector2.Zero;
            xmlNodeList = xmlElement.ChildNodes;
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                XmlNode xmlNode = xmlNodeList[i];
                switch (xmlNode.Name)
                {
                    case "Delay":
                        titleDelay = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "x":
                        position.X = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "y":
                        position.Y = (float)Convert.ToDouble(xmlNode.InnerText);
                        break;
                    case "textures":
                        {
                            XmlNodeList nodeList = xmlElement.ChildNodes[i].ChildNodes;
                            strTitleTextures = new string[nodeList.Count];
                            for (int j = 0; j < nodeList.Count; j++)
                            {
                                strTitleTextures[j] = nodeList[j].InnerText;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            //Load buttons
            _ButtonList = new List<MyXNAButton>();
            xmlElement = (XmlElement)xmlDoc.SelectSingleNode(@"//Buttons");
            string[] strButtonTextures = null;
            Vector2 buttonSize = Vector2.Zero;
            Vector2 buttonPosition = Vector2.Zero;
            List<float> buttonDelay = new List<float>();
            xmlNodeList = xmlElement.ChildNodes;
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                XmlNode xmlNode = xmlNodeList[i];
                for (int k = 0; k < xmlNode.ChildNodes.Count; k++)
                {
                    switch (xmlNode.ChildNodes[k].Name)
                    {
                        case "Delay":
                            {
                                float temp = (float)Convert.ToDouble(xmlNode.ChildNodes[k].InnerText);
                                buttonDelay.Add(temp);
                            }
                            break;
                        case "x":
                            buttonPosition.X = (float)Convert.ToDouble(xmlNode.ChildNodes[k].InnerText);
                            break;
                        case "y":
                            buttonPosition.Y = (float)Convert.ToDouble(xmlNode.ChildNodes[k].InnerText);
                            break;
                        case "height":
                            buttonSize.X = (float)Convert.ToDouble(xmlNode.ChildNodes[k].InnerText);
                            break;
                        case "width":
                            buttonSize.Y = (float)Convert.ToDouble(xmlNode.ChildNodes[k].InnerText);
                            break;
                        case "textures":
                            {
                                XmlNodeList nodeList = xmlNode.ChildNodes[k].ChildNodes;
                                strButtonTextures = new string[nodeList.Count];
                                for (int j = 0; j < nodeList.Count; j++)
                                {
                                    strButtonTextures[j] = nodeList[j].InnerText;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                _ButtonList.Add(new MyXNAButton(content, strButtonTextures, strButtonTextures.Length, buttonPosition, buttonSize));
            }
            _nButton = _ButtonList.Count;
            initBgAndTitle(content, strBgTextures, ref topLeft, ref size, strBgTextures.Length, strTitleTextures, strTitleTextures.Length, ref position, backgroundDelay, titleDelay);
            for (int i = 0; i < _nButton; i++)
            {
                _ButtonList[i].SetDelayAnimation(buttonDelay[i]);
            }
            _ButtonList[0].EnableAnimation(true);
            _focusButton = 0;
            _sprites[1].NormalDelay = 64;
        }

        public MainMenu(ContentManager content, string strBgPrefix, Vector2 topleft, Vector2 size, int nBgTexture,
            string strMenuTitlePrefix, int nMenuTitleTexture, Vector2 position,
            int nButton, string[] strButtonPrefixs, int[] nTextureButtons, Vector2[] buttonPositions, Vector2[] buttonSizes)
        {
            string[] strBgTextures = new string[nBgTexture];
            for (int i = 0; i < nBgTexture; i++)
                strBgTextures[i] = strBgPrefix + i.ToString("00");

            string[] strMenuTitleTextures = new string[nMenuTitleTexture];
            for (int i = 0; i < nMenuTitleTexture; i++)
                strMenuTitleTextures[i] = strMenuTitlePrefix + i.ToString("00");


            initGameMenu(content, strBgTextures, topleft, size, nBgTexture, strMenuTitleTextures, nMenuTitleTexture, position,
                nButton, strButtonPrefixs, nTextureButtons, buttonPositions, buttonSizes);

        }

        public MainMenu(ContentManager content, string[] strBgTextures, Vector2 topleft, Vector2 size, int nBgTexture,
            string[] strMenuTitleTextures, int nMenuTitleTexture, Vector2 position,
            int nButton, string[] strButtonTextures, int[] nTextureButtons, Vector2[] buttonPositions, Vector2[] buttonSizes
            )
        {
            initGameMenu(content, strBgTextures, topleft, size, nBgTexture,
                strMenuTitleTextures, nMenuTitleTexture, position,
                nButton, strButtonTextures, nTextureButtons, buttonPositions, buttonSizes);
        }


        private void initGameMenu(ContentManager content, string[] strBgTextures, Vector2 topleft, Vector2 size, int nBgTexture,
            string[] strMenuTitleTextures, int nMenuTitleTexture, Vector2 position,
            int nButton, string[] strButtonPrefix, int[] nTextureButtons, Vector2[] buttonPositions, Vector2[] buttonSizes
            )
        {
            initBgAndTitle(content, strBgTextures, ref topleft, ref size, nBgTexture, strMenuTitleTextures, nMenuTitleTexture, ref position, 16f, 16f);

            _ButtonList = new List<MyXNAButton>();
            for (int i = 0; i < nButton; i++)
            {
                MyXNAButton tempButton = new MyXNAButton(content, strButtonPrefix[i], nTextureButtons[i], buttonPositions[i], buttonSizes[i]);
                _ButtonList.Add(tempButton);
            }
            _nButton = nButton;
        }

        private void initBgAndTitle(ContentManager content, string[] strBgTextures, ref Vector2 topleft, ref Vector2 size, int nBgTexture, string[] strMenuTitleTextures, int nMenuTitleTexture, ref Vector2 position, float backgroundDelay, float titleDelay)
        {
            _sprites = new List<My2DSprite>();
            _nSprite = 2;

            Texture2D[] bgTextures = new Texture2D[nBgTexture];
            for (int i = 0; i < nBgTexture; i++)
                bgTextures[i] = content.Load<Texture2D>(strBgTextures[i]);
            My2DSprite bgSprite = new My2DSprite(bgTextures, topleft);
            bgSprite.NormalDelay = backgroundDelay;
            _TopLeft = topleft;
            _Size = size;
            _sprites.Add(bgSprite);

            Texture2D[] menuTitleTextures = new Texture2D[nMenuTitleTexture];
            for (int i = 0; i < nMenuTitleTexture; i++)
                menuTitleTextures[i] = content.Load<Texture2D>(strMenuTitleTextures[i]);
            My2DSprite menuTitleSprite = new My2DSprite(menuTitleTextures, position);
            menuTitleSprite.NormalDelay = titleDelay;
            _sprites.Add(menuTitleSprite);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            for (int i = 0; i < _nSprite; i++)
                _sprites[i].Draw(gameTime, spriteBatch);
            for (int i = 0; i < _nButton; i++) _ButtonList[i].Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
            ;
        }

        override public void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            int focusingButton = _focusButton;
            if(!this._fros && kbs.IsKeyDown(Keys.Down))
            {
                focusingButton = (++focusingButton) % _nButton;
                setFocusButton(focusingButton);
                this._fros = true;
            }

            else if (!this._fros && kbs.IsKeyDown(Keys.Up))
            {
                --focusingButton;
                if (focusingButton < 0)
                    focusingButton = _nButton - 1;
                setFocusButton(focusingButton);
                this._fros = true;
            }

            else if(kbs.IsKeyUp(Keys.Up) && kbs.IsKeyUp(Keys.Down))
            {
                this._fros = false;
            }
            
            for (int i = 0; i < _nSprite; i++)
                _sprites[i].Update(gameTime);
            for (int i = 0; i < _nButton; i++)
                _ButtonList[i].Update(gameTime, kbs, ms);
        }
    }
}
