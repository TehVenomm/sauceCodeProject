package com.zopim.android.sdk.prechat;

import android.app.Activity;
import android.app.AlertDialog;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentTransaction;
import android.util.Log;
import android.util.Patterns;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Toast;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.chatlog.ConnectionFragment;
import com.zopim.android.sdk.chatlog.ConnectionFragment.ConnectionListener;
import com.zopim.android.sdk.chatlog.ConnectionToastFragment;
import com.zopim.android.sdk.data.observers.FormsObserver;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.model.VisitorInfo.Builder;

public class ZopimOfflineFragment extends Fragment implements ConnectionListener {
    private static final String LOG_TAG = ZopimOfflineFragment.class.getSimpleName();
    public static final String STATE_MENU_ITEM_ENABLED = "MENU_ITEM_ENABLED";
    private static final String STATE_PROGRESS_VISIBITLITY = "PROGRESS_VISIBILITY";
    /* access modifiers changed from: private */
    public Chat mChat;
    /* access modifiers changed from: private */
    public ChatListener mChatListener;
    private EditText mEmailEdit;
    FormsObserver mFormsObserver = new C1262o(this);
    /* access modifiers changed from: private */
    public Handler mHandler = new Handler(Looper.getMainLooper());
    private Menu mMenu;
    private EditText mMessageEdit;
    private EditText mNameEdit;
    /* access modifiers changed from: private */
    public View mProgressBar;
    /* access modifiers changed from: private */
    public AlertDialog mSendTimeoutDialog;
    Runnable mShowSendTimeoutDialog = new C1259l(this);
    private boolean mStateMenuItemEnabled = true;
    private VisitorInfo mVisitorInfo;

    /* access modifiers changed from: private */
    public void close() {
        FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
        beginTransaction.remove(this);
        beginTransaction.commit();
    }

    /* access modifiers changed from: private */
    public void sendOfflineMessage() {
        String name;
        boolean z;
        String email;
        String str = null;
        if (this.mNameEdit.getVisibility() == 0) {
            name = this.mNameEdit.getText().toString().trim();
            if (name.isEmpty()) {
                this.mNameEdit.setError(getResources().getString(C1122R.string.offline_name_error_message));
                this.mNameEdit.setHint(C1122R.string.offline_name_error_hint);
                z = false;
            } else {
                z = true;
            }
        } else {
            name = this.mVisitorInfo.getName();
            z = true;
        }
        if (this.mEmailEdit.getVisibility() == 0) {
            email = this.mEmailEdit.getText().toString().trim();
            if (!Patterns.EMAIL_ADDRESS.matcher(email).matches()) {
                this.mEmailEdit.setError(getResources().getString(C1122R.string.offline_email_error_message));
                this.mEmailEdit.setHint(C1122R.string.offline_email_error_hint);
                z = false;
            }
        } else {
            email = this.mVisitorInfo.getEmail();
        }
        if (this.mMessageEdit.getVisibility() == 0) {
            str = this.mMessageEdit.getText().toString().trim();
            if (str.isEmpty()) {
                this.mMessageEdit.setError(getResources().getString(C1122R.string.offline_message_error_message));
                this.mMessageEdit.setHint(C1122R.string.offline_message_error_hint);
                z = false;
            }
        }
        if (!z) {
            Toast.makeText(getActivity(), C1122R.string.offline_validation_error_message, 1).show();
        } else if (!this.mChat.sendOfflineMessage(name, email, str)) {
            this.mHandler.post(this.mShowSendTimeoutDialog);
        } else {
            this.mProgressBar.setVisibility(0);
            this.mHandler.postDelayed(this.mShowSendTimeoutDialog, ZopimChat.getInitializationTimeout().longValue());
        }
    }

