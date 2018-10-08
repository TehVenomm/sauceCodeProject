using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel
{
	private const float OffSetX = 5000f;

	private static List<Transform> _models;

	private Transform model;

	private Transform _transform;

	private bool isLoading;

	public static List<Transform> models
	{
		get
		{
			if (_models == null)
			{
				_models = new List<Transform>();
			}
			return _models;
		}
	}

	public UIModel()
		: this()
	{
	}

	public static UIModel Get(Transform t)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		UIModel uIModel = t.GetComponent<UIModel>();
		if (uIModel == null)
		{
			uIModel = t.get_gameObject().AddComponent<UIModel>();
		}
		return uIModel;
	}

	private void OnDisable()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (model != null)
		{
			model.get_gameObject().SetActive(false);
		}
	}

	private void OnEnable()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (model != null)
		{
			model.get_gameObject().SetActive(true);
		}
	}

	public unsafe static void UpdateModelOffset()
	{
		List<Transform> models = UIModel.models;
		if (_003C_003Ef__am_0024cache4 == null)
		{
			_003C_003Ef__am_0024cache4 = new Action<Transform, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		models.DoAction(_003C_003Ef__am_0024cache4);
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
	}

	public void Init(string resource_name)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (model != null)
		{
			model.get_gameObject().SetActive(true);
		}
		else if (!isLoading)
		{
			this.StartCoroutine(DoInit(resource_name));
		}
	}

	public void Remove()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (model != null)
		{
			models.Remove(model);
			Object.Destroy(model.get_gameObject());
			model = null;
		}
	}

	public void SetActive(bool active)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (model != null)
		{
			model.get_gameObject().SetActive(active);
		}
	}

	private IEnumerator DoInit(string resource_name)
	{
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject load_object = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.COMMON, resource_name);
		yield return (object)load_queue.Wait();
		model = load_object.Realizes(MonoBehaviourSingleton<AppMain>.I._transform, -1);
		if (model != null)
		{
			models.Add(model);
			UpdateModelOffset();
		}
		isLoading = false;
	}

	private void OnDestroy()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (model != null)
		{
			models.Remove(model);
			Object.DestroyImmediate(model.get_gameObject());
		}
		UpdateModelOffset();
	}
}
