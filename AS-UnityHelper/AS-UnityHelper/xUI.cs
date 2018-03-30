using System;
using UnityEngine;

namespace AS_UnityHelper
{
    public class xUI
    {
        // convert from gui to screen wrapper
        public static Vector2 GUIToScreen(float x, float y)
        {
            return GUIUtility.GUIToScreenPoint(new Vector2(x, y));
        }
      
        // Draw a border around the given rect
        public static void Border(Rect rect, int thickness, Color clr)
        {
            var tex = xPlainTextures.CreatePlainTexture(clr);

            Texture(new Rect(rect.x, rect.y - thickness, rect.width, thickness), tex);
            Texture(new Rect(rect.x, rect.y + rect.height, rect.width, thickness), tex);
            Texture(new Rect(rect.x - thickness, rect.y - thickness, thickness, rect.height + (thickness - 0.5f) * 2), tex);
            Texture(new Rect(rect.x + rect.width, rect.y - thickness, thickness, rect.height + (thickness - 0.5f) * 2), tex);

            xPlainTextures.CleanUp(tex);
        }

        // alternative for GUI Box
        public static void Background(Rect rect, string title, int titleSize, Color titleColor, Color bgColor, 
            Color borderColor, int thickness = 2)
        {
            Texture(rect, bgColor);
            Border(rect, thickness, borderColor);
            float xCenter = (rect.x + rect.width) / 2;
            float yPos    = rect.y + 5f;
            Label(xCenter, yPos, titleSize, titleColor, true, title, true);
        }

        // GUI DrawTexture wrapper
        public static void Texture(Rect rect, Texture2D ptex)
        {
            if (ptex == null)
                throw new ArgumentNullException("Parameter texture is null");

            GUI.DrawTexture(rect, ptex);
        }

        // GUI DrawTexture wrapper (by color)
        public static void Texture(Rect rect, Color clr)
        {
            var ptex = xPlainTextures.CreatePlainTexture(clr);
            GUI.DrawTexture(rect, ptex);
            xPlainTextures.CleanUp(ptex);
        }

        // GUI Label wrapper
        public static void Label(float x, float y, int fontsize, Color clr, bool center, string text, bool outline)
        {
            var style       = new GUIStyle(GUI.skin.label);
            style.fontSize  = fontsize;
            style.fontStyle = FontStyle.Normal;
            var size        = style.CalcSize(new GUIContent(text));
            var rect        = new Rect((center ? x - (size.x / 2) : x), y, size.x, size.y);
            if (outline)
            {
                style.normal.textColor = Color.black;
                GUI.Label(new Rect(rect.x - 1f, rect.y, rect.width, rect.height), text, style);
                GUI.Label(new Rect(rect.x + 1f, rect.y, rect.width, rect.height), text, style);
                GUI.Label(new Rect(rect.x, rect.y - 1f, rect.width, rect.height), text, style);
                GUI.Label(new Rect(rect.x, rect.y + 1f, rect.width, rect.height), text, style);
            }
            style.normal.textColor = clr;
            GUI.Label(rect, text, style);
        }

        // GUI TextField wrapper
        public static string TextInput(string inStr, Rect rect, int maxLength)
        {
            return GUI.TextField(rect, inStr, maxLength);
        }

        // GUI HorizontalSlider wrapper
        public static float HorizontalSlider(float slider, Rect rect, int fontsize, float minLength, 
            float maxLength, string text)
        {
            Label(rect.x, rect.y, fontsize, Color.white, false, text, true);
            rect.y += (fontsize << 1) / 1.3f;
            return GUI.HorizontalSlider(rect, slider, minLength, maxLength);
        }

        // GUI Button wrapper
        public static bool Button(Rect rect, string str, int thickness = 2)
        {
            return GUI.Button(rect, str);
        }

        // GUI Toggle wrapper
        public static bool Toggle(Rect rect, string str, bool toggle)
        {
            return GUI.Toggle(rect, toggle, str);
        }

        // draw crosshair
        public static void StaticCrosshair(Color clr, float size)
        {
            var   _crosshair = xPlainTextures.CreatePlainTexture(clr);

            float thin       = size / 2;

            // Horizontal
            Texture(new Rect(Screen.width / 2 - thin, Screen.height / 2 - (thin / 2), size, thin), _crosshair);

            // Vertical
            Texture(new Rect(Screen.width / 2 - (thin / 2), Screen.height / 2 - thin, thin, size), _crosshair);

            xPlainTextures.CleanUp(_crosshair);
        }


        // Draw bounding box around the given rect
        public static void BoundingBox(Rect rect, Color clr, float thickness = 1)
        {
            var _boundingBox = xPlainTextures.CreatePlainTexture(clr);

            // top
            Texture(new Rect(rect.x, rect.y - thickness, rect.width, thickness), _boundingBox);
            //bottom
            Texture(new Rect(rect.x, rect.y + rect.height, rect.width, thickness), _boundingBox);
            // left
            Texture(new Rect(rect.x - thickness, rect.y - thickness, thickness, rect.height + thickness * 2), _boundingBox);
            // right
            Texture(new Rect(rect.x + rect.width, rect.y - thickness, thickness, rect.height + thickness * 2), _boundingBox);

            xPlainTextures.CleanUp(_boundingBox);
        }

        // draw fps
        public static void FPS(float xPos, float yPos, Color clr, int fontSize)
        {
            Label(xPos, yPos, fontSize, clr, false, string.Format("FPS: {0}", (int)(1.0f / Time.smoothDeltaTime)), true);
        }


        // draw hp bar at top of the given rect
        public static void HPBar(Rect pos, int hp)
        {
            var _healthBar = xPlainTextures.CreatePlainTexture(GetHpColor(hp));
            var maxHp      = 100f;

            Rect    rect   = new Rect(pos.x + (pos.width / 2) - (hp / 2), pos.y - 12f, maxHp / 2, 5f);
            Vector2 screen = GUIToScreen(rect.x, rect.y);
            Rect    _rect  = new Rect(screen.x, screen.y, rect.width, rect.height);

            Border(_rect, 1, Color.black);
            // back texture for max hp
            Texture(_rect, Color.black);
            // current hp 
            Texture(new Rect(screen.x, screen.y, hp, _rect.height), _healthBar);

            xPlainTextures.CleanUp(_healthBar);
        }


        public static Color GetHpColor(int hp)
        {
            Color clr = Color.clear;

            if (hp >= 65)
                clr = Color.green;

            if (hp >= 30 && hp < 65)
                clr = Color.yellow;

            if (hp < 30)
                clr = Color.red;

            return clr;
        }
    }
}
