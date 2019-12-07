using System.Collections;
using UnityEngine;

public class StatusSmithCharacter : MonoBehaviour
{
	private const int SMITH_NPC_ID = 4;

	private const int UNIQUE_SMITH_NPC_ID = 36;

	public bool isUnique;

	protected Animator animator;

	protected PlayerAnimCtrl animCtrl;

	protected bool isComplete;

	protected bool isActive;

	public Transform _transform
	{
		get;
		private set;
	}

	public ModelLoaderBase loader
	{
		get;
		protected set;
	}

	private void Awake()
	{
		_transform = base.transform;
	}

	private IEnumerator Start()
	{
		isComplete = false;
		loader = LoadModel();
		while (loader.IsLoading())
		{
			yield return null;
		}
		base.gameObject.SetActive(isActive);
		animator = loader.GetAnimator();
		if (!(animator == null))
		{
			animator.gameObject.AddComponent<RootMotionProxy>();
			InitAnim();
			OutGameSettingsManager.StatusScene statusScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
			_transform.position = statusScene.smithNPCPos;
			_transform.eulerAngles = statusScene.smithNPCRot;
			_transform.localScale = Vector3.one * statusScene.smithSize;
			isComplete = true;
		}
	}

	protected ModelLoaderBase LoadModel()
	{
		if (isUnique)
		{
			return Singleton<NPCTable>.I.GetNPCData(36).LoadModel(base.gameObject, need_shadow: true, enable_light_probe: true, null, useSpecialModel: false);
		}
		return Singleton<NPCTable>.I.GetNPCData(4).LoadModel(base.gameObject, need_shadow: true, enable_light_probe: true, null, useSpecialModel: false);
	}

	protected void InitAnim()
	{
		PLCA default_anim = PLCA.IDLE_02;
		animCtrl = PlayerAnimCtrl.Get(animator, default_anim);
	}

	public void SetActive(bool active)
	{
		if (!isComplete)
		{
			isActive = active;
		}
		else
		{
			base.gameObject.SetActive(active);
		}
	}
}
