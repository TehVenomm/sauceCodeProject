package com.appsflyer;

import android.content.Context;
import android.os.Build;
import android.os.Build.VERSION;
import android.security.KeyPairGeneratorSpec;
import android.security.keystore.KeyGenParameterSpec.Builder;
import java.io.IOException;
import java.math.BigInteger;
import java.security.KeyPairGenerator;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.cert.CertificateException;
import java.security.spec.AlgorithmParameterSpec;
import java.util.Calendar;
import javax.security.auth.x500.X500Principal;

class AFKeystoreWrapper {

    /* renamed from: ˊ */
    private String f134;

    /* renamed from: ˋ */
    private final Object f135 = new Object();

    /* renamed from: ˎ */
    private int f136;

    /* renamed from: ˏ */
    private Context f137;

    /* renamed from: ॱ */
    private KeyStore f138;

    public AFKeystoreWrapper(Context context) {
        this.f137 = context;
        this.f134 = "";
        this.f136 = 0;
        AFLogger.afInfoLog("Initialising KeyStore..");
        try {
            this.f138 = KeyStore.getInstance("AndroidKeyStore");
            this.f138.load(null);
        } catch (IOException | KeyStoreException | NoSuchAlgorithmException | CertificateException e) {
            AFLogger.afErrorLog("Couldn't load keystore instance of type: AndroidKeyStore", e);
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final void mo6413(String str) {
        this.f134 = str;
        this.f136 = 0;
        m177(m176());
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6412() {
        String r0 = m176();
        synchronized (this.f135) {
            this.f136++;
            AFLogger.afInfoLog("Deleting key with alias: ".concat(String.valueOf(r0)));
            try {
                synchronized (this.f135) {
                    this.f138.deleteEntry(r0);
                }
            } catch (KeyStoreException e) {
                AFLogger.afErrorLog(new StringBuilder("Exception ").append(e.getMessage()).append(" occurred").toString(), e);
            }
        }
        m177(m176());
        return;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:13:0x0026, code lost:
        r4 = r0.split(",");
     */
    /* JADX WARNING: Code restructure failed: missing block: B:14:0x002e, code lost:
        if (r4.length != 3) goto L_0x0075;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0030, code lost:
        com.appsflyer.AFLogger.afInfoLog("Found a matching AF key with alias:\n".concat(java.lang.String.valueOf(r0)));
     */
    /* JADX WARNING: Code restructure failed: missing block: B:18:?, code lost:
        r0 = r4[1].trim().split("=");
        r2 = r4[2].trim().split("=");
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x0058, code lost:
        if (r0.length != 2) goto L_0x0073;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:21:0x005b, code lost:
        if (r2.length != 2) goto L_0x0073;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:0x005d, code lost:
        r8.f134 = r0[1].trim();
        r8.f136 = java.lang.Integer.parseInt(r2[1].trim());
     */
    /* JADX WARNING: Code restructure failed: missing block: B:33:0x0097, code lost:
        r0 = th;
     */
    /* renamed from: ˋ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final boolean mo6411() {
        /*
            r8 = this;
            r7 = 2
            r1 = 1
            r2 = 0
            java.lang.Object r3 = r8.f135
            monitor-enter(r3)
            java.security.KeyStore r0 = r8.f138     // Catch:{ all -> 0x0094 }
            if (r0 == 0) goto L_0x0099
            java.security.KeyStore r0 = r8.f138     // Catch:{ Throwable -> 0x0077 }
            java.util.Enumeration r4 = r0.aliases()     // Catch:{ Throwable -> 0x0077 }
        L_0x0010:
            boolean r0 = r4.hasMoreElements()     // Catch:{ Throwable -> 0x0077 }
            if (r0 == 0) goto L_0x0075
            java.lang.Object r0 = r4.nextElement()     // Catch:{ Throwable -> 0x0077 }
            java.lang.String r0 = (java.lang.String) r0     // Catch:{ Throwable -> 0x0077 }
            if (r0 == 0) goto L_0x0010
            java.lang.String r5 = "com.appsflyer"
            boolean r5 = r0.startsWith(r5)     // Catch:{ Throwable -> 0x0077 }
            if (r5 == 0) goto L_0x0010
            java.lang.String r4 = ","
            java.lang.String[] r4 = r0.split(r4)     // Catch:{ Throwable -> 0x0077 }
            int r5 = r4.length     // Catch:{ Throwable -> 0x0077 }
            r6 = 3
            if (r5 != r6) goto L_0x0075
            java.lang.String r5 = "Found a matching AF key with alias:\n"
            java.lang.String r0 = java.lang.String.valueOf(r0)     // Catch:{ Throwable -> 0x0077 }
            java.lang.String r0 = r5.concat(r0)     // Catch:{ Throwable -> 0x0077 }
            com.appsflyer.AFLogger.afInfoLog(r0)     // Catch:{ Throwable -> 0x0077 }
            r0 = 1
            r0 = r4[r0]     // Catch:{ Throwable -> 0x0097 }
            java.lang.String r0 = r0.trim()     // Catch:{ Throwable -> 0x0097 }
            java.lang.String r2 = "="
            java.lang.String[] r0 = r0.split(r2)     // Catch:{ Throwable -> 0x0097 }
            r2 = 2
            r2 = r4[r2]     // Catch:{ Throwable -> 0x0097 }
            java.lang.String r2 = r2.trim()     // Catch:{ Throwable -> 0x0097 }
            java.lang.String r4 = "="
            java.lang.String[] r2 = r2.split(r4)     // Catch:{ Throwable -> 0x0097 }
            int r4 = r0.length     // Catch:{ Throwable -> 0x0097 }
            if (r4 != r7) goto L_0x0073
            int r4 = r2.length     // Catch:{ Throwable -> 0x0097 }
            if (r4 != r7) goto L_0x0073
            r4 = 1
            r0 = r0[r4]     // Catch:{ Throwable -> 0x0097 }
            java.lang.String r0 = r0.trim()     // Catch:{ Throwable -> 0x0097 }
            r8.f134 = r0     // Catch:{ Throwable -> 0x0097 }
            r0 = 1
            r0 = r2[r0]     // Catch:{ Throwable -> 0x0097 }
            java.lang.String r0 = r0.trim()     // Catch:{ Throwable -> 0x0097 }
            int r0 = java.lang.Integer.parseInt(r0)     // Catch:{ Throwable -> 0x0097 }
            r8.f136 = r0     // Catch:{ Throwable -> 0x0097 }
        L_0x0073:
            monitor-exit(r3)     // Catch:{ all -> 0x0094 }
            return r1
        L_0x0075:
            r1 = r2
            goto L_0x0073
        L_0x0077:
            r0 = move-exception
            r1 = r2
        L_0x0079:
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ all -> 0x0094 }
            java.lang.String r4 = "Couldn't list KeyStore Aliases: "
            r2.<init>(r4)     // Catch:{ all -> 0x0094 }
            java.lang.Class r4 = r0.getClass()     // Catch:{ all -> 0x0094 }
            java.lang.String r4 = r4.getName()     // Catch:{ all -> 0x0094 }
            java.lang.StringBuilder r2 = r2.append(r4)     // Catch:{ all -> 0x0094 }
            java.lang.String r2 = r2.toString()     // Catch:{ all -> 0x0094 }
            com.appsflyer.AFLogger.afErrorLog(r2, r0)     // Catch:{ all -> 0x0094 }
            goto L_0x0073
        L_0x0094:
            r0 = move-exception
            monitor-exit(r3)
            throw r0
        L_0x0097:
            r0 = move-exception
            goto L_0x0079
        L_0x0099:
            r1 = r2
            goto L_0x0073
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AFKeystoreWrapper.mo6411():boolean");
    }

    /* renamed from: ॱ */
    private void m177(String str) {
        boolean z = true;
        AFLogger.afInfoLog("Creating a new key with alias: ".concat(String.valueOf(str)));
        try {
            Calendar instance = Calendar.getInstance();
            Calendar instance2 = Calendar.getInstance();
            instance2.add(1, 5);
            AlgorithmParameterSpec algorithmParameterSpec = null;
            synchronized (this.f135) {
                if (!this.f138.containsAlias(str)) {
                    if (VERSION.SDK_INT >= 23) {
                        algorithmParameterSpec = new Builder(str, 3).setCertificateSubject(new X500Principal("CN=AndroidSDK, O=AppsFlyer")).setCertificateSerialNumber(BigInteger.ONE).setCertificateNotBefore(instance.getTime()).setCertificateNotAfter(instance2.getTime()).build();
                    } else if (VERSION.SDK_INT >= 18) {
                        if (!"OPPO".equals(Build.BRAND)) {
                            z = false;
                        }
                        if (!z) {
                            algorithmParameterSpec = new KeyPairGeneratorSpec.Builder(this.f137).setAlias(str).setSubject(new X500Principal("CN=AndroidSDK, O=AppsFlyer")).setSerialNumber(BigInteger.ONE).setStartDate(instance.getTime()).setEndDate(instance2.getTime()).build();
                        }
                    }
                    KeyPairGenerator instance3 = KeyPairGenerator.getInstance("RSA", "AndroidKeyStore");
                    instance3.initialize(algorithmParameterSpec);
                    instance3.generateKeyPair();
                } else {
                    AFLogger.afInfoLog("Alias already exists: ".concat(String.valueOf(str)));
                }
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog(new StringBuilder("Exception ").append(th.getMessage()).append(" occurred").toString(), th);
        }
    }

    /* renamed from: ˏ */
    private String m176() {
        StringBuilder sb = new StringBuilder();
        sb.append("com.appsflyer,");
        synchronized (this.f135) {
            sb.append("KSAppsFlyerId=").append(this.f134).append(",");
            sb.append("KSAppsFlyerRICounter=").append(this.f136);
        }
        return sb.toString();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final String mo6410() {
        String str;
        synchronized (this.f135) {
            str = this.f134;
        }
        return str;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final int mo6414() {
        int i;
        synchronized (this.f135) {
            i = this.f136;
        }
        return i;
    }
}
