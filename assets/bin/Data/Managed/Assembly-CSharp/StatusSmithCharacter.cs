using System.Collections;
using UnityEngine;

public class StatusSmithCharacter
{
	private const int SMITH_NPC_ID = 4;

	protected Animator animator;

	protected PlayerAnimCtrl animCtrl;

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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
	}

	private IEnumerator Start()
	{
		loader = LoadModel();
		while (loader.IsLoading())
		{
			yield return (object)null;
		}
		animator = loader.GetAnimator();
		if (!(animator == null))
		{
			animator.get_gameObject().AddComponent<RootMotionProxy>();
			InitAnim();
			OutGameSettingsManager.StatusScene param = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
			_transform.set_position(param.smithNPCPos);
			_transform.set_eulerAngles(param.smithNPCRot);
			_transform.set_localScale(Vector3.get_one() * param.smithSize);
		}
	}

	protected ModelLoaderBase LoadModel()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		return Singleton<NPCTable>.I.GetNPCData(4).LoadModel(this.get_gameObject(), true, true, null, false);
	}

	protected void InitAnim()
	{
		PLCA default_anim = PLCA.IDLE_02;
		animCtrl = PlayerAnimCtrl.Get(animator, default_anim, null, null, null);
	}
}
