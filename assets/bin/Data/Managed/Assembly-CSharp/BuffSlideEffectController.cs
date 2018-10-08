using UnityEngine;

public class BuffSlideEffectController : MonoBehaviour
{
	public const float END_STATE_PLAY_HEIGHT = 0.8f;

	private Player m_targetPlayer;

	private Transform m_footNode;

	private Animator m_animator;

	private int m_animHashLoop;

	private int m_animHashEnd;

	public void Initialize(Player targetPlayer)
	{
		m_targetPlayer = targetPlayer;
		m_footNode = m_targetPlayer.FindNode("L_Foot");
		m_animator = GetComponent<Animator>();
		m_animHashLoop = Animator.StringToHash("LOOP");
		m_animHashEnd = Animator.StringToHash("END");
	}

	private void Update()
	{
		if (!((Object)m_animator == (Object)null) && !((Object)m_targetPlayer == (Object)null))
		{
			int shortNameHash = m_animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
			Vector3 position = m_footNode.position;
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
