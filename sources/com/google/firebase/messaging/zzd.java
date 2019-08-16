package com.google.firebase.messaging;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.annotation.Nullable;
import android.support.p000v4.media.session.PlaybackStateCompat;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.internal.firebase_messaging.zzj;
import com.google.android.gms.internal.firebase_messaging.zzk;
import com.google.android.gms.internal.firebase_messaging.zzn;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.Tasks;
import java.io.Closeable;
import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.Executor;

final class zzd implements Closeable {
    private final URL url;
    @Nullable
    private Task<Bitmap> zzea;
    @Nullable
    private volatile InputStream zzeb;

    private zzd(URL url2) {
        this.url = url2;
    }

    private static /* synthetic */ void zza(Throwable th, InputStream inputStream) {
        if (th != null) {
            try {
                inputStream.close();
            } catch (Throwable th2) {
                zzn.zza(th, th2);
            }
        } else {
            inputStream.close();
        }
    }

    @Nullable
    public static zzd zzo(String str) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        try {
            return new zzd(new URL(str));
        } catch (MalformedURLException e) {
            String valueOf = String.valueOf(str);
            Log.w("FirebaseMessaging", valueOf.length() != 0 ? "Not downloading image, bad URL: ".concat(valueOf) : new String("Not downloading image, bad URL: "));
            return null;
        }
    }

    public final void close() {
        zzk.zza(this.zzeb);
    }

    public final Task<Bitmap> getTask() {
        return (Task) Preconditions.checkNotNull(this.zzea);
    }

    public final void zza(Executor executor) {
        this.zzea = Tasks.call(executor, new zze(this));
    }

    public final Bitmap zzat() throws IOException {
        Throwable th;
        Throwable th2;
        Throwable th3;
        Throwable th4 = null;
        String valueOf = String.valueOf(this.url);
        Log.i("FirebaseMessaging", new StringBuilder(String.valueOf(valueOf).length() + 22).append("Starting download of: ").append(valueOf).toString());
        try {
            InputStream inputStream = this.url.openConnection().getInputStream();
            try {
                InputStream zza = zzj.zza(inputStream, PlaybackStateCompat.ACTION_SET_CAPTIONING_ENABLED);
                try {
                    this.zzeb = inputStream;
                    Bitmap decodeStream = BitmapFactory.decodeStream(zza);
                    if (decodeStream == null) {
                        String valueOf2 = String.valueOf(this.url);
                        String sb = new StringBuilder(String.valueOf(valueOf2).length() + 24).append("Failed to decode image: ").append(valueOf2).toString();
                        Log.w("FirebaseMessaging", sb);
                        throw new IOException(sb);
                    }
                    if (Log.isLoggable("FirebaseMessaging", 3)) {
                        String valueOf3 = String.valueOf(this.url);
                        Log.d("FirebaseMessaging", new StringBuilder(String.valueOf(valueOf3).length() + 31).append("Successfully downloaded image: ").append(valueOf3).toString());
                    }
                    zza(null, zza);
                    if (inputStream != null) {
                        zza(null, inputStream);
                    }
                    return decodeStream;
                } catch (Throwable th5) {
                    th3 = th5;
                    th2 = r0;
                }
                zza(th2, zza);
                throw th3;
                if (inputStream != null) {
                    zza(th4, inputStream);
                }
                throw th;
            } catch (Throwable th6) {
                th = th6;
                th4 = r0;
            }
        } catch (IOException e) {
            String valueOf4 = String.valueOf(this.url);
            Log.w("FirebaseMessaging", new StringBuilder(String.valueOf(valueOf4).length() + 26).append("Failed to download image: ").append(valueOf4).toString());
            throw e;
        }
    }
}
