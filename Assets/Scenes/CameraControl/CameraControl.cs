using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public Transform player;//角色
							// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();//获取角色
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
	}
}