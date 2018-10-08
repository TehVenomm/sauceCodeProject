package com.google.android.gms.internal;

import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Pair;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.MetadataBuffer;
import com.google.android.gms.drive.events.ChangeEvent;
import com.google.android.gms.drive.events.ChangeListener;
import com.google.android.gms.drive.events.CompletionEvent;
import com.google.android.gms.drive.events.CompletionListener;
import com.google.android.gms.drive.events.DriveEvent;
import com.google.android.gms.drive.events.zzb;
import com.google.android.gms.drive.events.zzd;
import com.google.android.gms.drive.events.zzi;
import com.google.android.gms.drive.events.zzl;
import com.google.android.gms.drive.events.zzn;

final class zzbkt extends Handler {
    private final Context mContext;

    private zzbkt(Looper looper, Context context) {
        super(looper);
        this.mContext = context;
    }

    public final void handleMessage(Message message) {
        switch (message.what) {
            case 1:
                Pair pair = (Pair) message.obj;
                zzi zzi = (zzi) pair.first;
                DriveEvent driveEvent = (DriveEvent) pair.second;
                switch (driveEvent.getType()) {
                    case 1:
                        ((ChangeListener) zzi).onChange((ChangeEvent) driveEvent);
                        return;
                    case 2:
                        ((CompletionListener) zzi).onCompletion((CompletionEvent) driveEvent);
                        return;
                    case 3:
                        zzl zzl = (zzl) driveEvent;
                        DataHolder zzanf = zzl.zzanf();
                        if (zzanf != null) {
                            zzbku zzbku = new zzbku(new MetadataBuffer(zzanf));
                        }
                        if (zzl.zzang()) {
                            zzl.zzanh();
                            return;
                        }
                        return;
                    case 4:
                        ((zzd) zzi).zza((zzb) driveEvent);
                        return;
                    case 8:
                        zzbhc zzbhc = new zzbhc(((zzn) driveEvent).zzani());
                        return;
                    default:
                        String valueOf = String.valueOf(driveEvent);
                        zzbjv.zzy("EventCallback", new StringBuilder(String.valueOf(valueOf).length() + 18).append("Unexpected event: ").append(valueOf).toString());
                        return;
                }
            default:
                zzbjv.zzm(this.mContext, "EventCallback", "Don't know how to handle this event");
                return;
        }
    }
}
