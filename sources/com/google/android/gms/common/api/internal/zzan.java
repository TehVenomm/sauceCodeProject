package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import android.support.v4.app.FragmentActivity;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.concurrent.TimeUnit;

public class zzan extends GoogleApiClient {
    private final UnsupportedOperationException zzfla;

    public zzan(String str) {
        this.zzfla = new UnsupportedOperationException(str);
    }

    public ConnectionResult blockingConnect() {
        throw this.zzfla;
    }

    public ConnectionResult blockingConnect(long j, @NonNull TimeUnit timeUnit) {
        throw this.zzfla;
    }

    public PendingResult<Status> clearDefaultAccountAndReconnect() {
        throw this.zzfla;
    }

    public void connect() {
        throw this.zzfla;
    }

    public void disconnect() {
        throw this.zzfla;
    }

    public void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        throw this.zzfla;
    }

    @NonNull
    public ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        throw this.zzfla;
    }

    public boolean hasConnectedApi(@NonNull Api<?> api) {
        throw this.zzfla;
    }

    public boolean isConnected() {
        throw this.zzfla;
    }

    public boolean isConnecting() {
        throw this.zzfla;
    }

    public boolean isConnectionCallbacksRegistered(@NonNull ConnectionCallbacks connectionCallbacks) {
        throw this.zzfla;
    }

    public boolean isConnectionFailedListenerRegistered(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
        throw this.zzfla;
    }

    public void reconnect() {
        throw this.zzfla;
    }

    public void registerConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks) {
        throw this.zzfla;
    }

    public void registerConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
        throw this.zzfla;
    }

    public void stopAutoManage(@NonNull FragmentActivity fragmentActivity) {
        throw this.zzfla;
    }

    public void unregisterConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks) {
        throw this.zzfla;
    }

    public void unregisterConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
        throw this.zzfla;
    }
}
