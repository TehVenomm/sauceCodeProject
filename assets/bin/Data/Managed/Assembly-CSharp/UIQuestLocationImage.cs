using System;
using System.Collections;
using UnityEngine;

public class UIQuestLocationImage : MonoBehaviour
{
	private UITexture uiTexture;

	private int id = -1;

	private Transform image;

	private IEnumerator coroutine;

	private Action onLoadStart;

	private Action onLoadComplete;

	public static void Set(UITexture ui_texture, int qli_id, Action on_load_start, Action on_load_complete)
	{
		UIQuestLocationImage uIQuestLocationImage = ui_texture.GetComponent<UIQuestLocationImage>();
		if (uIQuestLocationImage == null)
		{
			uIQuestLocationImage = ui_texture.gameObject.AddComponent<UIQuestLocationImage>();
		}
		uIQuestLocationImage.Load(ui_texture, qli_id, on_load_start, on_load_complete);
	}

	private void Load(UITexture ui_texture, int qli_id, Action on_load_start, Action on_load_complete)
	{
		uiTexture = ui_texture;
		id = qli_id;
		onLoadStart = on_load_start;
		onLoadComplete = on_load_complete;
		DeleteImage();
		StartCoroutine(coroutine = DoLoad());
	}

	private IEnumerator DoLoad()
	{
		if (onLoadStart != null)
		{
			onLoadStart();
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_image = loadingQueue.Load(RESOURCE_CATEGORY.QUEST_LOCATION_IMAGE, ResourceName.GetQuestLocationImage(id));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		image = ResourceUtility.Realizes(lo_image.loadedObject, MonoBehaviourSingleton<StageManager>.I._transform, 5);
		QuestLocationImage component = image.GetComponent<QuestLocationImage>();
		if (component == null)
		{
			yield break;
		}
		int w = uiTexture.width;
		int h = uiTexture.height;
		UIRenderTexture.ToRealSize(ref w, ref h);
		component.Init(w, h);
		Camera component2 = image.GetComponent<Camera>();
		if (component2 == null)
		{
			yield break;
		}
		RenderTexture targetTexture = component2.targetTexture;
		if (!(targetTexture == null))
		{
			uiTexture.mainTexture = targetTexture;
			FloatInterpolator anim = new FloatInterpolator();
			anim.Set(0.25f, 0f, 1f, Curves.easeLinear, 0f);
			anim.Play();
			while (anim.IsPlaying())
			{
				yield return null;
				uiTexture.alpha = anim.Update();
			}
			if (onLoadComplete != null)
			{
				onLoadComplete();
			}
			coroutine = null;
		}
	}

	private void DeleteImage()
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			coroutine = null;
		}
		if (uiTexture != null)
		{
			uiTexture.alpha = 0f;
			uiTexture.mainTexture = null;
		}
		if (image != null)
		{
			UnityEngine.Object.DestroyImmediate(image.gameObject);
			image = null;
		}
	}

	private void OnEnable()
	{
		if (coroutine == null && image == null && uiTexture != null && id > -1)
		{
			StartCoroutine(coroutine = DoLoad());
		}
	}

	private void OnDisable()
	{
		DeleteImage();
	}
}
