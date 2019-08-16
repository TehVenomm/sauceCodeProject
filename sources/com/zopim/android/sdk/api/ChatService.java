package com.zopim.android.sdk.api;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Binder;
import android.os.IBinder;
import android.os.SystemClock;
import android.support.annotation.NonNull;
import android.support.p000v4.app.NotificationCompat;
import android.util.Log;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import com.zopim.android.sdk.attachment.SdkCache;
import com.zopim.android.sdk.data.ConnectionPath.ConnectivityReceiver;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.data.LivechatChatLogPath.ChatTimeoutReceiver;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.ChatLog.Rating;
import com.zopim.android.sdk.model.Connection;
import com.zopim.android.sdk.model.Connection.Status;
import com.zopim.android.sdk.model.Profile;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.model.VisitorInfo.Builder;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.store.Storage;
import java.io.File;
import java.io.Serializable;
import java.util.Queue;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

public class ChatService extends Service implements Chat {
    static final String ACTION_CHAT_RECONNECT = "chat.action.RECONNECT";
    static final String EXTRA_ACCOUNT_KEY = "ACCOUNT_KEY";
    static final String EXTRA_MACHINE_ID = "MACHINE_ID";
    static final String EXTRA_SESSION_CONFIG = "SESSION_CONFIG";
    /* access modifiers changed from: private */
    public static final String LOG_TAG = ChatService.class.getSimpleName();
    /* access modifiers changed from: private */
    public static C1137a mChat;
    private long mChatInitializationTimeout;
    /* access modifiers changed from: private */
    public boolean mChatInitialized;
    ChatLogObserver mChatLogObserver = new C1142c(this);
    private long mChatSessionTimeout;
    private final ChatTimeoutReceiver mChatTimeoutReceiver = new ChatTimeoutReceiver();
    ConnectionObserver mConnectionObserver = new C1145f(this);
    private final ConnectivityReceiver mConnectivityReceiver = new ConnectivityReceiver();
    /* access modifiers changed from: private */
    public String mDepartment;
    private boolean mEnded;
    ScheduledFuture mKeepAliveRunner;
    /* access modifiers changed from: private */
    public PreChatForm mPreChatForm;
    private String mReferrer;
    private final IBinder mServiceBinder = new LocalBinder();
    /* access modifiers changed from: private */
    public String[] mTags;
    private String mTitle;
    Queue<File> mUnsentFiles = new ConcurrentLinkedQueue();
    Queue<String> mUnsentMessages = new ConcurrentLinkedQueue();
    /* access modifiers changed from: private */
    public String mVisitorEmail;
    /* access modifiers changed from: private */
    public String mVisitorName;
    /* access modifiers changed from: private */
    public String mVisitorPhoneNumber;

    public class LocalBinder extends Binder {
        public LocalBinder() {
        }

        public Chat getService() {
            return ChatService.this;
        }
    }

    /* access modifiers changed from: private */
    public boolean canCommunicate() {
        if (this.mChatInitialized) {
            Connection connection = ZopimChat.getDataSource().getConnection();
            if ((connection != null ? connection.getStatus() : Status.UNKNOWN) == Status.CONNECTED) {
                return true;
            }
        }
        Logger.m575i(LOG_TAG, "Can not communicate at the moment. Chat is either not initialized or not connected.");
        return false;
    }

    private void configureInitializationTimeout(boolean z) {
        AlarmManager alarmManager = (AlarmManager) getSystemService(NotificationCompat.CATEGORY_ALARM);
        Intent intent = new Intent(this, ChatService.class);
        intent.setAction(ChatSession.ACTION_CHAT_INITIALIZATION_TIMEOUT);
        PendingIntent service = PendingIntent.getService(this, 0, intent, 134217728);
        if (alarmManager != null) {
            Logger.m577v(LOG_TAG, "Alarm manager acquired, scheduling chat initialization timeout");
            long elapsedRealtime = SystemClock.elapsedRealtime() + this.mChatInitializationTimeout;
            if (z) {
                alarmManager.set(3, elapsedRealtime, service);
            } else {
                alarmManager.cancel(service);
            }
        } else {
            Log.w(LOG_TAG, "Could not get the Alarm manager, will not set chat initialization timeout");
        }
    }

    /* access modifiers changed from: private */
    public void onChatInitialized() {
        Log.v(LOG_TAG, "Chat initialization completed");
        mChat.mo20638b();
        this.mChatInitialized = true;
        configureInitializationTimeout(false);
        Profile profile = ZopimChat.getDataSource().getProfile();
        if (profile != null) {
            Storage.machineId().setMachineId(profile.getMachineId());
        }
        setEmail(this.mVisitorEmail);
        setName(this.mVisitorName);
        setPhoneNumber(this.mVisitorPhoneNumber);
        setDepartment(this.mDepartment);
        if (this.mTags != null && this.mTags.length > 0) {
            mChat.mo20637a(this.mTags);
        }
    }

