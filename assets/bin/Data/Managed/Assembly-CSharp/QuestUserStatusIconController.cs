using UnityEngine;

public class QuestUserStatusIconController : MonoBehaviour
{
	public class InitParam
	{
		public uint StatusBit;
	}

	[SerializeField]
	private UISprite[] m_statusIcons;

	public void Initialize(InitParam _param)
	{
		ActivateIcons(_param.StatusBit);
	}

	private void ActivateIcons(uint _statusBit)
	{
		if (m_statusIcons != null && m_statusIcons.Length >= 1)
		{
			for (int i = 0; i < m_statusIcons.Length; i++)
			{
				if (!((Object)m_statusIcons[i] == (Object)null))
				{
					int num = 1 << i + 1;
					m_statusIcons[i].gameObject.SetActive((num & _statusBit) != 0);
				}
			}
		}
	}
}
