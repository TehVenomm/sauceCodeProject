package com.google.android.gms.internal;

import android.os.ParcelFileDescriptor;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzm;
import com.google.android.gms.drive.DriveApi.DriveContentsResult;
import com.google.android.gms.drive.DriveContents;
import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.ExecutionOptions;
import com.google.android.gms.drive.ExecutionOptions.Builder;
import com.google.android.gms.drive.MetadataChangeSet;
import com.google.android.gms.drive.zzc;
import com.google.android.gms.drive.zzp;
import com.google.android.gms.drive.zzr;
import java.io.InputStream;
import java.io.OutputStream;

public final class zzbjc implements DriveContents {
    private boolean mClosed = false;
    private final zzc zzghj;
    private boolean zzghk = false;
    private boolean zzghl = false;

    public zzbjc(zzc zzc) {
        this.zzghj = (zzc) zzbp.zzu(zzc);
    }

    private final PendingResult<Status> zza(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet, zzp zzp) {
        ExecutionOptions executionOptions = zzp == null ? (zzp) new zzr().build() : zzp;
        if (this.zzghj.getMode() == DriveFile.MODE_READ_ONLY) {
            throw new IllegalStateException("Cannot commit contents opened with MODE_READ_ONLY");
        }
        Object obj;
        switch (executionOptions.zzamv()) {
            case 1:
                obj = 1;
                break;
            default:
                obj = null;
                break;
        }
        if (obj == null || this.zzghj.zzamp()) {
            executionOptions.zze(googleApiClient);
            if (this.mClosed) {
                throw new IllegalStateException("DriveContents already closed.");
            } else if (getDriveId() == null) {
                throw new IllegalStateException("Only DriveContents obtained through DriveFile.open can be committed.");
            } else {
                if (metadataChangeSet == null) {
                    metadataChangeSet = MetadataChangeSet.zzgeb;
                }
                zzamr();
                return googleApiClient.zze(new zzbje(this, googleApiClient, metadataChangeSet, executionOptions));
            }
        }
        throw new IllegalStateException("DriveContents must be valid for conflict detection.");
    }

    public final PendingResult<Status> commit(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet) {
        return zza(googleApiClient, metadataChangeSet, null);
    }

    public final PendingResult<Status> commit(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet, ExecutionOptions executionOptions) {
        zzp zzp;
        if (executionOptions == null) {
            zzp = null;
        } else {
            Builder zzr = new zzr();
            if (executionOptions != null) {
                zzr.setConflictStrategy(executionOptions.zzamv());
                zzr.setNotifyOnCompletion(executionOptions.zzamu());
                String zzamt = executionOptions.zzamt();
                if (zzamt != null) {
                    zzr.setTrackingTag(zzamt);
                }
            }
            zzp = (zzp) zzr.build();
        }
        return zza(googleApiClient, metadataChangeSet, zzp);
    }

    public final void discard(GoogleApiClient googleApiClient) {
        if (this.mClosed) {
            throw new IllegalStateException("DriveContents already closed.");
        }
        zzamr();
        ((zzbjg) googleApiClient.zze(new zzbjg(this, googleApiClient))).setResultCallback(new zzbjf(this));
    }

    public final DriveId getDriveId() {
        return this.zzghj.getDriveId();
    }

    public final InputStream getInputStream() {
        if (this.mClosed) {
            throw new IllegalStateException("Contents have been closed, cannot access the input stream.");
        } else if (this.zzghj.getMode() != DriveFile.MODE_READ_ONLY) {
            throw new IllegalStateException("getInputStream() can only be used with contents opened with MODE_READ_ONLY.");
        } else if (this.zzghk) {
            throw new IllegalStateException("getInputStream() can only be called once per Contents instance.");
        } else {
            this.zzghk = true;
            return this.zzghj.getInputStream();
        }
    }

    public final int getMode() {
        return this.zzghj.getMode();
    }

    public final OutputStream getOutputStream() {
        if (this.mClosed) {
            throw new IllegalStateException("Contents have been closed, cannot access the output stream.");
        } else if (this.zzghj.getMode() != DriveFile.MODE_WRITE_ONLY) {
            throw new IllegalStateException("getOutputStream() can only be used with contents opened with MODE_WRITE_ONLY.");
        } else if (this.zzghl) {
            throw new IllegalStateException("getOutputStream() can only be called once per Contents instance.");
        } else {
            this.zzghl = true;
            return this.zzghj.getOutputStream();
        }
    }

    public final ParcelFileDescriptor getParcelFileDescriptor() {
        if (!this.mClosed) {
            return this.zzghj.getParcelFileDescriptor();
        }
        throw new IllegalStateException("Contents have been closed, cannot access the output stream.");
    }

    public final PendingResult<DriveContentsResult> reopenForWrite(GoogleApiClient googleApiClient) {
        if (this.mClosed) {
            throw new IllegalStateException("DriveContents already closed.");
        } else if (this.zzghj.getMode() != DriveFile.MODE_READ_ONLY) {
            throw new IllegalStateException("reopenForWrite can only be used with DriveContents opened with MODE_READ_ONLY.");
        } else {
            zzamr();
            return googleApiClient.zzd(new zzbjd(this, googleApiClient));
        }
    }

    public final zzc zzamq() {
        return this.zzghj;
    }

    public final void zzamr() {
        zzm.zza(this.zzghj.getParcelFileDescriptor());
        this.mClosed = true;
    }

    public final boolean zzams() {
        return this.mClosed;
    }
}
