package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.snapshot.SnapshotMetadataBuffer;
import com.google.android.gms.games.snapshot.Snapshots.LoadSnapshotsResult;

final class zzcm implements LoadSnapshotsResult {
    private /* synthetic */ Status zzeik;

    zzcm(zzcl zzcl, Status status) {
        this.zzeik = status;
    }

    public final SnapshotMetadataBuffer getSnapshots() {
        return new SnapshotMetadataBuffer(DataHolder.zzbx(14));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
