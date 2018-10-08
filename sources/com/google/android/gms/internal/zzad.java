package com.google.android.gms.internal;

import java.io.IOException;
import java.io.InputStream;
import java.util.Map;
import java.util.TreeMap;
import org.apache.http.Header;
import org.apache.http.HttpEntity;

public final class zzad implements zzk {
    private static boolean DEBUG = zzab.DEBUG;
    private static int zzbm = 3000;
    private static int zzbn = 4096;
    private zzan zzbo;
    private zzae zzbp;

    public zzad(zzan zzan) {
        this(zzan, new zzae(zzbn));
    }

    private zzad(zzan zzan, zzae zzae) {
        this.zzbo = zzan;
        this.zzbp = zzae;
    }

    private static Map<String, String> zza(Header[] headerArr) {
        Map<String, String> treeMap = new TreeMap(String.CASE_INSENSITIVE_ORDER);
        for (int i = 0; i < headerArr.length; i++) {
            treeMap.put(headerArr[i].getName(), headerArr[i].getValue());
        }
        return treeMap;
    }

    private static void zza(String str, zzp<?> zzp, zzaa zzaa) throws zzaa {
        zzx zzj = zzp.zzj();
        int zzi = zzp.zzi();
        try {
            zzj.zza(zzaa);
            zzp.zzb(String.format("%s-retry [timeout=%s]", new Object[]{str, Integer.valueOf(zzi)}));
        } catch (zzaa e) {
            zzp.zzb(String.format("%s-timeout-giveup [timeout=%s]", new Object[]{str, Integer.valueOf(zzi)}));
            throw e;
        }
    }

