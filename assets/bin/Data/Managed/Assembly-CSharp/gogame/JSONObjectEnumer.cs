using System.Collections;

namespace gogame
{
	public class JSONObjectEnumer : IEnumerator
	{
		public JSONObject _jobj;

		private int position = -1;

		public JSONObject Current
		{
			get
			{
				if (_jobj.IsArray)
				{
					return _jobj[position];
				}
				string index = _jobj.keys[position];
				return _jobj[index];
			}
		}

		object IEnumerator.Current => Current;

		public JSONObjectEnumer(JSONObject jsonObject)
		{
			_jobj = jsonObject;
		}

		public bool MoveNext()
		{
			position++;
			return position < _jobj.Count;
		}

		public void Reset()
		{
			position = -1;
		}
	}
}
