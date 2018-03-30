using System;
using UnityEngine;

namespace AS_UnityHelper
{
    public class Example : MonoBehaviour
    {

        bool   _toggle;
        string _txtfield;
        float  _slider;

        float  _totalHeight;

        // load our monobehaviour class
        public static void LoadMe()
        {
            xLoader.Load<Example>();
        }

        // unload our monobehaviour class
        public static void UnloadMe()
        {
            xLoader.Unload<Example>();
        }

        public void Start()
        {
            // Initialize stuff here
        }

        // Draw stuff
        public void OnGUI()
        {
            GUI.skin = xUISkin.CreateCustomSkin("mySkin", 20);

            // Starting x Pos
            float x = 100f;

            // Starting y Pos
            float y = 100f;

            xUI.Border(new Rect(x, y, 250, _totalHeight), 2, Color.cyan);

            if (xUI.Button(new Rect(x + 5f, y, 240, 35), "Click Me"))
            {
                // do something 
            }
            // Increase Y Pos for next element
            // 35 size of the earlier button and 5 for space, so 40
            y += 40;

            _toggle  = xUI.Toggle(new Rect(x + 5f, y, 240, 35), "Toggle Me", _toggle);
            if (_toggle)
            {
                // do something
            }
            // same routine
            y += 40;

            _txtfield = xUI.TextInput(_txtfield, new Rect(x + 5f, y, 240, 35), 255);
            y += 40;

            _slider   = xUI.HorizontalSlider(_slider, new Rect(x + 5f, y, 240, 35), 19, 0, 255, "My Slider");
            y += 35;

            // the final height of elemets, we can use this for our background so it fits our elements
            _totalHeight = y;

            // go back to original after we are done
            GUI.skin = xUISkin.OriginalSkin;
        }

        public void LateUpdate()
        {
            // Update stuff here
        }
    }
}
