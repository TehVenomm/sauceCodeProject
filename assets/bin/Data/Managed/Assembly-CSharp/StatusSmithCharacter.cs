using System.Collections;
using UnityEngine;

public class StatusSmithCharacter : MonoBehaviour
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

	private void Awake()
	{
		_transform = base.transform;
	}

	private IEnumerator Start()
	{
		loader = LoadModel();
		while (loader.IsLoading())
		{
			yield return (object)null;
		}
		animator = loader.GetAnimator();
		if (!((Object)animator == (Object)null))
		{
			animator.gameObject.AddComponent<RootMotionProxy>();
			InitAnim();
			OutGameSettingsManager.StatusScene param = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
			_transform.position = param.smithNPCPos;
			_transform.eulerAngles = param.smithNPCRot;
			_transform.localScale = Vector3.one * param.smithSize;
		}
	}

	protected ModelLoaderBase LoadModel()
	{
		return Singleton<NPCTable>.I.GetNPCData(4).LoadModel(base.gameObject, true, true, null, false);
	}

	protected void InitAnim()
	{
		PLCA default_anim = PLCA.IDLE_02;
		animCtrl = PlayerAnimCtrl.Get(animator, default_anim, null, null, null);
	}
}