    private void prepareTimeout() {
        AlarmManager alarmManager = (AlarmManager) getSystemService(NotificationCompat.CATEGORY_ALARM);
        Intent intent = new Intent(this, ChatService.class);
        intent.setAction(ChatSession.ACTION_CHAT_SESSION_TIMEOUT);
        PendingIntent service = PendingIntent.getService(this, 0, intent, 134217728);
        if (alarmManager != null) {
            Logger.m577v(LOG_TAG, "Alarm manager acquired, scheduling chat timeout");
            alarmManager.set(3, SystemClock.elapsedRealtime() + this.mChatSessionTimeout, service);
            return;
        }
        Log.w(LOG_TAG, "Could not get the Alarm manager, will not set chat timeout");
    }

    public boolean emailTranscript(String str) {
        if (canCommunicate()) {
            return mChat.emailTranscript(str);
        }
        return false;
    }

    public void endChat() {
        if (canCommunicate()) {
            mChat.endChat();
        }
        this.mEnded = true;
        if (this.mKeepAliveRunner != null) {
            this.mKeepAliveRunner.cancel(true);
        }
        this.mChatInitialized = false;
        SdkCache.INSTANCE.deleteCache(getApplicationContext());
        stopSelf();
    }

    /* access modifiers changed from: protected */
    public void finalize() {
        Logger.m577v(LOG_TAG, "Service cleared from memory by GC");
        super.finalize();
    }

    public ChatConfig getConfig() {
        return new C1146g(this);
    }

    public boolean hasEnded() {
        return this.mEnded;
    }

    public IBinder onBind(Intent intent) {
        return this.mServiceBinder;
    }

    public void onCreate() {
        super.onCreate();
        ZopimChat.getDataSource().addConnectionObserver(this.mConnectionObserver);
        ZopimChat.getDataSource().addChatLogObserver(this.mChatLogObserver);
        registerReceiver(this.mConnectivityReceiver, new IntentFilter("android.net.conn.CONNECTIVITY_CHANGE"));
        registerReceiver(this.mChatTimeoutReceiver, new IntentFilter(ChatSession.ACTION_CHAT_SESSION_TIMEOUT));
        Log.v(LOG_TAG, "Service created");
    }

    public void onDestroy() {
        super.onDestroy();
        if (this.mKeepAliveRunner != null) {
            this.mKeepAliveRunner.cancel(true);
        }
        ZopimChat.getDataSource().deleteConnectionObserver(this.mConnectionObserver);
        ZopimChat.getDataSource().deleteChatLogObserver(this.mChatLogObserver);
        unregisterReceiver(this.mConnectivityReceiver);
        unregisterReceiver(this.mChatTimeoutReceiver);
        Log.v(LOG_TAG, "Chat service destroyed");
    }

    public int onStartCommand(Intent intent, int i, int i2) {
        if (intent == null) {
            Logger.m577v(LOG_TAG, "Service restarted by the system, will not reinitialize the web binder");
            return 1;
        }
        String action = intent.getAction();
        if (ChatSession.ACTION_CHAT_INITIALIZATION_TIMEOUT.equals(action)) {
            if (this.mChatInitialized) {
                return 1;
            }
            Intent intent2 = new Intent();
            intent2.setAction(ChatSession.ACTION_CHAT_INITIALIZATION_TIMEOUT);
            intent2.setPackage(getApplicationContext().getPackageName());
            sendOrderedBroadcast(intent2, null);
            Log.i(LOG_TAG, "Chat initialization has timed out. Ending chat session.");
            endChat();
            return 2;
        } else if (ChatSession.ACTION_CHAT_SESSION_TIMEOUT.equals(action)) {
            Log.i(LOG_TAG, "Chat has timed out. Ending chat session.");
            Intent intent3 = new Intent();
            intent3.setAction(ChatSession.ACTION_CHAT_SESSION_TIMEOUT);
            intent3.setPackage(getApplicationContext().getPackageName());
            sendBroadcast(intent3);
            endChat();
            return 2;
        } else if (!ACTION_CHAT_RECONNECT.equals(action) || !this.mChatInitialized) {
            mChat = new C1164x(this);
            this.mChatInitialized = false;
            this.mEnded = false;
            ZopimChat.getDataSource().clear();
            String stringExtra = intent.getStringExtra(EXTRA_ACCOUNT_KEY);
            String stringExtra2 = intent.getStringExtra(EXTRA_MACHINE_ID);
            Serializable serializableExtra = intent.getSerializableExtra(EXTRA_SESSION_CONFIG);
            if (stringExtra == null) {
                Log.w(LOG_TAG, "Can not start chat service without account id. Have you passed account id as extras?");
                stopSelf();
                return 2;
            }
            if (serializableExtra instanceof SessionConfig) {
                SessionConfig sessionConfig = (SessionConfig) serializableExtra;
                VisitorInfo visitorInfo = sessionConfig.visitorInfo;
                if (visitorInfo != null) {
                    this.mVisitorName = visitorInfo.getName();
                    this.mVisitorEmail = visitorInfo.getEmail();
                    this.mVisitorPhoneNumber = visitorInfo.getPhoneNumber();
                }
                this.mDepartment = sessionConfig.department;
                this.mPreChatForm = sessionConfig.preChatForm;
                this.mTitle = sessionConfig.title;
                this.mReferrer = sessionConfig.referrer;
                this.mTags = sessionConfig.tags;
                this.mChatInitializationTimeout = sessionConfig.initializationTimeout.longValue();
                this.mChatSessionTimeout = sessionConfig.sessionTimeout.longValue();
            } else {
                Log.w(LOG_TAG, "Error getting chat session configuration. Chat will not be configured.");
            }
            if (this.mChatInitializationTimeout < 0) {
                Log.i(LOG_TAG, "Configured chat initialization timeout is below the minimum threshold. Will use default timeout");
                this.mChatInitializationTimeout = ChatSession.DEFAULT_CHAT_INITIALIZATION_TIMEOUT;
            }
            if (this.mChatSessionTimeout < 0) {
                Log.i(LOG_TAG, "Configured chat session timeout is below the minimum threshold. Will use default timeout");
                this.mChatSessionTimeout = ChatSession.DEFAULT_CHAT_SESSION_TIMEOUT;
            }
            configureInitializationTimeout(true);
            this.mKeepAliveRunner = Executors.newSingleThreadScheduledExecutor().scheduleAtFixedRate(new C1141b(this), 1, 1, TimeUnit.MINUTES);
            mChat.mo20636a(stringExtra, stringExtra2, this.mTitle, this.mReferrer);
            Log.v(LOG_TAG, "Chat service started");
            return 1;
        } else {
            Logger.m575i(LOG_TAG, "Chat service already running and initialized, no need to re-initialize the web widget");
            return 1;
        }
    }

