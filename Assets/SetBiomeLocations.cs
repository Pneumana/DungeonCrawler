using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.XR;

namespace UnityEngine.Localization
{
    public class SetBiomeLocations : MonoBehaviour
    {
        public int lushAngle;
        public int barrenAngle;
        public int ruinsAngle;

        public List<int> numbers = new List<int>();
        public int count = 3;
        public int maxRange;

        public GameObject ringGroup;


        bool doublecheck;

        public List<int> LastRand = new List<int> ();
        private int[] mainarray = new int[6]; //the array that you want to pick elements.

        PlaceRingComponent childscript;

        List<int> list = new List<int>(); // the list that you put non repetitive elements in it. for example we want 3 element.

        // Start is called before the first frame update
        void Start()
        {
            numbers.Clear();
            for (int i = 0; i < count; i++)
            {

                int temp = Random.Range(0, maxRange);
                while (numbers.Contains(temp))
                {
                    temp = Random.Range(0, maxRange);
                }

                numbers.Add(temp);

            }
            UpdateBiomeLocation();
            
        }
        //yeah this probably is the worst possible way to do this,
        //but whenever i tried to use for and while loops,
        //they would go on indefinately
        void RollRandom()
        {
            
        }
        public void UpdateBiomeLocation()
        {
            
            /*LastRand = new List<int>(new int[3]);

                 int output = Random.Range(0, 2);
                 int output1 = Random.Range(0, 2);
                 int output2 = 0;
                 if (output1 == output)
                 {
                     output1 = output - 1;
                 }
                 if (output1 <= -1)
                 {
                     output1 = 2;
                 }
                 if (output1 >= 3)
                 {
                     output1 = 0;
                 }
             if(output1 == 2 && output == 0) { output2 = 1; }
             if (output1 == 1 && output == 0) { output2 = 2; }
             if (output1 == 0 && output == 1) { output2 = 2; }
             if (output1 == 0 && output == 2) { output2 = 1; }
             if (output1 == 2 && output == 1) { output2 = 0; }
             if (output1 == 1 && output == 2) { output2 = 0; }*/

            lushAngle = numbers[0] * 120;
             barrenAngle = numbers[1] * 120;
             ruinsAngle = numbers[2] * 120;
             Debug.Log("lush @ " + lushAngle);
             Debug.Log("barren @ " + barrenAngle);
             Debug.Log("ruins @ " + ruinsAngle);
             foreach (Transform child in ringGroup.transform)
             {
                 childscript = child.GetComponent<PlaceRingComponent>();
                 child.GetComponent<PlaceRingComponent>().PlaceMe(
                 0.0f,
                 37.0f,
                 0.0f,
                 childscript.SpawnDistance,
                 (childscript.SpawnAngle + Random.Range(childscript.SpawnAngleOffset * -1, childscript.SpawnAngleOffset)) * (Mathf.PI / 180));
                 Debug.Log("child" + child.gameObject.name + " set to " + childscript.SpawnAngle + " with " + childscript.SpawnAngleOffset + " offset");
             }

            //update nav mesh
            //Surface2D.UpdateNavMesh(Surface2D.navMeshData);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
