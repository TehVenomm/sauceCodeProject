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
		if (m_baseIcon == null || m_bulletIcon == null)
		{
			return false;
		}
		m_baseIcon.depth = _param.DepthOffset + _param.IconIndex * 2;
		m_bulletIcon.depth = _param.DepthOffset + _param.IconIndex * 2 + 1;
		base.transform.localPosition = BULLET_ICON_POS_INTERVAL * _param.IconIndex;
		base.transform.localRotation = Quaternion.Euler(Vector3.zero);
		base.transform.localScale = Vector3.one;
		return true;
	}

	public bool SetVisibleAllIcon()
	{
		if (SwitchVisibleIcon(m_baseIcon, _isVisible: true))
		{
			return SwitchVisibleIcon(m_bulletIcon, _isVisible: true);
		}
		return false;
	}

	public bool SetInvisibleAllIcon()
	{
		if (SwitchVisibleIcon(m_baseIcon, _isVisible: false))
		{
			return SwitchVisibleIcon(m_bulletIcon, _isVisible: false);
		}
		return false;
	}

	public bool SetVisibleBulletIcon()
	{
		return SwitchVisibleIcon(m_bulletIcon, _isVisible: true);
	}

	public bool SetInvisibleBulletIcon()
	{
		return SwitchVisibleIcon(m_bulletIcon, _isVisible: false);
	}

	private bool SwitchVisibleIcon(UISprite _sprite, bool _isVisible)
	{
		if (_sprite == null || _sprite.enabled == _isVisible)
		{
			return false;
		}
		_sprite.enabled = _isVisible;
		return true;
	}
}
