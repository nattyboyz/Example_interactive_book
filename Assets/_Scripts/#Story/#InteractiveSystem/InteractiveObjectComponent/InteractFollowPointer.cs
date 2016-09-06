using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace ImaginMe
{
	namespace Interactive
	{
		public class InteractFollowPointer : MonoBehaviour,IDragHandler,IEndDragHandler ,IBeginDragHandler{

			[SerializeField] GameObject moveObject;
			[SerializeField] float moveDuration = 3;
			Tweener tween;
			Vector3 targetLastPos;

			void Start()
			{

			}
			public void OnDrag (PointerEventData eventData){
				/*
				if(eventData.delta.magnitude > 1){
					float xDelta = Mathf.Abs(moveObject.transform.position.x - Camera.main.ScreenToWorldPoint(eventData.position).x);
					float yDelta = Mathf.Abs(moveObject.transform.position.y - Camera.main.ScreenToWorldPoint(eventData.position).y);

					moveObject.transform.DOMove(Camera.main.ScreenToWorldPoint(eventData.position),xDelta + yDelta,false);

					Debug.LogError(eventData.delta.magnitude);
				}*/
				//moveObject.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
				/*float xDelta = Mathf.Abs(moveObject.transform.position.x - Camera.main.ScreenToWorldPoint(eventData.position).x);
				float yDelta = Mathf.Abs(moveObject.transform.position.y - Camera.main.ScreenToWorldPoint(eventData.position).y);
				
				moveObject.transform.DOMove(Camera.main.ScreenToWorldPoint(eventData.position),(xDelta + yDelta) * 0.5f);*/
				// Use an Update routine to change the tween's endValue each frame
				// so that it updates to the target's position if that changed

				if (targetLastPos == moveObject.transform.position) return;
				// Add a Restart in the end, so that if the tween was completed it will play again

				if(eventData.pointerCurrentRaycast.gameObject == this .gameObject)
				{
					tween.ChangeEndValue(Camera.main.ScreenToWorldPoint(eventData.position), true).Restart();
					targetLastPos = Camera.main.ScreenToWorldPoint(eventData.position);		
				}
			}
			
			public void OnEndDrag (PointerEventData eventData)
			{
				DOTween.Kill(moveObject.transform);
			}

			public 	void OnBeginDrag (PointerEventData eventData)
			{
				Debug.LogError("DOWN");
				//moveObject.transform.DOMove(Camera.main.ScreenToWorldPoint(eventData.pressPosition),1f,false);
				tween = moveObject.transform.DOMove(Camera.main.ScreenToWorldPoint(eventData.position), moveDuration).SetAutoKill(false).SetEase(Ease.OutQuad);
				targetLastPos = Camera.main.ScreenToWorldPoint(eventData.position);
			}
		}
	}
}
