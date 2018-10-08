using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	public sealed class WWWAuthenticateHeaderParser : KeyValuePairList
	{
		public WWWAuthenticateHeaderParser(string headerValue)
		{
			base.Values = ParseQuotedHeader(headerValue).AsReadOnly();
		}

		private unsafe List<KeyValuePair> ParseQuotedHeader(string str)
		{
			List<KeyValuePair> list = new List<KeyValuePair>();
			if (str != null)
			{
				int pos = 0;
				if (_003C_003Ef__am_0024cache0 == null)
				{
					_003C_003Ef__am_0024cache0 = new Func<char, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				string key = str.Read(ref pos, _003C_003Ef__am_0024cache0, true).TrimAndLower();
				list.Add(new KeyValuePair(key));
				while (pos < str.Length)
				{
					string key2 = str.Read(ref pos, '=', true).TrimAndLower();
					KeyValuePair keyValuePair = new KeyValuePair(key2);
					str.SkipWhiteSpace(ref pos);
					keyValuePair.Value = str.ReadQuotedText(ref pos);
					list.Add(keyValuePair);
				}
			}
			return list;
		}
	}
}
