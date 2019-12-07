using System;
using System.Collections;
using UnityEngine;

public class ManualCoroutine
{
	public int id;

	public object data;

	private MonoBehaviour mono;

	private IEnumerator instance;

	private IEnumerator updateInstance;

	public static ManualCoroutine current
	{
		get;
		private set;
	}

	public bool active
	{
		get;
		set;
	}

	public bool isEnabled => instance != null;

	public Action callback
	{
		get;
		set;
	}

	public ManualCoroutine()
	{
		active = true;
	}

	public ManualCoroutine(int _id, MonoBehaviour _mono, IEnumerator co, bool _active = true, Action _callback = null, object _data = null)
	{
		id = _id;
		Set(_mono, co);
		active = _active;
		callback = _callback;
		data = _data;
	}

	public void Set(MonoBehaviour _mono, IEnumerator co)
	{
		Clear();
		mono = _mono;
		instance = co;
		updateInstance = DoManualCoroutineUpdate();
		mono.StartCoroutine(updateInstance);
	}

	public void Clear()
	{
		if (mono != null && updateInstance != null)
		{
			mono.StopCoroutine(updateInstance);
		}
		instance = null;
		updateInstance = null;
		mono = null;
		active = true;
	}

	private IEnumerator DoManualCoroutineUpdate()
	{
		while (true)
		{
			if (instance == null || !active || !mono.enabled)
			{
				yield return null;
				continue;
			}
			current = this;
			if (!instance.MoveNext())
			{
				break;
			}
			current = null;
			yield return instance.Current;
		}
		if (callback != null)
		{
			callback();
		}
		current = null;
		Clear();
	}
}
