using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
	[SerializeField]
	private Camera orthoCamera;

	[SerializeField]
	private Transform playerTransform;

	public Camera OrthoCamera { get => orthoCamera; set => orthoCamera = value; }
    public Transform PlayerTransform { get => playerTransform; set => playerTransform = value; }
}