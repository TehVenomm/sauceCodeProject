package com.zopim.android.sdk.api;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.support.annotation.NonNull;
import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentActivity;
import android.support.p000v4.app.FragmentManager;
import android.support.p000v4.app.FragmentTransaction;
import android.util.Log;
import com.zopim.android.sdk.data.DataSource;
import com.zopim.android.sdk.data.PathDataSource;
import com.zopim.android.sdk.model.ChatLog.Rating;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.prechat.PreChatForm.Builder;
import com.zopim.android.sdk.store.Storage;
import com.zopim.android.sdk.util.AppInfo;
import java.io.File;
import java.io.Serializable;
import java.util.Queue;
import java.util.concurrent.ConcurrentLinkedQueue;

public class ZopimChat implements Chat, ChatSession {
    private static final DataSource DATA_SOURCE = new PathDataSource();
    /* access modifiers changed from: private */
    public static final String LOG_TAG = ZopimChat.class.getSimpleName();
    /* access modifiers changed from: private */
    public static boolean mDisableVisitorInfo;
    /* access modifiers changed from: private */
    public static Long mInitializationTimeout;
    /* access modifiers changed from: private */
    public static Long mReconnectTimeout;
    /* access modifiers changed from: private */
    public static String mReferrer;
    /* access modifiers changed from: private */
    public static Long mSessionTimeout;
    /* access modifiers changed from: private */
    public static String mTitle;
    /* access modifiers changed from: private */
    public static VisitorInfo mVisitorInfo;
    /* access modifiers changed from: private */
    public static ZopimChat singleton;
    /* access modifiers changed from: private */
    public String mAccountKey;
    /* access modifiers changed from: private */
    public Chat mChatService;
    /* access modifiers changed from: private */
    public ChatServiceBinder mChatServiceBinder;
    /* access modifiers changed from: private */
    public String mDepartment;
    /* access modifiers changed from: private */
    public boolean mEnded;
    /* access modifiers changed from: private */
    public PreChatForm mPreChatForm = new Builder().build();
    /* access modifiers changed from: private */
    public SessionConfig mSessionConfig;
    /* access modifiers changed from: private */
    public String[] mTags;
    Queue<File> mUnsentFiles = new ConcurrentLinkedQueue();
    Queue<String> mUnsentMessages = new ConcurrentLinkedQueue();

    public static class ChatServiceBinder extends Fragment {
        /* access modifiers changed from: private */
        public static final String LOG_TAG = ChatServiceBinder.class.getSimpleName();
        /* access modifiers changed from: private */
        public boolean mBound;
        private ServiceConnection mConnection = new C1140ac(this);

        private void bind() {
            if (getActivity() != null) {
                Intent intent = new Intent(getActivity(), ChatService.class);
                if (getArguments() != null) {
                    String string = getArguments().getString("ACCOUNT_KEY");
                    String string2 = getArguments().getString("MACHINE_ID");
                    if (!(string == null || string2 == null)) {
                        intent.putExtra("ACCOUNT_KEY", string);
                        intent.putExtra("MACHINE_ID", string2);
                    }
                }
                getActivity().bindService(intent, this.mConnection, 1);
                Logger.m577v(LOG_TAG, "Binding chat service with activity " + getActivity());
            }
        }

        /* access modifiers changed from: private */
        public void unbind() {
            if (this.mBound && getActivity() != null) {
                getActivity().unbindService(this.mConnection);
                this.mBound = false;
                Logger.m577v(LOG_TAG, "Unbinding chat service from activity " + getActivity());
            }
        }

        /* access modifiers changed from: protected */
        public void finalize() {
            Logger.m577v(LOG_TAG, "Service binder cleared from memory by GC");
            super.finalize();
        }

        public boolean isBound() {
            return this.mBound;
        }

        public void onAttach(Activity activity) {
            super.onAttach(activity);
            Logger.m577v(LOG_TAG, "Attached to " + activity);
        }

