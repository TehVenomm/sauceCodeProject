package com.google.android.gms.ads.identifier;

import java.util.Map;

final class zza extends Thread {
    private final /* synthetic */ Map zzl;

    zza(AdvertisingIdClient advertisingIdClient, Map map) {
        this.zzl = map;
    }

    /* JADX WARNING: No exception handlers in catch block: Catch:{  } */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
            r7 = this;
            com.google.android.gms.ads.identifier.zzc r0 = new com.google.android.gms.ads.identifier.zzc
            r0.<init>()
            java.util.Map r2 = r7.zzl
            java.lang.String r0 = "https://pagead2.googlesyndication.com/pagead/gen_204?id=gmob-apps"
            android.net.Uri r0 = android.net.Uri.parse(r0)
            android.net.Uri$Builder r3 = r0.buildUpon()
            java.util.Set r0 = r2.keySet()
            java.util.Iterator r4 = r0.iterator()
        L_0x0019:
            boolean r0 = r4.hasNext()
            if (r0 == 0) goto L_0x002f
            java.lang.Object r0 = r4.next()
            java.lang.String r0 = (java.lang.String) r0
            java.lang.Object r1 = r2.get(r0)
            java.lang.String r1 = (java.lang.String) r1
            r3.appendQueryParameter(r0, r1)
            goto L_0x0019
        L_0x002f:
            android.net.Uri r0 = r3.build()
            java.lang.String r2 = r0.toString()
            java.net.URL r0 = new java.net.URL     // Catch:{ IndexOutOfBoundsException -> 0x0083, IOException -> 0x00be, RuntimeException -> 0x00f9 }
            r0.<init>(r2)     // Catch:{ IndexOutOfBoundsException -> 0x0083, IOException -> 0x00be, RuntimeException -> 0x00f9 }
            java.net.URLConnection r0 = r0.openConnection()     // Catch:{ IndexOutOfBoundsException -> 0x0083, IOException -> 0x00be, RuntimeException -> 0x00f9 }
            java.net.HttpURLConnection r0 = (java.net.HttpURLConnection) r0     // Catch:{ IndexOutOfBoundsException -> 0x0083, IOException -> 0x00be, RuntimeException -> 0x00f9 }
            int r1 = r0.getResponseCode()     // Catch:{ all -> 0x007e }
            r3 = 200(0xc8, float:2.8E-43)
            if (r1 < r3) goto L_0x004e
            r3 = 300(0x12c, float:4.2E-43)
            if (r1 < r3) goto L_0x007a
        L_0x004e:
            java.lang.String r3 = java.lang.String.valueOf(r2)     // Catch:{ all -> 0x007e }
            int r3 = r3.length()     // Catch:{ all -> 0x007e }
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ all -> 0x007e }
            int r3 = r3 + 65
            r4.<init>(r3)     // Catch:{ all -> 0x007e }
            java.lang.String r3 = "HttpUrlPinger"
            java.lang.String r5 = "Received non-success response code "
            java.lang.StringBuilder r4 = r4.append(r5)     // Catch:{ all -> 0x007e }
            java.lang.StringBuilder r1 = r4.append(r1)     // Catch:{ all -> 0x007e }
            java.lang.String r4 = " from pinging URL: "
            java.lang.StringBuilder r1 = r1.append(r4)     // Catch:{ all -> 0x007e }
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ all -> 0x007e }
            java.lang.String r1 = r1.toString()     // Catch:{ all -> 0x007e }
            android.util.Log.w(r3, r1)     // Catch:{ all -> 0x007e }
        L_0x007a:
            r0.disconnect()     // Catch:{ IndexOutOfBoundsException -> 0x0083, IOException -> 0x00be, RuntimeException -> 0x00f9 }
        L_0x007d:
            return
        L_0x007e:
            r1 = move-exception
            r0.disconnect()     // Catch:{ IndexOutOfBoundsException -> 0x0083, IOException -> 0x00be, RuntimeException -> 0x00f9 }
            throw r1     // Catch:{ IndexOutOfBoundsException -> 0x0083, IOException -> 0x00be, RuntimeException -> 0x00f9 }
        L_0x0083:
            r0 = move-exception
            java.lang.String r1 = r0.getMessage()
            java.lang.String r3 = "HttpUrlPinger"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            java.lang.String r5 = java.lang.String.valueOf(r2)
            int r5 = r5.length()
            int r5 = r5 + 32
            java.lang.String r6 = java.lang.String.valueOf(r1)
            int r6 = r6.length()
            int r5 = r5 + r6
            r4.<init>(r5)
            java.lang.String r5 = "Error while parsing ping URL: "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.StringBuilder r2 = r4.append(r2)
            java.lang.String r4 = ". "
            java.lang.StringBuilder r2 = r2.append(r4)
            java.lang.StringBuilder r1 = r2.append(r1)
            java.lang.String r1 = r1.toString()
            android.util.Log.w(r3, r1, r0)
            goto L_0x007d
        L_0x00be:
            r0 = move-exception
        L_0x00bf:
            java.lang.String r1 = r0.getMessage()
            java.lang.String r3 = "HttpUrlPinger"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            java.lang.String r5 = java.lang.String.valueOf(r2)
            int r5 = r5.length()
            int r5 = r5 + 27
            java.lang.String r6 = java.lang.String.valueOf(r1)
            int r6 = r6.length()
            int r5 = r5 + r6
            r4.<init>(r5)
            java.lang.String r5 = "Error while pinging URL: "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.StringBuilder r2 = r4.append(r2)
            java.lang.String r4 = ". "
            java.lang.StringBuilder r2 = r2.append(r4)
            java.lang.StringBuilder r1 = r2.append(r1)
            java.lang.String r1 = r1.toString()
            android.util.Log.w(r3, r1, r0)
            goto L_0x007d
        L_0x00f9:
            r0 = move-exception
            goto L_0x00bf
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.ads.identifier.zza.run():void");
    }
}
