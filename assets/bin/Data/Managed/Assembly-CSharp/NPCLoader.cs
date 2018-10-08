using System;
using System.Collections;
using UnityEngine;

public class NPCLoader : ModelLoaderBase
{
	public static readonly Bounds BOUNDS = new Bounds(Vector3.zero, new Vector3(2f, 2f, 2f));

	private IEnumerator coroutine;

	private LoadingQueue loadingQueue;

	private Action callback;

	public Transform model
	{
		get;
		private set;
	}

	public Transform head
	{
		get;
		private set;
	}

	public NPCFacial facial
	{
		get;
		private set;
	}

	public Animator animator
	{
		get;
		private set;
	}

	public Renderer[] renderers
	{
		get;
		private set;
	}

	public Transform shadow
	{
		get;
		private set;
	}

	public bool isLoading => coroutine != null;

	public override bool IsLoading()
	{
		return isLoading;
	}

	public override Animator GetAnimator()
	{
		return animator;
	}

	public override Transform GetHead()
	{
		return head;
	}

	public override void SetEnabled(bool is_enable)
	{
		if ((UnityEngine.Object)animator != (UnityEngine.Object)null)
		{
			animator.enabled = is_enable;
		}
		if ((UnityEngine.Object)shadow != (UnityEngine.Object)null)
		{
			shadow.gameObject.SetActive(is_enable);
		}
		ModelLoaderBase.SetEnabled(renderers, is_enable);
	}

	public void Load(int npc_model_id, int layer, bool need_shadow, bool enable_light_probes, SHADER_TYPE shader_type, Action callback)
	{
		Clear();
		StartCoroutine(coroutine = DoLoad(npc_model_id, layer, need_shadow, enable_light_probes, shader_type, callback));
	}

	private IEnumerator DoLoad(int npc_model_id, int layer, bool need_shadow, bool enable_light_probes, SHADER_TYPE shader_type, Action callback)
	{
		loadingQueue = new LoadingQueue(this);
		string model_name = ResourceName.GetNPCModel(npc_model_id);
		LoadObject lo_model = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.NPC_MODEL, model_name);
		string anim_name = ResourceName.GetNPCAnim(npc_model_id);
		LoadObject lo_anim = loadingQueue.Load(RESOURCE_CATEGORY.NPC_ANIM, anim_name, new string[1]
		{
			anim_name + "Ctrl"
		}, false);
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		model = lo_model.Realizes(base.transform, layer);
		if ((UnityEngine.Object)model != (UnityEngine.Object)null)
		{
			head = Utility.Find(model, "Head");
			facial = model.GetComponentInChildren<NPCFacial>();
			if ((UnityEngine.Object)facial != (UnityEngine.Object)null)
			{
				facial.animNode = Utility.Find(model, "Face");
			}
			animator = model.GetComponentInChildren<Animator>();
			if (lo_anim != null && (UnityEngine.Object)animator != (UnityEngine.Object)null)
			{
				animator.runtimeAnimatorController = (lo_anim.loadedObjects[0].obj as RuntimeAnimatorController);
			}
		}
		PlayerLoader.SetLightProbes(model, enable_light_probes);
		renderers = model.GetComponentsInChildren<Renderer>();
		int j = 0;
		for (int i = renderers.Length; j < i; j++)
		{
			if (renderers[j] is SkinnedMeshRenderer)
			{
				(renderers[j] as SkinnedMeshRenderer).localBounds = BOUNDS;
			}
		}
		switch (shader_type)
		{
		case SHADER_TYPE.LIGHTWEIGHT:
			ShaderGlobal.ChangeWantLightweightShader(renderers);
			break;
		case SHADER_TYPE.UI:
			ShaderGlobal.ChangeWantUIShader(renderers);
			break;
		}
		if (need_shadow)
		{
			shadow = PlayerLoader.CreateShadow(base.transform, true, -1, shader_type == SHADER_TYPE.LIGHTWEIGHT);
		}
		coroutine = null;
		callback?.Invoke();
	}

	public void Clear()
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			coroutine = null;
		}
		if ((UnityEngine.Object)model != (UnityEngine.Object)null)
		{
			UnityEngine.Object.DestroyImmediate(model.gameObject);
			model = null;
			head = null;
		}
		loadingQueue = null;
		animator = null;
	}
}
