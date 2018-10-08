package com.google.android.gms.internal;

import android.os.SystemClock;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.EOFException;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Map.Entry;

public final class zzag implements zzb {
    private final Map<String, zzai> zzbv;
    private long zzbw;
    private final File zzbx;
    private final int zzby;

    public zzag(File file) {
        this(file, 5242880);
    }

    private zzag(File file, int i) {
        this.zzbv = new LinkedHashMap(16, 0.75f, true);
        this.zzbw = 0;
        this.zzbx = file;
        this.zzby = 5242880;
    }

    private final void remove(String str) {
        synchronized (this) {
            boolean delete = zze(str).delete();
            zzai zzai = (zzai) this.zzbv.get(str);
            if (zzai != null) {
                this.zzbw -= zzai.size;
                this.zzbv.remove(str);
            }
            if (!delete) {
                zzab.zzb("Could not delete cache entry for key=%s, filename=%s", str, zzd(str));
            }
        }
    }

    private static int zza(InputStream inputStream) throws IOException {
        int read = inputStream.read();
        if (read != -1) {
            return read;
        }
        throw new EOFException();
    }

    static void zza(OutputStream outputStream, int i) throws IOException {
        outputStream.write(i & 255);
        outputStream.write((i >> 8) & 255);
        outputStream.write((i >> 16) & 255);
        outputStream.write(i >>> 24);
    }

    static void zza(OutputStream outputStream, long j) throws IOException {
        outputStream.write((byte) ((int) j));
        outputStream.write((byte) ((int) (j >>> 8)));
        outputStream.write((byte) ((int) (j >>> 16)));
        outputStream.write((byte) ((int) (j >>> 24)));
        outputStream.write((byte) ((int) (j >>> 32)));
        outputStream.write((byte) ((int) (j >>> 40)));
        outputStream.write((byte) ((int) (j >>> 48)));
        outputStream.write((byte) ((int) (j >>> 56)));
    }

    static void zza(OutputStream outputStream, String str) throws IOException {
        byte[] bytes = str.getBytes("UTF-8");
        zza(outputStream, (long) bytes.length);
        outputStream.write(bytes, 0, bytes.length);
    }

    private final void zza(String str, zzai zzai) {
        if (this.zzbv.containsKey(str)) {
            zzai zzai2 = (zzai) this.zzbv.get(str);
            this.zzbw = (zzai.size - zzai2.size) + this.zzbw;
        } else {
            this.zzbw += zzai.size;
        }
        this.zzbv.put(str, zzai);
    }

    private static byte[] zza(InputStream inputStream, int i) throws IOException {
        byte[] bArr = new byte[i];
        int i2 = 0;
        while (i2 < i) {
            int read = inputStream.read(bArr, i2, i - i2);
            if (read == -1) {
                break;
            }
            i2 += read;
        }
        if (i2 == i) {
            return bArr;
        }
        throw new IOException("Expected " + i + " bytes, read " + i2 + " bytes");
    }

    static int zzb(InputStream inputStream) throws IOException {
        return (((zza(inputStream) | 0) | (zza(inputStream) << 8)) | (zza(inputStream) << 16)) | (zza(inputStream) << 24);
    }

    static long zzc(InputStream inputStream) throws IOException {
        return (((((((0 | (((long) zza(inputStream)) & 255)) | ((((long) zza(inputStream)) & 255) << 8)) | ((((long) zza(inputStream)) & 255) << 16)) | ((((long) zza(inputStream)) & 255) << 24)) | ((((long) zza(inputStream)) & 255) << 32)) | ((((long) zza(inputStream)) & 255) << 40)) | ((((long) zza(inputStream)) & 255) << 48)) | ((((long) zza(inputStream)) & 255) << 56);
    }

    static String zzd(InputStream inputStream) throws IOException {
        return new String(zza(inputStream, (int) zzc(inputStream)), "UTF-8");
    }

