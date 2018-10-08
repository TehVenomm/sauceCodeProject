package com.google.android.gms.internal;

public abstract class zzedg<MessageType extends zzedf<MessageType, BuilderType>, BuilderType extends zzedg<MessageType, BuilderType>> implements zzeez {
    public /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcbj();
    }

    protected abstract BuilderType zza(MessageType messageType);

    public final /* synthetic */ zzeez zzc(zzeey zzeey) {
        if (zzcco().getClass().isInstance(zzeey)) {
            return zza((zzedf) zzeey);
        }
        throw new IllegalArgumentException("mergeFrom(MessageLite) can only merge messages of the same type.");
    }

    public abstract BuilderType zzcbj();
}
