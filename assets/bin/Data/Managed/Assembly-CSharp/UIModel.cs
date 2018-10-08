using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel : MonoBehaviour
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

	public static UIModel Get(Transform t)
	{
		UIModel uIModel = t.GetComponent<UIModel>();
		if ((Object)uIModel == (Object)null)
		{
			uIModel = t.gameObject.AddComponent<UIModel>();
		}
		return uIModel;
	}

	private void OnDisable()
	{
		if ((Object)model != (Object)null)
		{
			model.gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		if ((Object)model != (Object)null)
		{
			model.gameObject.SetActive(true);
		}
	}

	public static void UpdateModelOffset()
	{
		models.DoAction(delegate(Transform t, int i)
		{
			t.position = new Vector3((float)(i + 1) * 5000f, 0f, 0f);
		});
	}

	private void Awake()
	{
		_transform = base.transform;
	}

	public void Init(string resource_name)
	{
		if ((Object)model != (Object)null)
		{
			model.gameObject.SetActive(true);
		}
		else if (!isLoading)
		{
			StartCoroutine(DoInit(resource_name));
		}
	}

	public void Remove()
	{
		if ((Object)model != (Object)null)
		{
			models.Remove(model);
			Object.Destroy(model.gameObject);
			model = null;
		}
	}

	public void SetActive(bool active)
	{
		if ((Object)model != (Object)null)
		{
			model.gameObject.SetActive(active);
		}
	}

	private IEnumerator DoInit(string resource_name)
	{
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject load_object = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.COMMON, resource_name);
		yield return (object)load_queue.Wait();
		model = load_object.Realizes(MonoBehaviourSingleton<AppMain>.I._transform, -1);
		if ((Object)model != (Object)null)
		{
			models.Add(model);
			UpdateModelOffset();
		}
		isLoading = false;
	}

	private void OnDestroy()
	{
		if ((Object)model != (Object)null)
		{
			models.Remove(model);
			Object.DestroyImmediate(model.gameObject);
		}
		UpdateModelOffset();
	}
}
