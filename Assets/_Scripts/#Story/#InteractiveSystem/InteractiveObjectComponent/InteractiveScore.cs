using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Add score")]
		public class InteractiveScore : InteractiveProperties,IPointerClickHandler
		{
			[SerializeField] string idName;
			public UnityEvent onEnable;

			public string IdName {
				get {
					return idName;
				}
				set {
					idName = value;
				}
			}

			public Action OnTrigger = null;

			public void OnPointerClick (PointerEventData eventData)
			{
				if(isValid)
				{
					isValid = false;
					if(EventManager.Instance) EventManager.Instance.QueueEvent(new Ev.Interactive.OnScored(gameObject));
				}
			}

			public void OnEnable(){
				if (onEnable != null)
					onEnable.Invoke ();
			}
		}
	}
}
