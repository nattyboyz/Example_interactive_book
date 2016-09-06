﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager> 
{
	Dictionary<int,Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>> ();

	public void CreatePool(GameObject prefab, int poolSize)
	{
		int poolKey = prefab.GetInstanceID ();
		
		if (!poolDictionary.ContainsKey (poolKey)) {
			poolDictionary.Add (poolKey, new Queue<ObjectInstance> ());
			
			GameObject poolHolder = new GameObject (prefab.name + " pool");
			poolHolder.transform.parent = transform;
			
			for (int i = 0; i < poolSize; i++) {
				ObjectInstance newObject = new ObjectInstance(Instantiate (prefab) as GameObject);
				newObject.gameObject.name = prefab.name + i;

				poolDictionary [poolKey].Enqueue (newObject);
				newObject.SetParent (poolHolder.transform);
			}
		}
	}
	
	public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation) {
		int poolKey = prefab.GetInstanceID ();
		
		if (poolDictionary.ContainsKey (poolKey)) {
			ObjectInstance objectToReuse = poolDictionary [poolKey].Dequeue ();
			poolDictionary [poolKey].Enqueue (objectToReuse);
			
			objectToReuse.Reuse (position, rotation);
			return objectToReuse.gameObject;
		}else 
			return null;
	}
	
	public class ObjectInstance
	{
		public GameObject gameObject;
		Transform transform;
		
		bool hasPoolObjectComponent;
		PoolObject poolObjectScript;
		
		public ObjectInstance(GameObject objectInstance)
		{
			gameObject = objectInstance;
			transform = gameObject.transform;
			gameObject.SetActive(false);
			
			if (gameObject.GetComponent<PoolObject>()) {
				hasPoolObjectComponent = true;
				poolObjectScript = gameObject.GetComponent<PoolObject>();
			}
		}
		
		public void Reuse(Vector3 position, Quaternion rotation)
		{
			gameObject.SetActive (true);
			transform.position = position;
			transform.rotation = rotation;
			
			if (hasPoolObjectComponent) {
				poolObjectScript.OnObjectReuse ();
			}
		}
		
		public void SetParent(Transform parent) {
			transform.parent = parent;
		}
	}

}