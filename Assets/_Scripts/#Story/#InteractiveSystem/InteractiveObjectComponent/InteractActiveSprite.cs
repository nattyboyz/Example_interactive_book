using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using DG.Tweening;



namespace ImaginMe.Interactive
{
	[AddComponentMenu("Pigsssgames/P'Bank story interactive/Sprite active[OnOff]")]
	public class InteractActiveSprite : InteractiveProperties,IPointerClickHandler {

		public SpriteRenderer[] targetSprites;
		[HideInInspector] public bool[] isActive;

		public UnityEvent onActive;
		public UnityEvent onInActive;
		public UnityEvent onTrigger;

		virtual public void Awake () 
		{
			InitFlag ();
		}

		void InitFlag()
		{
			if (targetSprites == null || targetSprites.Length == 0) {
				return;
			}

			isActive = new bool[targetSprites.Length];

			for(int i=0; i< targetSprites.Length; i++)
			{
				if(!targetSprites[i].gameObject.activeInHierarchy || targetSprites[i].color.a ==0)
					isActive[i] = false;
				else
					isActive[i] = true;
			}
		}

		public void Init(SpriteRenderer[] sprites){
			targetSprites = sprites;
			InitFlag ();
		}

		virtual public void OnPointerClick (PointerEventData eventData)
		{
			if(isValid)
			{
				ActiveSprite();
				isValid = false;
			}
		}

		virtual public void ActiveSprite()
		{
			if (onTrigger != null)
				onTrigger.Invoke ();

			for(int i =0;i<targetSprites.Length;i++)
			{
				if(isActive[i])
				{
					if(onInActive!= null)onInActive.Invoke();
					isActive[i] = false;
					targetSprites[i].DOFade(0,refreshTime - 0.1f);
				}
				else
				{
					if(onActive!= null)onActive.Invoke();
					isActive[i] = true;
					targetSprites[i].DOFade(1,refreshTime - 0.1f);
				}
			}
		}

		public virtual void OnDisable()
		{
			DOTween.PauseAll ();
			base.OnDisable ();
		}
	}

	public static class InteractActiveSpriteExt 
	{
		public static void IntActiveSpriteGetSprite(this GameObject obj , GameObject objWithSprite)
		{
			obj.GetComponent<InteractActiveSprite>().Init(new SpriteRenderer[]{ objWithSprite.GetComponent<SpriteRenderer>()});
		}
	}

}
