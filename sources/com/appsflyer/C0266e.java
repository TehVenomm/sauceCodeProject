package com.appsflyer;

import android.content.Context;
import com.android.installreferrer.api.InstallReferrerClient;
import com.android.installreferrer.api.InstallReferrerStateListener;
import com.android.installreferrer.api.ReferrerDetails;
import java.util.HashMap;
import java.util.Map;

/* renamed from: com.appsflyer.e */
final class C0266e implements InstallReferrerStateListener {
    /* renamed from: ˊ */
    private InstallReferrerClient f226;
    /* renamed from: ˎ */
    private C0259b f227;

    C0266e() {
    }

    /* renamed from: ˏ */
    protected final void m291(Context context, C0259b c0259b) {
        this.f227 = c0259b;
        this.f226 = InstallReferrerClient.newBuilder(context).build();
        try {
            this.f226.startConnection(this);
        } catch (Throwable e) {
            AFLogger.afErrorLog("referrerClient -> startConnection", e);
        }
    }

    public final void onInstallReferrerSetupFinished(int i) {
        Map hashMap = new HashMap();
        hashMap.put("code", String.valueOf(i));
        ReferrerDetails referrerDetails = null;
        switch (i) {
            case 0:
                try {
                    AFLogger.afDebugLog("InstallReferrer connected");
                    if (!this.f226.isReady()) {
                        AFLogger.afWarnLog("ReferrerClient: InstallReferrer is not ready");
                        hashMap.put("err", "ReferrerClient: InstallReferrer is not ready");
                        break;
                    }
                    referrerDetails = this.f226.getInstallReferrer();
                    this.f226.endConnection();
                    break;
                } catch (Throwable e) {
                    AFLogger.afWarnLog(new StringBuilder("Failed to get install referrer: ").append(e.getMessage()).toString());
                    hashMap.put("err", e.getMessage());
                    break;
                } catch (Throwable e2) {
                    AFLogger.afWarnLog(new StringBuilder("Failed to get install referrer: ").append(e2.getMessage()).toString());
                    hashMap.put("err", e2.getMessage());
                    break;
                } catch (Throwable e22) {
                    AFLogger.afWarnLog(new StringBuilder("Failed to get install referrer: ").append(e22.getMessage()).toString());
                    hashMap.put("err", e22.getMessage());
                    break;
                }
            case 1:
                AFLogger.afWarnLog("InstallReferrer not supported");
                break;
            case 2:
                AFLogger.afWarnLog("InstallReferrer not supported");
                break;
            default:
                AFLogger.afWarnLog("responseCode not found.");
                break;
        }
        if (referrerDetails != null) {
            if (referrerDetails.getInstallReferrer() != null) {
                hashMap.put("val", referrerDetails.getInstallReferrer());
            }
            hashMap.put("clk", Long.toString(referrerDetails.getReferrerClickTimestampSeconds()));
            hashMap.put("install", Long.toString(referrerDetails.getInstallBeginTimestampSeconds()));
        }
        if (this.f227 != null) {
            this.f227.onHandleReferrer(hashMap);
        }
    }

    public final void onInstallReferrerServiceDisconnected() {
        AFLogger.afDebugLog("Install Referrer service disconnected");
    }
}
