/// <summary>
/// Singleton. Taken from http://wiki.unity3d.com/index.php/Singleton , Sisada R.
/// </summary>


//-------------------------- USAGE --------------------------------------------------------------

//Manager.cs
//public class Manager : Singleton<Manager> {
//	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!
//
//	public string myGlobalVar = "whatever";
//}

//MyClass.cs
//public class MyClass : MonoBehaviour {
//	void Awake () {
//		Debug.Log(Manager.Instance.myGlobalVar);
//	}
//}



using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public virtual void Awake ()
	{
		if (_instance == null) 
		{
			_instance = this as T;
			DontDestroyOnLoad (this);
		} 
		else 
		{
			Destroy (this);
		}
	}
	
	private static T _instance;
	
	private static object _lock = new object();
	
	public static T Instance
	{
		get
		{
			if (applicationIsQuitting) {
				// Debug.LogWarning("[Singleton] Instance '"+ typeof(T) +
				// 	"' already destroyed on application quit." +
				// 	" Won't create again - returning null.");
				return null;
			}
			
			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType(typeof(T));
					
					if ( FindObjectsOfType(typeof(T)).Length > 1 )
					{
						Debug.LogError("Something went really wrong " +
							" - there should never be more than 1 singleton!" +
							" Reopening the scene might fix it."+_instance.name);
						return _instance;
					}
					
					if (_instance == null)
					{
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
						singleton.name = "(singleton) "+ typeof(T).ToString();
						
						DontDestroyOnLoad(singleton);
						
						Debug.Log("An instance of " + typeof(T) + 
							" is needed in the scene, so '" + singleton +
							"' was created with DontDestroyOnLoad.");
					} else {
						Debug.Log("Using instance already created: " +
							_instance.gameObject.name);
					}
				}
				
				return _instance;
			}
		}
	}
	
	
	private static bool applicationIsQuitting = false;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	public void OnDestroy () {
		
		//_instance will be assigned as soon as someone called the instance.
		applicationIsQuitting = true;
	}
	public void SetActive(bool value)
	{
		if (!value)
		{
			_instance = null;
			Destroy(gameObject);
			return;
		}
		_instance = Instance;
	}
	public void Init(){}
}