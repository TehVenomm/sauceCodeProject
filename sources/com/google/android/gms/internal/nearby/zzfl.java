package com.google.android.gms.internal.nearby;

import android.os.ParcelFileDescriptor;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.nearby.connection.Payload;
import com.google.android.gms.nearby.connection.Payload.File;
import com.google.android.gms.nearby.connection.Payload.Stream;
import java.io.FileNotFoundException;
import java.io.IOException;

public final class zzfl {
    static Pair<zzfh, Pair<ParcelFileDescriptor, ParcelFileDescriptor>> zza(Payload payload) throws IOException {
        switch (payload.getType()) {
            case 1:
                return Pair.create(new zzfj().zzb(payload.getId()).zzd(payload.getType()).zzb(payload.asBytes()).zzr(), null);
            case 2:
                return Pair.create(new zzfj().zzb(payload.getId()).zzd(payload.getType()).zzc(payload.asFile().asParcelFileDescriptor()).zze(payload.asFile().asJavaFile() == null ? null : payload.asFile().asJavaFile().getAbsolutePath()).zzc(payload.asFile().getSize()).zzr(), null);
            case 3:
                try {
                    ParcelFileDescriptor[] createPipe = ParcelFileDescriptor.createPipe();
                    ParcelFileDescriptor[] createPipe2 = ParcelFileDescriptor.createPipe();
                    return Pair.create(new zzfj().zzb(payload.getId()).zzd(payload.getType()).zzc(createPipe[0]).zzd(createPipe2[0]).zzr(), Pair.create(createPipe[1], createPipe2[1]));
                } catch (IOException e) {
                    Log.e("NearbyConnections", String.format("Unable to create PFD pipe for streaming payload %d from client to service.", new Object[]{Long.valueOf(payload.getId())}), e);
                    throw e;
                }
            default:
                IllegalArgumentException illegalArgumentException = new IllegalArgumentException(String.format("Outgoing Payload %d has unknown type %d", new Object[]{Long.valueOf(payload.getId()), Integer.valueOf(payload.getType())}));
                Log.wtf("NearbyConnections", "Unknown payload type!", illegalArgumentException);
                throw illegalArgumentException;
        }
    }

    static Payload zza(zzfh zzfh) {
        long id = zzfh.getId();
        switch (zzfh.getType()) {
            case 1:
                return Payload.zza(zzfh.getBytes(), id);
            case 2:
                String zzp = zzfh.zzp();
                if (zzp != null) {
                    try {
                        return Payload.zza(File.zza(new java.io.File(zzp), zzfh.zzq()), id);
                    } catch (FileNotFoundException e) {
                        FileNotFoundException fileNotFoundException = e;
                        String valueOf = String.valueOf(zzp);
                        Log.w("NearbyConnections", valueOf.length() != 0 ? "Failed to create Payload from ParcelablePayload: Java file not found at ".concat(valueOf) : new String("Failed to create Payload from ParcelablePayload: Java file not found at "), fileNotFoundException);
                    }
                }
                return Payload.zza(File.zza(zzfh.zzo()), id);
            case 3:
                return Payload.zza(Stream.zzb(zzfh.zzo()), id);
            default:
                Log.w("NearbyConnections", String.format("Incoming ParcelablePayload %d has unknown type %d", new Object[]{Long.valueOf(zzfh.getId()), Integer.valueOf(zzfh.getType())}));
                return null;
        }
    }
}
