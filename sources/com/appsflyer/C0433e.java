package com.appsflyer;

import android.content.Context;
import android.os.RemoteException;
import android.support.p000v4.app.NotificationCompat;
import com.android.installreferrer.api.InstallReferrerClient;
import com.android.installreferrer.api.InstallReferrerStateListener;
import com.android.installreferrer.api.ReferrerDetails;
import java.util.HashMap;

/* renamed from: com.appsflyer.e */
final class C0433e implements InstallReferrerStateListener {

    /* renamed from: ˊ */
    private InstallReferrerClient f247;

    /* renamed from: ˎ */
    private C0428b f248;

    C0433e() {
    }

    /* access modifiers changed from: protected */
    /* renamed from: ˏ */
    public final void mo6561(Context context, C0428b bVar) {
        this.f248 = bVar;
        this.f247 = InstallReferrerClient.newBuilder(context).build();
        try {
            this.f247.startConnection(this);
        } catch (Exception e) {
            AFLogger.afErrorLog("referrerClient -> startConnection", e);
        }
    }

    public final void onInstallReferrerSetupFinished(int i) {
        HashMap hashMap = new HashMap();
        hashMap.put("code", String.valueOf(i));
        ReferrerDetails referrerDetails = null;
        switch (i) {
            case 0:
                try {
                    AFLogger.afDebugLog("InstallReferrer connected");
                    if (!this.f247.isReady()) {
                        AFLogger.afWarnLog("ReferrerClient: InstallReferrer is not ready");
                        hashMap.put(NotificationCompat.CATEGORY_ERROR, "ReferrerClient: InstallReferrer is not ready");
                        break;
                    } else {
                        referrerDetails = this.f247.getInstallReferrer();
                        this.f247.endConnection();
                        break;
                    }
                } catch (RemoteException e) {
                    AFLogger.afWarnLog(new StringBuilder("Failed to get install referrer: ").append(e.getMessage()).toString());
                    hashMap.put(NotificationCompat.CATEGORY_ERROR, e.getMessage());
                    break;
                } catch (IllegalStateException e2) {
                    AFLogger.afWarnLog(new StringBuilder("Failed to get install referrer: ").append(e2.getMessage()).toString());
                    hashMap.put(NotificationCompat.CATEGORY_ERROR, e2.getMessage());
                    break;
                } catch (Throwable th) {
                    AFLogger.afWarnLog(new StringBuilder("Failed to get install referrer: ").append(th.getMessage()).toString());
                    hashMap.put(NotificationCompat.CATEGORY_ERROR, th.getMessage());
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
        if (this.f248 != null) {
            this.f248.onHandleReferrer(hashMap);
        }
    }

    public final void onInstallReferrerServiceDisconnected() {
        AFLogger.afDebugLog("Install Referrer service disconnected");
    }
}
