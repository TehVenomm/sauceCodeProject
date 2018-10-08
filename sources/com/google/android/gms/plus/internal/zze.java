package com.google.android.gms.plus.internal;

import android.app.PendingIntent;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.ICancelToken;
import com.google.android.gms.common.internal.zzi;
import com.google.android.gms.common.server.response.SafeParcelResponse;
import com.google.android.gms.plus.Moments.LoadMomentsResult;
import com.google.android.gms.plus.People.LoadPeopleResult;
import com.google.android.gms.plus.Plus;
import com.google.android.gms.plus.internal.model.moments.MomentEntity;
import com.google.android.gms.plus.internal.model.people.PersonEntity;
import com.google.android.gms.plus.model.moments.Moment;
import com.google.android.gms.plus.model.moments.MomentBuffer;
import com.google.android.gms.plus.model.people.Person;
import com.google.android.gms.plus.model.people.PersonBuffer;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Set;

public class zze extends zzi<zzd> {
    private Person zzayX;
    private final PlusSession zzayY;

    static final class zza implements LoadMomentsResult {
        private final Status zzKr;
        private final String zzayZ;
        private final String zzaza;
        private final MomentBuffer zzazb;

        public zza(Status status, DataHolder dataHolder, String str, String str2) {
            this.zzKr = status;
            this.zzayZ = str;
            this.zzaza = str2;
            this.zzazb = dataHolder != null ? new MomentBuffer(dataHolder) : null;
        }

        public MomentBuffer getMomentBuffer() {
            return this.zzazb;
        }

        public String getNextPageToken() {
            return this.zzayZ;
        }

        public Status getStatus() {
            return this.zzKr;
        }

        public String getUpdated() {
            return this.zzaza;
        }

        public void release() {
            if (this.zzazb != null) {
                this.zzazb.release();
            }
        }
    }

    static final class zzb implements LoadPeopleResult {
        private final Status zzKr;
        private final String zzayZ;
        private final PersonBuffer zzazc;

        public zzb(Status status, DataHolder dataHolder, String str) {
            this.zzKr = status;
            this.zzayZ = str;
            this.zzazc = dataHolder != null ? new PersonBuffer(dataHolder) : null;
        }

        public String getNextPageToken() {
            return this.zzayZ;
        }

        public PersonBuffer getPersonBuffer() {
            return this.zzazc;
        }

        public Status getStatus() {
            return this.zzKr;
        }

        public void release() {
            if (this.zzazc != null) {
                this.zzazc.release();
            }
        }
    }

    static final class zzc extends zza {
        private final com.google.android.gms.common.api.zza.zzb<Status> zzari;

        public zzc(com.google.android.gms.common.api.zza.zzb<Status> zzb) {
            this.zzari = zzb;
        }

        public void zzaI(Status status) {
            this.zzari.zzj(status);
        }
    }

    static final class zzd extends zza {
        private final com.google.android.gms.common.api.zza.zzb<LoadMomentsResult> zzari;

        public zzd(com.google.android.gms.common.api.zza.zzb<LoadMomentsResult> zzb) {
            this.zzari = zzb;
        }

        public void zza(DataHolder dataHolder, String str, String str2) {
            Status status = new Status(dataHolder.getStatusCode(), null, dataHolder.zzlm() != null ? (PendingIntent) dataHolder.zzlm().getParcelable("pendingIntent") : null);
            if (!(status.isSuccess() || dataHolder == null)) {
                if (!dataHolder.isClosed()) {
                    dataHolder.close();
                }
                dataHolder = null;
            }
            this.zzari.zzj(new zza(status, dataHolder, str, str2));
        }
    }

    static final class zze extends zza {
        private final com.google.android.gms.common.api.zza.zzb<LoadPeopleResult> zzari;

        public zze(com.google.android.gms.common.api.zza.zzb<LoadPeopleResult> zzb) {
            this.zzari = zzb;
        }

        public void zza(DataHolder dataHolder, String str) {
            Status status = new Status(dataHolder.getStatusCode(), null, dataHolder.zzlm() != null ? (PendingIntent) dataHolder.zzlm().getParcelable("pendingIntent") : null);
            if (!(status.isSuccess() || dataHolder == null)) {
                if (!dataHolder.isClosed()) {
                    dataHolder.close();
                }
                dataHolder = null;
            }
            this.zzari.zzj(new zzb(status, dataHolder, str));
        }
    }

    static final class zzf extends zza {
        private final com.google.android.gms.common.api.zza.zzb<Status> zzari;

        public zzf(com.google.android.gms.common.api.zza.zzb<Status> zzb) {
            this.zzari = zzb;
        }

        public void zzj(int i, Bundle bundle) {
            this.zzari.zzj(new Status(i, null, bundle != null ? (PendingIntent) bundle.getParcelable("pendingIntent") : null));
        }
    }

