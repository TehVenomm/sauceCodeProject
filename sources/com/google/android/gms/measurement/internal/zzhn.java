package com.google.android.gms.measurement.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.Preconditions;
import java.net.URL;
import java.util.List;
import java.util.Map;

@WorkerThread
final class zzhn implements Runnable {
    private final String packageName;
    private final URL url;
    private final byte[] zzlc = null;
    private final Map<String, String> zzle;
    private final zzhk zzqm;
    private final /* synthetic */ zzhl zzqn;

    public zzhn(zzhl zzhl, String str, URL url2, byte[] bArr, Map<String, String> map, zzhk zzhk) {
        this.zzqn = zzhl;
        Preconditions.checkNotEmpty(str);
        Preconditions.checkNotNull(url2);
        Preconditions.checkNotNull(zzhk);
        this.url = url2;
        this.zzqm = zzhk;
        this.packageName = str;
        this.zzle = null;
    }

    private final void zza(int i, Exception exc, byte[] bArr, Map<String, List<String>> map) {
        this.zzqn.zzaa().zza((Runnable) new zzhm(this, i, exc, bArr, map));
    }

    /* JADX WARNING: Removed duplicated region for block: B:14:0x003e  */
    /* JADX WARNING: Removed duplicated region for block: B:28:0x0062  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
            r6 = this;
            r2 = 0
            r4 = 0
            com.google.android.gms.measurement.internal.zzhl r0 = r6.zzqn
            r0.zzn()
            com.google.android.gms.measurement.internal.zzhl r0 = r6.zzqn     // Catch:{ IOException -> 0x0070, all -> 0x005c }
            java.net.URL r1 = r6.url     // Catch:{ IOException -> 0x0070, all -> 0x005c }
            java.net.HttpURLConnection r5 = r0.zza(r1)     // Catch:{ IOException -> 0x0070, all -> 0x005c }
            java.util.Map<java.lang.String, java.lang.String> r0 = r6.zzle     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            if (r0 == 0) goto L_0x0045
            java.util.Map<java.lang.String, java.lang.String> r0 = r6.zzle     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.util.Set r0 = r0.entrySet()     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.util.Iterator r3 = r0.iterator()     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
        L_0x001d:
            boolean r0 = r3.hasNext()     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            if (r0 == 0) goto L_0x0045
            java.lang.Object r0 = r3.next()     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.lang.Object r1 = r0.getKey()     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.lang.String r1 = (java.lang.String) r1     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.lang.Object r0 = r0.getValue()     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.lang.String r0 = (java.lang.String) r0     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            r5.addRequestProperty(r1, r0)     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            goto L_0x001d
        L_0x0039:
            r0 = move-exception
            r1 = r2
        L_0x003b:
            r3 = r4
        L_0x003c:
            if (r5 == 0) goto L_0x0041
            r5.disconnect()
        L_0x0041:
            r6.zza(r1, r0, r4, r3)
        L_0x0044:
            return
        L_0x0045:
            int r1 = r5.getResponseCode()     // Catch:{ IOException -> 0x0039, all -> 0x0069 }
            java.util.Map r2 = r5.getHeaderFields()     // Catch:{ IOException -> 0x007b, all -> 0x0078 }
            com.google.android.gms.measurement.internal.zzhl r0 = r6.zzqn     // Catch:{ IOException -> 0x0075, all -> 0x006d }
            byte[] r0 = com.google.android.gms.measurement.internal.zzhl.zza(r5)     // Catch:{ IOException -> 0x0075, all -> 0x006d }
            if (r5 == 0) goto L_0x0058
            r5.disconnect()
        L_0x0058:
            r6.zza(r1, r4, r0, r2)
            goto L_0x0044
        L_0x005c:
            r0 = move-exception
            r3 = r4
            r5 = r4
            r1 = r2
        L_0x0060:
            if (r5 == 0) goto L_0x0065
            r5.disconnect()
        L_0x0065:
            r6.zza(r1, r4, r4, r3)
            throw r0
        L_0x0069:
            r0 = move-exception
        L_0x006a:
            r3 = r4
            r1 = r2
            goto L_0x0060
        L_0x006d:
            r0 = move-exception
            r3 = r2
            goto L_0x0060
        L_0x0070:
            r0 = move-exception
            r3 = r4
            r5 = r4
            r1 = r2
            goto L_0x003c
        L_0x0075:
            r0 = move-exception
            r3 = r2
            goto L_0x003c
        L_0x0078:
            r0 = move-exception
            r2 = r1
            goto L_0x006a
        L_0x007b:
            r0 = move-exception
            goto L_0x003b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzhn.run():void");
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ void zzb(int i, Exception exc, byte[] bArr, Map map) {
        this.zzqm.zza(this.packageName, i, exc, bArr, map);
    }
}
