using System;
using System.Collections;
using UnityEngine;

public class NPCLoader : ModelLoaderBase
{
	public static readonly Bounds BOUNDS = new Bounds(Vector3.get_zero(), new Vector3(2f, 2f, 2f));

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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (animator != null)
		{
			animator.set_enabled(is_enable);
		}
		if (shadow != null)
		{
			shadow.get_gameObject().SetActive(is_enable);
		}
		ModelLoaderBase.SetEnabled(renderers, is_enable);
	}

	private void Update()
	{
		if (!(animator != null))
		{
			return;
		}
	}

	public void Load(int npc_model_id, int layer, bool need_shadow, bool enable_light_probes, SHADER_TYPE shader_type, Action callback, bool clearAll = false)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Clear();
		this.StartCoroutine(coroutine = DoLoad(npc_model_id, layer, need_shadow, enable_light_probes, shader_type, callback));
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
		model = lo_model.Realizes(this.get_transform(), layer);
		if (model != null)
		{
			head = Utility.Find(model, "Head");
			facial = model.GetComponentInChildren<NPCFacial>();
			if (facial != null)
			{
				facial.animNode = Utility.Find(model, "Face");
			}
			animator = model.GetComponentInChildren<Animator>();
			if (lo_anim != null && animator != null)
			{
				animator.set_runtimeAnimatorController(lo_anim.loadedObjects[0].obj as RuntimeAnimatorController);
			}
		}
		PlayerLoader.SetLightProbes(model, enable_light_probes);
		renderers = model.GetComponentsInChildren<Renderer>();
		int j = 0;
		for (int i = renderers.Length; j < i; j++)
		{
			if (renderers[j] is SkinnedMeshRenderer)
			{
				(renderers[j] as SkinnedMeshRenderer).set_localBounds(BOUNDS);
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
			shadow = PlayerLoader.CreateShadow(this.get_transform(), true, -1, shader_type == SHADER_TYPE.LIGHTWEIGHT);
		}
		coroutine = null;
		if (callback != null)
		{
			callback.Invoke();
		}
	}

	public void Clear()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (coroutine != null)
		{
			this.StopCoroutine(coroutine);
			coroutine = null;
		}
		if (model != null)
		{
			Object.DestroyImmediate(model.get_gameObject());
			model = null;
			head = null;
		}
		loadingQueue = null;
		animator = null;
	}
}
