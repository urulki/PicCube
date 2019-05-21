using UnityEngine;
using UnityEngine.UI;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        public Sprite SpriteToRedraw;
        public GameObject Cube;

        private Texture2D targetTexture;
        public Material TargetMaterial;
        private int Height;
        private int Width;
        public GameObject Handler;
        void Start()
        {
            targetTexture = SpriteToRedraw.texture;
            Height = targetTexture.height;
            Width = targetTexture.width;
            GenerateCube(Width, Height);
        }
        
        public void GenerateCube(int XValue, int YValue)
        {
            for (int j = 0; j < YValue; j++)
            {
                for (int i = 0; i < XValue; i++)
                {
                    GameObject obj;
                    GameObject reverseObj;
                    
                    obj =
                        Instantiate(Cube, new Vector3(i, j, 0f), Quaternion.identity, Handler.transform);
                    reverseObj = 
                        Instantiate(Cube, new Vector3(i, j, 0f), Quaternion.identity, Handler.transform);
                    SetupCube(obj,i,j,TargetMaterial,targetTexture,
                        2.5f,0.3f,false);
                    SetupCube(reverseObj,i,j,TargetMaterial,targetTexture,
                        2.5f,0.3f,true);
                }
            }
        }

        public void SetupCube(GameObject objToSetup, int x, int y, Material mat, Texture2D text,
            float alphaStep, float spacing, bool reverse)
        {
            objToSetup.GetComponent<Renderer>().material = mat;
            objToSetup.GetComponent<Renderer>().material.color = text.GetPixel(x, y);
            if (!reverse)
                objToSetup.transform.position += new Vector3(0, 0,
                    (Mathf.Round(targetTexture.GetPixel(x, y).a * 10) / alphaStep) + spacing);
            else objToSetup.transform.position -= new Vector3(0, 0,
                (Mathf.Round(targetTexture.GetPixel(x, y).a * 10) / alphaStep) + spacing);
        }
    }
}
