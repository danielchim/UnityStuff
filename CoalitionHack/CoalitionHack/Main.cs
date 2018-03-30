using System;
using UnityEngine;
using AS_UnityHelper;

/*
By Ruit - AutoSkillz.net
*/

namespace CoalitionHack
{
    public class Main : MonoBehaviour
    {
        public static void LoadMe()
        {
            xLoader.Load<Main>();
        }

        void Start()
        {
            xUIConfig.AdjustUI();
        }

        void OnGUI()
        {
            Hax.Instance.DrawHaxMenu();
        }

        void LateUpdate()
        {
            Hax.Instance.UpdateHax();
        }
    }
}
