package com.zopim.android.sdk.prechat;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.ChatConfig;
import com.zopim.android.sdk.api.ChatSession;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.embeddable.Contract;
import com.zopim.android.sdk.model.Account;
import com.zopim.android.sdk.model.Account.Status;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm.Field;

public class ZopimChatFragment extends Fragment {
    private static final String EXTRA_CHAT_CONFIG = "CHAT_CONFIG";
    private static final String LOG_TAG = ZopimChatFragment.class.getSimpleName();
    private static final String STATE_CHAT_INITIALIZED = "CHAT_INITIALIZED";
    private static final String STATE_COULD_NOT_CONNECT_ERROR_VISIBITLITY = "COULD_NOT_CONNECT_ERROR_VISIBILITY";
    private static final String STATE_NO_AGENTS_VISIBITLITY = "NO_AGENTS_VISIBILITY";
    private static final String STATE_NO_CONNECTION_ERROR_VISIBITLITY = "NO_CONNECTION_ERROR_VISIBILITY";
    private static final String STATE_PROGRESS_VISIBITLITY = "PROGRESS_VISIBILITY";
    private Chat mChat;
    BroadcastReceiver mChatInitializationTimeout = new C0888k(this);
    private boolean mChatInitialized = false;
    private ChatListener mChatListener;
    ConnectionObserver mConnectionObserver = new C0886i(this);
    private View mCouldNotConnectErrorView;
    private Handler mHandler = new Handler(Looper.getMainLooper());
    private View mNoAgentsView;
    private View mNoConnectionErrorView;
    BroadcastReceiver mOfflineMessageReceiver = new C0887j(this);
    private View mProgressBar;

    private void close() {
        FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
        beginTransaction.remove(this);
        beginTransaction.commit();
    }

    public static ZopimChatFragment newInstance(SessionConfig sessionConfig) {
        ZopimChatFragment zopimChatFragment = new ZopimChatFragment();
        Bundle bundle = new Bundle();
        bundle.putSerializable(EXTRA_CHAT_CONFIG, sessionConfig);
        zopimChatFragment.setArguments(bundle);
        return zopimChatFragment;
    }

    private void onChatInitializationFailed() {
        this.mHandler.post(new C0882e(this));
    }

