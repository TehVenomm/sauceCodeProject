package com.appsflyer;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import android.os.Build.VERSION;
import java.io.File;
import java.lang.ref.WeakReference;
import java.security.SecureRandom;
import java.util.UUID;

/* renamed from: com.appsflyer.p */
final class C0455p {

    /* renamed from: ॱ */
    private static String f315 = null;

    C0455p() {
    }

    /* renamed from: ˋ */
    public static synchronized String m326(WeakReference<Context> weakReference) {
        String str;
        String obj;
        String str2 = null;
        synchronized (C0455p.class) {
            if (weakReference.get() == null) {
                str = f315;
            } else {
                if (f315 == null) {
                    if (weakReference.get() != null) {
                        str2 = ((Context) weakReference.get()).getSharedPreferences("appsflyer-data", 0).getString("AF_INSTALLATION", null);
                    }
                    if (str2 != null) {
                        f315 = str2;
                    } else {
                        try {
                            File file = new File(((Context) weakReference.get()).getFilesDir(), "AF_INSTALLATION");
                            if (!file.exists()) {
                                if (VERSION.SDK_INT >= 9) {
                                    obj = new StringBuilder().append(System.currentTimeMillis()).append("-").append(Math.abs(new SecureRandom().nextLong())).toString();
                                } else {
                                    obj = UUID.randomUUID().toString();
                                }
                                f315 = obj;
                            } else {
                                f315 = m327(file);
                                file.delete();
                            }
                            String str3 = f315;
                            Editor edit = ((Context) weakReference.get()).getSharedPreferences("appsflyer-data", 0).edit();
                            edit.putString("AF_INSTALLATION", str3);
                            if (VERSION.SDK_INT >= 9) {
                                edit.apply();
                            } else {
                                edit.commit();
                            }
                        } catch (Exception e) {
                            AFLogger.afErrorLog("Error getting AF unique ID", e);
                        }
                    }
                    if (f315 != null) {
                        AppsFlyerProperties.getInstance().set("uid", f315);
                    }
                }
                str = f315;
            }
        }
        return str;
    }

    /* JADX WARNING: Removed duplicated region for block: B:21:0x0031 A[SYNTHETIC, Splitter:B:21:0x0031] */
    /* JADX WARNING: Removed duplicated region for block: B:28:0x0040 A[SYNTHETIC, Splitter:B:28:0x0040] */
    /* JADX WARNING: Removed duplicated region for block: B:33:0x004b  */
    /* renamed from: ˏ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.String m327(java.io.File r4) {
        /*
            r2 = 0
            java.io.RandomAccessFile r3 = new java.io.RandomAccessFile     // Catch:{ IOException -> 0x0027, all -> 0x003c }
            java.lang.String r0 = "r"
            r3.<init>(r4, r0)     // Catch:{ IOException -> 0x0027, all -> 0x003c }
            long r0 = r3.length()     // Catch:{ IOException -> 0x0051 }
            int r0 = (int) r0     // Catch:{ IOException -> 0x0051 }
            byte[] r0 = new byte[r0]     // Catch:{ IOException -> 0x0051 }
            r3.readFully(r0)     // Catch:{ IOException -> 0x0054 }
            r3.close()     // Catch:{ IOException -> 0x0054 }
            r3.close()     // Catch:{ IOException -> 0x0020 }
        L_0x0018:
            java.lang.String r1 = new java.lang.String
            if (r0 == 0) goto L_0x004b
        L_0x001c:
            r1.<init>(r0)
            return r1
        L_0x0020:
            r1 = move-exception
            java.lang.String r2 = "Exception while trying to close the InstallationFile"
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
            goto L_0x0018
        L_0x0027:
            r1 = move-exception
            r0 = r2
            r3 = r2
        L_0x002a:
            java.lang.String r2 = "Exception while reading InstallationFile: "
            com.appsflyer.AFLogger.afErrorLog(r2, r1)     // Catch:{ all -> 0x004f }
            if (r3 == 0) goto L_0x0018
            r3.close()     // Catch:{ IOException -> 0x0035 }
            goto L_0x0018
        L_0x0035:
            r1 = move-exception
            java.lang.String r2 = "Exception while trying to close the InstallationFile"
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
            goto L_0x0018
        L_0x003c:
            r0 = move-exception
            r3 = r2
        L_0x003e:
            if (r3 == 0) goto L_0x0043
            r3.close()     // Catch:{ IOException -> 0x0044 }
        L_0x0043:
            throw r0
        L_0x0044:
            r1 = move-exception
            java.lang.String r2 = "Exception while trying to close the InstallationFile"
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
            goto L_0x0043
        L_0x004b:
            r0 = 0
            byte[] r0 = new byte[r0]
            goto L_0x001c
        L_0x004f:
            r0 = move-exception
            goto L_0x003e
        L_0x0051:
            r1 = move-exception
            r0 = r2
            goto L_0x002a
        L_0x0054:
            r1 = move-exception
            goto L_0x002a
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.C0455p.m327(java.io.File):java.lang.String");
    }
}
