public class FieldMapInfo
{
	public FieldMapTable.FieldMapTableData fieldMap;

	public CLEAR_STATUS status;

	public FieldMapInfo(FieldMapTable.FieldMapTableData _table, int _status)
	{
		fieldMap = _table;
		status = (CLEAR_STATUS)_status;
	}
}
