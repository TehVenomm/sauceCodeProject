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

	public StatusSmithCharacter()
		: this()
	{
	}

	private void Awake()
	{
		_transform = this.get_transform();
	}

	private IEnumerator Start()
	{
		isComplete = false;
		loader = LoadModel();
		while (loader.IsLoading())
		{
			yield return null;
		}
		this.get_gameObject().SetActive(isActive);
		animator = loader.GetAnimator();
		if (!(animator == null))
		{
			animator.get_gameObject().AddComponent<RootMotionProxy>();
			InitAnim();
			OutGameSettingsManager.StatusScene param = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
			_transform.set_position(param.smithNPCPos);
			_transform.set_eulerAngles(param.smithNPCRot);
			_transform.set_localScale(Vector3.get_one() * param.smithSize);
			isComplete = true;
		}
	}

	protected ModelLoaderBase LoadModel()
	{
		if (isUnique)
		{
			return Singleton<NPCTable>.I.GetNPCData(36).LoadModel(this.get_gameObject(), need_shadow: true, enable_light_probe: true, null, useSpecialModel: false);
		}
		return Singleton<NPCTable>.I.GetNPCData(4).LoadModel(this.get_gameObject(), need_shadow: true, enable_light_probe: true, null, useSpecialModel: false);
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
			this.get_gameObject().SetActive(active);
		}
	}
}
