using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel : MonoBehaviour
{
	private static List<Transform> _models;

	private const float OffSetX = 5000f;

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
		UIModel uIModel = t.GetComponent<UIModel>();
		if (uIModel == null)
		{
			uIModel = t.get_gameObject().AddComponent<UIModel>();
		}
		return uIModel;
	}

	private void OnDisable()
	{
		if (model != null)
		{
			model.get_gameObject().SetActive(false);
		}
	}

	private void OnEnable()
	{
		if (model != null)
		{
			model.get_gameObject().SetActive(true);
		}
	}

	public static void UpdateModelOffset()
	{
		models.DoAction(delegate(Transform t, int i)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			t.set_position(new Vector3((float)(i + 1) * 5000f, 0f, 0f));
		});
	}

	private void Awake()
	{
		_transform = this.get_transform();
	}

	public void Init(string resource_name)
	{
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
		if (model != null)
		{
			models.Remove(model);
			Object.Destroy(model.get_gameObject());
			model = null;
		}
	}

	public void SetActive(bool active)
	{
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
		yield return load_queue.Wait();
		model = load_object.Realizes(MonoBehaviourSingleton<AppMain>.I._transform);
		if (model != null)
		{
			models.Add(model);
			UpdateModelOffset();
		}
		isLoading = false;
	}

	private void OnDestroy()
	{
		if (model != null)
		{
			models.Remove(model);
			Object.DestroyImmediate(model.get_gameObject());
		}
		UpdateModelOffset();
	}
}
