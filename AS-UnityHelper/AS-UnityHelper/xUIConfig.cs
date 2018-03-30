using System;
using UnityEngine;

namespace AS_UnityHelper
{

    // recommended configuration for UI
    public class xUIConfig
    {
        public static float _btnWidth;
        public static float _btnHeight;
        public static float _btnSpace    = 5f;
        public static float _ScaleWidth  = 1280f;
        public static float _ScaleHeight = 720f;
        public static float _groupHeight = 300f;
        public static int   _fontSize;
        public static Vector3 GUIScale { get { return new Vector3(_ScaleWidth, _ScaleHeight, 1f); } }
        public static Matrix4x4 GUIMatrix { get { return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, GUIScale); } }

        public static void AdjustUI()
        {

            if (Screen.width > 1920)
            {
                _btnHeight = 50f;
                _btnWidth  = 270f;
                _fontSize  = 25;
            }

            else if (Screen.width == 1920)
            {
                _btnHeight = 45f;
                _btnWidth  = 260f;
                _fontSize  = 23;
            }

            else
            {
                _btnHeight = 33f;
                _btnWidth  = 237f;
                _fontSize  = 20;
            }

        }

        public static void ForceScreenResolution(int width, int height)
        {
            if (Screen.width != width || Screen.height != height)
                Screen.SetResolution(width, height, true);
        }
    }
}
