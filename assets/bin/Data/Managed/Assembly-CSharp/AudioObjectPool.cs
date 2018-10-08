using System;
using System.Collections;
using UnityEngine;

public class AudioObjectPool : MonoBehaviourSingleton<AudioObjectPool>
{
	private const int STACK_MAX = 20;

	private int current_index;

	private AudioObject[] audio_object_stack;

	public int CachedObjectCount => current_index;

	protected override void Awake()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.StartCoroutine(CreateStack());
	}

	public static void StopAllLentObjects()
	{
		if (MonoBehaviourSingleton<AudioObjectPool>.IsValid())
		{
			MonoBehaviourSingleton<AudioObjectPool>.I.ForEachLentObjects(delegate(AudioObject ao)
			{
				if (ao != null)
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
				if (ao != null && ao.PlayPhase == AudioObject.Phase.PLAYING)
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
				for (int i = num; i < 20; i++)
				{
					act(audio_object_stack[i]);
				}
			}
		}
	}

	private IEnumerator CreateStack()
	{
		audio_object_stack = new AudioObject[20];
		for (int i = 0; i < 20; i++)
		{
			audio_object_stack[i] = CreateObject(i + 1);
			audio_object_stack[i].get_transform().set_parent(this.get_transform());
			audio_object_stack[i].get_gameObject().SetActive(false);
			yield return (object)null;
		}
		SetCursorTail();
	}

	private AudioObject CreateObject(int managed_id)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Expected O, but got Unknown
		GameObject val = new GameObject("AudioObject");
		AudioObject audioObject = val.AddComponent<AudioObject>();
		AudioSource val2 = val.AddComponent<AudioSource>();
		val2.set_playOnAwake(false);
		AudioObject.Init(audioObject, val2, managed_id);
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
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (CachedObjectCount > 0)
		{
			AudioObject audioObject = audio_object_stack[current_index];
			audioObject.get_gameObject().SetActive(true);
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
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (obj.ID > 0)
		{
			UpCursor();
			audio_object_stack[current_index] = obj;
			audio_object_stack[current_index].get_transform().set_parent(this.get_transform());
			obj.get_gameObject().SetActive(false);
		}
		else
		{
			Object.Destroy(obj.get_gameObject());
		}
	}

	private void SetCursorTail()
	{
		current_index = 19;
	}

	private void UpCursor()
	{
		current_index = Mathf.Min(current_index + 1, 19);
	}

	private void DownCursor()
	{
		current_index = Mathf.Max(current_index - 1, 0);
	}
}
