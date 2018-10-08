public interface IDoubleUIntKeyBinaryTableData
{
	void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2);
}