    private void setViewVisibility(View view, int i) {
        if (view == null) {
            Log.w(LOG_TAG, "View must not be null. Can not apply visibility change");
            return;
        }
        switch (i) {
            case 0:
                view.setVisibility(0);
                return;
            case 4:
                view.setVisibility(4);
                return;
            case 8:
                view.setVisibility(8);
                return;
            default:
                return;
        }
    }

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (activity instanceof ChatListener) {
            this.mChatListener = (ChatListener) activity;
        }
    }

    public void onConnected() {
        if (this.mMenu != null) {
            MenuItem findItem = this.mMenu.findItem(C1122R.C1125id.start_chat);
            if (findItem != null && findItem.isEnabled()) {
                findItem.setEnabled(false);
            }
        }
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setHasOptionsMenu(true);
        this.mChat = ZopimChat.resume(getActivity());
        VisitorInfo visitorInfo = this.mChat.getConfig().getVisitorInfo();
        if (visitorInfo == null) {
            visitorInfo = new Builder().build();
        }
        this.mVisitorInfo = visitorInfo;
        if (bundle == null) {
            ConnectionToastFragment connectionToastFragment = new ConnectionToastFragment();
            ConnectionFragment connectionFragment = new ConnectionFragment();
            FragmentTransaction beginTransaction = getChildFragmentManager().beginTransaction();
            beginTransaction.add(C1122R.C1125id.toast_fragment_container, connectionToastFragment, ConnectionToastFragment.class.getName());
            beginTransaction.add((Fragment) connectionFragment, ConnectionFragment.class.getName());
            beginTransaction.commit();
        }
    }

    public void onCreateOptionsMenu(Menu menu, MenuInflater menuInflater) {
        super.onCreateOptionsMenu(menu, menuInflater);
        menuInflater.inflate(C1122R.menu.chat_offline_message_menu, menu);
        menu.findItem(C1122R.C1125id.send).setEnabled(this.mStateMenuItemEnabled);
        this.mMenu = menu;
    }

    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        return layoutInflater.inflate(C1122R.C1126layout.zopim_offline_message_fragment, viewGroup, false);
    }

    public void onDisconnected() {
        if (this.mMenu != null) {
            MenuItem findItem = this.mMenu.findItem(C1122R.C1125id.start_chat);
            if (findItem != null && !findItem.isEnabled()) {
                findItem.setEnabled(true);
            }
        }
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        if (16908332 == menuItem.getItemId()) {
            this.mChat.endChat();
            close();
            if (this.mChatListener != null) {
                this.mChatListener.onChatEnded();
            }
            return super.onOptionsItemSelected(menuItem);
        } else if (C1122R.C1125id.send != menuItem.getItemId()) {
            return super.onOptionsItemSelected(menuItem);
        } else {
            sendOfflineMessage();
            return true;
        }
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putBoolean(STATE_MENU_ITEM_ENABLED, this.mMenu.findItem(C1122R.C1125id.send).isEnabled());
        bundle.putInt(STATE_PROGRESS_VISIBITLITY, this.mProgressBar.getVisibility());
    }

    public void onStart() {
        super.onStart();
        ZopimChat.getDataSource().addFormsObserver(this.mFormsObserver);
    }

    public void onStop() {
        super.onStop();
        this.mHandler.removeCallbacksAndMessages(null);
        ZopimChat.getDataSource().deleteFormsObserver(this.mFormsObserver);
    }

    public void onViewCreated(View view, @Nullable Bundle bundle) {
        super.onViewCreated(view, bundle);
        this.mNameEdit = (EditText) view.findViewById(C1122R.C1125id.name);
        this.mEmailEdit = (EditText) view.findViewById(C1122R.C1125id.email);
        this.mMessageEdit = (EditText) view.findViewById(C1122R.C1125id.message);
        this.mProgressBar = view.findViewById(C1122R.C1125id.progress);
        this.mNameEdit.setHint(String.format(getResources().getString(C1122R.string.required_field_template), new Object[]{this.mNameEdit.getHint()}));
        this.mEmailEdit.setHint(String.format(getResources().getString(C1122R.string.required_field_template), new Object[]{this.mEmailEdit.getHint()}));
        this.mMessageEdit.setHint(String.format(getResources().getString(C1122R.string.required_field_template), new Object[]{this.mMessageEdit.getHint()}));
        if (this.mVisitorInfo.getName() != null && !this.mVisitorInfo.getName().isEmpty()) {
            this.mNameEdit.setVisibility(8);
        }
        if (this.mVisitorInfo.getEmail() != null && !this.mVisitorInfo.getEmail().isEmpty()) {
            this.mEmailEdit.setVisibility(8);
        }
    }

    public void onViewStateRestored(@Nullable Bundle bundle) {
        super.onViewStateRestored(bundle);
        if (bundle != null) {
            this.mStateMenuItemEnabled = bundle.getBoolean(STATE_MENU_ITEM_ENABLED, true);
            setViewVisibility(this.mProgressBar, bundle.getInt(STATE_PROGRESS_VISIBITLITY, 8));
        }
    }
}
