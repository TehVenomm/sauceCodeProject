package com.zopim.android.sdk.chatlog;

import android.annotation.TargetApi;
import android.app.Activity;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.anim.AnimatorPack;
import com.zopim.android.sdk.anim.AnimatorPack.Direction;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

public class ConnectionToastFragment extends Fragment {
    private static final String LOG_TAG = ConnectionToastFragment.class.getSimpleName();
    private static final String STATE_SHOW_TOAST = "SHOW_TOAST";
    ConnectionObserver mConnectionObserver = new C0857x(this);
    private Handler mHandler = new Handler(Looper.getMainLooper());
    private TextView mMessageView;
    private C0831a mToastListener;
    private View mToastView;

    /* renamed from: com.zopim.android.sdk.chatlog.ConnectionToastFragment$a */
    interface C0831a {
        void onHideToast();

        void onShowToast();
    }

    @TargetApi(11)
    private void hideToast() {
        if (this.mToastView != null && this.mToastView.getVisibility() == 0) {
            Log.v(LOG_TAG, "Hide no network toast");
            getView().bringToFront();
            if (VERSION.SDK_INT >= 11) {
                AnimatorPack.slideOut(this.mToastView, Direction.BOTTOM).start();
            } else {
                this.mToastView.setVisibility(4);
            }
            if (this.mToastListener != null) {
                this.mToastListener.onHideToast();
            }
        }
    }

    @TargetApi(11)
    private void showToast() {
        if (this.mToastView != null && this.mToastView.getVisibility() != 0) {
            Log.v(LOG_TAG, "Show no network toast");
            getView().bringToFront();
            if (VERSION.SDK_INT >= 11) {
                AnimatorPack.slideIn(this.mToastView, Direction.BOTTOM).start();
            } else {
                this.mToastView.setVisibility(0);
            }
            if (this.mToastListener != null) {
                this.mToastListener.onShowToast();
            }
        }
    }

    private void updateToastView(Connection connection) {
        if (connection == null) {
            Log.w(LOG_TAG, "Connection must not be null. Can not update visibility.");
            return;
        }
        switch (C0859z.f842a[connection.getStatus().ordinal()]) {
            case 1:
                this.mMessageView.setText(getResources().getString(C0784R.string.no_connectivity_toast_message));
                showToast();
                return;
            case 2:
                this.mMessageView.setText(getResources().getString(C0784R.string.reconnecting_toast_message));
                showToast();
                return;
            case 3:
                this.mMessageView.setText(getResources().getString(C0784R.string.no_connectivity_toast_message));
                showToast();
                return;
            case 4:
                hideToast();
                return;
            default:
                return;
        }
    }

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (activity instanceof C0831a) {
            this.mToastListener = (C0831a) activity;
        }
        if (getParentFragment() instanceof C0831a) {
            this.mToastListener = (C0831a) getParentFragment();
        }
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        if (bundle == null) {
            FragmentTransaction beginTransaction = getChildFragmentManager().beginTransaction();
            beginTransaction.add(new ConnectionFragment(), ConnectionFragment.class.getName());
            beginTransaction.commit();
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, @Nullable Bundle bundle) {
        return layoutInflater.inflate(C0784R.layout.zopim_toast_fragment, viewGroup, false);
    }

    public void onDestroy() {
        super.onDestroy();
        this.mHandler.removeCallbacksAndMessages(null);
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        if (this.mToastView.getVisibility() == 0) {
            bundle.putBoolean(STATE_SHOW_TOAST, true);
        } else {
            bundle.putBoolean(STATE_SHOW_TOAST, false);
        }
    }

    public void onStart() {
        super.onStart();
        updateToastView(ZopimChat.getDataSource().getConnection());
        ZopimChat.getDataSource().addConnectionObserver(this.mConnectionObserver);
    }

    public void onStop() {
        super.onStop();
        ZopimChat.getDataSource().deleteConnectionObserver(this.mConnectionObserver);
    }

    public void onViewCreated(View view, @Nullable Bundle bundle) {
        super.onViewCreated(view, bundle);
        this.mToastView = view.findViewById(C0784R.id.network_no_connectivity);
        this.mMessageView = (TextView) view.findViewById(C0784R.id.message_text);
    }

    public void onViewStateRestored(@Nullable Bundle bundle) {
        super.onViewStateRestored(bundle);
        if (bundle == null) {
            return;
        }
        if (bundle.getBoolean(STATE_SHOW_TOAST, false)) {
            showToast();
        } else {
            hideToast();
        }
    }
}