        public void onDestroy() {
            super.onDestroy();
            Logger.m577v(LOG_TAG, "On host activity destroy " + getActivity());
        }

        public void onDetach() {
            super.onDetach();
            Logger.m577v(LOG_TAG, "Detached from " + getActivity());
        }

        public void onPause() {
            super.onPause();
            Logger.m577v(LOG_TAG, "Host activity pause");
            unbind();
        }

        public void onResume() {
            super.onResume();
            Logger.m577v(LOG_TAG, "Host activity resume");
            if (!ZopimChat.singleton.hasEnded()) {
                bind();
                ZopimChat.singleton.mChatServiceBinder = this;
            }
        }
    }

    public static class ChatTimeoutReceiver extends BroadcastReceiver {
        public void onReceive(Context context, Intent intent) {
            if (intent == null || !ChatSession.ACTION_CHAT_SESSION_TIMEOUT.equals(intent.getAction())) {
                Log.w(ZopimChat.LOG_TAG, "onReceive: intent was null or getAction() was mismatched");
            } else if (ZopimChat.isInitialized()) {
                Logger.m575i(ZopimChat.LOG_TAG, "Received chat timeout. Ending chat.");
                ZopimChat.singleton.endChat();
                if (!ZopimChat.singleton.hasEnded()) {
                    Logger.m575i(ZopimChat.LOG_TAG, "Chat previously expired. Updating chat state as ended.");
                    ZopimChat.singleton.mEnded = true;
                }
            }
        }
    }

    public class DefaultConfig extends C1148i<DefaultConfig> {
        private static final long serialVersionUID = -3486736815047202381L;
        boolean disableVisitorInfoStorage;
        Long initializationTimeout;
        Long reconnectTimeout;
        Long sessionTimeout;

        private DefaultConfig() {
        }

        /* synthetic */ DefaultConfig(ZopimChat zopimChat, C1139ab abVar) {
            this();
        }

        public Void build() {
            if (this.department != null) {
                ZopimChat.this.mDepartment = this.department;
            }
            if (this.preChatForm != null) {
                ZopimChat.this.mPreChatForm = this.preChatForm;
            }
            if (this.tags != null) {
                ZopimChat.this.mTags = this.tags;
            }
            if (this.title != null) {
                ZopimChat.mTitle = this.title;
            }
            if (this.referrer != null) {
                ZopimChat.mReferrer = this.referrer;
            }
            if (this.initializationTimeout != null) {
                ZopimChat.mInitializationTimeout = this.initializationTimeout;
            }
            if (this.reconnectTimeout != null) {
                ZopimChat.mReconnectTimeout = this.reconnectTimeout;
            }
            if (this.sessionTimeout != null) {
                ZopimChat.mSessionTimeout = this.sessionTimeout;
            }
            if (this.disableVisitorInfoStorage) {
                ZopimChat.mDisableVisitorInfo = true;
            }
            return null;
        }

        public DefaultConfig disableVisitorInfoStorage() {
            this.disableVisitorInfoStorage = true;
            return this;
        }

        public DefaultConfig initializationTimeout(long j) {
            if (j < 0) {
                Log.i(ZopimChat.LOG_TAG, "Can not configure initialization timeout. Timeout must not be less then 0");
            } else {
                this.initializationTimeout = Long.valueOf(j);
            }
            return this;
        }

        public DefaultConfig reconnectTimeout(long j) {
            if (j < 0) {
                Log.i(ZopimChat.LOG_TAG, "Can not configure reconnect timeout. Timeout must not be less then 0");
            } else {
                this.reconnectTimeout = Long.valueOf(j);
            }
            return this;
        }

        public DefaultConfig sessionTimeout(long j) {
            if (j < 0) {
                Log.i(ZopimChat.LOG_TAG, "Can not configure session timeout. Timeout must not be less then 0");
            } else {
                this.sessionTimeout = Long.valueOf(j);
            }
            return this;
        }
    }