    public zze(Context context, Looper looper, zze zze, PlusSession plusSession, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 2, connectionCallbacks, onConnectionFailedListener, zze);
        this.zzayY = plusSession;
    }

    public static boolean zze(Set<Scope> set) {
        return (set == null || set.isEmpty()) ? false : (set.size() == 1 && set.contains(new Scope("plus_one_placeholder_scope"))) ? false : true;
    }

    private Bundle zzvz() {
        Bundle zzvK = this.zzayY.zzvK();
        zzvK.putStringArray("request_visible_actions", this.zzayY.zzvD());
        zzvK.putString("auth_package", this.zzayY.zzvF());
        return zzvK;
    }

    public String getAccountName() {
        zzlW();
        try {
            return ((zzd) zzlX()).getAccountName();
        } catch (Throwable e) {
            throw new IllegalStateException(e);
        }
    }

    protected /* synthetic */ IInterface zzD(IBinder iBinder) {
        return zzcy(iBinder);
    }

    public ICancelToken zza(com.google.android.gms.common.api.zza.zzb<LoadPeopleResult> zzb, int i, String str) {
        zzlW();
        Object zze = new zze(zzb);
        try {
            return ((zzd) zzlX()).zza(zze, 1, i, -1, str);
        } catch (RemoteException e) {
            zze.zza(DataHolder.zzaE(8), null);
            return null;
        }
    }

    protected void zza(int i, IBinder iBinder, Bundle bundle) {
        if (i == 0 && bundle != null && bundle.containsKey("loaded_person")) {
            this.zzayX = PersonEntity.zzl(bundle.getByteArray("loaded_person"));
        }
        super.zza(i, iBinder, bundle);
    }

    public void zza(com.google.android.gms.common.api.zza.zzb<LoadMomentsResult> zzb, int i, String str, Uri uri, String str2, String str3) {
        zzlW();
        Object zzd = zzb != null ? new zzd(zzb) : null;
        try {
            ((zzd) zzlX()).zza(zzd, i, str, uri, str2, str3);
        } catch (RemoteException e) {
            zzd.zza(DataHolder.zzaE(8), null, null);
        }
    }

    public void zza(com.google.android.gms.common.api.zza.zzb<Status> zzb, Moment moment) {
        zzlW();
        zzb zzc = zzb != null ? new zzc(zzb) : null;
        try {
            ((zzd) zzlX()).zza(zzc, SafeParcelResponse.zza((MomentEntity) moment));
        } catch (Throwable e) {
            if (zzc == null) {
                throw new IllegalStateException(e);
            }
            zzc.zzaI(new Status(8, null, null));
        }
    }

    public void zza(com.google.android.gms.common.api.zza.zzb<LoadPeopleResult> zzb, Collection<String> collection) {
        zzlW();
        zzb zze = new zze(zzb);
        try {
            ((zzd) zzlX()).zza(zze, new ArrayList(collection));
        } catch (RemoteException e) {
            zze.zza(DataHolder.zzaE(8), null);
        }
    }

    protected zzd zzcy(IBinder iBinder) {
        return com.google.android.gms.plus.internal.zzd.zza.zzcx(iBinder);
    }

    public void zzd(com.google.android.gms.common.api.zza.zzb<LoadPeopleResult> zzb, String[] strArr) {
        zza((com.google.android.gms.common.api.zza.zzb) zzb, Arrays.asList(strArr));
    }

    public void zzdp(String str) {
        zzlW();
        try {
            ((zzd) zzlX()).zzdp(str);
        } catch (Throwable e) {
            throw new IllegalStateException(e);
        }
    }

    protected String zzeq() {
        return "com.google.android.gms.plus.service.START";
    }

    protected String zzer() {
        return "com.google.android.gms.plus.internal.IPlusService";
    }

    public void zzi(com.google.android.gms.common.api.zza.zzb<LoadMomentsResult> zzb) {
        zza(zzb, 20, null, null, null, "me");
    }

    public void zzj(com.google.android.gms.common.api.zza.zzb<LoadPeopleResult> zzb) {
        zzlW();
        Object zze = new zze(zzb);
        try {
            ((zzd) zzlX()).zza(zze, 2, 1, -1, null);
        } catch (RemoteException e) {
            zze.zza(DataHolder.zzaE(8), null);
        }
    }

    public boolean zzjM() {
        return zze(zzlU().zzb(Plus.API));
    }

    public void zzk(com.google.android.gms.common.api.zza.zzb<Status> zzb) {
        zzlW();
        zzvv();
        Object zzf = new zzf(zzb);
        try {
            ((zzd) zzlX()).zzb(zzf);
        } catch (RemoteException e) {
            zzf.zzj(8, null);
        }
    }

    protected Bundle zzka() {
        return zzvz();
    }

    protected Bundle zzlY() {
        return zzvz();
    }

    public ICancelToken zzr(com.google.android.gms.common.api.zza.zzb<LoadPeopleResult> zzb, String str) {
        return zza((com.google.android.gms.common.api.zza.zzb) zzb, 0, str);
    }

    public void zzvv() {
        zzlW();
        try {
            this.zzayX = null;
            ((zzd) zzlX()).zzvv();
        } catch (Throwable e) {
            throw new IllegalStateException(e);
        }
    }

    public Person zzvy() {
        zzlW();
        return this.zzayX;
    }
}
