package com.google.android.gms.internal;

import android.os.ParcelFileDescriptor;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.nearby.connection.Payload;
import com.google.android.gms.nearby.connection.Payload.File;
import com.google.android.gms.nearby.connection.Payload.Stream;
import java.io.IOException;

public final class zzckt {
    static Pair<zzckr, Pair<ParcelFileDescriptor, ParcelFileDescriptor>> zza(Payload payload) throws IOException {
        Throwable e;
        switch (payload.getType()) {
            case 1:
                return Pair.create(new zzckr(payload.getId(), payload.getType(), payload.asBytes(), null, null, -1, null), null);
            case 2:
                return Pair.create(new zzckr(payload.getId(), payload.getType(), null, payload.asFile().asParcelFileDescriptor(), payload.asFile().asJavaFile() == null ? null : payload.asFile().asJavaFile().getAbsolutePath(), payload.asFile().getSize(), null), null);
            case 3:
                try {
                    ParcelFileDescriptor[] createPipe = ParcelFileDescriptor.createPipe();
                    ParcelFileDescriptor[] createPipe2 = ParcelFileDescriptor.createPipe();
                    return Pair.create(new zzckr(payload.getId(), payload.getType(), null, createPipe[0], null, -1, createPipe2[0]), Pair.create(createPipe[1], createPipe2[1]));
                } catch (Throwable e2) {
                    Log.e("NearbyConnections", String.format("Unable to create PFD pipe for streaming payload %d from client to service.", new Object[]{Long.valueOf(payload.getId())}), e2);
                    throw e2;
                }
            default:
                e2 = new IllegalArgumentException(String.format("Outgoing Payload %d has unknown type %d", new Object[]{Long.valueOf(payload.getId()), Integer.valueOf(payload.getType())}));
                Log.wtf("NearbyConnections", "Unknown payload type!", e2);
                throw e2;
        }
    }

    static Payload zza(zzckr zzckr) {
        long id = zzckr.getId();
        switch (zzckr.getType()) {
            case 1:
                return Payload.zza(zzckr.getBytes(), id);
            case 2:
                String zzbas = zzckr.zzbas();
                if (zzbas != null) {
                    try {
                        return Payload.zza(File.zza(new java.io.File(zzbas), zzckr.zzbat()), id);
                    } catch (Throwable e) {
                        Throwable th = e;
                        String valueOf = String.valueOf(zzbas);
                        Log.w("NearbyConnections", valueOf.length() != 0 ? "Failed to create Payload from ParcelablePayload: Java file not found at ".concat(valueOf) : new String("Failed to create Payload from ParcelablePayload: Java file not found at "), th);
                    }
                }
                return Payload.zza(File.zzb(zzckr.zzbar()), id);
            case 3:
                return Payload.zza(Stream.zzc(zzckr.zzbar()), id);
            default:
                Log.w("NearbyConnections", String.format("Incoming ParcelablePayload %d has unknown type %d", new Object[]{Long.valueOf(zzckr.getId()), Integer.valueOf(zzckr.getType())}));
                return null;
        }
    }
}
