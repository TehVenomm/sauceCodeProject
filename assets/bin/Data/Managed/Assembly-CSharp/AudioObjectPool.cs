using System;
using UnityEngine;

public class AudioObjectPool : MonoBehaviourSingleton<AudioObjectPool>
{
	private const int STACK_MAX = 100;

	private int current_index;

	private AudioObject[] audio_object_stack;

	public int CachedObjectCount => current_index;

	protected override void Awake()
	{
		base.Awake();
		CreateStack();
	}

	public static void StopAllLentObjects()
	{
		if (MonoBehaviourSingleton<AudioObjectPool>.IsValid())
		{
			MonoBehaviourSingleton<AudioObjectPool>.I.ForEachLentObjects(delegate(AudioObject ao)
			{
				if ((UnityEngine.Object)ao != (UnityEngine.Object)null)
				{
					ao.Stop(0);
				}
			});
		}
	}

	public static void StopAll()
	{
		if (MonoBehaviourSingleton<AudioObjectPool>.IsValid())
		{
			MonoBehaviourSingleton<AudioObjectPool>.I.ForEach(delegate(AudioObject ao)
			{
				if ((UnityEngine.Object)ao != (UnityEngine.Object)null && ao.PlayPhase == AudioObject.Phase.PLAYING)
				{
					ao.Stop(0);
				}
			});
		}
	}

	private void ForEach(Action<AudioObject> act)
	{
		if (audio_object_stack != null)
		{
			AudioObject[] array = audio_object_stack;
			foreach (AudioObject obj in array)
			{
				act(obj);
			}
		}
	}

	private void ForEachLentObjects(Action<AudioObject> act)
	{
		if (audio_object_stack != null)
		{
			int num = current_index - 1;
			if (num >= 0)
			{
				for (int i = num; i < 100; i++)
				{
					act(audio_object_stack[i]);
				}
			}
		}
	}

	private void CreateStack()
	{
		audio_object_stack = new AudioObject[100];
		for (int i = 0; i < 100; i++)
		{
			audio_object_stack[i] = CreateObject(i + 1);
			audio_object_stack[i].transform.parent = base.transform;
			audio_object_stack[i].gameObject.SetActive(false);
		}
		SetCursorTail();
	}

	private AudioObject CreateObject(int managed_id)
	{
		GameObject gameObject = new GameObject("AudioObject");
		AudioObject audioObject = gameObject.AddComponent<AudioObject>();
		AudioSource source = gameObject.AddComponent<AudioSource>();
		AudioObject.Init(audioObject, source, managed_id);
		return audioObject;
	}

	public static AudioObject Borrow()
	{
		if (!MonoBehaviourSingleton<AudioObjectPool>.IsValid())
		{
			return null;
		}
		return MonoBehaviourSingleton<AudioObjectPool>.I.Borrow_Imm();
	}

	private AudioObject Borrow_Imm()
	{
		if (CachedObjectCount > 0)
		{
			AudioObject audioObject = audio_object_stack[current_index];
			audioObject.gameObject.SetActive(true);
			DownCursor();
			return audioObject;
		}
		return CreateObject(-1);
	}

	public static void Release(AudioObject obj)
	{
		if (MonoBehaviourSingleton<AudioObjectPool>.IsValid())
		{
			MonoBehaviourSingleton<AudioObjectPool>.I.Release_Imm(obj);
		}
	}

	private void Release_Imm(AudioObject obj)
	{
		if (obj.ID > 0)
		{
			UpCursor();
			audio_object_stack[current_index] = obj;
			audio_object_stack[current_index].transform.parent = base.transform;
			obj.gameObject.SetActive(false);
		}
		else
		{
			UnityEngine.Object.Destroy(obj.gameObject);
		}
	}

	private void SetCursorTail()
	{
		current_index = 99;
	}

	private void UpCursor()
	{
		current_index = Mathf.Min(current_index + 1, 99);
	}

	private void DownCursor()
	{
		current_index = Mathf.Max(current_index - 1, 0);
	}
}
