using System;
using System.IO;
using UnityEngine;

namespace AS_UnityHelper
{
    public class xPlainTextures
    {
         // Creates Texture From Color
        public static Texture2D CreatePlainTexture(Color clr)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, clr);
            texture.Apply();
            Texture2D ptex = texture;
            CleanUp(texture);
            return ptex;
        }

        // Creates Texture From RGBA (255-0)
        public static Texture2D CreatePlainTexture(byte r, byte g, byte b, byte a)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, new Color32(r, g, b, a));
            texture.Apply();
            Texture2D ptex = texture;
            CleanUp(texture);
            return ptex;
        }

        // Load Image From Its Bytes And Store It Into an Existing Texture
        public static bool LoadImageIntoTexture(Texture2D tex, byte[] imgData)
        {
            if (tex == null || imgData.Length <= 0)
                throw new ArgumentNullException();


            return tex.LoadImage(imgData);
        }

        // Creates New Texture And Load Image From It Bytes And Store It Into That Texture
        // Returns The Texture That Stores The Image
        public static Texture2D ImageFromRawData(byte[] rawData)
        {
            Texture2D ptex = null;

            if (rawData.Length <= 0)
                return ptex;

            Texture2D tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
            if (tex.LoadImage(rawData))
            {
                ptex = tex;
                CleanUp(tex);
            }
            return ptex;
        }

        // Convert Texture To PNG Bytes
        // Returns PNG Bytes
        public static byte[] TexureToPNG(Texture2D ptex)
        {
            if (ptex == null)
                throw new ArgumentNullException("Parameter texture is null");

            Texture2D tex = new Texture2D(ptex.width, ptex.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, ptex.width, ptex.height), 0, 0);
            tex.Apply();
            var bytes = tex.EncodeToPNG();
            CleanUp(tex);
            return bytes;
        }

        // Takes a Screen Shot Of Screen
        // Needs WRITE_EXTERNAL_STORAGE Permission in manifest
        public void ScreenShot(string directory)
        {
            var width  = Screen.width;
            var height = Screen.height;
            var tex    = new Texture2D(width, height, TextureFormat.RGB24, false);
            var bytes  = TexureToPNG(tex);
            CleanUp(tex);

            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                if (!Directory.Exists(directory + "/" + "_ScreenShots"))
                {
                    Directory.CreateDirectory(directory + "/" + "_ScreenShots");
                }
                string str = string.Format("Screenshot_{0}.png", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                File.WriteAllBytes(str, bytes);
            }
            catch (IOException ex)
            {
                Debug.LogException(ex);
            }
        }

        // Get Color From an Existing Texture And Returns The Color
        public static Color GetColorFromTxeture(Texture2D ptex)
        {
            if (ptex == null)
                return Color.clear;

            return ptex.GetPixel(0, 0);
        }

        public static void CleanUp(Texture2D tex)
        {
            UnityEngine.Object.Destroy(tex);
        }
    }
}