    public void resend(String str) {
        if (canCommunicate()) {
            mChat.resend(str);
            prepareTimeout();
            return;
        }
        Log.v(LOG_TAG, "Unable to re-send message at the moment.");
    }

    public void resetTimeout() {
        prepareTimeout();
    }

    public void send(File file) {
        if (canCommunicate()) {
            C1134a find = FileTransfers.INSTANCE.find(file);
            if (find == null || find.f659b != C1135b.f664e) {
                mChat.send(file);
                return;
            }
            Logger.m577v(LOG_TAG, "Re-sending file");
            find.f659b = C1135b.f661b;
            LivechatChatLogPath.getInstance().broadcast();
            return;
        }
        this.mUnsentFiles.add(file);
    }

    public void send(String str) {
        if (canCommunicate()) {
            mChat.send(str);
            prepareTimeout();
            return;
        }
        Log.v(LOG_TAG, "Unable to send message at the moment. Caching it for resending.");
        this.mUnsentMessages.add(str);
    }

    public void sendChatComment(@NonNull String str) {
        if (canCommunicate()) {
            mChat.sendChatComment(str);
        }
    }

    public void sendChatRating(@NonNull Rating rating) {
        if (canCommunicate()) {
            mChat.sendChatRating(rating);
        }
    }

    public boolean sendOfflineMessage(String str, String str2, String str3) {
        if (canCommunicate()) {
            return mChat.sendOfflineMessage(str, str2, str3);
        }
        return false;
    }

    public void setDepartment(String str) {
        if (str != null) {
            mChat.setDepartment(str);
        }
    }

    public void setEmail(String str) {
        if (str != null) {
            mChat.setEmail(str);
            VisitorInfo visitorInfo = Storage.visitorInfo().getVisitorInfo();
            if (visitorInfo == null) {
                visitorInfo = new Builder().email(str).build();
            } else {
                visitorInfo.setEmail(str);
            }
            Storage.visitorInfo().setVisitorInfo(visitorInfo);
            ZopimChat.setVisitorInfo(visitorInfo);
        }
    }

    public void setName(String str) {
        if (str != null) {
            mChat.setName(str);
            VisitorInfo visitorInfo = Storage.visitorInfo().getVisitorInfo();
            if (visitorInfo == null) {
                visitorInfo = new Builder().name(str).build();
            } else {
                visitorInfo.setName(str);
            }
            Storage.visitorInfo().setVisitorInfo(visitorInfo);
            ZopimChat.setVisitorInfo(visitorInfo);
        }
    }

    public void setPhoneNumber(String str) {
        if (str != null) {
            mChat.setPhoneNumber(str);
            VisitorInfo visitorInfo = Storage.visitorInfo().getVisitorInfo();
            if (visitorInfo == null) {
                visitorInfo = new Builder().phoneNumber(str).build();
            } else {
                visitorInfo.setPhoneNumber(str);
            }
            Storage.visitorInfo().setVisitorInfo(visitorInfo);
            ZopimChat.setVisitorInfo(visitorInfo);
        }
    }
}