    public static class SessionConfig extends C1148i<SessionConfig> implements Serializable {
        private static final long serialVersionUID = -4343330703382755112L;
        Long initializationTimeout;
        Long sessionTimeout;
        VisitorInfo visitorInfo;

        public Chat build(FragmentActivity fragmentActivity) {
            if (!ZopimChat.isInitialized()) {
                Log.e(ZopimChat.LOG_TAG, "Have you initialized?");
                return new C1162v();
            } else if (fragmentActivity == null) {
                Log.e(ZopimChat.LOG_TAG, "Can not build the chat. Activity must not be null.");
                return new C1162v();
            } else {
                Storage.init(fragmentActivity);
                if (ZopimChat.mDisableVisitorInfo) {
                    Storage.visitorInfo().disable();
                }
                ZopimChat.singleton.mEnded = false;
                FragmentManager supportFragmentManager = fragmentActivity.getSupportFragmentManager();
                if (supportFragmentManager.findFragmentByTag(ChatServiceBinder.class.getName()) == null) {
                    Logger.m577v(ZopimChat.LOG_TAG, "Adding chat service binder fragment to the host activity");
                    FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
                    ZopimChat.singleton.mChatServiceBinder = new ChatServiceBinder();
                    beginTransaction.add((Fragment) ZopimChat.singleton.mChatServiceBinder, ChatServiceBinder.class.getName());
                    beginTransaction.commit();
                    if (ZopimChat.mVisitorInfo != null) {
                        this.visitorInfo = ZopimChat.mVisitorInfo;
                    } else {
                        this.visitorInfo = Storage.visitorInfo().getVisitorInfo();
                    }
                    if (this.department == null || this.department.isEmpty()) {
                        this.department = ZopimChat.singleton.mDepartment;
                    }
                    if (this.preChatForm == null) {
                        this.preChatForm = ZopimChat.singleton.mPreChatForm;
                    }
                    if (this.title == null) {
                        if (ZopimChat.mTitle != null) {
                            this.title = ZopimChat.mTitle;
                        } else {
                            this.title = AppInfo.getApplicationName(fragmentActivity);
                        }
                    }
                    if (this.referrer == null) {
                        if (ZopimChat.mReferrer != null) {
                            this.referrer = ZopimChat.mReferrer;
                        } else {
                            this.referrer = AppInfo.getApplicationName(fragmentActivity) + ", v" + AppInfo.getApplicationVersionName(fragmentActivity);
                        }
                    }
                    if (ZopimChat.mInitializationTimeout != null) {
                        this.initializationTimeout = ZopimChat.mInitializationTimeout;
                    } else {
                        this.initializationTimeout = Long.valueOf(ChatSession.DEFAULT_CHAT_INITIALIZATION_TIMEOUT);
                    }
                    if (ZopimChat.mSessionTimeout != null) {
                        this.sessionTimeout = ZopimChat.mSessionTimeout;
                    } else {
                        this.sessionTimeout = Long.valueOf(ChatSession.DEFAULT_CHAT_SESSION_TIMEOUT);
                    }
                    ZopimChat.singleton.mSessionConfig = this;
                    Intent intent = new Intent(fragmentActivity.getApplicationContext(), ChatService.class);
                    intent.putExtra("ACCOUNT_KEY", ZopimChat.singleton.mAccountKey);
                    intent.putExtra("SESSION_CONFIG", this);
                    String machineId = Storage.machineId().getMachineId();
                    if (machineId != null) {
                        intent.putExtra("MACHINE_ID", machineId);
                    }
                    fragmentActivity.getApplicationContext().startService(intent);
                } else {
                    Log.v(ZopimChat.LOG_TAG, "Activity is already bound to Chat Service, skipping service start");
                }
                return ZopimChat.singleton;
            }
        }
    }

    /* renamed from: com.zopim.android.sdk.api.ZopimChat$a */
    private static class C1136a {

        /* renamed from: a */
        private static final ZopimChat f667a = new ZopimChat();

