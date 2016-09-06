using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace ImaginMe
{
	namespace Interactive
	{
		public class ChangeBGState : InteractActiveSprite
		{
			public GameObject activeObject;

			override public void Awake () 
			{
				base.Awake();
			}

			void OnEnable()
			{
				base.Awake();
			}

			void Start()
			{
				//base.Awake();
				base.onActive .AddListener( () => {
					EventManager.Instance.QueueEvent (new Ev.Story.OnTriggerBgTutorial ());
				});
				ActiveObject();
			}

			override public void Update()
			{
				base.Update();
			}

			override public void OnPointerClick (PointerEventData eventData)
			{
				base.OnPointerClick(eventData);
				ActiveObject();
			}

			void ActiveObject()
			{
				if (isActive == null || isActive.Length == 0)
					return;
				
				if(isActive[0]){
					if(activeObject!=null)
						activeObject.SetActive(true);
				}else{
					if(activeObject!=null)
						activeObject.SetActive(false);
				}
			}
		}
	}
}