using System.Collections.Generic;
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
        public List<int> Greys = new List<int>();
        void Start()
        {
            //Get the properties of the image to reproduce.
            targetTexture = SpriteToRedraw.texture;
            Height = targetTexture.height;
            Width = targetTexture.width;
            GenerateCube(Width, Height);
            
        }
        /// <summary>
        /// Generate the picture interpolation
        /// </summary>
        /// <param name="XValue">image width</param>
        /// <param name="YValue">image height</param>
        public void GenerateCube(int XValue, int YValue)
        {
            for (int j = 0; j < YValue; j++)
            {
                for (int i = 0; i < XValue; i++)
                {
                    GameObject obj;
                    GameObject reverseObj;
                    //instantiate the front GameObject.
                    obj =
                        Instantiate(Cube, new Vector3(i, j, 0f), Quaternion.identity, Handler.transform);
                    //instantiate the back GameObject.
                    reverseObj = 
                        Instantiate(Cube, new Vector3(i, j, 0f), Quaternion.identity, Handler.transform);
                    //setting up the two GameObjects.
                    SetupCube(obj,i,j,TargetMaterial,targetTexture,false);
                    SetupCube(reverseObj,i,j,TargetMaterial,targetTexture,true);
                }
            }
        }

        /// <summary>
        /// Modify the properties of the given GameObject according to the pixels of an image.
        /// </summary>
        /// <param name="objToSetup">GameObject to modify</param>
        /// <param name="x">x value of the pixel to check</param>
        /// <param name="y">y value of the pixel to check</param>
        /// <param name="mat">Material preset to apply on the GameObject</param>
        /// <param name="text">The reference texture</param>
        /// <param name="reverse">if the GameObject is in the front or back face</param>
        public void SetupCube(GameObject objToSetup, int x, int y, Material mat, Texture2D text, bool reverse)
        {
            // Put a material preset on the GameObject.
            objToSetup.GetComponent<Renderer>().material = mat;
            //Get the color of the current pixel in temp variable.
            Color color = text.GetPixel(x, y);
            // set GameObject's color acording to the current pixel's color.
            objToSetup.GetComponent<Renderer>().material.color = color;
            
            //Get Color in octet values.
            Color32 colorValue = text.GetPixel(x, y);
            int greyR = 255 - colorValue.r;
            int greyG = 255 - colorValue.g;
            int greyB = 255 - colorValue.b;
            int greyA = 255 - colorValue.a;
            //calculating alpha value to set the z Axis.
            int offset = (greyR + greyG + greyB + greyA) / 40;
            // Setting the Z displacement to the cube (positive for front side / négative for the back one)
            if (!reverse) objToSetup.transform.position += new Vector3(0, 0,offset);
            else objToSetup.transform.position -= new Vector3(0, 0,offset);
            //Keeping the grey value in a list (Debug)
            Greys.Add(offset);
        }
    }
}
