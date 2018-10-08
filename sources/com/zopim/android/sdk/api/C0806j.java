package com.zopim.android.sdk.api;

import android.support.annotation.NonNull;
import android.util.Log;
import android.util.Patterns;
import java.io.File;
import java.net.URL;

/* renamed from: com.zopim.android.sdk.api.j */
final class C0806j implements HttpRequest {
    /* renamed from: b */
    private static final String f638b = C0806j.class.getSimpleName();
    /* renamed from: c */
    private C0800u<File> f639c;

    C0806j() {
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: b */
    private void m586b(java.net.URL r10, java.io.File r11) {
        /*
        r9 = this;
        r2 = 0;
        r0 = r10.openConnection();	 Catch:{ Exception -> 0x021f, all -> 0x01d1 }
        r0 = (javax.net.ssl.HttpsURLConnection) r0;	 Catch:{ Exception -> 0x021f, all -> 0x01d1 }
        r1 = "User-Agent";
        r3 = "http.agent";
        r3 = java.lang.System.getProperty(r3);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r0.setRequestProperty(r1, r3);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = "Accept-Charset";
        r3 = "UTF-8";
        r0.setRequestProperty(r1, r3);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = 0;
        r0.setInstanceFollowRedirects(r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = a;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = (int) r4;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r0.setReadTimeout(r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = r0.getResponseCode();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = f638b;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = "response connection.getResponseMessage()";
        android.util.Log.v(r3, r4);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = com.zopim.android.sdk.api.HttpRequest.Status.getStatus(r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = com.zopim.android.sdk.api.C0807k.f640a;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r3.ordinal();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r4[r3];	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        switch(r3) {
            case 1: goto L_0x0063;
            case 2: goto L_0x017e;
            case 3: goto L_0x017e;
            case 4: goto L_0x017e;
            default: goto L_0x003d;
        };
    L_0x003d:
        r4 = r2;
    L_0x003e:
        if (r0 == 0) goto L_0x004a;
    L_0x0040:
        r1 = f638b;
        r3 = "Disconnecting url connection";
        com.zopim.android.sdk.api.Logger.m564v(r1, r3);
        r0.disconnect();
    L_0x004a:
        if (r4 == 0) goto L_0x0056;
    L_0x004c:
        r0 = f638b;	 Catch:{ Exception -> 0x01b3 }
        r1 = "Closing input stream";
        com.zopim.android.sdk.api.Logger.m564v(r0, r1);	 Catch:{ Exception -> 0x01b3 }
        r4.close();	 Catch:{ Exception -> 0x01b3 }
    L_0x0056:
        if (r2 == 0) goto L_0x0062;
    L_0x0058:
        r0 = f638b;	 Catch:{ Exception -> 0x01bd }
        r1 = "Closing file output stream";
        com.zopim.android.sdk.api.Logger.m564v(r0, r1);	 Catch:{ Exception -> 0x01bd }
        r2.close();	 Catch:{ Exception -> 0x01bd }
    L_0x0062:
        return;
    L_0x0063:
        r1 = "Content-Disposition";
        r1 = r0.getHeaderField(r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r0.getContentType();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = r0.getContentLength();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r5 = f638b;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r6 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r6.<init>();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r7 = "Content-Type = ";
        r6 = r6.append(r7);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r6.append(r3);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r3.toString();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        com.zopim.android.sdk.api.Logger.m564v(r5, r3);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = f638b;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r5 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r5.<init>();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r6 = "Content-Disposition = ";
        r5 = r5.append(r6);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = r5.append(r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = r1.toString();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        com.zopim.android.sdk.api.Logger.m564v(r3, r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = f638b;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3.<init>();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r5 = "Content-Length = ";
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r3.append(r4);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r3.toString();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        com.zopim.android.sdk.api.Logger.m564v(r1, r3);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = r0.getInputStream();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = new java.io.BufferedOutputStream;	 Catch:{ Exception -> 0x0224, all -> 0x0211 }
        r1 = new java.io.FileOutputStream;	 Catch:{ Exception -> 0x0224, all -> 0x0211 }
        r1.<init>(r11);	 Catch:{ Exception -> 0x0224, all -> 0x0211 }
        r3.<init>(r1);	 Catch:{ Exception -> 0x0224, all -> 0x0211 }
        r1 = 4096; // 0x1000 float:5.74E-42 double:2.0237E-320;
        r1 = new byte[r1];	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
    L_0x00cb:
        r2 = r4.read(r1);	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r5 = -1;
        if (r2 == r5) goto L_0x014d;
    L_0x00d2:
        r5 = 0;
        r3.write(r1, r5, r2);	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        goto L_0x00cb;
    L_0x00d7:
        r1 = move-exception;
        r2 = r3;
        r3 = r4;
        r8 = r1;
        r1 = r0;
        r0 = r8;
    L_0x00dd:
        r4 = f638b;	 Catch:{ all -> 0x021c }
        r5 = new java.lang.StringBuilder;	 Catch:{ all -> 0x021c }
        r5.<init>();	 Catch:{ all -> 0x021c }
        r6 = "Error downloading file from ";
        r5 = r5.append(r6);	 Catch:{ all -> 0x021c }
        r5 = r5.append(r10);	 Catch:{ all -> 0x021c }
        r5 = r5.toString();	 Catch:{ all -> 0x021c }
        android.util.Log.e(r4, r5, r0);	 Catch:{ all -> 0x021c }
        r4 = new com.zopim.android.sdk.api.l$a;	 Catch:{ all -> 0x021c }
        r4.<init>();	 Catch:{ all -> 0x021c }
        r5 = com.zopim.android.sdk.api.ErrorResponse.Kind.UNEXPECTED;	 Catch:{ all -> 0x021c }
        r4 = r4.m596a(r5);	 Catch:{ all -> 0x021c }
        r0 = r0.getMessage();	 Catch:{ all -> 0x021c }
        r0 = r4.m597a(r0);	 Catch:{ all -> 0x021c }
        r4 = r10.toExternalForm();	 Catch:{ all -> 0x021c }
        r0 = r0.m599b(r4);	 Catch:{ all -> 0x021c }
        r0 = r0.m598a();	 Catch:{ all -> 0x021c }
        r4 = r9.f639c;	 Catch:{ all -> 0x021c }
        if (r4 == 0) goto L_0x011d;
    L_0x0118:
        r4 = r9.f639c;	 Catch:{ all -> 0x021c }
        r4.mo4226a(r0);	 Catch:{ all -> 0x021c }
    L_0x011d:
        if (r1 == 0) goto L_0x0129;
    L_0x011f:
        r0 = f638b;
        r4 = "Disconnecting url connection";
        com.zopim.android.sdk.api.Logger.m564v(r0, r4);
        r1.disconnect();
    L_0x0129:
        if (r3 == 0) goto L_0x0135;
    L_0x012b:
        r0 = f638b;	 Catch:{ Exception -> 0x01c7 }
        r1 = "Closing input stream";
        com.zopim.android.sdk.api.Logger.m564v(r0, r1);	 Catch:{ Exception -> 0x01c7 }
        r3.close();	 Catch:{ Exception -> 0x01c7 }
    L_0x0135:
        if (r2 == 0) goto L_0x0062;
    L_0x0137:
        r0 = f638b;	 Catch:{ Exception -> 0x0143 }
        r1 = "Closing file output stream";
        com.zopim.android.sdk.api.Logger.m564v(r0, r1);	 Catch:{ Exception -> 0x0143 }
        r2.close();	 Catch:{ Exception -> 0x0143 }
        goto L_0x0062;
    L_0x0143:
        r0 = move-exception;
        r1 = f638b;
        r2 = "Failed to close file output stream";
        android.util.Log.w(r1, r2, r0);
        goto L_0x0062;
    L_0x014d:
        r3.flush();	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r3.close();	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r4.close();	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r1 = f638b;	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r2 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r2.<init>();	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r5 = "File downloaded ";
        r2 = r2.append(r5);	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r5 = r11.getPath();	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r2 = r2.append(r5);	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r2 = r2.toString();	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        com.zopim.android.sdk.api.Logger.m564v(r1, r2);	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r1 = r9.f639c;	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        if (r1 == 0) goto L_0x022b;
    L_0x0176:
        r1 = r9.f639c;	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r1.mo4227a(r11);	 Catch:{ Exception -> 0x00d7, all -> 0x0216 }
        r2 = r3;
        goto L_0x003e;
    L_0x017e:
        r3 = r0.getResponseMessage();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = new com.zopim.android.sdk.api.l$a;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4.<init>();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r5 = com.zopim.android.sdk.api.ErrorResponse.Kind.HTTP;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = r4.m596a(r5);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = r4.m595a(r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r4 = r10.toExternalForm();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = r1.m599b(r4);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = r1.m600c(r3);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r1 = r1.m598a();	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3 = r9.f639c;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        if (r3 == 0) goto L_0x003d;
    L_0x01a5:
        r3 = r9.f639c;	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        r3.mo4226a(r1);	 Catch:{ Exception -> 0x01ac, all -> 0x020b }
        goto L_0x003d;
    L_0x01ac:
        r1 = move-exception;
        r3 = r2;
        r8 = r0;
        r0 = r1;
        r1 = r8;
        goto L_0x00dd;
    L_0x01b3:
        r0 = move-exception;
        r1 = f638b;
        r3 = "Failed to close output stream";
        android.util.Log.w(r1, r3, r0);
        goto L_0x0056;
    L_0x01bd:
        r0 = move-exception;
        r1 = f638b;
        r2 = "Failed to close file output stream";
        android.util.Log.w(r1, r2, r0);
        goto L_0x0062;
    L_0x01c7:
        r0 = move-exception;
        r1 = f638b;
        r3 = "Failed to close output stream";
        android.util.Log.w(r1, r3, r0);
        goto L_0x0135;
    L_0x01d1:
        r0 = move-exception;
        r1 = r2;
        r4 = r2;
    L_0x01d4:
        if (r1 == 0) goto L_0x01e0;
    L_0x01d6:
        r3 = f638b;
        r5 = "Disconnecting url connection";
        com.zopim.android.sdk.api.Logger.m564v(r3, r5);
        r1.disconnect();
    L_0x01e0:
        if (r4 == 0) goto L_0x01ec;
    L_0x01e2:
        r1 = f638b;	 Catch:{ Exception -> 0x01f9 }
        r3 = "Closing input stream";
        com.zopim.android.sdk.api.Logger.m564v(r1, r3);	 Catch:{ Exception -> 0x01f9 }
        r4.close();	 Catch:{ Exception -> 0x01f9 }
    L_0x01ec:
        if (r2 == 0) goto L_0x01f8;
    L_0x01ee:
        r1 = f638b;	 Catch:{ Exception -> 0x0202 }
        r3 = "Closing file output stream";
        com.zopim.android.sdk.api.Logger.m564v(r1, r3);	 Catch:{ Exception -> 0x0202 }
        r2.close();	 Catch:{ Exception -> 0x0202 }
    L_0x01f8:
        throw r0;
    L_0x01f9:
        r1 = move-exception;
        r3 = f638b;
        r4 = "Failed to close output stream";
        android.util.Log.w(r3, r4, r1);
        goto L_0x01ec;
    L_0x0202:
        r1 = move-exception;
        r2 = f638b;
        r3 = "Failed to close file output stream";
        android.util.Log.w(r2, r3, r1);
        goto L_0x01f8;
    L_0x020b:
        r1 = move-exception;
        r4 = r2;
        r8 = r0;
        r0 = r1;
        r1 = r8;
        goto L_0x01d4;
    L_0x0211:
        r1 = move-exception;
        r8 = r1;
        r1 = r0;
        r0 = r8;
        goto L_0x01d4;
    L_0x0216:
        r1 = move-exception;
        r2 = r3;
        r8 = r0;
        r0 = r1;
        r1 = r8;
        goto L_0x01d4;
    L_0x021c:
        r0 = move-exception;
        r4 = r3;
        goto L_0x01d4;
    L_0x021f:
        r0 = move-exception;
        r1 = r2;
        r3 = r2;
        goto L_0x00dd;
    L_0x0224:
        r1 = move-exception;
        r3 = r4;
        r8 = r0;
        r0 = r1;
        r1 = r8;
        goto L_0x00dd;
    L_0x022b:
        r2 = r3;
        goto L_0x003e;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.api.j.b(java.net.URL, java.io.File):void");
    }

    /* renamed from: a */
    public void m587a(C0800u<File> c0800u) {
        this.f639c = c0800u;
    }

    /* renamed from: a */
    public void m588a(@NonNull URL url, @NonNull File file) {
        if (file == null || file.getName() == null || file.getName().isEmpty()) {
            Log.e(f638b, "File validation failed. Upload aborted.");
        } else if (url == null || !Patterns.WEB_URL.matcher(url.toString()).matches()) {
            Log.e(f638b, "URL validation failed. Upload aborted.");
        } else {
            Logger.m564v(f638b, "Start of download.");
            m586b(url, file);
            Logger.m564v(f638b, "End of download.");
        }
    }
}
