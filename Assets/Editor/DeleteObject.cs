 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEditor;
 using UnityEditor.SceneManagement;
 using UnityEngine.SceneManagement;
 
 [InitializeOnLoad]
 public class DeleteObject : Editor {
 
     static DeleteObject() {
 
         List<GameObject> rootObjects = new List<GameObject>();
 
         // get root objects in scene
 
         Scene scene = SceneManager.GetActiveScene();
         scene.GetRootGameObjects( rootObjects );
 
         // iterate root objects and do something
 
         for (int i = 0; i < rootObjects.Count; ++i) {
 
             GameObject gameObject = rootObjects[i];
 
             if (gameObject != null) { 
 
                 //Debug.Log ("Found the hidden passage");


                 if (gameObject.hideFlags == HideFlags.HideInHierarchy)
                 {
                     gameObject.hideFlags = HideFlags.None;
                 }
                 if (gameObject.name == "HiddenObject") {
 
                     gameObject.hideFlags = HideFlags.None;
                 }
             }
         }
     }
 }
