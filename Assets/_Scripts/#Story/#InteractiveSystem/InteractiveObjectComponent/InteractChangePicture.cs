using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using DG.Tweening;

namespace ImaginMe.Interactive
{
	[AddComponentMenu("Pigsssgames/P'Bank story interactive/Sprite change [in order]")]
	public class InteractChangePicture : InteractiveProperties, IPointerClickHandler {
		
		[SerializeField] SpriteRenderer[] spriteOb;
		int current = 0;
		
		public UnityEvent onTrigger = null;


		void Awake()
		{
			if (spriteOb.Length > 0) {
				spriteOb [0].DOFade (1, 0);
				for (int i = 1; i < spriteOb.Length; i++) {
					spriteOb [i].DOFade (0, 0);
				}
			}
		}

		public void OnPointerClick (PointerEventData eventData)
		{
			if(isValid)
			{
				ChangeSprite(current);
				isValid = false;
			}
		}
		public void Init(SpriteRenderer[] sprites){
			spriteOb = sprites;
			spriteOb [0].DOFade (1, 0);
			for (int i = 1; i < spriteOb.Length; i++) {
				spriteOb [i].DOFade (0, 0);
			}
		}

		void ChangeSprite(int index)
		{
			if(onTrigger != null)onTrigger.Invoke();

			if(spriteOb != null && spriteOb.Length >1)
			{
				float fadeTime = refreshTime;
				int fadeOutObject = index;
				int fadeInObject = 0;

				if(current >= spriteOb.Length -1)
				{
					current = fadeInObject = 0;
					current = 0;
				}
				else
				{
					current ++;
					fadeInObject = current ;
				}

				spriteOb[fadeInObject].DOFade(1,fadeTime).OnUpdate(()=>{

					spriteOb[fadeOutObject].DOFade(0,fadeTime).OnComplete(()=>{
					});
				});
			}
		}
	}
}