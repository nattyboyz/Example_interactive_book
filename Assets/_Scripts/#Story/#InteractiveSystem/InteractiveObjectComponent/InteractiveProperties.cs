using UnityEngine;
using System.Collections;

namespace ImaginMe
{
	namespace Interactive
	{
		public class InteractiveProperties : MonoBehaviour 
		{
			public enum InteractiveObjectActiveState {Active_On_Start, Deactive_On_Start, Active_After_End_Scene_Trigger}

			public InteractiveObjectActiveState  activeStatus = InteractiveObjectActiveState.Active_On_Start;
			[SerializeField] public bool oneTime = false;
			public float refreshTime = 0.5f;
			protected bool isEnable = true;
			protected bool isValid = true;

			protected float time = 0;
	
			virtual public void Start()
			{
				if (activeStatus == InteractiveObjectActiveState.Deactive_On_Start){
					isEnable = false;
				}
				else if (activeStatus == InteractiveObjectActiveState.Active_After_End_Scene_Trigger) 
				{
					isEnable = false;
					isValid = false;

					if (EventManager.Instance != null) {
						EventManager.Instance.AddListener<Ev.OnTriggerEndScene> (ActiveOnEndScene);
					}
				}
			}
			
			virtual public void Update()
			{
				if (isEnable) {
					if (!isValid) {
						time += Time.deltaTime;
						if (!oneTime && time > refreshTime) {
							time = 0;
							isValid = true;
						}
					}
				}
			}

			public virtual void OnDisable(){
				if (EventManager.Instance != null) {
					EventManager.Instance.RemoveListener<Ev.OnTriggerEndScene> (ActiveOnEndScene);
				}
			}

			void ActiveOnEndScene(Ev.OnTriggerEndScene e)
			{
				isEnable = true;
			}
				
		}
	}
}
