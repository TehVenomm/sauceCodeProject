package com.google.android.gms.drive.events;

import android.os.IBinder;
import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable.Creator;
import android.os.RemoteException;
import android.text.TextUtils;
import com.google.android.gms.common.data.BitmapTeleporter;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.util.zzm;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.MetadataChangeSet;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.internal.zzbjv;
import com.google.android.gms.internal.zzbli;
import com.google.android.gms.internal.zzbnr;
import java.io.FileInputStream;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public final class CompletionEvent extends zza implements ResourceEvent {
    public static final Creator<CompletionEvent> CREATOR = new zzg();
    public static final int STATUS_CANCELED = 3;
    public static final int STATUS_CONFLICT = 2;
    public static final int STATUS_FAILURE = 1;
    public static final int STATUS_SUCCESS = 0;
    private int zzbyx;
    private String zzdxg;
    private DriveId zzgcx;
    private ParcelFileDescriptor zzgey;
    private ParcelFileDescriptor zzgez;
    private MetadataBundle zzgfa;
    private List<String> zzgfb;
    private IBinder zzgfc;
    private boolean zzgfd = false;
    private boolean zzgfe = false;
    private boolean zzgff = false;

    CompletionEvent(DriveId driveId, String str, ParcelFileDescriptor parcelFileDescriptor, ParcelFileDescriptor parcelFileDescriptor2, MetadataBundle metadataBundle, List<String> list, int i, IBinder iBinder) {
        this.zzgcx = driveId;
        this.zzdxg = str;
        this.zzgey = parcelFileDescriptor;
        this.zzgez = parcelFileDescriptor2;
        this.zzgfa = metadataBundle;
        this.zzgfb = list;
        this.zzbyx = i;
        this.zzgfc = iBinder;
    }

    private final void zzanc() {
        if (this.zzgff) {
            throw new IllegalStateException("Event has already been dismissed or snoozed.");
        }
    }

    private final void zzr(boolean z) {
        zzanc();
        this.zzgff = true;
        zzm.zza(this.zzgey);
        zzm.zza(this.zzgez);
        if (this.zzgfa != null && this.zzgfa.zzc(zzbnr.zzgly)) {
            ((BitmapTeleporter) this.zzgfa.zza(zzbnr.zzgly)).release();
        }
        if (this.zzgfc == null) {
            String valueOf = String.valueOf(z ? "snooze" : "dismiss");
            zzbjv.zzz("CompletionEvent", valueOf.length() != 0 ? "No callback on ".concat(valueOf) : new String("No callback on "));
            return;
        }
        try {
            zzbli.zzam(this.zzgfc).zzr(z);
        } catch (RemoteException e) {
            RemoteException remoteException = e;
            valueOf = z ? "snooze" : "dismiss";
            String valueOf2 = String.valueOf(remoteException);
            zzbjv.zzz("CompletionEvent", new StringBuilder((String.valueOf(valueOf).length() + 21) + String.valueOf(valueOf2).length()).append("RemoteException on ").append(valueOf).append(": ").append(valueOf2).toString());
        }
    }

    public final void dismiss() {
        zzr(false);
    }

    public final String getAccountName() {
        zzanc();
        return this.zzdxg;
    }

    public final InputStream getBaseContentsInputStream() {
        zzanc();
        if (this.zzgey == null) {
            return null;
        }
        if (this.zzgfd) {
            throw new IllegalStateException("getBaseInputStream() can only be called once per CompletionEvent instance.");
        }
        this.zzgfd = true;
        return new FileInputStream(this.zzgey.getFileDescriptor());
    }

    public final DriveId getDriveId() {
        zzanc();
        return this.zzgcx;
    }

    public final InputStream getModifiedContentsInputStream() {
        zzanc();
        if (this.zzgez == null) {
            return null;
        }
        if (this.zzgfe) {
            throw new IllegalStateException("getModifiedInputStream() can only be called once per CompletionEvent instance.");
        }
        this.zzgfe = true;
        return new FileInputStream(this.zzgez.getFileDescriptor());
    }

    public final MetadataChangeSet getModifiedMetadataChangeSet() {
        zzanc();
        return this.zzgfa != null ? new MetadataChangeSet(this.zzgfa) : null;
    }

    public final int getStatus() {
        zzanc();
        return this.zzbyx;
    }

    public final List<String> getTrackingTags() {
        zzanc();
        return new ArrayList(this.zzgfb);
    }

    public final int getType() {
        return 2;
    }

    public final void snooze() {
        zzr(true);
    }

    public final String toString() {
        String str;
        if (this.zzgfb == null) {
            str = "<null>";
        } else {
            str = TextUtils.join("','", this.zzgfb);
            str = new StringBuilder(String.valueOf(str).length() + 2).append("'").append(str).append("'").toString();
        }
        return String.format(Locale.US, "CompletionEvent [id=%s, status=%s, trackingTag=%s]", new Object[]{this.zzgcx, Integer.valueOf(this.zzbyx), str});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int i2 = i | 1;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgcx, i2, false);
        zzd.zza(parcel, 3, this.zzdxg, false);
        zzd.zza(parcel, 4, this.zzgey, i2, false);
        zzd.zza(parcel, 5, this.zzgez, i2, false);
        zzd.zza(parcel, 6, this.zzgfa, i2, false);
        zzd.zzb(parcel, 7, this.zzgfb, false);
        zzd.zzc(parcel, 8, this.zzbyx);
        zzd.zza(parcel, 9, this.zzgfc, false);
        zzd.zzai(parcel, zze);
    }
}
