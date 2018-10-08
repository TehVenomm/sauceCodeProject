package com.google.android.gms.dynamic;

import android.content.Context;
import android.os.IBinder;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.zzo;

public abstract class zzp<T> {
    private final String zzgow;
    private T zzgox;

    protected zzp(String str) {
        this.zzgow = str;
    }

    protected abstract T zzb(IBinder iBinder);

    protected final T zzcv(Context context) throws zzq {
        if (this.zzgox == null) {
            zzbp.zzu(context);
            Context remoteContext = zzo.getRemoteContext(context);
            if (remoteContext == null) {
                throw new zzq("Could not get remote context.");
            }
            try {
                this.zzgox = zzb((IBinder) remoteContext.getClassLoader().loadClass(this.zzgow).newInstance());
            } catch (Throwable e) {
                throw new zzq("Could not load creator class.", e);
            } catch (Throwable e2) {
                throw new zzq("Could not instantiate creator.", e2);
            } catch (Throwable e22) {
                throw new zzq("Could not access creator.", e22);
            }
        }
        return this.zzgox;
    }
}
