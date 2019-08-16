package com.google.android.gms.internal.nearby;

import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.internal.ListenerHolder;
import com.google.android.gms.common.api.internal.ListenerHolder.ListenerKey;
import com.google.android.gms.common.api.internal.ListenerHolders;
import com.google.android.gms.common.api.internal.RegisterListenerMethod;
import com.google.android.gms.common.api.internal.UnregisterListenerMethod;
import com.google.android.gms.tasks.Task;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public final class zzk {
    private static zzk zzal;
    private final Map<String, Set<ListenerKey<?>>> zzam = new HashMap();
    /* access modifiers changed from: private */
    public final Set<ListenerKey<?>> zzan = new HashSet();
    private final Map<String, ListenerHolder<String>> zzao = new HashMap();

    private zzk() {
    }

    public static zzk zza() {
        zzk zzk;
        synchronized (zzk.class) {
            try {
                if (zzal == null) {
                    zzal = new zzk();
                }
                zzk = zzal;
            } finally {
                Class<zzk> cls = zzk.class;
            }
        }
        return zzk;
    }

    private final void zza(String str, ListenerKey<?> listenerKey) {
        Set set = (Set) this.zzam.get(str);
        if (set == null) {
            set = new HashSet();
            this.zzam.put(str, set);
        }
        set.add(listenerKey);
    }

    public final <T> ListenerHolder<T> zza(GoogleApi googleApi, T t, String str) {
        ListenerHolder<T> registerListener;
        synchronized (this) {
            registerListener = googleApi.registerListener(t, str);
            zza(str, registerListener.getListenerKey());
        }
        return registerListener;
    }

    public final ListenerHolder<String> zza(GoogleApi googleApi, String str, String str2) {
        ListenerHolder<String> registerListener;
        synchronized (this) {
            if (!this.zzao.containsKey(str) || !((ListenerHolder) this.zzao.get(str)).hasListener()) {
                registerListener = googleApi.registerListener(str, str2);
                zza(str2, registerListener.getListenerKey());
                this.zzao.put(str, registerListener);
            } else {
                registerListener = (ListenerHolder) this.zzao.get(str);
            }
        }
        return registerListener;
    }

    public final Task<Boolean> zza(GoogleApi googleApi, ListenerKey<?> listenerKey) {
        Task<Boolean> doUnregisterEventListener;
        synchronized (this) {
            this.zzan.remove(listenerKey);
            doUnregisterEventListener = googleApi.doUnregisterEventListener(listenerKey);
        }
        return doUnregisterEventListener;
    }

    public final Task<Void> zza(GoogleApi googleApi, RegisterListenerMethod registerListenerMethod, UnregisterListenerMethod unregisterListenerMethod) {
        Task<Void> addOnFailureListener;
        synchronized (this) {
            this.zzan.add(registerListenerMethod.getListenerKey());
            addOnFailureListener = googleApi.doRegisterEventListener(registerListenerMethod, unregisterListenerMethod).addOnFailureListener(new zzl(this, registerListenerMethod));
        }
        return addOnFailureListener;
    }

    public final void zza(GoogleApi googleApi, String str) {
        synchronized (this) {
            Set<ListenerKey> set = (Set) this.zzam.get(str);
            if (set != null) {
                for (ListenerKey listenerKey : set) {
                    if (this.zzan.contains(listenerKey)) {
                        zza(googleApi, listenerKey);
                    }
                }
                this.zzam.remove(str);
            }
        }
    }

    public final <T> ListenerKey<T> zzb(GoogleApi googleApi, T t, String str) {
        ListenerKey<T> createListenerKey;
        synchronized (this) {
            createListenerKey = t instanceof String ? zza(googleApi, (String) t, str).getListenerKey() : ListenerHolders.createListenerKey(t, str);
        }
        return createListenerKey;
    }
}
