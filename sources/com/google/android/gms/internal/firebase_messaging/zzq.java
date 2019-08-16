package com.google.android.gms.internal.firebase_messaging;

final class zzq extends zzm {
    private final zzp zzp = new zzp();

    zzq() {
    }

    public final void zza(Throwable th, Throwable th2) {
        if (th2 == th) {
            throw new IllegalArgumentException("Self suppression is not allowed.", th2);
        } else if (th2 == null) {
            throw new NullPointerException("The suppressed exception cannot be null.");
        } else {
            this.zzp.zza(th, true).add(th2);
        }
    }
}
