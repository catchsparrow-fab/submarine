using UnityEngine;
using System.Collections;

public class Scene_controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI() {
		if (Application.loadedLevelName == "menu"){
		 if (GUI.Button(new Rect(Screen.width/2-50,150-130,100,30),"Autogyro"))
			 Application.LoadLevel ("autogyro");
		 if (GUI.Button(new Rect(Screen.width/2-50,225-130,100,30),"Plane"))
			 Application.LoadLevel ("plane");
		 if (GUI.Button(new Rect(Screen.width/2-50,300-130,100,30),"Rocket"))
			 Application.LoadLevel ("rocket_plane");
		 if (GUI.Button(new Rect(Screen.width/2-50,375-130,100,30),"Helicopter"))
			 Application.LoadLevel ("helicopter");
		if (GUI.Button(new Rect(Screen.width/2-50,450-130,100,30),"Helicopter 2D"))
				Application.LoadLevel ("heli_2d");
	     if (GUI.Button(new Rect(Screen.width/2-50,525-130,100,30),"UFO"))
			 Application.LoadLevel ("ufo");
		 if (GUI.Button(new Rect(Screen.width/2-50,600-130,100,30),"Dirigible"))
			 Application.LoadLevel ("dirigible");
			if (GUI.Button(new Rect(Screen.width/2-50,675-130,100,30),"Dragon"))
				Application.LoadLevel ("dragon");
		 
		}else{
		 if (GUI.Button(new Rect(10,10,50,30),"Menu"))
			 Application.LoadLevel ("Menu");
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