    private static String zzd(String str) {
        int length = str.length() / 2;
        String valueOf = String.valueOf(String.valueOf(str.substring(0, length).hashCode()));
        String valueOf2 = String.valueOf(String.valueOf(str.substring(length).hashCode()));
        return valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
    }

    private final File zze(String str) {
        return new File(this.zzbx, zzd(str));
    }

    static Map<String, String> zze(InputStream inputStream) throws IOException {
        int zzb = zzb(inputStream);
        Map<String, String> emptyMap = zzb == 0 ? Collections.emptyMap() : new HashMap(zzb);
        for (int i = 0; i < zzb; i++) {
            emptyMap.put(zzd(inputStream).intern(), zzd(inputStream).intern());
        }
        return emptyMap;
    }

    public final void initialize() {
        Throwable th;
        BufferedInputStream bufferedInputStream = null;
        synchronized (this) {
            if (this.zzbx.exists()) {
                File[] listFiles = this.zzbx.listFiles();
                if (listFiles != null) {
                    for (File file : listFiles) {
                        BufferedInputStream bufferedInputStream2;
                        try {
                            bufferedInputStream2 = new BufferedInputStream(new FileInputStream(file));
                            try {
                                zzai zzf = zzai.zzf(bufferedInputStream2);
                                zzf.size = file.length();
                                zza(zzf.key, zzf);
                                try {
                                    bufferedInputStream2.close();
                                } catch (IOException e) {
                                }
                            } catch (IOException e2) {
                                if (file != null) {
                                    try {
                                        file.delete();
                                    } catch (Throwable th2) {
                                        th = th2;
                                        bufferedInputStream = bufferedInputStream2;
                                    }
                                }
                                if (bufferedInputStream2 != null) {
                                    try {
                                        bufferedInputStream2.close();
                                    } catch (IOException e3) {
                                    }
                                }
                            }
                        } catch (IOException e4) {
                            bufferedInputStream2 = null;
                            if (file != null) {
                                file.delete();
                            }
                            if (bufferedInputStream2 != null) {
                                bufferedInputStream2.close();
                            }
                        } catch (Throwable th3) {
                            th = th3;
                        }
                    }
                }
            } else if (!this.zzbx.mkdirs()) {
                zzab.zzc("Unable to create cache dir %s", this.zzbx.getAbsolutePath());
            }
        }
        return;
        if (bufferedInputStream != null) {
            try {
                bufferedInputStream.close();
            } catch (IOException e5) {
            }
        }
        throw th;
        throw th;
    }

