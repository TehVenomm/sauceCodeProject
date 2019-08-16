using UnityEngine;

public class BuffSlideEffectController : MonoBehaviour
{
	public const float END_STATE_PLAY_HEIGHT = 0.8f;

	private Player m_targetPlayer;

	private Transform m_footNode;

	private Animator m_animator;

	private int m_animHashLoop;

	private int m_animHashEnd;

	public BuffSlideEffectController()
		: this()
	{
	}

	public void Initialize(Player targetPlayer)
	{
		m_targetPlayer = targetPlayer;
		m_footNode = m_targetPlayer.FindNode("L_Foot");
		m_animator = this.GetComponent<Animator>();
		m_animHashLoop = Animator.StringToHash("LOOP");
		m_animHashEnd = Animator.StringToHash("END");
	}

	private void Update()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_animator == null) && !(m_targetPlayer == null))
		{
			AnimatorStateInfo currentAnimatorStateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
			int shortNameHash = currentAnimatorStateInfo.get_shortNameHash();
			Vector3 position = m_footNode.get_position();
			float y = position.y;
			float height = StageManager.GetHeight(m_targetPlayer._position);
			int num = m_animHashLoop;
			if (Mathf.Abs(y - height) > 0.8f)
			{
				num = m_animHashEnd;
			}
			if (num != shortNameHash)
			{
				m_animator.Play(num, 0, 0f);
			}
		}
	}
}
