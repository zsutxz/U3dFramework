/// <summary>
/// <para> Class Name : MonoSingleton </para>
/// <para> Description : It's a level singleton. Generic Base Singleton Template for MonoBehaviour. </para>
/// <para> Create Author : XJY </para>
/// <para> Update Author : XJY </para>
/// <para> Update Date : 2013/6/6 </para>
/// </summary>

using UnityEngine;

public class Singleton<T> where T : new()
{
	static protected T sInstance;
	static protected bool IsCreate = false;

	public static T Instance
	{
		get
		{
			if (IsCreate == false)
			{
				CreateInstance();
			}

			return sInstance;
		}
	}

	public static void CreateInstance()
	{
		if (IsCreate == true)
			return;

		IsCreate = true;
		sInstance = new T();
	}

	public static void ReleaseInstance()
	{
		sInstance = default(T);
		IsCreate = false;
	}
}
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	// Mono singleton's default name
	private const string MONO_SINGLETON_NAME 	= "_MonoSingleton";
	
	
	// Instance's name of the singleton.
	protected static string s_singletonName 		= MONO_SINGLETON_NAME;
	// Instance's object
	protected static GameObject s_singletinObject 	= null;
	// MonoSingleton's Instance.
	private static T s_instance = null;
	public static T Instance
	{
		get
		{
			if (s_instance==null)
				Register ();
			return s_instance;
		}
	}
	// Gets a value indicating whether this instance is registered.
	public static bool IsRegistered
	{
		get { return (s_instance!=null); }
	}
	
	
	/// <summary>
	/// Initializer this instance.
	/// </summary>
	static void Initializer ()
	{
		// Set name
		s_singletonName = typeof(T).ToString ();
		
		// Load singleton prefab ressouce
		s_singletinObject = new GameObject (s_singletonName, typeof(T));
		s_singletinObject.name = MONO_SINGLETON_NAME+"("+s_singletonName+")";
		s_instance = s_singletinObject.GetComponent<T> ();
		if (s_instance==null)
		{
			s_instance = s_singletinObject.AddComponent <T> ();
		}
	}

	
	/// <summary>
	/// It mainly use to register this instance to current application before init.
	/// </summary>
	public static void Register ()
	{
		if (s_instance == null)
		{
			// Find the object first while null.
			s_instance = GameObject.FindObjectOfType (typeof(T)) as T;
			
			// None instance on scene, we should create a new one.
			if (s_instance==null)
			{
				//Debug.LogWarning ("-->> Lost \""+typeof(T).ToString()+"\" object, and will create a new one!");
				
				// Initializer singleton's instance.
				Initializer ();
				
				// Create MonoSingleton fail.
				if (s_instance==null)
					Debug.LogError ("--Error-->> Create \""+s_singletonName+"\" "+MONO_SINGLETON_NAME+" fail!");
			}
		}
	}
    

	#region Virtual Interface
	
	/// <summary>
	/// Init this singleton instance on awake once.
	/// </summary>
	public virtual void Init () {}
	
	#endregion
	
	// If you want to do something on Awake, you need to override it.
	void Awake ()
	{
		if (s_instance == null)
		{
			s_singletonName = this.name;
			s_singletinObject = this.gameObject;
			s_instance = this as T;
#if UNITY_EDITOR
			Debug.Log (">> Registed "+MONO_SINGLETON_NAME+" : "+s_singletonName+".");
#endif
		}
		else
		{
			DestroyImmediate (this);
			return;
		}
		
		Init ();
	}
	
}