    private final byte[] zza(HttpEntity httpEntity) throws IOException, zzy {
        zzaq zzaq = new zzaq(this.zzbp, (int) httpEntity.getContentLength());
        byte[] bArr = null;
        try {
            InputStream content = httpEntity.getContent();
            if (content == null) {
                throw new zzy();
            }
            bArr = this.zzbp.zzb(1024);
            while (true) {
                int read = content.read(bArr);
                if (read == -1) {
                    break;
                }
                zzaq.write(bArr, 0, read);
            }
            byte[] toByteArray = zzaq.toByteArray();
            return toByteArray;
        } finally {
            try {
                httpEntity.consumeContent();
            } catch (IOException e) {
                zzab.zza("Error occured when calling consumingContent", new Object[0]);
            }
            this.zzbp.zza(bArr);
            zzaq.close();
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.google.android.gms.internal.zzn zza(com.google.android.gms.internal.zzp<?> r20) throws com.google.android.gms.internal.zzaa {
        /*
        r19 = this;
        r16 = android.os.SystemClock.elapsedRealtime();
    L_0x0004:
        r3 = 0;
        r6 = java.util.Collections.emptyMap();
        r2 = new java.util.HashMap;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r2.<init>();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r4 = r20.zze();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        if (r4 == 0) goto L_0x0037;
    L_0x0014:
        r5 = r4.zza;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        if (r5 == 0) goto L_0x001f;
    L_0x0018:
        r5 = "If-None-Match";
        r7 = r4.zza;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r2.put(r5, r7);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
    L_0x001f:
        r8 = r4.zzc;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r10 = 0;
        r5 = (r8 > r10 ? 1 : (r8 == r10 ? 0 : -1));
        if (r5 <= 0) goto L_0x0037;
    L_0x0027:
        r5 = new java.util.Date;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r8 = r4.zzc;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r5.<init>(r8);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r4 = "If-Modified-Since";
        r5 = org.apache.http.impl.cookie.DateUtils.formatDate(r5);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r2.put(r4, r5);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
    L_0x0037:
        r0 = r19;
        r4 = r0.zzbo;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r0 = r20;
        r14 = r4.zza(r0, r2);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x0140 }
        r3 = r14.getStatusLine();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r4 = r3.getStatusCode();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r2 = r14.getAllHeaders();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r6 = zza(r2);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r2 = 304; // 0x130 float:4.26E-43 double:1.5E-321;
        if (r4 != r2) goto L_0x0084;
    L_0x0055:
        r2 = r20.zze();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        if (r2 != 0) goto L_0x006b;
    L_0x005b:
        r3 = new com.google.android.gms.internal.zzn;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r4 = 304; // 0x130 float:4.26E-43 double:1.5E-321;
        r5 = 0;
        r7 = 1;
        r8 = android.os.SystemClock.elapsedRealtime();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r8 = r8 - r16;
        r3.<init>(r4, r5, r6, r7, r8);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
    L_0x006a:
        return r3;
    L_0x006b:
        r3 = r2.zzf;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r3.putAll(r6);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r7 = new com.google.android.gms.internal.zzn;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r8 = 304; // 0x130 float:4.26E-43 double:1.5E-321;
        r9 = r2.data;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r10 = r2.zzf;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r11 = 1;
        r2 = android.os.SystemClock.elapsedRealtime();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r12 = r2 - r16;
        r7.<init>(r8, r9, r10, r11, r12);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r3 = r7;
        goto L_0x006a;
    L_0x0084:
        r2 = r14.getEntity();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        if (r2 == 0) goto L_0x00f8;
    L_0x008a:
        r2 = r14.getEntity();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        r0 = r19;
        r5 = r0.zza(r2);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
    L_0x0094:
        r8 = android.os.SystemClock.elapsedRealtime();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r8 = r8 - r16;
        r2 = DEBUG;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        if (r2 != 0) goto L_0x00a5;
    L_0x009e:
        r2 = zzbm;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r10 = (long) r2;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r2 = (r8 > r10 ? 1 : (r8 == r10 ? 0 : -1));
        if (r2 <= 0) goto L_0x00db;
    L_0x00a5:
        if (r5 == 0) goto L_0x00fc;
    L_0x00a7:
        r2 = r5.length;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r2 = java.lang.Integer.valueOf(r2);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
    L_0x00ac:
        r7 = "HTTP response for request=<%s> [lifetime=%d], [size=%s], [rc=%d], [retryCount=%s]";
        r10 = 5;
        r10 = new java.lang.Object[r10];	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r11 = 0;
        r10[r11] = r20;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r11 = 1;
        r8 = java.lang.Long.valueOf(r8);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r10[r11] = r8;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r8 = 2;
        r10[r8] = r2;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r2 = 3;
        r3 = r3.getStatusCode();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r3 = java.lang.Integer.valueOf(r3);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r10[r2] = r3;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r2 = 4;
        r3 = r20.zzj();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r3 = r3.zzb();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r3 = java.lang.Integer.valueOf(r3);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r10[r2] = r3;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        com.google.android.gms.internal.zzab.zzb(r7, r10);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
    L_0x00db:
        r2 = 200; // 0xc8 float:2.8E-43 double:9.9E-322;
        if (r4 < r2) goto L_0x00e3;
    L_0x00df:
        r2 = 299; // 0x12b float:4.19E-43 double:1.477E-321;
        if (r4 <= r2) goto L_0x00ff;
    L_0x00e3:
        r2 = new java.io.IOException;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r2.<init>();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        throw r2;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
    L_0x00e9:
        r2 = move-exception;
        r2 = "socket";
        r3 = new com.google.android.gms.internal.zzz;
        r3.<init>();
        r0 = r20;
        zza(r2, r0, r3);
        goto L_0x0004;
    L_0x00f8:
        r2 = 0;
        r5 = new byte[r2];	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c1 }
        goto L_0x0094;
    L_0x00fc:
        r2 = "null";
        goto L_0x00ac;
    L_0x00ff:
        r3 = new com.google.android.gms.internal.zzn;	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r7 = 0;
        r8 = android.os.SystemClock.elapsedRealtime();	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        r8 = r8 - r16;
        r3.<init>(r4, r5, r6, r7, r8);	 Catch:{ SocketTimeoutException -> 0x00e9, ConnectTimeoutException -> 0x010d, MalformedURLException -> 0x011c, IOException -> 0x01c6 }
        goto L_0x006a;
    L_0x010d:
        r2 = move-exception;
        r2 = "connection";
        r3 = new com.google.android.gms.internal.zzz;
        r3.<init>();
        r0 = r20;
        zza(r2, r0, r3);
        goto L_0x0004;
    L_0x011c:
        r2 = move-exception;
        r3 = r2;
        r2 = r20.getUrl();
        r2 = java.lang.String.valueOf(r2);
        r4 = r2.length();
        if (r4 == 0) goto L_0x0138;
    L_0x012c:
        r4 = "Bad URL ";
        r2 = r4.concat(r2);
    L_0x0132:
        r4 = new java.lang.RuntimeException;
        r4.<init>(r2, r3);
        throw r4;
    L_0x0138:
        r2 = new java.lang.String;
        r4 = "Bad URL ";
        r2.<init>(r4);
        goto L_0x0132;
    L_0x0140:
        r2 = move-exception;
        r5 = 0;
        r18 = r3;
        r3 = r2;
        r2 = r18;
    L_0x0147:
        if (r2 == 0) goto L_0x018b;
    L_0x0149:
        r2 = r2.getStatusLine();
        r4 = r2.getStatusCode();
        r2 = "Unexpected response code %d for %s";
        r3 = 2;
        r3 = new java.lang.Object[r3];
        r7 = 0;
        r8 = java.lang.Integer.valueOf(r4);
        r3[r7] = r8;
        r7 = 1;
        r8 = r20.getUrl();
        r3[r7] = r8;
        com.google.android.gms.internal.zzab.zzc(r2, r3);
        if (r5 == 0) goto L_0x01b3;
    L_0x0169:
        r3 = new com.google.android.gms.internal.zzn;
        r7 = 0;
        r8 = android.os.SystemClock.elapsedRealtime();
        r8 = r8 - r16;
        r3.<init>(r4, r5, r6, r7, r8);
        r2 = 401; // 0x191 float:5.62E-43 double:1.98E-321;
        if (r4 == r2) goto L_0x017d;
    L_0x0179:
        r2 = 403; // 0x193 float:5.65E-43 double:1.99E-321;
        if (r4 != r2) goto L_0x0191;
    L_0x017d:
        r2 = "auth";
        r4 = new com.google.android.gms.internal.zza;
        r4.<init>(r3);
        r0 = r20;
        zza(r2, r0, r4);
        goto L_0x0004;
    L_0x018b:
        r2 = new com.google.android.gms.internal.zzo;
        r2.<init>(r3);
        throw r2;
    L_0x0191:
        r2 = 400; // 0x190 float:5.6E-43 double:1.976E-321;
        if (r4 < r2) goto L_0x019f;
    L_0x0195:
        r2 = 499; // 0x1f3 float:6.99E-43 double:2.465E-321;
        if (r4 > r2) goto L_0x019f;
    L_0x0199:
        r2 = new com.google.android.gms.internal.zzf;
        r2.<init>(r3);
        throw r2;
    L_0x019f:
        r2 = 500; // 0x1f4 float:7.0E-43 double:2.47E-321;
        if (r4 < r2) goto L_0x01ad;
    L_0x01a3:
        r2 = 599; // 0x257 float:8.4E-43 double:2.96E-321;
        if (r4 > r2) goto L_0x01ad;
    L_0x01a7:
        r2 = new com.google.android.gms.internal.zzy;
        r2.<init>(r3);
        throw r2;
    L_0x01ad:
        r2 = new com.google.android.gms.internal.zzy;
        r2.<init>(r3);
        throw r2;
    L_0x01b3:
        r2 = "network";
        r3 = new com.google.android.gms.internal.zzm;
        r3.<init>();
        r0 = r20;
        zza(r2, r0, r3);
        goto L_0x0004;
    L_0x01c1:
        r2 = move-exception;
        r5 = 0;
        r3 = r2;
        r2 = r14;
        goto L_0x0147;
    L_0x01c6:
        r2 = move-exception;
        r3 = r2;
        r2 = r14;
        goto L_0x0147;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzad.zza(com.google.android.gms.internal.zzp):com.google.android.gms.internal.zzn");
    }
}
