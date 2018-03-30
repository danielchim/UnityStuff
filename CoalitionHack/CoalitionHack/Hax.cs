using System;
using UnityEngine;
using AS_UnityHelper;
using SideEffect.Coalition.Damage;


namespace CoalitionHack
{
    public class Hax
    {

        private static Hax _instance;
        public static Hax Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Hax();

                return _instance;
            }
        }

        public static class Options
        {
            public static bool
                _espLines,
                _massMurder,
                _chatSpam;
        }

        public static bool isValidEnemy(PhotonPlayer photonPlayer)
        {
            GameObject playerObject = PlayerNetworkHandler.Instance.GetPlayerObject(photonPlayer.ID);
            return playerObject != null &&
                playerObject.GetComponent<PlayerStats>().PlayerTeam != PlayerNetworkHandler.Instance.GetOurPlayer().GetComponent<PlayerStats>().PlayerTeam;
        }

        public static bool canUpdate()
        {
            return PlayerNetworkHandler.Instance != null &&
                PlayerNetworkHandler.Instance.GetOurPlayer() != null;
        }

        public static void Esp()
        {
            xGL.BeginRender();
            var from = new Vector2(0.5f, 0.0f); // bottom center of screen
            foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
            {
                if (isValidEnemy(photonPlayer))
                {
                    GameObject playerObject = PlayerNetworkHandler.Instance.GetPlayerObject(photonPlayer.ID);

                    var screenPoint = Camera.main.WorldToScreenPoint(playerObject.transform.position);
                    if (screenPoint.z > 0) // check if not behind camera check
                    {
                        var to = new Vector2(screenPoint.x / Screen.width, screenPoint.y / Screen.height); // normalize screen point
                        xGL.DrawLine(from, to, Color.red);
                    }
                }
            }
            xGL.EndRender();
            //==============================================================================================================================

            foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
            {
                if (isValidEnemy(photonPlayer))
                {
                    GameObject playerObject = PlayerNetworkHandler.Instance.GetPlayerObject(photonPlayer.ID);

                    var screenPoint = Camera.main.WorldToScreenPoint(playerObject.transform.position);
                    if (screenPoint.z > 0) // check if not behind camera check
                    {
                        string playerName = playerObject.GetComponent<PlayerStats>().NameTag.ToString().Replace(
                            "(DrawName)", string.Empty);
                        xUI.Label(screenPoint.x, Screen.height - (screenPoint.y + 25f), xUIConfig._fontSize, Color.red, true, playerName, true);
                    }
                }
            }
        }

        public static void MassMurder()
        {
            foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
            {
                if (isValidEnemy(photonPlayer))
                {
                    SendDamagePacket(photonPlayer);
                }
            }
        }

        public static void SendDamagePacket(PhotonPlayer victim)
        {
            string weaponName = WeaponManager.Instance.ActiveWeapon.WeaponName;
            bool headShot = true;
            float damage = 100;
            int localID = PhotonNetwork.player.ID;
            Vector3 dir = Vector3.down;

            GameObject playerObject = PlayerNetworkHandler.Instance.GetPlayerObject(victim.ID);
            if (playerObject == null)
            {
                return;
            }

            dir = playerObject.transform.TransformDirection(Vector3.down);
            var dmg = playerObject.GetComponent<DamageManagerBase>();

            dmg.TakeDamage(
                damage, PhotonNetwork.player.ID,
                weaponName, dir, headShot);

            DamageHandler.Instance.photonView.RPC("SyncDamage", victim, new object[]
            {
                damage,
                localID,
                victim.ID,
                weaponName,
                dir,
                headShot
            });
        }


        public static void TryKickEnemies()
        {
            foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
            {
                if (isValidEnemy(photonPlayer))
                {
                    SendKickPacket(photonPlayer.ID);
                }
            }
        }

        public static void SendKickPacket(int victimId)
        {
            bool isHost = PhotonNetwork.player.isMasterClient;
            byte connectionClose = 203;

            // check if we are the host
            if (!isHost)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.player);
            }
            if (!isHost)
            {
                // couldn't re-set host
                return;
            }
            RaiseEventOptions kickEvent = new RaiseEventOptions
            {
                TargetActors = new int[]
                {
                   victimId
                }
            };
            PhotonNetwork.RaiseEvent(connectionClose, null, true, kickEvent);
        }

        public static void SendChatPacket(string msg)
        {
            var view = DamageHandler.Instance.photonView;

            if (view == null)
                return;

            view.RPC("Chat", PhotonTargets.All, new object[]
            {
                msg
            });
        }


        public void DrawHaxMenu()
        {
            GUI.skin = xUISkin.CreateCustomSkin("mySkin", xUIConfig._fontSize);

            float xStart = 200f;
            float yStart = 200f;

            Rect btnsRect = new Rect(xStart, yStart, xUIConfig._btnWidth, xUIConfig._btnHeight);

            if (xUI.Button(btnsRect, "Try Kick Enemies"))
            {
                TryKickEnemies();
            }
            // make space for the next element
            btnsRect.y += (xUIConfig._btnHeight + xUIConfig._btnSpace);

            Options._espLines = xUI.Toggle(btnsRect, "ESP", Options._espLines);
            btnsRect.y += (xUIConfig._btnHeight + xUIConfig._btnSpace);

            Options._massMurder = xUI.Toggle(btnsRect, "MassMurder", Options._massMurder);
            btnsRect.y += (xUIConfig._btnHeight + xUIConfig._btnSpace);

            Options._chatSpam = xUI.Toggle(btnsRect, "Chat Spam", Options._chatSpam);

            GUI.skin = xUISkin.OriginalSkin;

            if (canUpdate() && Options._espLines)
                Esp();

        }

        public void UpdateHax()
        {
            if (canUpdate())
            {
                if (Options._chatSpam)
                    SendChatPacket("AutoSkillz.net");
                if (Options._massMurder)
                    MassMurder();
            }
        }
    }
}