    public final zzc zza(String str) {
        zzc zzc;
        zzaj zzaj;
        IOException e;
        Throwable th;
        NegativeArraySizeException e2;
        zzaj zzaj2;
        synchronized (this) {
            zzai zzai = (zzai) this.zzbv.get(str);
            if (zzai == null) {
                zzc = null;
            } else {
                File zze = zze(str);
                try {
                    zzaj = new zzaj(new BufferedInputStream(new FileInputStream(zze)));
                    try {
                        zzai.zzf(zzaj);
                        byte[] zza = zza((InputStream) zzaj, (int) (zze.length() - ((long) zzaj.zzbz)));
                        zzc zzc2 = new zzc();
                        zzc2.data = zza;
                        zzc2.zza = zzai.zza;
                        zzc2.zzb = zzai.zzb;
                        zzc2.zzc = zzai.zzc;
                        zzc2.zzd = zzai.zzd;
                        zzc2.zze = zzai.zze;
                        zzc2.zzf = zzai.zzf;
                        try {
                            zzaj.close();
                            zzc = zzc2;
                        } catch (IOException e3) {
                            zzc = null;
                        }
                    } catch (IOException e4) {
                        e = e4;
                        try {
                            zzab.zzb("%s: %s", zze.getAbsolutePath(), e.toString());
                            remove(str);
                            if (zzaj != null) {
                                try {
                                    zzaj.close();
                                } catch (IOException e5) {
                                    zzc = null;
                                }
                            }
                            zzc = null;
                            return zzc;
                        } catch (Throwable th2) {
                            th = th2;
                            if (zzaj != null) {
                                try {
                                    zzaj.close();
                                } catch (IOException e6) {
                                    zzc = null;
                                }
                            }
                            throw th;
                        }
                    } catch (NegativeArraySizeException e7) {
                        e2 = e7;
                        zzaj2 = zzaj;
                        try {
                            zzab.zzb("%s: %s", zze.getAbsolutePath(), e2.toString());
                            remove(str);
                            if (zzaj2 != null) {
                                try {
                                    zzaj2.close();
                                } catch (IOException e8) {
                                    zzc = null;
                                }
                            }
                            zzc = null;
                            return zzc;
                        } catch (Throwable th3) {
                            th = th3;
                            zzaj = zzaj2;
                            if (zzaj != null) {
                                zzaj.close();
                            }
                            throw th;
                        }
                    } catch (Throwable th4) {
                        th = th4;
                        if (zzaj != null) {
                            zzaj.close();
                        }
                        throw th;
                    }
                } catch (IOException e9) {
                    e = e9;
                    zzaj = null;
                    zzab.zzb("%s: %s", zze.getAbsolutePath(), e.toString());
                    remove(str);
                    if (zzaj != null) {
                        zzaj.close();
                    }
                    zzc = null;
                    return zzc;
                } catch (NegativeArraySizeException e10) {
                    e2 = e10;
                    zzaj2 = null;
                    zzab.zzb("%s: %s", zze.getAbsolutePath(), e2.toString());
                    remove(str);
                    if (zzaj2 != null) {
                        zzaj2.close();
                    }
                    zzc = null;
                    return zzc;
                } catch (Throwable th5) {
                    th = th5;
                    zzaj = null;
                    if (zzaj != null) {
                        zzaj.close();
                    }
                    throw th;
                }
            }
        }
        return zzc;
    }

    public final void zza(String str, zzc zzc) {
        int i = 0;
        synchronized (this) {
            int length = zzc.data.length;
            if (this.zzbw + ((long) length) >= ((long) this.zzby)) {
                int i2;
                if (zzab.DEBUG) {
                    zzab.zza("Pruning old cache entries.", new Object[0]);
                }
                long j = this.zzbw;
                long elapsedRealtime = SystemClock.elapsedRealtime();
                Iterator it = this.zzbv.entrySet().iterator();
                while (it.hasNext()) {
                    zzai zzai = (zzai) ((Entry) it.next()).getValue();
                    if (zze(zzai.key).delete()) {
                        this.zzbw -= zzai.size;
                    } else {
                        zzab.zzb("Could not delete cache entry for key=%s, filename=%s", zzai.key, zzd(zzai.key));
                    }
                    it.remove();
                    i2 = i + 1;
                    if (((float) (this.zzbw + ((long) length))) < ((float) this.zzby) * 0.9f) {
                        break;
                    }
                    i = i2;
                }
                i2 = i;
                if (zzab.DEBUG) {
                    zzab.zza("pruned %d files, %d bytes, %d ms", Integer.valueOf(i2), Long.valueOf(this.zzbw - j), Long.valueOf(SystemClock.elapsedRealtime() - elapsedRealtime));
                }
            }
            File zze = zze(str);
            try {
                OutputStream bufferedOutputStream = new BufferedOutputStream(new FileOutputStream(zze));
                zzai zzai2 = new zzai(str, zzc);
                if (zzai2.zza(bufferedOutputStream)) {
                    bufferedOutputStream.write(zzc.data);
                    bufferedOutputStream.close();
                    zza(str, zzai2);
                } else {
                    bufferedOutputStream.close();
                    zzab.zzb("Failed to write header for %s", zze.getAbsolutePath());
                    throw new IOException();
                }
            } catch (IOException e) {
                if (!zze.delete()) {
                    zzab.zzb("Could not clean up file %s", zze.getAbsolutePath());
                }
            }
        }
    }
}
