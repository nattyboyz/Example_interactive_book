using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;


namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Object active[OnOff]")]
		public class InteractActiveObject : InteractiveProperties ,IPointerClickHandler{
			
			[SerializeField] protected GameObject[] targetObject;
			 public bool[] isActive;

			public UnityEvent onActive;
			public UnityEvent onInactive;
			public UnityEvent onTrigger;

			virtual public void Awake () 
			{
				InitFlag ();
			}

			void InitFlag()
			{

				if (targetObject == null || targetObject.Length == 0) {
					return;
				}

				isActive = new bool[targetObject.Length];

				for(int i=0; i< isActive.Length; i++)
				{
					if(targetObject[i].activeInHierarchy)
						isActive[i] = true;
					else
						isActive[i] = false;
				}
			}
			
			virtual public void OnPointerClick (PointerEventData eventData)
			{
				if(isValid)
				{
					ActiveObject();
					isValid = false;
				}
			}

			virtual public void ActiveObject(){
				
				if (onTrigger != null)
					onTrigger.Invoke ();

				for(int no =0; no<targetObject.Length; no++)
				{
					if(isActive[no]){
						if (onInactive != null)
							onInactive.Invoke ();
						isActive[no] = false;
						targetObject[no].SetActive(false);

					}else{
						if (onActive != null)
							onActive.Invoke ();
						isActive[no] = true;
						targetObject[no].SetActive(true);
					}
				}
			}

			public void AddObject(GameObject[] objs)
			{
				targetObject = objs;
				InitFlag ();
			}
		}
	}
}
