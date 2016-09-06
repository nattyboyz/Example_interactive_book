////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: Events.cs
//Copy Rights : © PIGSSS GAMES Co.,Ltd. All rights reserved.
//Description : Contain all events.
///////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;


namespace Ev{
	// story
	public class OnTriggerEndScene : GameEvent{ 
		public OnTriggerEndScene(){}
	}
	namespace Story
	{
		public class OnTriggerBgTutorial: GameEvent{ 
			public OnTriggerBgTutorial(){}
		}

		public class OnStartStory:GameEvent{
			public OnStartStory(){}
		}

		public class OnExitStory: GameEvent{
			public OnExitStory(){}
		}
	}

	namespace Interactive
	{
		public class OnScored: GameEvent
		{ 
			public GameObject gob;
			public OnScored(GameObject _gob){
				gob = _gob;
			}
		}
	}

}
	


