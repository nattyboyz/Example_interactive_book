using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

// Work only in orthographic camera
namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Follow drag")]
		public class InteractFollowTouch : MonoBehaviour, IDragHandler {

			public GameObject targetGameobject;

			public void OnDrag(PointerEventData eventData)
			{

				targetGameobject.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(eventData.position).x,
				                                                   Camera.main.ScreenToWorldPoint(eventData.position).y,
																	targetGameobject.transform.position.z);
			}
		}
	}
}