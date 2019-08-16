package com.google.android.gms.measurement.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.Preconditions;
import java.net.URL;
import java.util.Map;

@WorkerThread
final class zzen implements Runnable {
    private final String packageName;
    private final URL url;
    private final byte[] zzlc;
    private final zzel zzld;
    private final Map<String, String> zzle;
    private final /* synthetic */ zzej zzlf;

    public zzen(zzej zzej, String str, URL url2, byte[] bArr, Map<String, String> map, zzel zzel) {
        this.zzlf = zzej;
        Preconditions.checkNotEmpty(str);
        Preconditions.checkNotNull(url2);
        Preconditions.checkNotNull(zzel);
        this.url = url2;
        this.zzlc = bArr;
        this.zzld = zzel;
        this.packageName = str;
        this.zzle = map;
    }

    /* JADX WARNING: Removed duplicated region for block: B:36:0x00e4 A[SYNTHETIC, Splitter:B:36:0x00e4] */
    /* JADX WARNING: Removed duplicated region for block: B:39:0x00e9  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
            r13 = this;
            r3 = 0
            r4 = 0
            com.google.android.gms.measurement.internal.zzej r0 = r13.zzlf
            r0.zzn()
            com.google.android.gms.measurement.internal.zzej r0 = r13.zzlf     // Catch:{ IOException -> 0x0125, all -> 0x00dd }
            java.net.URL r1 = r13.url     // Catch:{ IOException -> 0x0125, all -> 0x00dd }
            java.net.HttpURLConnection r2 = r0.zza(r1)     // Catch:{ IOException -> 0x0125, all -> 0x00dd }
            java.util.Map<java.lang.String, java.lang.String> r0 = r13.zzle     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            if (r0 == 0) goto L_0x005e
            java.util.Map<java.lang.String, java.lang.String> r0 = r13.zzle     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.util.Set r0 = r0.entrySet()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.util.Iterator r5 = r0.iterator()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
        L_0x001d:
            boolean r0 = r5.hasNext()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            if (r0 == 0) goto L_0x005e
            java.lang.Object r0 = r5.next()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.lang.Object r1 = r0.getKey()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.lang.String r1 = (java.lang.String) r1     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.lang.Object r0 = r0.getValue()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.lang.String r0 = (java.lang.String) r0     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            r2.addRequestProperty(r1, r0)     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            goto L_0x001d
        L_0x0039:
            r0 = move-exception
            r11 = r4
            r9 = r0
            r1 = r2
            r5 = r4
            r8 = r3
        L_0x003f:
            if (r5 == 0) goto L_0x0044
            r5.close()     // Catch:{ IOException -> 0x00c5 }
        L_0x0044:
            if (r1 == 0) goto L_0x0049
            r1.disconnect()
        L_0x0049:
            com.google.android.gms.measurement.internal.zzej r0 = r13.zzlf
            com.google.android.gms.measurement.internal.zzfc r0 = r0.zzaa()
            com.google.android.gms.measurement.internal.zzek r5 = new com.google.android.gms.measurement.internal.zzek
            java.lang.String r6 = r13.packageName
            com.google.android.gms.measurement.internal.zzel r7 = r13.zzld
            r10 = r4
            r12 = r4
            r5.<init>(r6, r7, r8, r9, r10, r11)
            r0.zza(r5)
        L_0x005d:
            return
        L_0x005e:
            byte[] r0 = r13.zzlc     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            if (r0 == 0) goto L_0x009e
            com.google.android.gms.measurement.internal.zzej r0 = r13.zzlf     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            com.google.android.gms.measurement.internal.zzjo r0 = r0.zzgw()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            byte[] r1 = r13.zzlc     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            byte[] r0 = r0.zzc(r1)     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            com.google.android.gms.measurement.internal.zzej r1 = r13.zzlf     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            com.google.android.gms.measurement.internal.zzef r1 = r1.zzab()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgs()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.lang.String r5 = "Uploading data. size"
            int r6 = r0.length     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.lang.Integer r6 = java.lang.Integer.valueOf(r6)     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            r1.zza(r5, r6)     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            r1 = 1
            r2.setDoOutput(r1)     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.lang.String r1 = "Content-Encoding"
            java.lang.String r5 = "gzip"
            r2.addRequestProperty(r1, r5)     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            int r1 = r0.length     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            r2.setFixedLengthStreamingMode(r1)     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            r2.connect()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.io.OutputStream r5 = r2.getOutputStream()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            r5.write(r0)     // Catch:{ IOException -> 0x012d, all -> 0x011d }
            r5.close()     // Catch:{ IOException -> 0x012d, all -> 0x011d }
        L_0x009e:
            int r3 = r2.getResponseCode()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            java.util.Map r6 = r2.getHeaderFields()     // Catch:{ IOException -> 0x0039, all -> 0x0118 }
            com.google.android.gms.measurement.internal.zzej r0 = r13.zzlf     // Catch:{ IOException -> 0x0134, all -> 0x0121 }
            byte[] r5 = com.google.android.gms.measurement.internal.zzej.zza(r2)     // Catch:{ IOException -> 0x0134, all -> 0x0121 }
            if (r2 == 0) goto L_0x00b1
            r2.disconnect()
        L_0x00b1:
            com.google.android.gms.measurement.internal.zzej r0 = r13.zzlf
            com.google.android.gms.measurement.internal.zzfc r8 = r0.zzaa()
            com.google.android.gms.measurement.internal.zzek r0 = new com.google.android.gms.measurement.internal.zzek
            java.lang.String r1 = r13.packageName
            com.google.android.gms.measurement.internal.zzel r2 = r13.zzld
            r7 = r4
            r0.<init>(r1, r2, r3, r4, r5, r6)
            r8.zza(r0)
            goto L_0x005d
        L_0x00c5:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzej r2 = r13.zzlf
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()
            java.lang.String r3 = "Error closing HTTP compressed POST connection output stream. appId"
            java.lang.String r5 = r13.packageName
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r5)
            r2.zza(r3, r5, r0)
            goto L_0x0044
        L_0x00dd:
            r0 = move-exception
            r2 = r4
            r8 = r0
            r5 = r4
            r6 = r4
        L_0x00e2:
            if (r5 == 0) goto L_0x00e7
            r5.close()     // Catch:{ IOException -> 0x0101 }
        L_0x00e7:
            if (r2 == 0) goto L_0x00ec
            r2.disconnect()
        L_0x00ec:
            com.google.android.gms.measurement.internal.zzej r0 = r13.zzlf
            com.google.android.gms.measurement.internal.zzfc r9 = r0.zzaa()
            com.google.android.gms.measurement.internal.zzek r0 = new com.google.android.gms.measurement.internal.zzek
            java.lang.String r1 = r13.packageName
            com.google.android.gms.measurement.internal.zzel r2 = r13.zzld
            r5 = r4
            r7 = r4
            r0.<init>(r1, r2, r3, r4, r5, r6)
            r9.zza(r0)
            throw r8
        L_0x0101:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzej r1 = r13.zzlf
            com.google.android.gms.measurement.internal.zzef r1 = r1.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()
            java.lang.String r5 = "Error closing HTTP compressed POST connection output stream. appId"
            java.lang.String r7 = r13.packageName
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r7)
            r1.zza(r5, r7, r0)
            goto L_0x00e7
        L_0x0118:
            r0 = move-exception
            r8 = r0
            r5 = r4
            r6 = r4
            goto L_0x00e2
        L_0x011d:
            r0 = move-exception
            r8 = r0
            r6 = r4
            goto L_0x00e2
        L_0x0121:
            r0 = move-exception
            r8 = r0
            r5 = r4
            goto L_0x00e2
        L_0x0125:
            r0 = move-exception
            r11 = r4
            r9 = r0
            r1 = r4
            r5 = r4
            r8 = r3
            goto L_0x003f
        L_0x012d:
            r0 = move-exception
            r11 = r4
            r9 = r0
            r1 = r2
            r8 = r3
            goto L_0x003f
        L_0x0134:
            r0 = move-exception
            r11 = r6
            r9 = r0
            r1 = r2
            r5 = r4
            r8 = r3
            goto L_0x003f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzen.run():void");
    }
}
