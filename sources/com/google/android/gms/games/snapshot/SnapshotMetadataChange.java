package com.google.android.gms.games.snapshot;

import android.graphics.Bitmap;
import android.net.Uri;
import com.google.android.gms.common.data.BitmapTeleporter;

public interface SnapshotMetadataChange {
    public static final SnapshotMetadataChange EMPTY_CHANGE = new zze();

    public static final class Builder {
        private String zzdmz;
        private Long zzhoi;
        private Long zzhoj;
        private BitmapTeleporter zzhok;
        private Uri zzhol;

        public final SnapshotMetadataChange build() {
            return new zze(this.zzdmz, this.zzhoi, this.zzhok, this.zzhol, this.zzhoj);
        }

        public final Builder fromMetadata(SnapshotMetadata snapshotMetadata) {
            this.zzdmz = snapshotMetadata.getDescription();
            this.zzhoi = Long.valueOf(snapshotMetadata.getPlayedTime());
            this.zzhoj = Long.valueOf(snapshotMetadata.getProgressValue());
            if (this.zzhoi.longValue() == -1) {
                this.zzhoi = null;
            }
            this.zzhol = snapshotMetadata.getCoverImageUri();
            if (this.zzhol != null) {
                this.zzhok = null;
            }
            return this;
        }

        public final Builder setCoverImage(Bitmap bitmap) {
            this.zzhok = new BitmapTeleporter(bitmap);
            this.zzhol = null;
            return this;
        }

        public final Builder setDescription(String str) {
            this.zzdmz = str;
            return this;
        }

        public final Builder setPlayedTimeMillis(long j) {
            this.zzhoi = Long.valueOf(j);
            return this;
        }

        public final Builder setProgressValue(long j) {
            this.zzhoj = Long.valueOf(j);
            return this;
        }
    }

    Bitmap getCoverImage();

    String getDescription();

    Long getPlayedTimeMillis();

    Long getProgressValue();

    BitmapTeleporter zzary();
}
