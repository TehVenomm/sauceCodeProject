using GooglePlayGames.OurUtils;
using System;
using System.Linq;

namespace GooglePlayGames.BasicApi.Video
{
	public class VideoCapabilities
	{
		private bool mIsCameraSupported;

		private bool mIsMicSupported;

		private bool mIsWriteStorageSupported;

		private bool[] mCaptureModesSupported;

		private bool[] mQualityLevelsSupported;

		public bool IsCameraSupported => mIsCameraSupported;

		public bool IsMicSupported => mIsMicSupported;

		public bool IsWriteStorageSupported => mIsWriteStorageSupported;

		internal VideoCapabilities(bool isCameraSupported, bool isMicSupported, bool isWriteStorageSupported, bool[] captureModesSupported, bool[] qualityLevelsSupported)
		{
			mIsCameraSupported = isCameraSupported;
			mIsMicSupported = isMicSupported;
			mIsWriteStorageSupported = isWriteStorageSupported;
			mCaptureModesSupported = captureModesSupported;
			mQualityLevelsSupported = qualityLevelsSupported;
		}

		public bool SupportsCaptureMode(VideoCaptureMode captureMode)
		{
			if (captureMode != VideoCaptureMode.Unknown)
			{
				return mCaptureModesSupported[(int)captureMode];
			}
			Logger.w("SupportsCaptureMode called with an unknown captureMode.");
			return false;
		}

		public bool SupportsQualityLevel(VideoQualityLevel qualityLevel)
		{
			if (qualityLevel != VideoQualityLevel.Unknown)
			{
				return mQualityLevelsSupported[(int)qualityLevel];
			}
			Logger.w("SupportsCaptureMode called with an unknown qualityLevel.");
			return false;
		}

		public unsafe override string ToString()
		{
			object[] obj = new object[5]
			{
				mIsCameraSupported,
				mIsMicSupported,
				mIsWriteStorageSupported,
				null,
				null
			};
			bool[] source = mCaptureModesSupported;
			if (_003C_003Ef__am_0024cache5 == null)
			{
				_003C_003Ef__am_0024cache5 = new Func<bool, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			obj[3] = string.Join(",", source.Select<bool, string>(_003C_003Ef__am_0024cache5).ToArray());
			bool[] source2 = mQualityLevelsSupported;
			if (_003C_003Ef__am_0024cache6 == null)
			{
				_003C_003Ef__am_0024cache6 = new Func<bool, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			obj[4] = string.Join(",", source2.Select<bool, string>(_003C_003Ef__am_0024cache6).ToArray());
			return string.Format("[VideoCapabilities: mIsCameraSupported={0}, mIsMicSupported={1}, mIsWriteStorageSupported={2}, mCaptureModesSupported={3}, mQualityLevelsSupported={4}]", obj);
		}
	}
}
