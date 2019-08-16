package com.google.android.gms.common.api.internal;

import android.app.Activity;
import android.support.annotation.MainThread;
import android.support.annotation.VisibleForTesting;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.List;

public final class zaa extends ActivityLifecycleObserver {
    private final WeakReference<C1829zaa> zack;

    @VisibleForTesting(otherwise = 2)
    /* renamed from: com.google.android.gms.common.api.internal.zaa$zaa reason: collision with other inner class name */
    static class C1829zaa extends LifecycleCallback {
        private List<Runnable> zacl = new ArrayList();

        private C1829zaa(LifecycleFragment lifecycleFragment) {
            super(lifecycleFragment);
            this.mLifecycleFragment.addCallback("LifecycleObserverOnStop", this);
        }

        /* access modifiers changed from: private */
        public static C1829zaa zaa(Activity activity) {
            C1829zaa zaa;
            synchronized (activity) {
                LifecycleFragment fragment = getFragment(activity);
                zaa = (C1829zaa) fragment.getCallbackOrNull("LifecycleObserverOnStop", C1829zaa.class);
                if (zaa == null) {
                    zaa = new C1829zaa(fragment);
                }
            }
            return zaa;
        }

        /* access modifiers changed from: private */
        public final void zaa(Runnable runnable) {
            synchronized (this) {
                this.zacl.add(runnable);
            }
        }

        @MainThread
        public void onStop() {
            List<Runnable> list;
            synchronized (this) {
                list = this.zacl;
                this.zacl = new ArrayList();
            }
            for (Runnable run : list) {
                run.run();
            }
        }
    }

    public zaa(Activity activity) {
        this(C1829zaa.zaa(activity));
    }

    @VisibleForTesting(otherwise = 2)
    private zaa(C1829zaa zaa) {
        this.zack = new WeakReference<>(zaa);
    }

    public final ActivityLifecycleObserver onStopCallOnce(Runnable runnable) {
        C1829zaa zaa = (C1829zaa) this.zack.get();
        if (zaa == null) {
            throw new IllegalStateException("The target activity has already been GC'd");
        }
        zaa.zaa(runnable);
        return this;
    }
}
