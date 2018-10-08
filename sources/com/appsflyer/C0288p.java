package com.appsflyer;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import android.os.Build.VERSION;
import java.io.File;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.lang.ref.WeakReference;
import java.security.SecureRandom;
import java.util.UUID;

/* renamed from: com.appsflyer.p */
final class C0288p {
    /* renamed from: ॱ */
    private static String f294 = null;

    C0288p() {
    }

    /* renamed from: ˋ */
    public static synchronized String m335(WeakReference<Context> weakReference) {
        String str = null;
        synchronized (C0288p.class) {
            if (weakReference.get() == null) {
                str = f294;
            } else {
                if (f294 == null) {
                    if (weakReference.get() != null) {
                        str = ((Context) weakReference.get()).getSharedPreferences("appsflyer-data", 0).getString("AF_INSTALLATION", null);
                    }
                    if (str != null) {
                        f294 = str;
                    } else {
                        try {
                            File file = new File(((Context) weakReference.get()).getFilesDir(), "AF_INSTALLATION");
                            if (file.exists()) {
                                f294 = C0288p.m336(file);
                                file.delete();
                            } else {
                                if (VERSION.SDK_INT >= 9) {
                                    str = new StringBuilder().append(System.currentTimeMillis()).append("-").append(Math.abs(new SecureRandom().nextLong())).toString();
                                } else {
                                    str = UUID.randomUUID().toString();
                                }
                                f294 = str;
                            }
                            String str2 = f294;
                            Editor edit = ((Context) weakReference.get()).getSharedPreferences("appsflyer-data", 0).edit();
                            edit.putString("AF_INSTALLATION", str2);
                            if (VERSION.SDK_INT >= 9) {
                                edit.apply();
                            } else {
                                edit.commit();
                            }
                        } catch (Throwable e) {
                            AFLogger.afErrorLog("Error getting AF unique ID", e);
                        }
                    }
                    if (f294 != null) {
                        AppsFlyerProperties.getInstance().set("uid", f294);
                    }
                }
                str = f294;
            }
        }
        return str;
    }

    /* renamed from: ˏ */
    private static String m336(File file) {
        byte[] bArr;
        Throwable e;
        Throwable th;
        RandomAccessFile randomAccessFile;
        try {
            randomAccessFile = new RandomAccessFile(file, "r");
            try {
                bArr = new byte[((int) randomAccessFile.length())];
                try {
                    randomAccessFile.readFully(bArr);
                    randomAccessFile.close();
                    try {
                        randomAccessFile.close();
                    } catch (Throwable e2) {
                        AFLogger.afErrorLog("Exception while trying to close the InstallationFile", e2);
                    }
                } catch (IOException e3) {
                    e2 = e3;
                    try {
                        AFLogger.afErrorLog("Exception while reading InstallationFile: ", e2);
                        if (randomAccessFile != null) {
                            try {
                                randomAccessFile.close();
                            } catch (Throwable e22) {
                                AFLogger.afErrorLog("Exception while trying to close the InstallationFile", e22);
                            }
                        }
                        if (bArr == null) {
                            bArr = new byte[0];
                        }
                        return new String(bArr);
                    } catch (Throwable th2) {
                        th = th2;
                        if (randomAccessFile != null) {
                            try {
                                randomAccessFile.close();
                            } catch (Throwable e222) {
                                AFLogger.afErrorLog("Exception while trying to close the InstallationFile", e222);
                            }
                        }
                        throw th;
                    }
                }
            } catch (Throwable th3) {
                Throwable th4 = th3;
                bArr = null;
                e222 = th4;
                AFLogger.afErrorLog("Exception while reading InstallationFile: ", e222);
                if (randomAccessFile != null) {
                    randomAccessFile.close();
                }
                if (bArr == null) {
                    bArr = new byte[0];
                }
                return new String(bArr);
            }
        } catch (Throwable th32) {
            randomAccessFile = null;
            e222 = th32;
            bArr = null;
            AFLogger.afErrorLog("Exception while reading InstallationFile: ", e222);
            if (randomAccessFile != null) {
                randomAccessFile.close();
            }
            if (bArr == null) {
                bArr = new byte[0];
            }
            return new String(bArr);
        } catch (Throwable th5) {
            th32 = th5;
            randomAccessFile = null;
            if (randomAccessFile != null) {
                randomAccessFile.close();
            }
            throw th32;
        }
        if (bArr == null) {
            bArr = new byte[0];
        }
        return new String(bArr);
    }
}
