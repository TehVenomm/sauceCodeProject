using Network;

public class ExplorePortalPoint
{
	public static readonly int USEDFLAG_CLOSED;

	public static readonly int USEDFLAG_OPENED = 1;

	public static readonly int USEDFLAG_PASSED = 2;

	private FieldPortal fieldPortal;

	public int used;

	public int portaiId => fieldPortal.pId;

	public int point
	{
		get
		{
			return fieldPortal.point;
		}
		set
		{
			fieldPortal.point = value;
		}
	}

	public bool closed => USEDFLAG_CLOSED == used;

	public bool opened => USEDFLAG_OPENED == used;

	public bool passed => USEDFLAG_PASSED == used;

	public int linkPortalId
	{
		get;
		private set;
	}

	public FieldMapTable.PortalTableData portalData
	{
		get;
		private set;
	}

	public ExplorePortalPoint(FieldMapTable.PortalTableData tableData)
	{
		fieldPortal = new FieldPortal();
		fieldPortal.pId = (int)tableData.portalID;
		used = USEDFLAG_CLOSED;
		linkPortalId = (int)tableData.linkPortalId;
		portalData = tableData;
		if (tableData.portalPoint == 0)
		{
			used = USEDFLAG_PASSED;
		}
	}

	public void UpdatePoint(int point, bool force = false)
	{
		if (fieldPortal.point < point || force)
		{
			fieldPortal.point = point;
		}
	}

	public void UpdateUsedFlag(int usedFlag)
	{
		if (USEDFLAG_CLOSED == usedFlag || USEDFLAG_OPENED == usedFlag || USEDFLAG_PASSED == usedFlag)
		{
			used = usedFlag;
		}
	}
}
