package net.gogame.gowrap.unity;

import android.util.Log;
import com.unity3d.player.UnityPlayer;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.sdk.GoWrap;
import net.gogame.gowrap.sdk.GoWrapDelegateV2;
import org.json.JSONObject;

public class GoWrapUnityPlugin {
    private static final GoWrapUnityPlugin INSTANCE = new GoWrapUnityPlugin();
    private static final String TAG = "goWrap-Unity";
    private final GoWrapDelegateV2 delegate = new C15631();
    private String gameObjectName = Constants.TAG;

    /* renamed from: net.gogame.gowrap.unity.GoWrapUnityPlugin$1 */
    class C15631 implements GoWrapDelegateV2 {
        C15631() {
        }

        public void didCompleteRewardedAd(String str, int i) {
            if (GoWrapUnityPlugin.this.gameObjectName != null) {
                try {
                    JSONObject jSONObject = new JSONObject();
                    jSONObject.put("rewardId", str);
                    jSONObject.put("rewardQuantity", i);
                    UnityPlayer.UnitySendMessage(GoWrapUnityPlugin.this.gameObjectName, "handleAdsCompletedWithReward", jSONObject.toString());
                } catch (Throwable e) {
                    Log.e(GoWrapUnityPlugin.TAG, "Exception", e);
                }
            }
        }

        public void onCustomUrl(String str) {
            if (GoWrapUnityPlugin.this.gameObjectName != null) {
                try {
                    UnityPlayer.UnitySendMessage(GoWrapUnityPlugin.this.gameObjectName, "handleOnCustomUrl", str);
                } catch (Throwable e) {
                    Log.e(GoWrapUnityPlugin.TAG, "Exception", e);
                }
            }
        }

        public void onMenuClosed() {
            if (GoWrapUnityPlugin.this.gameObjectName != null) {
                try {
                    UnityPlayer.UnitySendMessage(GoWrapUnityPlugin.this.gameObjectName, "handleMenuClosed", new JSONObject().toString());
                } catch (Throwable e) {
                    Log.e(GoWrapUnityPlugin.TAG, "Exception", e);
                }
            }
        }

        public void onMenuOpened() {
            if (GoWrapUnityPlugin.this.gameObjectName != null) {
                try {
                    UnityPlayer.UnitySendMessage(GoWrapUnityPlugin.this.gameObjectName, "handleMenuOpened", new JSONObject().toString());
                } catch (Throwable e) {
                    Log.e(GoWrapUnityPlugin.TAG, "Exception", e);
                }
            }
        }

        public void onOffersAvailable() {
            if (GoWrapUnityPlugin.this.gameObjectName != null) {
                try {
                    UnityPlayer.UnitySendMessage(GoWrapUnityPlugin.this.gameObjectName, "handleOnOffersAvailable", new JSONObject().toString());
                } catch (Throwable e) {
                    Log.e(GoWrapUnityPlugin.TAG, "Exception", e);
                }
            }
        }
    }

    public static void initialize(String str) {
        INSTANCE.gameObjectName = str;
        GoWrap.setDelegate(INSTANCE.delegate);
    }
}
