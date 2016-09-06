using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Animation change ")]
		public class InteractChangeAnim : InteractiveProperties,IPointerClickHandler {

			[SerializeField] List<Animator> animators;
			[SerializeField] List<string> triggerName;
			int current = 0;

			void Awake ()
			{
				if (triggerName != null && triggerName.Count > 0) {
					foreach (Animator a in animators) {
						a.SetTrigger (triggerName [current]);
					}
				}
			}
			
			public void OnPointerClick (PointerEventData eventData)
			{
				if(isValid)
				{
					isValid = false;
					TriggerAnimator();
				}
			}

			public void Init(List<Animator> anims,int startIndex = 0)
			{
				animators = anims;
				current = startIndex;

				if (triggerName != null && triggerName.Count > 0)
				{
					foreach (Animator a in animators) {
						a.SetTrigger (triggerName [startIndex]);
					}
				}
			}
					
			public void TriggerAnimator(){
				
				if(animators != null && triggerName.Count >1)
				{
					if(current >= triggerName.Count -1)
					{
						current = 0;
					}
					else
					{
						current ++;
					}
					
					foreach(Animator a in animators){
						a.SetTrigger(triggerName[current]);
					}
				}
			}
		}
	}
}
