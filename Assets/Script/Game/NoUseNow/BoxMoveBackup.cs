using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMoveBackup : MonoBehaviour {

	// space jump
	// f fire1
	// wasd move

	GameObject[] allBox;

	void Start () {
		// input 생성해두기?
		allBox = GameObject.FindGameObjectsWithTag ("Box");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 버튼을 누른 상ㅐ에서만 상자를 움ㅣㄱ일수 있다
        //if (Input.GetButtonDown("Fire1"))
        //{ 
        //    Debug.Log("버튼 누른상태");
            
        //}
        changeKinematic(Input.GetButton("Fire1"));
    }

    private void changeKinematic(bool button)
    {
        for (int x = 1; x < allBox.Length; x++)
        {

            GameObject nineBox = allBox[x];
            Rigidbody rb = nineBox.GetComponent<Rigidbody>();
            rb.isKinematic = !button;
        }
    }
}
