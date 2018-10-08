package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.zzbp;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.Map;
import java.util.Map.Entry;
import net.gogame.gowrap.InternalConstants;

@WorkerThread
final class zzcbw implements Runnable {
    private final String mPackageName;
    private final URL zzbvm;
    private final byte[] zzgad;
    private final zzcbu zzipz;
    private final Map<String, String> zziqa;
    private /* synthetic */ zzcbs zziqb;

    public zzcbw(zzcbs zzcbs, String str, URL url, byte[] bArr, Map<String, String> map, zzcbu zzcbu) {
        this.zziqb = zzcbs;
        zzbp.zzgf(str);
        zzbp.zzu(url);
        zzbp.zzu(zzcbu);
        this.zzbvm = url;
        this.zzgad = bArr;
        this.zzipz = zzcbu;
        this.mPackageName = str;
        this.zziqa = map;
    }

    public final void run() {
        Map map;
        Throwable th;
        int i;
        OutputStream outputStream;
        Throwable th2;
        Map map2;
        int i2 = 0;
        this.zziqb.zzatv();
        HttpURLConnection httpURLConnection;
        OutputStream outputStream2;
        try {
            URLConnection openConnection = this.zzbvm.openConnection();
            if (openConnection instanceof HttpURLConnection) {
                httpURLConnection = (HttpURLConnection) openConnection;
                httpURLConnection.setDefaultUseCaches(false);
                zzcap.zzawe();
                httpURLConnection.setConnectTimeout(InternalConstants.FAB_BLINKING_TIME_INTERVAL);
                zzcap.zzawf();
                httpURLConnection.setReadTimeout(61000);
                httpURLConnection.setInstanceFollowRedirects(false);
                httpURLConnection.setDoInput(true);
                try {
                    if (this.zziqa != null) {
                        for (Entry entry : this.zziqa.entrySet()) {
                            httpURLConnection.addRequestProperty((String) entry.getKey(), (String) entry.getValue());
                        }
                    }
                    if (this.zzgad != null) {
                        byte[] zzo = this.zziqb.zzaug().zzo(this.zzgad);
                        this.zziqb.zzauk().zzayi().zzj("Uploading data. size", Integer.valueOf(zzo.length));
                        httpURLConnection.setDoOutput(true);
                        httpURLConnection.addRequestProperty(HttpRequest.HEADER_CONTENT_ENCODING, HttpRequest.ENCODING_GZIP);
                        httpURLConnection.setFixedLengthStreamingMode(zzo.length);
                        httpURLConnection.connect();
                        outputStream2 = httpURLConnection.getOutputStream();
                        try {
                            outputStream2.write(zzo);
                            outputStream2.close();
                        } catch (Throwable e) {
                            map = null;
                            th = e;
                            i = 0;
                            outputStream = outputStream2;
                            if (outputStream != null) {
                                try {
                                    outputStream.close();
                                } catch (IOException e2) {
                                    this.zziqb.zzauk().zzayc().zze("Error closing HTTP compressed POST connection output stream. appId", zzcbo.zzjf(this.mPackageName), e2);
                                }
                            }
                            if (httpURLConnection != null) {
                                httpURLConnection.disconnect();
                            }
                            this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i, th, null, map));
                        } catch (Throwable e3) {
                            th2 = e3;
                            map2 = null;
                            if (outputStream2 != null) {
                                try {
                                    outputStream2.close();
                                } catch (IOException e22) {
                                    this.zziqb.zzauk().zzayc().zze("Error closing HTTP compressed POST connection output stream. appId", zzcbo.zzjf(this.mPackageName), e22);
                                }
                            }
                            if (httpURLConnection != null) {
                                httpURLConnection.disconnect();
                            }
                            this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i2, null, null, map2));
                            throw th2;
                        }
                    }
                    i2 = httpURLConnection.getResponseCode();
                    map2 = httpURLConnection.getHeaderFields();
                } catch (Throwable e32) {
                    map = null;
                    th = e32;
                    i = i2;
                    outputStream = null;
                    if (outputStream != null) {
                        outputStream.close();
                    }
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i, th, null, map));
                } catch (Throwable e322) {
                    th2 = e322;
                    map2 = null;
                    outputStream2 = null;
                    if (outputStream2 != null) {
                        outputStream2.close();
                    }
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i2, null, null, map2));
                    throw th2;
                }
                try {
                    byte[] zza = zzcbs.zzc(httpURLConnection);
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i2, null, zza, map2));
                    return;
                } catch (Throwable e3222) {
                    map = map2;
                    th = e3222;
                    i = i2;
                    outputStream = null;
                    if (outputStream != null) {
                        outputStream.close();
                    }
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i, th, null, map));
                } catch (Throwable e32222) {
                    th2 = e32222;
                    outputStream2 = null;
                    if (outputStream2 != null) {
                        outputStream2.close();
                    }
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i2, null, null, map2));
                    throw th2;
                }
            }
            throw new IOException("Failed to obtain HTTP connection");
        } catch (Throwable e4) {
            map = null;
            th = e4;
            outputStream = null;
            i = 0;
            httpURLConnection = null;
            if (outputStream != null) {
                outputStream.close();
            }
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
            this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i, th, null, map));
        } catch (Throwable e42) {
            th2 = e42;
            map2 = null;
            outputStream2 = null;
            httpURLConnection = null;
            if (outputStream2 != null) {
                outputStream2.close();
            }
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
            this.zziqb.zzauj().zzg(new zzcbv(this.mPackageName, this.zzipz, i2, null, null, map2));
            throw th2;
        }
    }
}
