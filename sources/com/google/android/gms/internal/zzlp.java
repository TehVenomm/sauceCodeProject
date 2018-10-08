package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.zza.zzb;
import com.google.android.gms.plus.People;
import com.google.android.gms.plus.People.LoadPeopleResult;
import com.google.android.gms.plus.Plus;
import com.google.android.gms.plus.internal.zze;
import com.google.android.gms.plus.model.people.Person;
import com.google.android.gms.plus.model.people.PersonBuffer;
import java.util.Collection;

public final class zzlp implements People {

    private static abstract class zza extends com.google.android.gms.plus.Plus.zza<LoadPeopleResult> {
        private zza(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public /* synthetic */ Result createFailedResult(Status status) {
            return zzaK(status);
        }

        public LoadPeopleResult zzaK(final Status status) {
            return new LoadPeopleResult(this) {
                final /* synthetic */ zza zzazB;

                public String getNextPageToken() {
                    return null;
                }

                public PersonBuffer getPersonBuffer() {
                    return null;
                }

                public Status getStatus() {
                    return status;
                }

                public void release() {
                }
            };
        }
    }

    public Person getCurrentPerson(GoogleApiClient googleApiClient) {
        return Plus.zzf(googleApiClient, true).zzvy();
    }

    public PendingResult<LoadPeopleResult> load(GoogleApiClient googleApiClient, final Collection<String> collection) {
        return googleApiClient.zza(new zza(this, googleApiClient) {
            final /* synthetic */ zzlp zzazy;

            protected void zza(zze zze) {
                zze.zza((zzb) this, collection);
            }
        });
    }

    public PendingResult<LoadPeopleResult> load(GoogleApiClient googleApiClient, final String... strArr) {
        return googleApiClient.zza(new zza(this, googleApiClient) {
            final /* synthetic */ zzlp zzazy;

            protected void zza(zze zze) {
                zze.zzd(this, strArr);
            }
        });
    }

    public PendingResult<LoadPeopleResult> loadConnected(GoogleApiClient googleApiClient) {
        return googleApiClient.zza(new zza(this, googleApiClient) {
            final /* synthetic */ zzlp zzazy;

            protected void zza(zze zze) {
                zze.zzj(this);
            }
        });
    }

    public PendingResult<LoadPeopleResult> loadVisible(GoogleApiClient googleApiClient, final int i, final String str) {
        return googleApiClient.zza(new zza(this, googleApiClient) {
            final /* synthetic */ zzlp zzazy;

            protected void zza(zze zze) {
                setCancelToken(zze.zza((zzb) this, i, str));
            }
        });
    }

    public PendingResult<LoadPeopleResult> loadVisible(GoogleApiClient googleApiClient, final String str) {
        return googleApiClient.zza(new zza(this, googleApiClient) {
            final /* synthetic */ zzlp zzazy;

            protected void zza(zze zze) {
                setCancelToken(zze.zzr(this, str));
            }
        });
    }
}
