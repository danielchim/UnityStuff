using System;
using UnityEngine;

namespace AS_UnityHelper
{
    public class xGL
    {
        static Material _lineMat;
        public static Material lineMaterial
        {
            get
            {
                if (_lineMat == null)
                {
                    //https://docs.unity3d.com/ScriptReference/Material.SetPass.html
                    var shader = Shader.Find("Hidden/Internal-Colored");
                    _lineMat   = new Material(shader);
                    _lineMat.hideFlags = HideFlags.HideAndDontSave;
                    _lineMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcColor);
                    _lineMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.SrcColor);
                    _lineMat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                    _lineMat.SetInt("_ZWrite", 0);
                    _lineMat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
                }
                return _lineMat;
            }
        }

        public static void BeginRender()
        {
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadOrtho();
        }
        public static void EndRender()
        {
            GL.PopMatrix();
        }

        public static void DrawLine(Vector2 p1, Vector2 p2, Color clr)
        {
            GL.Begin(GL.LINES);

            GL.Color(clr);
            GL.Vertex3(p1.x, p1.y, 0);
            GL.Vertex3(p2.x, p2.y, 0);

            GL.End();
        }

        public static void DrawBoundingBox(Rect rect, Color clr)
        {
            GL.Begin(GL.LINES);

            GL.Color(clr);
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);

            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);

            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);

            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMin, 0);

            GL.End();
        }

            /* fail
             * public static void DrawCircle(Vector2 pos, float radius, Color clr)
             {
                 float _add = 0.2f;
                 for (float theta = 0.0f; theta < (2 * Mathf.PI); theta += _add)
                 {
                     float x   = Mathf.Cos(theta) * radius + pos.x;
                     float y   = Mathf.Sin(theta) * radius + pos.y;

                     var   p1  = new Vector2(x, y);
                     var   p2  = new Vector2(x, y + _add);

                     DrawLine(p1, p2, clr);
                 }
             }*/
        }
}
