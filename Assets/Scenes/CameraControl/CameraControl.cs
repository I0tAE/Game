using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public Transform player;//��ɫ
							// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();//��ȡ��ɫ
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
	}
}