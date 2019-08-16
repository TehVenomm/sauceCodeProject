package com.zopim.android.sdk.api;

import android.util.Log;
import android.util.Patterns;
import com.zopim.android.sdk.api.HttpRequest.ProgressListener;
import java.io.File;
import java.net.URL;
import p017io.fabric.sdk.android.services.network.HttpRequest;

/* renamed from: com.zopim.android.sdk.api.s */
final class C1159s implements HttpRequest {

    /* renamed from: b */
    private static final String f709b = C1159s.class.getSimpleName();

    /* renamed from: d */
    private static final String f710d = Long.toHexString(System.currentTimeMillis());

    /* renamed from: c */
    private String f711c = HttpRequest.METHOD_POST;

    /* renamed from: e */
    private C1161u<Void> f712e;

    /* renamed from: f */
    private ProgressListener f713f;

    C1159s() {
    }

    /* renamed from: a */
    private void m631a(int i) {
        if (this.f713f != null) {
            this.f713f.onProgressUpdate(i);
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:104:? A[RETURN, SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:35:0x0227 A[Catch:{ all -> 0x0330 }] */
    /* JADX WARNING: Removed duplicated region for block: B:37:0x022e  */
    /* JADX WARNING: Removed duplicated region for block: B:39:0x023a A[SYNTHETIC, Splitter:B:39:0x023a] */
    /* JADX WARNING: Removed duplicated region for block: B:42:0x0246 A[SYNTHETIC, Splitter:B:42:0x0246] */
    /* JADX WARNING: Removed duplicated region for block: B:45:0x0252 A[SYNTHETIC, Splitter:B:45:0x0252] */
    /* JADX WARNING: Removed duplicated region for block: B:56:0x029a  */
    /* JADX WARNING: Removed duplicated region for block: B:58:0x02a6 A[SYNTHETIC, Splitter:B:58:0x02a6] */
    /* JADX WARNING: Removed duplicated region for block: B:61:0x02b2 A[SYNTHETIC, Splitter:B:61:0x02b2] */
    /* JADX WARNING: Removed duplicated region for block: B:64:0x02be A[SYNTHETIC, Splitter:B:64:0x02be] */
    /* renamed from: b */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void m632b(@android.support.annotation.NonNull java.io.File r12, @android.support.annotation.NonNull java.net.URL r13) {
        /*
            r11 = this;
            r3 = 0
            java.net.URLConnection r0 = r13.openConnection()     // Catch:{ Exception -> 0x0334, all -> 0x0316 }
            javax.net.ssl.HttpsURLConnection r0 = (javax.net.ssl.HttpsURLConnection) r0     // Catch:{ Exception -> 0x0334, all -> 0x0316 }
            java.lang.String r1 = r11.f711c     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            r0.setRequestMethod(r1)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            r1 = 1
            r0.setDoOutput(r1)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.lang.String r1 = "User-Agent"
            java.lang.String r2 = "http.agent"
            java.lang.String r2 = java.lang.System.getProperty(r2)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            r0.setRequestProperty(r1, r2)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.lang.String r1 = "Accept-Charset"
            java.lang.String r2 = "UTF-8"
            r0.setRequestProperty(r1, r2)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.lang.String r1 = "Content-Type"
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            r2.<init>()     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.lang.String r4 = "multipart/form-data; boundary="
            java.lang.StringBuilder r2 = r2.append(r4)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.lang.String r4 = f710d     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.lang.StringBuilder r2 = r2.append(r4)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.lang.String r2 = r2.toString()     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            r0.setRequestProperty(r1, r2)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            r1 = 0
            r0.setInstanceFollowRedirects(r1)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            long r4 = f666a     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            int r1 = (int) r4     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            r0.setReadTimeout(r1)     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.io.OutputStream r6 = r0.getOutputStream()     // Catch:{ Exception -> 0x033c, all -> 0x031e }
            java.io.PrintWriter r4 = new java.io.PrintWriter     // Catch:{ Exception -> 0x0343, all -> 0x0325 }
            java.io.OutputStreamWriter r1 = new java.io.OutputStreamWriter     // Catch:{ Exception -> 0x0343, all -> 0x0325 }
            java.lang.String r2 = "UTF-8"
            r1.<init>(r6, r2)     // Catch:{ Exception -> 0x0343, all -> 0x0325 }
            r2 = 1
            r4.<init>(r1, r2)     // Catch:{ Exception -> 0x0343, all -> 0x0325 }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r1.<init>()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "--"
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = f710d     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r1 = r1.toString()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.io.PrintWriter r1 = r4.append(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "\r\n"
            r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r1.<init>()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "Content-Disposition: form-data; name=\"binaryFile\"; filename=\""
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = r12.getName()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "\""
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r1 = r1.toString()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.io.PrintWriter r1 = r4.append(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "\r\n"
            r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r1.<init>()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "Content-Type: "
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = r12.getName()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = java.net.URLConnection.guessContentTypeFromName(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r1 = r1.toString()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.io.PrintWriter r1 = r4.append(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "\r\n"
            r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r1.<init>()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "Content-Length: "
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            long r8 = r12.length()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.StringBuilder r1 = r1.append(r8)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r1 = r1.toString()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r4.append(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r1 = "Content-Transfer-Encoding: binary"
            java.io.PrintWriter r1 = r4.append(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r2 = "\r\n"
            r1.append(r2)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.lang.String r1 = "\r\n"
            java.io.PrintWriter r1 = r4.append(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r1.flush()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r7 = 99
            long r8 = r12.length()     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r1 = 1
            r11.m631a(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.io.BufferedInputStream r5 = new java.io.BufferedInputStream     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            java.io.FileInputStream r1 = new java.io.FileInputStream     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r1.<init>(r12)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            r5.<init>(r1)     // Catch:{ Exception -> 0x0349, all -> 0x032b }
            int r1 = r5.available()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r2 = 4096(0x1000, float:5.74E-42)
            int r1 = java.lang.Math.min(r1, r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            byte[] r10 = new byte[r1]     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r2 = 0
            int r2 = r5.read(r10, r2, r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r1 = f709b     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r3 = "Reading bytes from fis"
            android.util.Log.v(r1, r3)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r1 = r2
            r3 = r2
        L_0x011b:
            if (r3 <= 0) goto L_0x013f
            r2 = 0
            r6.write(r10, r2, r3)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            int r2 = r7 * r1
            long r2 = (long) r2     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            long r2 = r2 / r8
            float r2 = (float) r2     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            int r2 = java.lang.Math.round(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r11.m631a(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            int r2 = r5.available()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r3 = 4096(0x1000, float:5.74E-42)
            int r2 = java.lang.Math.min(r2, r3)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r3 = 0
            int r2 = r5.read(r10, r3, r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            int r1 = r1 + r2
            r3 = r2
            goto L_0x011b
        L_0x013f:
            java.lang.String r1 = f709b     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r2 = "Finished write to output stream. Closing file input stream"
            com.zopim.android.sdk.api.Logger.m577v(r1, r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r5.close()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r6.flush()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r1 = "\r\n"
            java.io.PrintWriter r1 = r4.append(r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r1.flush()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r1.<init>()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r2 = "--"
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r2 = f710d     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r2 = "--"
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r1 = r1.toString()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.io.PrintWriter r1 = r4.append(r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r2 = "\r\n"
            java.io.PrintWriter r1 = r1.append(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r1.flush()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r4.close()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r6.close()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            int r1 = r0.getResponseCode()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.HttpRequest$Status r2 = com.zopim.android.sdk.api.HttpRequest.Status.getStatus(r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            int[] r3 = com.zopim.android.sdk.api.C1160t.f714a     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            int r2 = r2.ordinal()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r2 = r3[r2]     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            switch(r2) {
                case 1: goto L_0x01c7;
                case 2: goto L_0x0268;
                case 3: goto L_0x0268;
                case 4: goto L_0x0268;
                default: goto L_0x0196;
            }
        L_0x0196:
            if (r0 == 0) goto L_0x01a2
            java.lang.String r1 = f709b
            java.lang.String r2 = "Disconnecting url connection"
            com.zopim.android.sdk.api.Logger.m577v(r1, r2)
            r0.disconnect()
        L_0x01a2:
            if (r4 == 0) goto L_0x01ae
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x02c9 }
            java.lang.String r1 = "Closing print writer"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x02c9 }
            r4.close()     // Catch:{ Exception -> 0x02c9 }
        L_0x01ae:
            if (r6 == 0) goto L_0x01ba
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x02d3 }
            java.lang.String r1 = "Closing output stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x02d3 }
            r6.close()     // Catch:{ Exception -> 0x02d3 }
        L_0x01ba:
            if (r5 == 0) goto L_0x01c6
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x02dd }
            java.lang.String r1 = "Closing file input stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x02dd }
            r5.close()     // Catch:{ Exception -> 0x02dd }
        L_0x01c6:
            return
        L_0x01c7:
            java.lang.String r2 = f709b     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r3.<init>()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r7 = "Request completed. Status "
            java.lang.StringBuilder r3 = r3.append(r7)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.StringBuilder r1 = r3.append(r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r1 = r1.toString()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.Logger.m575i(r2, r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.u<java.lang.Void> r1 = r11.f712e     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            if (r1 == 0) goto L_0x0196
            com.zopim.android.sdk.api.u<java.lang.Void> r1 = r11.f712e     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r2 = 0
            r1.mo20680b(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            goto L_0x0196
        L_0x01ea:
            r1 = move-exception
            r2 = r0
        L_0x01ec:
            java.lang.String r0 = f709b     // Catch:{ all -> 0x0330 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ all -> 0x0330 }
            r3.<init>()     // Catch:{ all -> 0x0330 }
            java.lang.String r7 = "Error uploading file to "
            java.lang.StringBuilder r3 = r3.append(r7)     // Catch:{ all -> 0x0330 }
            java.lang.StringBuilder r3 = r3.append(r13)     // Catch:{ all -> 0x0330 }
            java.lang.String r3 = r3.toString()     // Catch:{ all -> 0x0330 }
            android.util.Log.e(r0, r3, r1)     // Catch:{ all -> 0x0330 }
            com.zopim.android.sdk.api.l$a r0 = new com.zopim.android.sdk.api.l$a     // Catch:{ all -> 0x0330 }
            r0.<init>()     // Catch:{ all -> 0x0330 }
            com.zopim.android.sdk.api.ErrorResponse$Kind r3 = com.zopim.android.sdk.api.ErrorResponse.Kind.UNEXPECTED     // Catch:{ all -> 0x0330 }
            com.zopim.android.sdk.api.l$a r0 = r0.mo20657a(r3)     // Catch:{ all -> 0x0330 }
            java.lang.String r1 = r1.getMessage()     // Catch:{ all -> 0x0330 }
            com.zopim.android.sdk.api.l$a r0 = r0.mo20658a(r1)     // Catch:{ all -> 0x0330 }
            java.lang.String r1 = r13.toExternalForm()     // Catch:{ all -> 0x0330 }
            com.zopim.android.sdk.api.l$a r0 = r0.mo20660b(r1)     // Catch:{ all -> 0x0330 }
            com.zopim.android.sdk.api.l r0 = r0.mo20659a()     // Catch:{ all -> 0x0330 }
            com.zopim.android.sdk.api.u<java.lang.Void> r1 = r11.f712e     // Catch:{ all -> 0x0330 }
            if (r1 == 0) goto L_0x022c
            com.zopim.android.sdk.api.u<java.lang.Void> r1 = r11.f712e     // Catch:{ all -> 0x0330 }
            r1.mo20679b(r0)     // Catch:{ all -> 0x0330 }
        L_0x022c:
            if (r2 == 0) goto L_0x0238
            java.lang.String r0 = f709b
            java.lang.String r1 = "Disconnecting url connection"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)
            r2.disconnect()
        L_0x0238:
            if (r4 == 0) goto L_0x0244
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x02e7 }
            java.lang.String r1 = "Closing print writer"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x02e7 }
            r4.close()     // Catch:{ Exception -> 0x02e7 }
        L_0x0244:
            if (r6 == 0) goto L_0x0250
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x02f1 }
            java.lang.String r1 = "Closing output stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x02f1 }
            r6.close()     // Catch:{ Exception -> 0x02f1 }
        L_0x0250:
            if (r5 == 0) goto L_0x01c6
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x025e }
            java.lang.String r1 = "Closing file input stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r1)     // Catch:{ Exception -> 0x025e }
            r5.close()     // Catch:{ Exception -> 0x025e }
            goto L_0x01c6
        L_0x025e:
            r0 = move-exception
            java.lang.String r1 = f709b
            java.lang.String r2 = "Failed to close file input stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x01c6
        L_0x0268:
            java.lang.String r2 = r0.getResponseMessage()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.l$a r3 = new com.zopim.android.sdk.api.l$a     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r3.<init>()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.ErrorResponse$Kind r7 = com.zopim.android.sdk.api.ErrorResponse.Kind.HTTP     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.l$a r3 = r3.mo20657a(r7)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.l$a r1 = r3.mo20656a(r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            java.lang.String r3 = r13.toExternalForm()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.l$a r1 = r1.mo20660b(r3)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.l$a r1 = r1.mo20661c(r2)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.l r1 = r1.mo20659a()     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            com.zopim.android.sdk.api.u<java.lang.Void> r2 = r11.f712e     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            if (r2 == 0) goto L_0x0196
            com.zopim.android.sdk.api.u<java.lang.Void> r2 = r11.f712e     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            r2.mo20679b(r1)     // Catch:{ Exception -> 0x01ea, all -> 0x0296 }
            goto L_0x0196
        L_0x0296:
            r1 = move-exception
            r2 = r0
        L_0x0298:
            if (r2 == 0) goto L_0x02a4
            java.lang.String r0 = f709b
            java.lang.String r3 = "Disconnecting url connection"
            com.zopim.android.sdk.api.Logger.m577v(r0, r3)
            r2.disconnect()
        L_0x02a4:
            if (r4 == 0) goto L_0x02b0
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x02fb }
            java.lang.String r2 = "Closing print writer"
            com.zopim.android.sdk.api.Logger.m577v(r0, r2)     // Catch:{ Exception -> 0x02fb }
            r4.close()     // Catch:{ Exception -> 0x02fb }
        L_0x02b0:
            if (r6 == 0) goto L_0x02bc
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x0304 }
            java.lang.String r2 = "Closing output stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r2)     // Catch:{ Exception -> 0x0304 }
            r6.close()     // Catch:{ Exception -> 0x0304 }
        L_0x02bc:
            if (r5 == 0) goto L_0x02c8
            java.lang.String r0 = f709b     // Catch:{ Exception -> 0x030d }
            java.lang.String r2 = "Closing file input stream"
            com.zopim.android.sdk.api.Logger.m577v(r0, r2)     // Catch:{ Exception -> 0x030d }
            r5.close()     // Catch:{ Exception -> 0x030d }
        L_0x02c8:
            throw r1
        L_0x02c9:
            r0 = move-exception
            java.lang.String r1 = f709b
            java.lang.String r2 = "Failed to close writer"
            android.util.Log.w(r1, r2, r0)
            goto L_0x01ae
        L_0x02d3:
            r0 = move-exception
            java.lang.String r1 = f709b
            java.lang.String r2 = "Failed to close output stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x01ba
        L_0x02dd:
            r0 = move-exception
            java.lang.String r1 = f709b
            java.lang.String r2 = "Failed to close file input stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x01c6
        L_0x02e7:
            r0 = move-exception
            java.lang.String r1 = f709b
            java.lang.String r2 = "Failed to close writer"
            android.util.Log.w(r1, r2, r0)
            goto L_0x0244
        L_0x02f1:
            r0 = move-exception
            java.lang.String r1 = f709b
            java.lang.String r2 = "Failed to close output stream"
            android.util.Log.w(r1, r2, r0)
            goto L_0x0250
        L_0x02fb:
            r0 = move-exception
            java.lang.String r2 = f709b
            java.lang.String r3 = "Failed to close writer"
            android.util.Log.w(r2, r3, r0)
            goto L_0x02b0
        L_0x0304:
            r0 = move-exception
            java.lang.String r2 = f709b
            java.lang.String r3 = "Failed to close output stream"
            android.util.Log.w(r2, r3, r0)
            goto L_0x02bc
        L_0x030d:
            r0 = move-exception
            java.lang.String r2 = f709b
            java.lang.String r3 = "Failed to close file input stream"
            android.util.Log.w(r2, r3, r0)
            goto L_0x02c8
        L_0x0316:
            r0 = move-exception
            r1 = r0
            r2 = r3
            r4 = r3
            r5 = r3
            r6 = r3
            goto L_0x0298
        L_0x031e:
            r1 = move-exception
            r2 = r0
            r4 = r3
            r5 = r3
            r6 = r3
            goto L_0x0298
        L_0x0325:
            r1 = move-exception
            r2 = r0
            r4 = r3
            r5 = r3
            goto L_0x0298
        L_0x032b:
            r1 = move-exception
            r2 = r0
            r5 = r3
            goto L_0x0298
        L_0x0330:
            r0 = move-exception
            r1 = r0
            goto L_0x0298
        L_0x0334:
            r0 = move-exception
            r1 = r0
            r2 = r3
            r4 = r3
            r5 = r3
            r6 = r3
            goto L_0x01ec
        L_0x033c:
            r1 = move-exception
            r2 = r0
            r4 = r3
            r5 = r3
            r6 = r3
            goto L_0x01ec
        L_0x0343:
            r1 = move-exception
            r2 = r0
            r4 = r3
            r5 = r3
            goto L_0x01ec
        L_0x0349:
            r1 = move-exception
            r2 = r0
            r5 = r3
            goto L_0x01ec
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.api.C1159s.m632b(java.io.File, java.net.URL):void");
    }

    /* renamed from: a */
    public void mo20676a(ProgressListener progressListener) {
        this.f713f = progressListener;
    }

    /* renamed from: a */
    public void mo20677a(C1161u<Void> uVar) {
        this.f712e = uVar;
    }

    /* renamed from: a */
    public void mo20678a(File file, URL url) {
        if (file == null || file.getName() == null || file.getName().isEmpty() || !file.exists()) {
            Log.e(f709b, "File validation failed. Upload aborted.");
        } else if (url == null || !Patterns.WEB_URL.matcher(url.toString()).matches()) {
            Log.e(f709b, "URL validation failed. Upload aborted.");
        } else {
            Logger.m577v(f709b, "Start of upload.");
            m632b(file, url);
            Logger.m577v(f709b, "End of upload.");
        }
    }
}
