using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class GetSocialError : IConvertableFromNative<GetSocialError>
	{
		public int ErrorCode
		{
			get;
			private set;
		}

		public string Message
		{
			get;
			private set;
		}

		public GetSocialError(string message)
		{
			Message = message;
			ErrorCode = -1;
		}

		public GetSocialError()
		{
			ErrorCode = -1;
		}

		public override string ToString()
		{
			return $"Error code: {ErrorCode}. Message: {Message}";
		}

		public GetSocialError ParseFromAJO(AndroidJavaObject ajo)
		{
			try
			{
				ErrorCode = ajo.CallInt("getErrorCode");
				Message = ajo.CallStr("getMessage");
				return this;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
