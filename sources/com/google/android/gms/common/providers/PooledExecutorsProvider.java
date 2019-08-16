package com.google.android.gms.common.providers;

import com.google.android.gms.common.annotation.KeepForSdk;
import java.util.concurrent.ScheduledExecutorService;

@KeepForSdk
public class PooledExecutorsProvider {
    private static PooledExecutorFactory zzey;

    public interface PooledExecutorFactory {
        @KeepForSdk
        ScheduledExecutorService newSingleThreadScheduledExecutor();
    }

    private PooledExecutorsProvider() {
    }

    @KeepForSdk
    public static PooledExecutorFactory getInstance() {
        PooledExecutorFactory pooledExecutorFactory;
        synchronized (PooledExecutorsProvider.class) {
            try {
                if (zzey == null) {
                    zzey = new zza();
                }
                pooledExecutorFactory = zzey;
            } finally {
                Class<PooledExecutorsProvider> cls = PooledExecutorsProvider.class;
            }
        }
        return pooledExecutorFactory;
    }
}
