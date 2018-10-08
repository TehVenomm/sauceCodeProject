using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegreePlate : MonoBehaviour
{
	private const int DEGREE_PART = 4;

	private const string PREFAB_NAME = "DegreePlate";

	private const string UNKNOWN_FRAME = "DF_UNKNOWN";

	[SerializeField]
	private UILabel mText;

	[SerializeField]
	private UITexture mFrame;

	private string mFrameName;

	public static void Create(MonoBehaviour call_mono, List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(RESOURCE_CATEGORY.UI, "DegreePlate");
		if (cachedResourceObject != null)
		{
			DegreePlate component = (UnityEngine.Object.Instantiate(cachedResourceObject.obj) as GameObject).GetComponent<DegreePlate>();
			component.Initialize(degreeIds, isButton, onFinish);
		}
		else
		{
			MonoBehaviourSingleton<ResourceManager>.I.Load(ResourceLoad.GetResourceLoad(call_mono, true), RESOURCE_CATEGORY.UI, "DegreePlate", delegate(ResourceManager.LoadRequest x, ResourceObject[] y)
			{
				y[0].refCount++;
				DegreePlate component2 = (UnityEngine.Object.Instantiate(y[0].obj) as GameObject).GetComponent<DegreePlate>();
				component2.Initialize(degreeIds, isButton, onFinish);
			}, null, false, null);
		}
	}

	public void Initialize(List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		StartCoroutine(DoInitialize(degreeIds, isButton, onFinish));
	}

	private IEnumerator DoInitialize(List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		if (degreeIds == null || degreeIds.Count != 4)
		{
			onFinish?.Invoke(null);
		}
		else
		{
			DegreeTable.DegreeData frameData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[0]);
			if (frameData == null || (frameData.type != DEGREE_TYPE.FRAME && frameData.type != DEGREE_TYPE.SPECIAL_FRAME))
			{
				onFinish?.Invoke(null);
			}
			else
			{
				string degreeText;
				if (frameData.type == DEGREE_TYPE.SPECIAL_FRAME)
				{
					degreeText = string.Empty;
				}
				else
				{
					DegreeTable.DegreeData prefixData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[1]);
					DegreeTable.DegreeData conData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[2]);
					DegreeTable.DegreeData suffixData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[3]);
					degreeText = prefixData.name + " " + conData.name + " " + suffixData.name;
				}
				mText.text = degreeText;
				mText.gameObject.SetActive(false);
				yield return (object)StartCoroutine(_SetFrame(ResourceName.GetDegreeFrameName(degreeIds[0])));
				mText.gameObject.SetActive(true);
				SetEnableButtonCollider(isButton);
				onFinish?.Invoke(this);
			}
		}
	}

	public void SetFrame(int degreeId)
	{
		StartCoroutine(_SetFrame(ResourceName.GetDegreeFrameName(degreeId)));
	}

	public void SetUnknownFrame()
	{
		StartCoroutine(_SetFrame("DF_UNKNOWN"));
	}

	private IEnumerator _SetFrame(string frameName)
	{
		if (!(mFrameName == frameName))
		{
			LoadingQueue load = new LoadingQueue(this);
			LoadObject frameTexture = load.Load(RESOURCE_CATEGORY.DEGREE_FRAME, frameName, false);
			mFrame.enabled = false;
			if (load.IsLoading())
			{
				yield return (object)load.Wait();
			}
			mFrame.mainTexture = (frameTexture.loadedObject as Texture);
			mFrameName = frameName;
			mFrame.enabled = true;
		}
	}

	public void SetEnableButtonCollider(bool enable)
	{
		base.transform.GetComponent<Collider>().enabled = enable;
	}
}
