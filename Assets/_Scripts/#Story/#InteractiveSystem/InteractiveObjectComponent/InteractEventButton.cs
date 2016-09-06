using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Make object to button event")]
		public class InteractEventButton : InteractiveProperties, IPointerClickHandler {

			bool allowToClick = true;
			public Button.ButtonClickedEvent clickEvent;
			
			public void OnPointerClick (PointerEventData eventData)
			{
				if(allowToClick)
					clickEvent.Invoke();
			}

			public void SetActive(bool value)
			{
				allowToClick = value;
			}
		}
	}
}
