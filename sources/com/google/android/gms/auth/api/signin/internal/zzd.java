package com.google.android.gms.auth.api.signin.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.PendingResults;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.StatusPendingResult;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.logging.Logger;

public final class zzd implements Runnable {
    private static final Logger zzbd = new Logger("RevokeAccessOperation", new String[0]);
    private final String zzbe;
    private final StatusPendingResult zzbf = new StatusPendingResult((GoogleApiClient) null);

    private zzd(String str) {
        Preconditions.checkNotEmpty(str);
        this.zzbe = str;
    }

    public static PendingResult<Status> zzc(String str) {
        if (str == null) {
            return PendingResults.immediateFailedResult(new Status(4), null);
        }
        zzd zzd = new zzd(str);
        new Thread(zzd).start();
        return zzd.zzbf;
    }

    /* JADX WARNING: Removed duplicated region for block: B:24:0x0097  */
    /* JADX WARNING: Removed duplicated region for block: B:27:0x00ab  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
            r6 = this;
            r5 = 0
            com.google.android.gms.common.api.Status r1 = com.google.android.gms.common.api.Status.RESULT_INTERNAL_ERROR
            java.net.URL r2 = new java.net.URL     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            java.lang.String r0 = "https://accounts.google.com/o/oauth2/revoke?token="
            java.lang.String r3 = java.lang.String.valueOf(r0)     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            java.lang.String r0 = r6.zzbe     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            java.lang.String r0 = java.lang.String.valueOf(r0)     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            int r4 = r0.length()     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            if (r4 == 0) goto L_0x0058
            java.lang.String r0 = r3.concat(r0)     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
        L_0x001b:
            r2.<init>(r0)     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            java.net.URLConnection r0 = r2.openConnection()     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            java.net.HttpURLConnection r0 = (java.net.HttpURLConnection) r0     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            java.lang.String r2 = "Content-Type"
            java.lang.String r3 = "application/x-www-form-urlencoded"
            r0.setRequestProperty(r2, r3)     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            int r0 = r0.getResponseCode()     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            r2 = 200(0xc8, float:2.8E-43)
            if (r0 != r2) goto L_0x007b
            com.google.android.gms.common.api.Status r1 = com.google.android.gms.common.api.Status.RESULT_SUCCESS     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
        L_0x0035:
            com.google.android.gms.common.logging.Logger r2 = zzbd     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
            r4 = 26
            r3.<init>(r4)     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
            java.lang.String r4 = "Response Code: "
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
            java.lang.StringBuilder r0 = r3.append(r0)     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
            java.lang.String r0 = r0.toString()     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
            r3 = 0
            java.lang.Object[] r3 = new java.lang.Object[r3]     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
            r2.mo14040d(r0, r3)     // Catch:{ IOException -> 0x00b3, Exception -> 0x00b5 }
        L_0x0052:
            com.google.android.gms.common.api.internal.StatusPendingResult r0 = r6.zzbf
            r0.setResult(r1)
            return
        L_0x0058:
            java.lang.String r0 = new java.lang.String     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            r0.<init>(r3)     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            goto L_0x001b
        L_0x005e:
            r0 = move-exception
        L_0x005f:
            com.google.android.gms.common.logging.Logger r2 = zzbd
            java.lang.String r0 = r0.toString()
            java.lang.String r0 = java.lang.String.valueOf(r0)
            int r3 = r0.length()
            if (r3 == 0) goto L_0x00a3
            java.lang.String r3 = "IOException when revoking access: "
            java.lang.String r0 = r3.concat(r0)
        L_0x0075:
            java.lang.Object[] r3 = new java.lang.Object[r5]
            r2.mo14042e(r0, r3)
            goto L_0x0052
        L_0x007b:
            com.google.android.gms.common.logging.Logger r2 = zzbd     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            java.lang.String r3 = "Unable to revoke access!"
            r4 = 0
            java.lang.Object[] r4 = new java.lang.Object[r4]     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            r2.mo14042e(r3, r4)     // Catch:{ IOException -> 0x005e, Exception -> 0x0086 }
            goto L_0x0035
        L_0x0086:
            r0 = move-exception
        L_0x0087:
            com.google.android.gms.common.logging.Logger r2 = zzbd
            java.lang.String r0 = r0.toString()
            java.lang.String r0 = java.lang.String.valueOf(r0)
            int r3 = r0.length()
            if (r3 == 0) goto L_0x00ab
            java.lang.String r3 = "Exception when revoking access: "
            java.lang.String r0 = r3.concat(r0)
        L_0x009d:
            java.lang.Object[] r3 = new java.lang.Object[r5]
            r2.mo14042e(r0, r3)
            goto L_0x0052
        L_0x00a3:
            java.lang.String r0 = new java.lang.String
            java.lang.String r3 = "IOException when revoking access: "
            r0.<init>(r3)
            goto L_0x0075
        L_0x00ab:
            java.lang.String r0 = new java.lang.String
            java.lang.String r3 = "Exception when revoking access: "
            r0.<init>(r3)
            goto L_0x009d
        L_0x00b3:
            r0 = move-exception
            goto L_0x005f
        L_0x00b5:
            r0 = move-exception
            goto L_0x0087
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.auth.api.signin.internal.zzd.run():void");
    }
}
