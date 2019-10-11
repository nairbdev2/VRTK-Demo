/*
 * name: Brian De Villa
 * netid: bdevil2
 * fileName: SpawnObject.cs
 * desc: Created a Custom SpawnObject Script that has a limit on how much objects you can spawn and can despawn them accordingly using two buttons.
 */
namespace VRTK.Examples.Prefabs.SpawnObject
{
    using UnityEngine;
    using Malimbe.MemberClearanceMethod;
    using Malimbe.XmlDocumentationAttribute;
    using Malimbe.PropertySerializationAttribute;
    using Zinnia.Extension;
    using Zinnia.Data.Type;
    using System;
    using System.Collections;
    using System.Collections.Generic;
   

    /// <summary>
    /// Controls the Spawning and Despawning of a generic object.
    /// </summary>
    public class SpawnObject : MonoBehaviour
    {
        /// <summary>
        /// The generic Object to Control
        /// </summary>
        [Serialized, Cleared]
        [field: DocumentedByXml]
        public GameObject genericObject { get; set; }

        /// <summary>
        /// The Parent Object that holds the ButtonGroup and ObjectContainer (Empty GameObject)
        /// </summary>
        [Serialized, Cleared]
        [field: DocumentedByXml]
        public GameObject parentObject { get; set; }

        /// <summary>
        /// The maximum clones that can be spawned.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        public int SpawnLimits { get; set; }

        //Cloned Object
        private GameObject clone;
        //Counter if Reached SpawnLimit
        private int spawnLimitCounter = 0;
        //List of GameObjects
        private List<GameObject> clonedList = new List<GameObject>();

        /// <summary>
        /// Spawn Object Boolean that allows the player to spawn an object. (REQUIRES SpawnObject.CS on main GameObject)
        /// </summary>
        public bool spawnObject { get; set; }

        /// <summary>
        /// Despawn Object Boolean that allows the player to despawn an object. (REQUIRES SpawnObject.CS on main GameObject)
        /// </summary>
        public bool despawnObject { get; set; }
        
        protected virtual void Update()
        {
            if (spawnLimitCounter < SpawnLimits)
            {
                if (spawnObject == true)
                {

                    clone = Instantiate(genericObject);                                 //Clones an Object
                    clone.SetActive(true);                                              //Sets Object to Appear
                    Vector3 currentPositionOfClone = clone.transform.position;          //Grabs the current Vector3 position from Cloned Object
                    clone.transform.parent = parentObject.transform;                    //Set the clone object inside the Parent Object
                    clone.transform.localPosition = currentPositionOfClone;             //Set position to the previous cloned object's position               
                    clonedList.Add(clone);                                              //Add Clone object to the list
                    spawnObject = false;                                                //Set Boolean flag off
                    spawnLimitCounter += 1;                         
                }

                if (despawnObject == true)
                {
                    if (clonedList != null) //prevent IndexOutOfRangeException for empty list
                    {
                        Destroy(clonedList[clonedList.Count - 1]);  //Destroy GameObject
                        clonedList.RemoveAt(clonedList.Count - 1);  //Remove from List
                    }
                    despawnObject = false;
                    spawnLimitCounter -= 1;
                }
            } else
            {
                if (despawnObject == true)
                {
                    if (clonedList != null) //prevent IndexOutOfRangeException for empty list
                    {
                        Destroy(clonedList[clonedList.Count - 1]);  //Destroy GameObject
                        clonedList.RemoveAt(clonedList.Count - 1);  //Remove from List
                    }
                    despawnObject = false;
                    spawnLimitCounter -= 1;
                }
            }
        }
    }
}