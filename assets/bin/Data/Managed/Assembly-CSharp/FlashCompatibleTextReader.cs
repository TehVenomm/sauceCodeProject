public class FlashCompatibleTextReader
{
	private string text_;

	private int index_;

	private int size_;

	public FlashCompatibleTextReader(string text)
	{
		text_ = text;
		size_ = text.Length;
	}

	public int Peek()
	{
		if (index_ >= size_)
		{
			return -1;
		}
		return text_[index_];
	}

	public int Read()
	{
		if (index_ >= size_)
		{
			return -1;
		}
		return text_[index_++];
	}
}
