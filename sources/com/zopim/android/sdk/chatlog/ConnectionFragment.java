package com.zopim.android.sdk.chatlog;

import android.app.Activity;
import android.os.Handler;
import android.os.Looper;
import android.support.p000v4.app.Fragment;
import android.util.Log;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

public class ConnectionFragment extends Fragment {
    private static final String LOG_TAG = ConnectionFragment.class.getSimpleName();
    ConnectionObserver mConnectionObserver = new C1223u(this);
    /* access modifiers changed from: private */
    public Handler mHandler = new Handler(Looper.getMainLooper());
    private ConnectionListener mListener;

    public interface ConnectionListener {
        void onConnected();

        void onDisconnected();
    }

    /* access modifiers changed from: private */
    public void updateConnection(Connection connection) {
        if (connection == null) {
            Log.w(LOG_TAG, "Connection must not be null. Can not update visibility.");
            return;
        }
        switch (C1226w.f882a[connection.getStatus().ordinal()]) {
            case 1:
            case 2:
            case 3:
                if (this.mListener != null) {
                    this.mListener.onDisconnected();
                    return;
                }
                return;
            case 4:
                if (this.mListener != null) {
                    this.mListener.onConnected();
                    return;
                }
                return;
            default:
                return;
        }
    }

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (activity instanceof ConnectionListener) {
            this.mListener = (ConnectionListener) activity;
        }
        if (getParentFragment() instanceof ConnectionListener) {
            this.mListener = (ConnectionListener) getParentFragment();
        }
    }

    public void onStart() {
        super.onStart();
        updateConnection(ZopimChat.getDataSource().getConnection());
        ZopimChat.getDataSource().addConnectionObserver(this.mConnectionObserver);
    }

    public void onStop() {
        super.onStop();
        ZopimChat.getDataSource().deleteConnectionObserver(this.mConnectionObserver);
    }
}
