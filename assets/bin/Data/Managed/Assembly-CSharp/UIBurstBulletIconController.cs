using UnityEngine;

public class UIBurstBulletIconController : MonoBehaviour
{
	public class InitParam
	{
		public int IconIndex;

		public int DepthOffset;
	}

	private readonly Vector3 BULLET_ICON_POS_INTERVAL = new Vector3(22f, 0f, 0f);

	[SerializeField]
	private UISprite m_baseIcon;

	[SerializeField]
	private UISprite m_bulletIcon;

	public bool Initialize(InitParam _param)
	{
		if (_param == null)
		{
			return false;
		}
		if ((Object)m_baseIcon == (Object)null || (Object)m_bulletIcon == (Object)null)
		{
			return false;
		}
		m_baseIcon.depth = _param.DepthOffset + _param.IconIndex * 2;
		m_bulletIcon.depth = _param.DepthOffset + _param.IconIndex * 2 + 1;
		base.transform.localPosition = BULLET_ICON_POS_INTERVAL * (float)_param.IconIndex;
		base.transform.localRotation = Quaternion.Euler(Vector3.zero);
		base.transform.localScale = Vector3.one;
		return true;
	}

	public bool SetVisibleAllIcon()
	{
		return SwitchVisibleIcon(m_baseIcon, true) && SwitchVisibleIcon(m_bulletIcon, true);
	}

	public bool SetInvisibleAllIcon()
	{
		return SwitchVisibleIcon(m_baseIcon, false) && SwitchVisibleIcon(m_bulletIcon, false);
	}

	public bool SetVisibleBulletIcon()
	{
		return SwitchVisibleIcon(m_bulletIcon, true);
	}

	public bool SetInvisibleBulletIcon()
	{
		return SwitchVisibleIcon(m_bulletIcon, false);
	}

	private bool SwitchVisibleIcon(UISprite _sprite, bool _isVisible)
	{
		if ((Object)_sprite == (Object)null || _sprite.enabled == _isVisible)
		{
			return false;
		}
		_sprite.enabled = _isVisible;
		return true;
	}
}