        /* access modifiers changed from: private */
        /* renamed from: b */
        public static ZopimChat m582b() {
            return f667a;
        }
    }

    ZopimChat() {
        Logger.setEnabled(false);
    }

    private boolean canCommunicate() {
        boolean z = this.mChatServiceBinder != null && this.mChatServiceBinder.isBound();
        boolean z2 = this.mChatService != null;
        if (z && z2) {
            return true;
        }
        Logger.m575i(LOG_TAG, "Can not chat at the moment. Chat is not connected to the chat service.");
        return false;
    }

    public static DataSource getDataSource() {
        return DATA_SOURCE;
    }

    public static Long getInitializationTimeout() {
        if (!isInitialized()) {
            Log.w(LOG_TAG, "Chat must be initialized to use initialization timeout configuration. Will return default timeout.");
            return Long.valueOf(DEFAULT_CHAT_INITIALIZATION_TIMEOUT);
        }
        ZopimChat zopimChat = singleton;
        return mInitializationTimeout != null ? mInitializationTimeout : Long.valueOf(DEFAULT_CHAT_INITIALIZATION_TIMEOUT);
    }

    public static Long getReconnectTimeout() {
        if (!isInitialized()) {
            Log.w(LOG_TAG, "Chat must be initialized to use reconnect timeout configuration. Will return default timeout.");
            return Long.valueOf(DEFAULT_RECONNECT_TIMEOUT);
        }
        ZopimChat zopimChat = singleton;
        return mReconnectTimeout != null ? mReconnectTimeout : Long.valueOf(DEFAULT_RECONNECT_TIMEOUT);
    }

    public static DefaultConfig init(String str) {
        if (str == null || str.isEmpty()) {
            Log.e(LOG_TAG, "Account key must not be empty or null. Chat initialization will fail!");
        }
        if (singleton == null) {
            Log.i(LOG_TAG, "Initializing Chat SDK");
            singleton = C1136a.m582b();
        }
        singleton.mAccountKey = str;
        Log.v(LOG_TAG, "Staring chat configuration");
        ZopimChat zopimChat = singleton;
        zopimChat.getClass();
        return new DefaultConfig(zopimChat, null);
    }

    /* access modifiers changed from: private */
    public static boolean isInitialized() {
        if (singleton != null) {
            return true;
        }
        Log.v(LOG_TAG, "Initialization verification failed. Did you initialize?");
        return false;
    }

    /* access modifiers changed from: private */
    public void resendUnsentFiles() {
        if (!this.mUnsentFiles.isEmpty()) {
            Log.v(LOG_TAG, "Resending cached unsent files");
            while (true) {
                File file = (File) this.mUnsentFiles.poll();
                if (file != null) {
                    send(file);
                } else {
                    return;
                }
            }
        }
    }

    /* access modifiers changed from: private */
    public void resendUnsentMessages() {
        if (!this.mUnsentMessages.isEmpty()) {
            Log.v(LOG_TAG, "Resending cached unsent messages");
            while (true) {
                String str = (String) this.mUnsentMessages.poll();
                if (str != null) {
                    send(str);
                } else {
                    return;
                }
            }
        }
    }

