package com.appsflyer;

import android.content.Context;
import android.os.Build;
import android.os.Build.VERSION;
import android.security.KeyPairGeneratorSpec;
import android.security.keystore.KeyGenParameterSpec.Builder;
import java.io.IOException;
import java.math.BigInteger;
import java.security.KeyPairGenerator;
import java.security.KeyPairGeneratorSpi;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.cert.CertificateException;
import java.security.spec.AlgorithmParameterSpec;
import java.util.Calendar;
import java.util.Enumeration;
import javax.security.auth.x500.X500Principal;

class AFKeystoreWrapper {
    /* renamed from: ˊ */
    private String f115;
    /* renamed from: ˋ */
    private final Object f116 = new Object();
    /* renamed from: ˎ */
    private int f117;
    /* renamed from: ˏ */
    private Context f118;
    /* renamed from: ॱ */
    private KeyStore f119;

    public AFKeystoreWrapper(Context context) {
        Throwable e;
        this.f118 = context;
        this.f115 = "";
        this.f117 = 0;
        AFLogger.afInfoLog("Initialising KeyStore..");
        try {
            this.f119 = KeyStore.getInstance("AndroidKeyStore");
            this.f119.load(null);
            return;
        } catch (IOException e2) {
            e = e2;
        } catch (NoSuchAlgorithmException e3) {
            e = e3;
        } catch (CertificateException e4) {
            e = e4;
        } catch (KeyStoreException e5) {
            e = e5;
        }
        AFLogger.afErrorLog("Couldn't load keystore instance of type: AndroidKeyStore", e);
    }

    /* renamed from: ˏ */
    final void m186(String str) {
        this.f115 = str;
        this.f117 = 0;
        m182(m181());
    }

    /* renamed from: ˎ */
    final void m185() {
        String ˏ = m181();
        synchronized (this.f116) {
            this.f117++;
            AFLogger.afInfoLog("Deleting key with alias: ".concat(String.valueOf(ˏ)));
            try {
                synchronized (this.f116) {
                    this.f119.deleteEntry(ˏ);
                }
            } catch (Throwable e) {
                AFLogger.afErrorLog(new StringBuilder("Exception ").append(e.getMessage()).append(" occurred").toString(), e);
            }
        }
        m182(m181());
    }

    /* renamed from: ˋ */
    final boolean m184() {
        boolean z = true;
        synchronized (this.f116) {
            if (this.f119 != null) {
                try {
                    Enumeration aliases = this.f119.aliases();
                    while (aliases.hasMoreElements()) {
                        String str = (String) aliases.nextElement();
                        if (str != null && str.startsWith(BuildConfig.APPLICATION_ID)) {
                            String[] split = str.split(",");
                            if (split.length == 3) {
                                AFLogger.afInfoLog("Found a matching AF key with alias:\n".concat(String.valueOf(str)));
                                try {
                                    String[] split2 = split[1].trim().split("=");
                                    String[] split3 = split[2].trim().split("=");
                                    if (split2.length == 2 && split3.length == 2) {
                                        this.f115 = split2[1].trim();
                                        this.f117 = Integer.parseInt(split3[1].trim());
                                    }
                                } catch (Throwable th) {
                                    Throwable th2 = th;
                                    AFLogger.afErrorLog(new StringBuilder("Couldn't list KeyStore Aliases: ").append(th2.getClass().getName()).toString(), th2);
                                    return z;
                                }
                            }
                            z = false;
                        }
                    }
                    z = false;
                } catch (Throwable th3) {
                    th2 = th3;
                    z = false;
                    AFLogger.afErrorLog(new StringBuilder("Couldn't list KeyStore Aliases: ").append(th2.getClass().getName()).toString(), th2);
                    return z;
                }
            }
            z = false;
        }
        return z;
    }

    /* renamed from: ॱ */
    private void m182(String str) {
        Object obj = 1;
        AFLogger.afInfoLog("Creating a new key with alias: ".concat(String.valueOf(str)));
        try {
            Calendar instance = Calendar.getInstance();
            Calendar instance2 = Calendar.getInstance();
            instance2.add(1, 5);
            AlgorithmParameterSpec algorithmParameterSpec = null;
            synchronized (this.f116) {
                if (this.f119.containsAlias(str)) {
                    AFLogger.afInfoLog("Alias already exists: ".concat(String.valueOf(str)));
                } else {
                    if (VERSION.SDK_INT >= 23) {
                        algorithmParameterSpec = new Builder(str, 3).setCertificateSubject(new X500Principal("CN=AndroidSDK, O=AppsFlyer")).setCertificateSerialNumber(BigInteger.ONE).setCertificateNotBefore(instance.getTime()).setCertificateNotAfter(instance2.getTime()).build();
                    } else if (VERSION.SDK_INT >= 18) {
                        if (!"OPPO".equals(Build.BRAND)) {
                            obj = null;
                        }
                        if (obj == null) {
                            algorithmParameterSpec = new KeyPairGeneratorSpec.Builder(this.f118).setAlias(str).setSubject(new X500Principal("CN=AndroidSDK, O=AppsFlyer")).setSerialNumber(BigInteger.ONE).setStartDate(instance.getTime()).setEndDate(instance2.getTime()).build();
                        }
                    }
                    KeyPairGeneratorSpi instance3 = KeyPairGenerator.getInstance("RSA", "AndroidKeyStore");
                    instance3.initialize(algorithmParameterSpec);
                    instance3.generateKeyPair();
                }
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog(new StringBuilder("Exception ").append(th.getMessage()).append(" occurred").toString(), th);
        }
    }

    /* renamed from: ˏ */
    private String m181() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("com.appsflyer,");
        synchronized (this.f116) {
            stringBuilder.append("KSAppsFlyerId=").append(this.f115).append(",");
            stringBuilder.append("KSAppsFlyerRICounter=").append(this.f117);
        }
        return stringBuilder.toString();
    }

    /* renamed from: ˊ */
    final String m183() {
        String str;
        synchronized (this.f116) {
            str = this.f115;
        }
        return str;
    }

    /* renamed from: ॱ */
    final int m187() {
        int i;
        synchronized (this.f116) {
            i = this.f117;
        }
        return i;
    }
}
