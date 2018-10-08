package com.google.android.gms.appstate;

import com.google.android.gms.common.internal.zzu;

public final class zza implements AppState {
    private final int zzKb;
    private final String zzKc;
    private final byte[] zzKd;
    private final boolean zzKe;
    private final String zzKf;
    private final byte[] zzKg;

    public zza(AppState appState) {
        this.zzKb = appState.getKey();
        this.zzKc = appState.getLocalVersion();
        this.zzKd = appState.getLocalData();
        this.zzKe = appState.hasConflict();
        this.zzKf = appState.getConflictVersion();
        this.zzKg = appState.getConflictData();
    }

    static int zza(AppState appState) {
        return zzu.hashCode(new Object[]{Integer.valueOf(appState.getKey()), appState.getLocalVersion(), appState.getLocalData(), Boolean.valueOf(appState.hasConflict()), appState.getConflictVersion(), appState.getConflictData()});
    }

    static boolean zza(AppState appState, Object obj) {
        if (!(obj instanceof AppState)) {
            return false;
        }
        if (appState != obj) {
            AppState appState2 = (AppState) obj;
            if (!(zzu.equal(Integer.valueOf(appState2.getKey()), Integer.valueOf(appState.getKey())) && zzu.equal(appState2.getLocalVersion(), appState.getLocalVersion()) && zzu.equal(appState2.getLocalData(), appState.getLocalData()) && zzu.equal(Boolean.valueOf(appState2.hasConflict()), Boolean.valueOf(appState.hasConflict())) && zzu.equal(appState2.getConflictVersion(), appState.getConflictVersion()) && zzu.equal(appState2.getConflictData(), appState.getConflictData()))) {
                return false;
            }
        }
        return true;
    }

    static String zzb(AppState appState) {
        return zzu.zzq(appState).zzg("Key", Integer.valueOf(appState.getKey())).zzg("LocalVersion", appState.getLocalVersion()).zzg("LocalData", appState.getLocalData()).zzg("HasConflict", Boolean.valueOf(appState.hasConflict())).zzg("ConflictVersion", appState.getConflictVersion()).zzg("ConflictData", appState.getConflictData()).toString();
    }

    public boolean equals(Object obj) {
        return zza(this, obj);
    }

    public /* synthetic */ Object freeze() {
        return zzjK();
    }

    public byte[] getConflictData() {
        return this.zzKg;
    }

    public String getConflictVersion() {
        return this.zzKf;
    }

    public int getKey() {
        return this.zzKb;
    }

    public byte[] getLocalData() {
        return this.zzKd;
    }

    public String getLocalVersion() {
        return this.zzKc;
    }

    public boolean hasConflict() {
        return this.zzKe;
    }

    public int hashCode() {
        return zza(this);
    }

    public boolean isDataValid() {
        return true;
    }

    public String toString() {
        return zzb(this);
    }

    public AppState zzjK() {
        return this;
    }
}
