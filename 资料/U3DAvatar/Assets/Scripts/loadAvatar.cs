using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadAvatar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (AvatarSys._instance.nowCount == 0)
        {
            AvatarSys._instance.GirlAvatar();
        }
        else {
            AvatarSys._instance.BoyAvatar();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
