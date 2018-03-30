using System;
using UnityEngine;

namespace AS_UnityHelper
{
    public class xUISkin
    {
        static GUISkin _skin;
        static GUISkin _originalSkin;
        static bool _didBackup;
        public static GUISkin OriginalSkin { get { return _originalSkin; } set { _originalSkin = value; } }

        // can be called in OnGUI only
        public static GUISkin CreateCustomSkin(string skinName, int fontSize)
        {
            // back up original gui skin for later usage
            if (!_didBackup)
            {
                OriginalSkin = GUI.skin;
                _didBackup = true;
            }

            if (_skin == null)
            {
                GUISkin mySkin = ScriptableObject.CreateInstance(typeof(GUISkin)) as GUISkin;

                #region Assignment
                mySkin.name = skinName;
                mySkin.box                            = new GUIStyle(OriginalSkin.box);
                mySkin.button                         = new GUIStyle(OriginalSkin.button);
                mySkin.toggle                         = new GUIStyle(OriginalSkin.toggle);
                mySkin.label                          = new GUIStyle(OriginalSkin.label);
                mySkin.window                         = new GUIStyle(OriginalSkin.scrollView);
                mySkin.textField                      = new GUIStyle(OriginalSkin.textField);
                mySkin.textArea                       = new GUIStyle(OriginalSkin.textArea);
                mySkin.scrollView                     = new GUIStyle(OriginalSkin.scrollView);
                mySkin.horizontalScrollbarThumb       = new GUIStyle(OriginalSkin.horizontalScrollbarThumb);
                mySkin.horizontalSliderThumb          = new GUIStyle(OriginalSkin.horizontalSliderThumb);
                mySkin.horizontalSlider               = new GUIStyle(OriginalSkin.horizontalSlider);
                mySkin.horizontalScrollbar            = new GUIStyle(OriginalSkin.horizontalScrollbar);
                mySkin.horizontalScrollbarRightButton = new GUIStyle(OriginalSkin.horizontalScrollbarRightButton);
                mySkin.horizontalScrollbarLeftButton  = new GUIStyle(OriginalSkin.horizontalScrollbarLeftButton);
                mySkin.verticalScrollbarThumb         = new GUIStyle(OriginalSkin.verticalScrollbarThumb);
                mySkin.verticalSlider                 = new GUIStyle(OriginalSkin.verticalSlider);
                mySkin.verticalScrollbar              = new GUIStyle(OriginalSkin.verticalScrollbar);
                mySkin.horizontalScrollbarLeftButton  = new GUIStyle(OriginalSkin.horizontalScrollbarLeftButton);
                mySkin.verticalScrollbarDownButton    = new GUIStyle(OriginalSkin.verticalScrollbarDownButton);
                mySkin.verticalSliderThumb            = new GUIStyle(OriginalSkin.verticalSliderThumb);
                mySkin.font      = OriginalSkin.font;
                mySkin.hideFlags = OriginalSkin.hideFlags;
                #endregion

                #region GUIBox
                mySkin.box.normal.background = xPlainTextures.CreatePlainTexture(35, 35, 35, 75);
                mySkin.box.normal.textColor  = Color.white;
                mySkin.box.onNormal          = mySkin.box.normal;
                mySkin.box.onNormal          = mySkin.box.normal;
                mySkin.box.active            = mySkin.box.normal;
                mySkin.box.onActive          = mySkin.box.normal;
                mySkin.box.alignment         = TextAnchor.UpperCenter;
                mySkin.box.fontSize          = fontSize;
                mySkin.box.normal.textColor  = Color.white;
                #endregion

                #region GUIButton
                mySkin.button.normal.background   = xPlainTextures.CreatePlainTexture(20, 20, 20, 200);
                mySkin.button.normal.textColor    = Color.white;
                mySkin.button.onNormal.background = xPlainTextures.CreatePlainTexture(0, 0, 0, 135);
                mySkin.button.onNormal.textColor  = Color.white;
                mySkin.button.active.background   = xPlainTextures.CreatePlainTexture(0, 0, 0, 135);
                mySkin.button.active.textColor    = Color.white;
                mySkin.button.onActive.background = xPlainTextures.CreatePlainTexture(0, 0, 0, 135);
                mySkin.button.onActive.textColor  = Color.white;
                mySkin.button.hover.background    = xPlainTextures.CreatePlainTexture(25, 25, 25, 6);
                mySkin.button.hover.textColor     = Color.white;
                mySkin.button.onHover.background  = xPlainTextures.CreatePlainTexture(25, 25, 25, 60);
                mySkin.button.onHover.textColor   = Color.white;
                mySkin.button.alignment           = TextAnchor.MiddleCenter;
                mySkin.button.fontSize            = fontSize;
                #endregion

                #region GUIToggle
                mySkin.toggle.normal.background   = xPlainTextures.CreatePlainTexture(255, 0, 0, 200);
                mySkin.toggle.normal.textColor    = Color.black;
                mySkin.toggle.onNormal.background = xPlainTextures.CreatePlainTexture(0, 255, 0, 200);
                mySkin.toggle.onNormal.textColor  = Color.black;
                mySkin.toggle.active.background   = mySkin.toggle.normal.background;
                mySkin.toggle.active.textColor    = Color.black;
                mySkin.toggle.onActive.background = mySkin.toggle.onNormal.background;
                mySkin.toggle.onActive.textColor  = Color.black;
                mySkin.toggle.hover               = mySkin.toggle.normal;
                mySkin.toggle.onHover             = mySkin.toggle.onNormal;
                mySkin.toggle.onHover.textColor   = Color.black;
                mySkin.toggle.alignment           = TextAnchor.MiddleLeft;
                mySkin.toggle.fontSize            = fontSize;
                #endregion

                #region GUIHorizontalSlider
                mySkin.horizontalSliderThumb.normal.background   = xPlainTextures.CreatePlainTexture(Color.white);
                mySkin.horizontalSliderThumb.onNormal.background = mySkin.horizontalSliderThumb.normal.background;
                mySkin.horizontalSliderThumb.active.background   = mySkin.horizontalSliderThumb.normal.background;
                mySkin.horizontalSliderThumb.onActive.background = mySkin.horizontalSliderThumb.normal.background;
                mySkin.horizontalSliderThumb.hover.background    = mySkin.horizontalSliderThumb.normal.background;
                mySkin.horizontalSliderThumb.onHover.background  = mySkin.horizontalSliderThumb.normal.background;
                #endregion

                #region GUIVerticalScrollBar
                mySkin.verticalScrollbarThumb.normal.background   = xPlainTextures.CreatePlainTexture(Color.white);
                mySkin.verticalScrollbarThumb.onNormal.background = mySkin.horizontalSliderThumb.normal.background;
                mySkin.verticalScrollbarThumb.active.background   = mySkin.horizontalSliderThumb.normal.background;
                mySkin.verticalScrollbarThumb.onActive.background = mySkin.horizontalSliderThumb.normal.background;
                mySkin.verticalScrollbarThumb.hover.background    = mySkin.horizontalSliderThumb.normal.background;
                mySkin.verticalScrollbarThumb.onHover.background  = mySkin.horizontalSliderThumb.normal.background;
                mySkin.verticalScrollbar.normal.background        = xPlainTextures.CreatePlainTexture(40, 40, 40, 90);
                mySkin.verticalScrollbar.onNormal.background      = xPlainTextures.CreatePlainTexture(40, 40, 40, 90);
                mySkin.verticalScrollbar.active.background        = xPlainTextures.CreatePlainTexture(40, 40, 40, 90);
                mySkin.verticalScrollbar.onActive.background      = xPlainTextures.CreatePlainTexture(40, 40, 40, 90);
                mySkin.verticalScrollbar.hover.background         = xPlainTextures.CreatePlainTexture(40, 40, 40, 90);
                mySkin.verticalScrollbar.onHover.background       = xPlainTextures.CreatePlainTexture(40, 40, 40, 90);
                #endregion

                #region GUITextField
                mySkin.textField.normal.background = xPlainTextures.CreatePlainTexture(55, 55, 55, 90);
                mySkin.textField.normal.textColor  = Color.black;
                mySkin.textField.onNormal          = mySkin.textField.normal;
                mySkin.textField.active            = mySkin.textField.normal;
                mySkin.textField.onActive          = mySkin.textField.normal;
                mySkin.textField.focused           = mySkin.textField.normal;
                mySkin.textField.onFocused         = mySkin.textField.normal;
                mySkin.textField.hover             = mySkin.textField.normal;
                mySkin.textField.onHover           = mySkin.textField.normal;
                mySkin.textField.fontSize          = fontSize;
                #endregion

                #region GUITextArea
                mySkin.textArea = mySkin.textField;
                #endregion

                _skin = mySkin;
                CleanUp(mySkin);
            }
            return _skin;
        }

        public static void CleanUp(GUISkin skin)
        {
            UnityEngine.Object.Destroy(skin);
        }
    }
}
