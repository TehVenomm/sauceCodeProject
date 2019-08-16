package com.zopim.android.sdk.data;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.util.Log;
import com.fasterxml.jackson.core.type.TypeReference;
import com.zopim.android.sdk.model.Connection;
import com.zopim.android.sdk.model.Connection.Status;

public class ConnectionPath extends Path<Connection> {
    /* access modifiers changed from: private */
    public static final ConnectionPath INSTANCE = new ConnectionPath();
    private static final String LOG_TAG = ConnectionPath.class.getSimpleName();
    /* access modifiers changed from: private */
    public Boolean mDeviceNoConnectivity;
    private final Object mLock = new Object();

    public static class ConnectivityReceiver extends BroadcastReceiver {
        private final String LOG_TAG = ConnectivityReceiver.class.getSimpleName();

        public void onReceive(Context context, Intent intent) {
            if (intent == null || !"android.net.conn.CONNECTIVITY_CHANGE".equals(intent.getAction())) {
                Log.w(this.LOG_TAG, "onReceive: intent was null or getAction() was mismatched");
                return;
            }
            if (intent.hasExtra("noConnectivity")) {
                ConnectionPath.INSTANCE.mDeviceNoConnectivity = Boolean.valueOf(intent.getBooleanExtra("noConnectivity", false));
            } else {
                Log.w(this.LOG_TAG, "Network change occurred, but no connectivity extras available");
                if (context.checkCallingOrSelfPermission("android.permission.ACCESS_NETWORK_STATE") == 0) {
                    Log.v(this.LOG_TAG, "Looking up active network info...");
                    NetworkInfo activeNetworkInfo = ((ConnectivityManager) context.getSystemService("connectivity")).getActiveNetworkInfo();
                    ConnectionPath access$000 = ConnectionPath.INSTANCE;
                    boolean z = activeNetworkInfo == null ? true : !activeNetworkInfo.isConnected();
                    access$000.mDeviceNoConnectivity = Boolean.valueOf(z);
                } else {
                    Log.v(this.LOG_TAG, "Unable to check device connection state. Assuming device is connected and leaving it to the web widget to verify connection.");
                    ConnectionPath.INSTANCE.mDeviceNoConnectivity = Boolean.valueOf(false);
                }
            }
            Log.v(this.LOG_TAG, "Device " + (ConnectionPath.INSTANCE.mDeviceNoConnectivity.booleanValue() ? "disconnected" : "connected"));
            ConnectionPath.INSTANCE.broadcast(ConnectionPath.INSTANCE.getData());
        }
    }

    private ConnectionPath() {
    }

    public static synchronized ConnectionPath getInstance() {
        ConnectionPath connectionPath;
        synchronized (ConnectionPath.class) {
            connectionPath = INSTANCE;
        }
        return connectionPath;
    }

    /* access modifiers changed from: 0000 */
    public void clear() {
        this.mData = null;
        this.mDeviceNoConnectivity = null;
    }

    public Connection getData() {
        if (this.mDeviceNoConnectivity == null || !this.mDeviceNoConnectivity.booleanValue()) {
            return this.mData == null ? new Connection(Status.UNKNOWN) : (Connection) this.mData;
        }
        Log.v(LOG_TAG, "Device has no connection. Will return widget's connection as NO_CONNECTION");
        return new Connection(Status.NO_CONNECTION);
    }

    /* access modifiers changed from: 0000 */
    public void update(String str) {
        if (str != null && !str.isEmpty()) {
            synchronized (this.mLock) {
                this.mData = this.PARSER.parse(str, (TypeReference<T>) new C1232a<T>(this));
            }
            broadcast(getData());
        }
    }
}