    public static synchronized Chat resume(FragmentActivity fragmentActivity) {
        Chat vVar;
        synchronized (ZopimChat.class) {
            if (!isInitialized()) {
                Log.e(LOG_TAG, "Have you initialized?");
                vVar = new C1162v();
            } else if (fragmentActivity == null) {
                Log.e(LOG_TAG, "Chat can not be resumed. Activity must not be null.");
                vVar = new C1162v();
            } else {
                FragmentManager supportFragmentManager = fragmentActivity.getSupportFragmentManager();
                if (supportFragmentManager.findFragmentByTag(ChatServiceBinder.class.getName()) == null) {
                    Logger.m577v(LOG_TAG, "Adding chat service binder fragment to the host activity");
                    FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
                    singleton.mChatServiceBinder = new ChatServiceBinder();
                    beginTransaction.add((Fragment) singleton.mChatServiceBinder, ChatServiceBinder.class.getName());
                    beginTransaction.commit();
                }
                if (singleton.mChatService == null || singleton.hasEnded()) {
                    Storage.init(fragmentActivity);
                    if (mDisableVisitorInfo) {
                        Storage.visitorInfo().disable();
                    }
                    String machineId = Storage.machineId().getMachineId();
                    if (machineId == null || machineId.isEmpty()) {
                        Logger.m575i(LOG_TAG, "Can not resume chat without machine id. Chat either expired or not yet started.");
                        vVar = new C1162v();
                    } else {
                        Intent intent = new Intent(fragmentActivity.getApplicationContext(), ChatService.class);
                        intent.putExtra("ACCOUNT_KEY", singleton.mAccountKey);
                        intent.putExtra("MACHINE_ID", machineId);
                        intent.setAction("chat.action.RECONNECT");
                        fragmentActivity.getApplicationContext().startService(intent);
                        vVar = singleton;
                    }
                } else {
                    vVar = singleton;
                }
            }
        }
        return vVar;
    }

    public static void setVisitorInfo(VisitorInfo visitorInfo) {
        mVisitorInfo = visitorInfo;
    }

    public static synchronized Chat start(FragmentActivity fragmentActivity) {
        Chat build;
        synchronized (ZopimChat.class) {
            build = new SessionConfig().build(fragmentActivity);
        }
        return build;
    }

    public boolean emailTranscript(String str) {
        if (canCommunicate()) {
            return this.mChatService.emailTranscript(str);
        }
        return false;
    }

    public void endChat() {
        if (canCommunicate()) {
            this.mChatService.endChat();
            this.mChatServiceBinder.unbind();
            this.mEnded = true;
            return;
        }
        Log.i(LOG_TAG, "Can not end chat while disconnected from the chat service");
    }

    public ChatConfig getConfig() {
        return new C1139ab(this);
    }

    public boolean hasEnded() {
        return canCommunicate() ? this.mChatService.hasEnded() : this.mEnded;
    }

    public void resend(String str) {
        if (canCommunicate()) {
            this.mChatService.resend(str);
        } else {
            Log.v(LOG_TAG, "Unable to re-send message at the moment.");
        }
    }

    public void resetTimeout() {
        if (canCommunicate()) {
            this.mChatService.resetTimeout();
        }
    }

    public void send(File file) {
        if (canCommunicate()) {
            this.mChatService.send(file);
        } else {
            this.mUnsentFiles.add(file);
        }
    }

    public void send(String str) {
        if (canCommunicate()) {
            this.mChatService.send(str);
            return;
        }
        Log.v(LOG_TAG, "Unable to send message at the moment. Caching it for resending.");
        this.mUnsentMessages.add(str);
    }

    public void sendChatComment(@NonNull String str) {
        if (canCommunicate()) {
            this.mChatService.sendChatComment(str);
        }
    }

    public void sendChatRating(@NonNull Rating rating) {
        if (canCommunicate()) {
            this.mChatService.sendChatRating(rating);
        }
    }

    public boolean sendOfflineMessage(String str, String str2, String str3) {
        if (canCommunicate()) {
            return this.mChatService.sendOfflineMessage(str, str2, str3);
        }
        return false;
    }

    public void setDepartment(String str) {
        if (canCommunicate() && str != null) {
            this.mChatService.setDepartment(str);
        }
    }

    public void setEmail(String str) {
        if (canCommunicate() && str != null) {
            this.mChatService.setEmail(str);
        }
    }

    public void setName(String str) {
        if (canCommunicate() && str != null) {
            this.mChatService.setName(str);
        }
    }

    public void setPhoneNumber(String str) {
        if (canCommunicate() && str != null) {
            this.mChatService.setPhoneNumber(str);
        }
    }
}
