package com.google.firebase.iid;

import com.google.firebase.components.ComponentContainer;
import com.google.firebase.components.ComponentFactory;

final /* synthetic */ class zzao implements ComponentFactory {
    static final ComponentFactory zzcr = new zzao();

    private zzao() {
    }

    public final Object create(ComponentContainer componentContainer) {
        return new zza((FirebaseInstanceId) componentContainer.get(FirebaseInstanceId.class));
    }
}
