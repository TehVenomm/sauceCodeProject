using UnityEngine;

public class QuestUserStatusIconController
{
	public class InitParam
	{
		public uint StatusBit;
	}

	[SerializeField]
	private UISprite[] m_statusIcons;

	public QuestUserStatusIconController()
		: this()
	{
	}

	public void Initialize(InitParam _param)
	{
		ActivateIcons(_param.StatusBit);
	}

	private void ActivateIcons(uint _statusBit)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (m_statusIcons != null && m_statusIcons.Length >= 1)
		{
			for (int i = 0; i < m_statusIcons.Length; i++)
			{
				if (!(m_statusIcons[i] == null))
				{
					int num = 1 << i + 1;
					m_statusIcons[i].get_gameObject().SetActive((num & _statusBit) != 0);
				}
			}
		}
	}
}
