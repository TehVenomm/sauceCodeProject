package com.zopim.android.sdk.api;

import android.support.annotation.NonNull;
import android.util.Log;
import android.util.Patterns;
import java.io.File;
import java.net.URL;

/* renamed from: com.zopim.android.sdk.api.j */
final class C1149j implements HttpRequest {

    /* renamed from: b */
    private static final String f681b = C1149j.class.getSimpleName();

    /* renamed from: c */
    private C1161u<File> f682c;

    C1149j() {
    }

    /* JADX WARNING: Code restructure failed: missing block: B:10:0x004b, code lost:
        if (r5 == null) goto L_0x0057;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:12:?, code lost:
        com.zopim.android.sdk.api.Logger.m577v(f681b, "Closing input stream");
        r5.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:64:0x01d5, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:65:0x01d6, code lost:
        android.util.Log.w(f681b, "Failed to close output stream", r0);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:7:0x003d, code lost:
        r4 = null;
        r5 = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:8:0x003f, code lost:
        if (r0 == null) goto L_0x004b;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:9:0x0041, code lost:
        com.zopim.android.sdk.api.Logger.m577v(f681b, "Disconnecting url connection");
        r0.disconnect();
     */
    /* JADX WARNING: Removed duplicated region for block: B:32:0x0115 A[Catch:{ all -> 0x0217 }] */
    /* JADX WARNING: Removed duplicated region for block: B:34:0x011c  */
    /* JADX WARNING: Removed duplicated region for block: B:36:0x0128 A[SYNTHETIC, Splitter:B:36:0x0128] */
    /* JADX WARNING: Removed duplicated region for block: B:39:0x0134 A[SYNTHETIC, Splitter:B:39:0x0134] */
    /* JADX WARNING: Removed duplicated region for block: B:50:0x017e  */
    /* JADX WARNING: Removed duplicated region for block: B:52:0x018a A[SYNTHETIC, Splitter:B:52:0x018a] */
    /* JADX WARNING: Removed duplicated region for block: B:55:0x0196 A[SYNTHETIC, Splitter:B:55:0x0196] */
    /* JADX WARNING: Removed duplicated region for block: B:88:? A[RETURN, SYNTHETIC] */
    /* renamed from: b */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void m595b(java.net.URL r9, java.io.File r10) {
        /*
            r8 = this;
            r3 = 0
            java.net.URLConnection r0 = r9.openConnection()     // Catch:{ Exception -> 0x021b, all -> 0x0205 }
            javax.net.ssl.HttpsURLConnection r0 = (javax.net.ssl.HttpsURLConnection) r0     // Catch:{ Exception -> 0x021b, all -> 0x0205 }
            java.lang.String r1 = "User-Agent"
            java.lang.String r2 = "http.agent"
            java.lang.String r2 = java.lang.System.getProperty(r2)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r0.setRequestProperty(r1, r2)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r1 = "Accept-Charset"
            java.lang.String r2 = "UTF-8"
            r0.setRequestProperty(r1, r2)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r1 = 0
            r0.setInstanceFollowRedirects(r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            long r4 = f666a     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            int r1 = (int) r4     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r0.setReadTimeout(r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            int r1 = r0.getResponseCode()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r2 = f681b     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r4 = "response connection.getResponseMessage()"
            android.util.Log.v(r2, r4)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.HttpRequest$Status r2 = com.zopim.android.sdk.api.HttpRequest.Status.getStatus(r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            int[] r4 = com.zopim.android.sdk.api.C1150k.f683a     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            int r2 = r2.ordinal()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r2 = r4[r2]     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            switch(r2) {
                case 1: goto L_0x0064;
                case 2: goto L_0x01a1;
                case 3: goto L_0x01a1;
                case 4: goto L_0x01a1;
                default: goto L_0x003d;
            }
        L_0x003d:
            r4 = r3
            r5 = r3
        L_0x003f:
            if (r0 == 0) goto L_0x004b
            java.lang.String r1 = f681b
            java.lang.String r2 = "Disconnecting url connection"
            com.zopim.android.sdk.api.Logger.m577v(r1, r2)
            r0.disconnect()
        L_0x004b:
            if (r5 == 0) goto L_0x0057
            java.lang.String r0 = f681b     // Catch:{ Exception -> 0x01d5 }
            java.lang.String r1 = "Closing input stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x01d5 }
            r5.close()     // Catch:{ Exception -> 0x01d5 }
        L_0x0057:
            if (r4 == 0) goto L_0x0063
            java.lang.String r0 = f681b     // Catch:{ Exception -> 0x01df }
            java.lang.String r1 = "Closing file output stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x01df }
            r4.close()     // Catch:{ Exception -> 0x01df }
        L_0x0063:
            return
        L_0x0064:
            java.lang.String r1 = "Content-Disposition"
            java.lang.String r1 = r0.getHeaderField(r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r2 = r0.getContentType()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            int r4 = r0.getContentLength()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r5 = f681b     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.StringBuilder r6 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r6.<init>()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r7 = "Content-Type = "
            java.lang.StringBuilder r6 = r6.append(r7)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.StringBuilder r2 = r6.append(r2)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r2 = r2.toString()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.Logger.m577v(r5, r2)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r2 = f681b     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.StringBuilder r5 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r5.<init>()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r6 = "Content-Disposition = "
            java.lang.StringBuilder r5 = r5.append(r6)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.StringBuilder r1 = r5.append(r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r1 = r1.toString()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.Logger.m577v(r2, r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r1 = f681b     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r2.<init>()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r5 = "Content-Length = "
            java.lang.StringBuilder r2 = r2.append(r5)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.StringBuilder r2 = r2.append(r4)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r2 = r2.toString()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.Logger.m577v(r1, r2)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.io.InputStream r5 = r0.getInputStream()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.io.BufferedOutputStream r4 = new java.io.BufferedOutputStream     // Catch:{ Exception -> 0x0222, all -> 0x0212 }
            java.io.FileOutputStream r1 = new java.io.FileOutputStream     // Catch:{ Exception -> 0x0222, all -> 0x0212 }
            r1.<init>(r10)     // Catch:{ Exception -> 0x0222, all -> 0x0212 }
            r4.<init>(r1)     // Catch:{ Exception -> 0x0222, all -> 0x0212 }
            r1 = 4096(0x1000, float:5.74E-42)
            byte[] r1 = new byte[r1]     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
        L_0x00cc:
            int r2 = r5.read(r1)     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            r3 = -1
            if (r2 == r3) goto L_0x014a
            r3 = 0
            r4.write(r1, r3, r2)     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            goto L_0x00cc
        L_0x00d8:
            r1 = move-exception
            r2 = r0
        L_0x00da:
            java.lang.String r0 = f681b     // Catch:{ all -> 0x0217 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ all -> 0x0217 }
            r3.<init>()     // Catch:{ all -> 0x0217 }
            java.lang.String r6 = "Error downloading file from "
            java.lang.StringBuilder r3 = r3.append(r6)     // Catch:{ all -> 0x0217 }
            java.lang.StringBuilder r3 = r3.append(r9)     // Catch:{ all -> 0x0217 }
            java.lang.String r3 = r3.toString()     // Catch:{ all -> 0x0217 }
            android.util.Log.e(r0, r3, r1)     // Catch:{ all -> 0x0217 }
            com.zopim.android.sdk.api.l$a r0 = new com.zopim.android.sdk.api.l$a     // Catch:{ all -> 0x0217 }
            r0.<init>()     // Catch:{ all -> 0x0217 }
            com.zopim.android.sdk.api.ErrorResponse$Kind r3 = com.zopim.android.sdk.api.ErrorResponse.Kind.UNEXPECTED     // Catch:{ all -> 0x0217 }
            com.zopim.android.sdk.api.l$a r0 = r0.mo20657a(r3)     // Catch:{ all -> 0x0217 }
            java.lang.String r1 = r1.getMessage()     // Catch:{ all -> 0x0217 }
            com.zopim.android.sdk.api.l$a r0 = r0.mo20658a(r1)     // Catch:{ all -> 0x0217 }
            java.lang.String r1 = r9.toExternalForm()     // Catch:{ all -> 0x0217 }
            com.zopim.android.sdk.api.l$a r0 = r0.mo20660b(r1)     // Catch:{ all -> 0x0217 }
            com.zopim.android.sdk.api.l r0 = r0.mo20659a()     // Catch:{ all -> 0x0217 }
            com.zopim.android.sdk.api.u<java.io.File> r1 = r8.f682c     // Catch:{ all -> 0x0217 }
            if (r1 == 0) goto L_0x011a
            com.zopim.android.sdk.api.u<java.io.File> r1 = r8.f682c     // Catch:{ all -> 0x0217 }
            r1.mo20643a(r0)     // Catch:{ all -> 0x0217 }
        L_0x011a:
            if (r2 == 0) goto L_0x0126
            java.lang.String r0 = f681b
            java.lang.String r1 = "Disconnecting url connection"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)
            r2.disconnect()
        L_0x0126:
            if (r5 == 0) goto L_0x0132
            java.lang.String r0 = f681b     // Catch:{ Exception -> 0x01e9 }
            java.lang.String r1 = "Closing input stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x01e9 }
            r5.close()     // Catch:{ Exception -> 0x01e9 }
        L_0x0132:
            if (r4 == 0) goto L_0x0063
            java.lang.String r0 = f681b     // Catch:{ Exception -> 0x0140 }
            java.lang.String r1 = "Closing file output stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x0140 }
            r4.close()     // Catch:{ Exception -> 0x0140 }
            goto L_0x0063
        L_0x0140:
            r0 = move-exception
            java.lang.String r1 = f681b
            java.lang.String r2 = "Failed to close file output stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x0063
        L_0x014a:
            r4.flush()     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            r4.close()     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            r5.close()     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            java.lang.String r1 = f681b     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            r2.<init>()     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            java.lang.String r3 = "File downloaded "
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            java.lang.String r3 = r10.getPath()     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            java.lang.String r2 = r2.toString()     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            com.zopim.android.sdk.api.Logger.m577v(r1, r2)     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            com.zopim.android.sdk.api.u<java.io.File> r1 = r8.f682c     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            if (r1 == 0) goto L_0x003f
            com.zopim.android.sdk.api.u<java.io.File> r1 = r8.f682c     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            r1.mo20644a(r10)     // Catch:{ Exception -> 0x00d8, all -> 0x017a }
            goto L_0x003f
        L_0x017a:
            r1 = move-exception
            r2 = r0
        L_0x017c:
            if (r2 == 0) goto L_0x0188
            java.lang.String r0 = f681b
            java.lang.String r3 = "Disconnecting url connection"
            com.zopim.android.sdk.api.Logger.m577v(r0, r3)
            r2.disconnect()
        L_0x0188:
            if (r5 == 0) goto L_0x0194
            java.lang.String r0 = f681b     // Catch:{ Exception -> 0x01f3 }
            java.lang.String r2 = "Closing input stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r2)     // Catch:{ Exception -> 0x01f3 }
            r5.close()     // Catch:{ Exception -> 0x01f3 }
        L_0x0194:
            if (r4 == 0) goto L_0x01a0
            java.lang.String r0 = f681b     // Catch:{ Exception -> 0x01fc }
            java.lang.String r2 = "Closing file output stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r2)     // Catch:{ Exception -> 0x01fc }
            r4.close()     // Catch:{ Exception -> 0x01fc }
        L_0x01a0:
            throw r1
        L_0x01a1:
            java.lang.String r2 = r0.getResponseMessage()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.l$a r4 = new com.zopim.android.sdk.api.l$a     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r4.<init>()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.ErrorResponse$Kind r5 = com.zopim.android.sdk.api.ErrorResponse.Kind.HTTP     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.l$a r4 = r4.mo20657a(r5)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.l$a r1 = r4.mo20656a(r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            java.lang.String r4 = r9.toExternalForm()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.l$a r1 = r1.mo20660b(r4)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.l$a r1 = r1.mo20661c(r2)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.l r1 = r1.mo20659a()     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            com.zopim.android.sdk.api.u<java.io.File> r2 = r8.f682c     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            if (r2 == 0) goto L_0x003d
            com.zopim.android.sdk.api.u<java.io.File> r2 = r8.f682c     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            r2.mo20643a(r1)     // Catch:{ Exception -> 0x01cf, all -> 0x020c }
            goto L_0x003d
        L_0x01cf:
            r1 = move-exception
            r2 = r0
            r4 = r3
            r5 = r3
            goto L_0x00da
        L_0x01d5:
            r0 = move-exception
            java.lang.String r1 = f681b
            java.lang.String r2 = "Failed to close output stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x0057
        L_0x01df:
            r0 = move-exception
            java.lang.String r1 = f681b
            java.lang.String r2 = "Failed to close file output stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x0063
        L_0x01e9:
            r0 = move-exception
            java.lang.String r1 = f681b
            java.lang.String r2 = "Failed to close output stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x0132
        L_0x01f3:
            r0 = move-exception
            java.lang.String r2 = f681b
            java.lang.String r3 = "Failed to close output stream"
            android.util.Log.w(r2, r3, r0)
            goto L_0x0194
        L_0x01fc:
            r0 = move-exception
            java.lang.String r2 = f681b
            java.lang.String r3 = "Failed to close file output stream"
            android.util.Log.w(r2, r3, r0)
            goto L_0x01a0
        L_0x0205:
            r0 = move-exception
            r1 = r0
            r2 = r3
            r4 = r3
            r5 = r3
            goto L_0x017c
        L_0x020c:
            r1 = move-exception
            r2 = r0
            r4 = r3
            r5 = r3
            goto L_0x017c
        L_0x0212:
            r1 = move-exception
            r2 = r0
            r4 = r3
            goto L_0x017c
        L_0x0217:
            r0 = move-exception
            r1 = r0
            goto L_0x017c
        L_0x021b:
            r0 = move-exception
            r1 = r0
            r2 = r3
            r4 = r3
            r5 = r3
            goto L_0x00da
        L_0x0222:
            r1 = move-exception
            r2 = r0
            r4 = r3
            goto L_0x00da
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.api.C1149j.m595b(java.net.URL, java.io.File):void");
    }

    /* renamed from: a */
    public void mo20653a(C1161u<File> uVar) {
        this.f682c = uVar;
    }

    /* renamed from: a */
    public void mo20654a(@NonNull URL url, @NonNull File file) {
        if (file == null || file.getName() == null || file.getName().isEmpty()) {
            Log.e(f681b, "File validation failed. Upload aborted.");
        } else if (url == null || !Patterns.WEB_URL.matcher(url.toString()).matches()) {
            Log.e(f681b, "URL validation failed. Upload aborted.");
        } else {
            Logger.m577v(f681b, "Start of download.");
            m595b(url, file);
            Logger.m577v(f681b, "End of download.");
        }
    }
}
