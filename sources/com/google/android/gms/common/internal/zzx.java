package com.google.android.gms.common.internal;

import android.content.Intent;
import com.google.android.gms.common.api.internal.zzcg;

final class zzx extends zzu {
    private /* synthetic */ Intent val$intent;
    private /* synthetic */ int val$requestCode;
    private /* synthetic */ zzcg zzftr;

    zzx(Intent intent, zzcg zzcg, int i) {
        this.val$intent = intent;
        this.zzftr = zzcg;
        this.val$requestCode = i;
    }

    public final void zzaka() {
        if (this.val$intent != null) {
            this.zzftr.startActivityForResult(this.val$intent, this.val$requestCode);
        }
    }
}
