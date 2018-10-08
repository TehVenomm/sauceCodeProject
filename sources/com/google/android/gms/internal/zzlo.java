package com.google.android.gms.internal;

import android.net.Uri;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.plus.Moments;
import com.google.android.gms.plus.Moments.LoadMomentsResult;
import com.google.android.gms.plus.internal.zze;
import com.google.android.gms.plus.model.moments.Moment;
import com.google.android.gms.plus.model.moments.MomentBuffer;

public final class zzlo implements Moments {

    private static abstract class zza extends com.google.android.gms.plus.Plus.zza<LoadMomentsResult> {
        private zza(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public /* synthetic */ Result createFailedResult(Status status) {
            return zzaJ(status);
        }

        public LoadMomentsResult zzaJ(final Status status) {
            return new LoadMomentsResult(this) {
                final /* synthetic */ zza zzazw;

                public MomentBuffer getMomentBuffer() {
                    return null;
                }

                public String getNextPageToken() {
                    return null;
                }

                public Status getStatus() {
                    return status;
                }

                public String getUpdated() {
                    return null;
                }

                public void release() {
                }
            };
        }
    }

    private static abstract class zzc extends com.google.android.gms.plus.Plus.zza<Status> {
        private zzc(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public /* synthetic */ Result createFailedResult(Status status) {
            return zzb(status);
        }

        public Status zzb(Status status) {
            return status;
        }
    }

    private static abstract class zzb extends com.google.android.gms.plus.Plus.zza<Status> {
        private zzb(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public /* synthetic */ Result createFailedResult(Status status) {
            return zzb(status);
        }

        public Status zzb(Status status) {
            return status;
        }
    }

    public PendingResult<LoadMomentsResult> load(GoogleApiClient googleApiClient) {
        return googleApiClient.zza(new zza(this, googleApiClient) {
            final /* synthetic */ zzlo zzazp;

            protected void zza(zze zze) {
                zze.zzi(this);
            }
        });
    }

    public PendingResult<LoadMomentsResult> load(GoogleApiClient googleApiClient, int i, String str, Uri uri, String str2, String str3) {
        final int i2 = i;
        final String str4 = str;
        final Uri uri2 = uri;
        final String str5 = str2;
        final String str6 = str3;
        return googleApiClient.zza(new zza(this, googleApiClient) {
            final /* synthetic */ zzlo zzazp;

            protected void zza(zze zze) {
                zze.zza(this, i2, str4, uri2, str5, str6);
            }
        });
    }

    public PendingResult<Status> remove(GoogleApiClient googleApiClient, final String str) {
        return googleApiClient.zzb(new zzb(this, googleApiClient) {
            final /* synthetic */ zzlo zzazp;

            protected void zza(zze zze) {
                zze.zzdp(str);
                setResult(Status.zzQU);
            }
        });
    }

    public PendingResult<Status> write(GoogleApiClient googleApiClient, final Moment moment) {
        return googleApiClient.zzb(new zzc(this, googleApiClient) {
            final /* synthetic */ zzlo zzazp;

            protected void zza(zze zze) {
                zze.zza((com.google.android.gms.common.api.zza.zzb) this, moment);
            }
        });
    }
}
