using Network;

public class FieldMapPortalInfo
{
	public FieldMapTable.PortalTableData portalData;

	public FieldPortal fieldPortal;

	public FieldMapPortalInfo(FieldMapTable.PortalTableData _portal, FieldPortal _fieldPortal)
	{
		portalData = _portal;
		fieldPortal = _fieldPortal;
	}

	public void SetPortalData(FieldMapTable.PortalTableData _portal, FieldPortal _fieldPortal)
	{
		portalData = _portal;
		fieldPortal = _fieldPortal;
	}

	public void Clear()
	{
		portalData = null;
		fieldPortal = null;
	}

	public bool IsValid()
	{
		return portalData != null;
	}

	public bool IsFull()
	{
		if (portalData == null)
		{
			return true;
		}
		if (portalData.portalPoint == 0)
		{
			return true;
		}
		if (fieldPortal == null)
		{
			return false;
		}
		return fieldPortal.point >= portalData.portalPoint;
	}

	public int GetNowPortalPoint()
	{
		if (portalData == null)
		{
			return 0;
		}
		if (fieldPortal == null)
		{
			return 0;
		}
		return fieldPortal.point;
	}

	public uint GetMaxPortalPoint()
	{
		if (portalData == null)
		{
			return 0u;
		}
		return portalData.portalPoint;
	}

	public bool IsAddPortalPoint()
	{
		if (IsFull())
		{
			return false;
		}
		if (!FieldManager.IsOpenPortalClearOrder(portalData))
		{
			return false;
		}
		if (!portalData.isUnlockedTime())
		{
			return false;
		}
		return true;
	}
}