    private void onChatInitialized() {
        Logger.m564v(LOG_TAG, "Chat initialization completed");
        if (this.mChatListener != null) {
            this.mChatListener.onChatInitialized();
        }
        Account account = ZopimChat.getDataSource().getAccount();
        if (account == null || Status.OFFLINE != account.getStatus()) {
            this.mHandler.post(new C0881d(this));
        } else {
            showNoAgents();
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

    private void showCouldNotConnectError() {
        this.mHandler.post(new C0885h(this));
    }

    private boolean showField(Field field, String str) {
        return field.equals(Field.OPTIONAL_EDITABLE) || field.equals(Field.REQUIRED_EDITABLE) || (!field.equals(Field.NOT_REQUIRED) && (str == null || str.isEmpty()));
    }

    private void showNoAgents() {
        this.mHandler.post(new C0884g(this));
    }

    private void showNoConnectionError() {
        this.mHandler.post(new C0883f(this));
    }

    private boolean showPreChat() {
        ChatConfig config = this.mChat.getConfig();
        PreChatForm preChatForm = config.getPreChatForm();
        if (config.getDepartment() == null || !config.getDepartment().isEmpty()) {
        }
        boolean z = (showField(preChatForm.getDepartment(), config.getDepartment())) || showField(preChatForm.getMessage(), null);
        VisitorInfo visitorInfo = this.mChat.getConfig().getVisitorInfo();
        if (visitorInfo == null) {
            return z;
        }
        if (visitorInfo.getEmail() == null || !visitorInfo.getEmail().isEmpty()) {
        }
        if (visitorInfo.getName() == null || !visitorInfo.getName().isEmpty()) {
        }
        if (visitorInfo.getPhoneNumber() == null || !visitorInfo.getPhoneNumber().isEmpty()) {
            z = z || showField(preChatForm.getEmail(), visitorInfo.getEmail());
            z = z || showField(preChatForm.getName(), visitorInfo.getName());
            return z || showField(preChatForm.getPhoneNumber(), visitorInfo.getPhoneNumber());
        } else {
            if (!z) {
            }
            if (!z) {
            }
            if (!z) {
            }
        }
    }

    public void onActivityCreated(@Nullable Bundle bundle) {
        super.onActivityCreated(bundle);
        setHasOptionsMenu(true);
        if (bundle == null) {
            this.mProgressBar.setVisibility(0);
            if (getArguments() == null || !getArguments().containsKey(EXTRA_CHAT_CONFIG)) {
                this.mChat = ZopimChat.start(getActivity());
            } else {
                try {
                    Log.v(LOG_TAG, "Starting chat with session config");
                    SessionConfig sessionConfig = (SessionConfig) getArguments().getSerializable(EXTRA_CHAT_CONFIG);
                    this.mChat = sessionConfig != null ? sessionConfig.build(getActivity()) : ZopimChat.start(getActivity());
                } catch (ClassCastException e) {
                    Log.w(LOG_TAG, "Unexpected configuration extras. Will ignore session configuration.");
                    this.mChat = ZopimChat.start(getActivity());
                }
            }
        } else {
            this.mChatInitialized = bundle.getBoolean(STATE_CHAT_INITIALIZED, false);
            this.mChat = ZopimChat.resume(getActivity());
            Logger.m564v(LOG_TAG, "Restoring states. chat initialized: " + this.mChatInitialized);
        }
        if (this.mChatListener != null) {
            this.mChatListener.onChatLoaded(this.mChat);
        }
    }

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (activity instanceof ChatListener) {
            this.mChatListener = (ChatListener) activity;
        } else {
            Log.i(LOG_TAG, activity.getClass() + " should implement " + ChatListener.class);
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        return layoutInflater.inflate(C0784R.layout.zopim_chat_fragment, viewGroup, false);
    }

    public void onDetach() {
        super.onDetach();
        this.mChatListener = null;
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        if (16908332 == menuItem.getItemId()) {
            this.mChat.endChat();
            close();
            if (this.mChatListener != null) {
                this.mChatListener.onChatEnded();
            }
        }
        return super.onOptionsItemSelected(menuItem);
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putBoolean(STATE_CHAT_INITIALIZED, this.mChatInitialized);
        bundle.putInt(STATE_NO_CONNECTION_ERROR_VISIBITLITY, this.mNoConnectionErrorView.getVisibility());
        bundle.putInt(STATE_COULD_NOT_CONNECT_ERROR_VISIBITLITY, this.mCouldNotConnectErrorView.getVisibility());
        bundle.putInt(STATE_NO_AGENTS_VISIBITLITY, this.mNoAgentsView.getVisibility());
        bundle.putInt(STATE_PROGRESS_VISIBITLITY, this.mProgressBar.getVisibility());
        Logger.m564v(LOG_TAG, "Saving states. chat initialized: " + this.mChatInitialized + ", no conn visibility: " + this.mNoConnectionErrorView.getVisibility() + ", progress visibility: " + this.mProgressBar.getVisibility());
    }

    public void onStart() {
        super.onStart();
        ZopimChat.getDataSource().addConnectionObserver(this.mConnectionObserver);
        IntentFilter intentFilter = new IntentFilter(Contract.ACTION_CREATE_REQUEST);
        intentFilter.setPriority(-1000);
        getActivity().registerReceiver(this.mOfflineMessageReceiver, intentFilter);
        intentFilter = new IntentFilter(ChatSession.ACTION_CHAT_INITIALIZATION_TIMEOUT);
        intentFilter.setPriority(-1000);
        getActivity().registerReceiver(this.mChatInitializationTimeout, intentFilter);
    }

    public void onStop() {
        super.onStop();
        this.mHandler.removeCallbacksAndMessages(null);
        ZopimChat.getDataSource().deleteConnectionObserver(this.mConnectionObserver);
        getActivity().unregisterReceiver(this.mOfflineMessageReceiver);
        getActivity().unregisterReceiver(this.mChatInitializationTimeout);
    }

    public void onViewCreated(View view, @Nullable Bundle bundle) {
        super.onViewCreated(view, bundle);
        setHasOptionsMenu(true);
        this.mProgressBar = view.findViewById(C0784R.id.progress_container);
        this.mNoConnectionErrorView = view.findViewById(C0784R.id.no_connection_error);
        this.mNoAgentsView = view.findViewById(C0784R.id.no_agents);
        this.mCouldNotConnectErrorView = view.findViewById(C0784R.id.could_not_connect_error);
        ((Button) this.mNoAgentsView.findViewById(C0784R.id.no_agents_button)).setOnClickListener(new C0879b(this));
    }

    public void onViewStateRestored(@Nullable Bundle bundle) {
        super.onViewStateRestored(bundle);
        if (bundle != null) {
            int i = bundle.getInt(STATE_NO_CONNECTION_ERROR_VISIBITLITY, 8);
            int i2 = bundle.getInt(STATE_COULD_NOT_CONNECT_ERROR_VISIBITLITY, 8);
            int i3 = bundle.getInt(STATE_NO_AGENTS_VISIBITLITY, 8);
            int i4 = bundle.getInt(STATE_PROGRESS_VISIBITLITY, 8);
            setViewVisibility(this.mNoConnectionErrorView, i);
            setViewVisibility(this.mCouldNotConnectErrorView, i2);
            setViewVisibility(this.mNoAgentsView, i3);
            setViewVisibility(this.mProgressBar, i4);
        }
    }
}
