package com.zopim.android.sdk.widget;

import android.animation.AnimatorSet;
import android.annotation.TargetApi;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.content.res.Resources.NotFoundException;
import android.os.Binder;
import android.os.Build.VERSION;
import android.os.Handler;
import android.os.IBinder;
import android.os.Looper;
import android.os.SystemClock;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.view.ViewConfiguration;
import android.view.WindowManager;
import android.view.WindowManager.LayoutParams;
import android.widget.ImageView;
import android.widget.TextView;
import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.games.GamesStatusCodes;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.anim.AnimatorPack;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.chatlog.view.TypingIndicatorView;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.embeddable.ChatActions;
import com.zopim.android.sdk.model.ChatLog.Type;
import com.zopim.android.sdk.util.Dimensions;
import com.zopim.android.sdk.widget.view.WidgetView;
import java.util.Timer;
import java.util.TimerTask;

public class ChatWidgetService extends Service {
    private static final int ANIMATION_FRAME_RATE = 30;
    private static final int DEFAULT_WIDGET_HEIGHT_DP = 50;
    private static final int DEFAULT_WIDGET_WIDTH_DP = 50;
    /* access modifiers changed from: private */
    public static final String LOG_TAG = ChatWidgetService.class.getSimpleName();
    private static final int WIDGET_INIT_DELAY = 150;
    AgentsObserver mAgentsObserver = new C1274c(this);
    /* access modifiers changed from: private */
    public Handler mAnimationHandler = new Handler(Looper.getMainLooper());
    private final IBinder mBinder = new LocalBinder();
    ChatLogObserver mChannelLogObserver = new C1277f(this);
    /* access modifiers changed from: private */
    public AnimatorSet mCrossfadeAnimator;
    /* access modifiers changed from: private */
    public int mHorizontalMargin;
    /* access modifiers changed from: private */
    public int mInitialAgentMessageCount;
    /* access modifiers changed from: private */
    public double mOffsetX;
    /* access modifiers changed from: private */
    public double mOffsetY;
    /* access modifiers changed from: private */
    public LayoutParams mRootLayoutParams;
    /* access modifiers changed from: private */
    public Timer mTimer;
    /* access modifiers changed from: private */
    public TypingIndicatorView mTypingIndicatorView;
    /* access modifiers changed from: private */
    public int mUnreadCount;
    /* access modifiers changed from: private */
    public TextView mUnreadNotificationView;
    /* access modifiers changed from: private */
    public int mVerticalMargin;
    /* access modifiers changed from: private */
    public C1270a mWidgetAnimatorTask;
    private ImageView mWidgetBackground;
    /* access modifiers changed from: private */
    public WidgetView mWidgetView;
    /* access modifiers changed from: private */
    public WindowManager mWindowManager;

    public class LocalBinder extends Binder {
        public LocalBinder() {
        }

        public ChatWidgetService getService() {
            return ChatWidgetService.this;
        }
    }

    /* renamed from: com.zopim.android.sdk.widget.ChatWidgetService$a */
    private class C1270a extends TimerTask {

        /* renamed from: a */
        int f961a;

        /* renamed from: b */
        int f962b;

        /* renamed from: c */
        int f963c;

        /* renamed from: d */
        int f964d;

        /* renamed from: e */
        final /* synthetic */ ChatWidgetService f965e;

        public C1270a(ChatWidgetService chatWidgetService, int i, int i2) {
            int i3 = 0;
            this.f965e = chatWidgetService;
            this.f961a = i < 0 ? 0 : i;
            if (i2 >= 0) {
                i3 = i2;
            }
            this.f962b = i3;
            int i4 = chatWidgetService.getApplicationContext().getResources().getDisplayMetrics().heightPixels;
            int i5 = chatWidgetService.getApplicationContext().getResources().getDisplayMetrics().widthPixels;
            int width = (i5 - chatWidgetService.mWidgetView.getWidth()) - i;
            int a = (((mo20947a() + i4) - mo20948b()) - chatWidgetService.mWidgetView.getHeight()) - i2;
            int abs = Math.abs(chatWidgetService.mRootLayoutParams.x - i);
            int abs2 = Math.abs(chatWidgetService.mRootLayoutParams.x - width);
            int abs3 = Math.abs(chatWidgetService.mRootLayoutParams.y - i2);
            int abs4 = Math.abs(chatWidgetService.mRootLayoutParams.y - a);
            if (Math.min(abs3, abs4) < Math.min(abs, abs2)) {
                if (abs3 < abs4) {
                    this.f964d = i2;
                } else {
                    this.f964d = a;
                }
                if (chatWidgetService.mRootLayoutParams.x < i) {
                    this.f963c = i;
                } else if (chatWidgetService.mRootLayoutParams.x > width) {
                    this.f963c = width;
                } else {
                    this.f963c = chatWidgetService.mRootLayoutParams.x;
                }
            } else {
                if (abs < abs2) {
                    this.f963c = i;
                } else {
                    this.f963c = width;
                }
                if (chatWidgetService.mRootLayoutParams.y < i2) {
                    this.f964d = i2;
                } else if (chatWidgetService.mRootLayoutParams.y > a) {
                    this.f964d = a;
                } else {
                    this.f964d = chatWidgetService.mRootLayoutParams.y;
                }
            }
            chatWidgetService.mOffsetX = (double) ((this.f963c * 100) / i5);
            chatWidgetService.mOffsetY = (double) ((this.f964d * 100) / i4);
        }

        /* renamed from: a */
        public int mo20947a() {
            int identifier = this.f965e.getResources().getIdentifier("status_bar_height", "dimen", "android");
            if (identifier > 0) {
                return this.f965e.getResources().getDimensionPixelSize(identifier);
            }
            return 0;
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: b */
        public int mo20948b() {
            Resources resources = this.f965e.getBaseContext().getResources();
            int identifier = resources.getIdentifier(this.f965e.getResources().getConfiguration().orientation == 1 ? "navigation_bar_height" : "navigation_bar_height_landscape", "dimen", "android");
            if (identifier > 0) {
                return resources.getDimensionPixelSize(identifier);
            }
            return 0;
        }

        public void run() {
            this.f965e.mAnimationHandler.post(new C1280i(this));
        }
    }

    /* renamed from: com.zopim.android.sdk.widget.ChatWidgetService$b */
    private class C1271b implements OnTouchListener {

        /* renamed from: a */
        long f966a;

        /* renamed from: c */
        private final int f968c;

        /* renamed from: d */
        private float f969d;

        /* renamed from: e */
        private float f970e;

        /* renamed from: f */
        private float f971f;

        /* renamed from: g */
        private float f972g;

        private C1271b() {
            this.f968c = ViewConfiguration.get(ChatWidgetService.this.getApplicationContext()).getScaledTouchSlop();
        }

        /* synthetic */ C1271b(ChatWidgetService chatWidgetService, C1272a aVar) {
            this();
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: a */
        public void mo20950a() {
            Logger.m577v(ChatWidgetService.LOG_TAG, "onClick() chat widget");
            Log.i(ChatWidgetService.LOG_TAG, "Broadcasting intent action zopim.action.RESUME_CHAT to resume a chat activity");
            Intent intent = new Intent();
            intent.setAction(ChatActions.ACTION_RESUME_CHAT);
            intent.addCategory("android.intent.category.DEFAULT");
            intent.addFlags(DriveFile.MODE_READ_WRITE);
            intent.setPackage(ChatWidgetService.this.getApplicationContext().getPackageName());
            ChatWidgetService.this.startActivity(intent);
            ChatWidgetService.this.stopSelf();
        }

        public boolean onTouch(View view, MotionEvent motionEvent) {
            int actionMasked = motionEvent.getActionMasked();
            float rawX = motionEvent.getRawX();
            float rawY = motionEvent.getRawY();
            switch (actionMasked) {
                case 0:
                    this.f966a = SystemClock.elapsedRealtime();
                    if (ChatWidgetService.this.mWidgetAnimatorTask != null) {
                        ChatWidgetService.this.mWidgetAnimatorTask.cancel();
                        ChatWidgetService.this.mTimer.cancel();
                    }
                    this.f969d = rawX;
                    this.f970e = rawY;
                    this.f971f = rawX;
                    this.f972g = rawY;
                    return true;
                case 1:
                case 3:
                    if (SystemClock.elapsedRealtime() - this.f966a < 200) {
                        float abs = Math.abs(rawX - this.f969d);
                        float abs2 = Math.abs(rawY - this.f970e);
                        if (((int) Math.sqrt((double) ((abs * abs) + (abs2 * abs2)))) < this.f968c) {
                            mo20950a();
                            return false;
                        }
                    }
                    ChatWidgetService.this.mWidgetAnimatorTask = new C1270a(ChatWidgetService.this, ChatWidgetService.this.mHorizontalMargin, ChatWidgetService.this.mVerticalMargin);
                    ChatWidgetService.this.mTimer = new Timer();
                    ChatWidgetService.this.mTimer.schedule(ChatWidgetService.this.mWidgetAnimatorTask, 0, 30);
                    return true;
                case 2:
                    float f = rawX - this.f971f;
                    float f2 = rawY - this.f972g;
                    LayoutParams access$400 = ChatWidgetService.this.mRootLayoutParams;
                    access$400.x = (int) (f + ((float) access$400.x));
                    LayoutParams access$4002 = ChatWidgetService.this.mRootLayoutParams;
                    access$4002.y = (int) (f2 + ((float) access$4002.y));
                    this.f971f = rawX;
                    this.f972g = rawY;
                    ChatWidgetService.this.mWindowManager.updateViewLayout(ChatWidgetService.this.mWidgetView, ChatWidgetService.this.mRootLayoutParams);
                    return true;
                default:
                    return false;
            }
        }
    }

    @TargetApi(19)
    private boolean hasSystemAlertWindowPermission(Context context) {
        return VERSION.SDK_INT >= 19 ? context.checkCallingOrSelfPermission("android.permission.SYSTEM_ALERT_WINDOW") == 0 : context.checkCallingOrSelfPermission("android.permission.SYSTEM_ALERT_WINDOW") == 0;
    }

    /* access modifiers changed from: private */
    @TargetApi(11)
    public void showUnreadNotification() {
        if (VERSION.SDK_INT >= 11) {
            this.mCrossfadeAnimator.start();
            return;
        }
        this.mTypingIndicatorView.setVisibility(4);
        this.mUnreadNotificationView.setVisibility(0);
    }

    public IBinder onBind(Intent intent) {
        return this.mBinder;
    }

    public void onConfigurationChanged(Configuration configuration) {
        super.onConfigurationChanged(configuration);
        int i = getApplicationContext().getResources().getDisplayMetrics().widthPixels;
        int i2 = getApplicationContext().getResources().getDisplayMetrics().heightPixels;
        this.mRootLayoutParams.x = (int) ((this.mOffsetX / 100.0d) * ((double) i));
        this.mRootLayoutParams.y = (int) ((this.mOffsetY / 100.0d) * ((double) i2));
        this.mWidgetAnimatorTask = new C1270a(this, this.mHorizontalMargin, this.mVerticalMargin);
        this.mTimer = new Timer();
        this.mTimer.schedule(this.mWidgetAnimatorTask, 0, 30);
    }

    @TargetApi(11)
    public void onCreate() {
        if (!hasSystemAlertWindowPermission(this)) {
            Log.i(LOG_TAG, "Not presenting chat widget. Missing permission SYSTEM_ALERT_WINDOW");
            stopSelf();
            return;
        }
        int i = getApplicationContext().getResources().getDisplayMetrics().heightPixels;
        int i2 = getApplicationContext().getResources().getDisplayMetrics().widthPixels;
        try {
            this.mHorizontalMargin = getResources().getDimensionPixelSize(C1122R.dimen.widget_horizontal_margin);
            this.mVerticalMargin = getResources().getDimensionPixelSize(C1122R.dimen.widget_vertical_margin);
        } catch (NotFoundException e) {
            Log.w(LOG_TAG, "Could not find margin resources. Will use zero margin", e);
            this.mHorizontalMargin = 0;
            this.mVerticalMargin = 0;
        }
        this.mWindowManager = (WindowManager) getSystemService("window");
        this.mWidgetView = (WidgetView) LayoutInflater.from(this).inflate(C1122R.C1126layout.chat_widget, null);
        this.mTypingIndicatorView = (TypingIndicatorView) this.mWidgetView.findViewById(C1122R.C1125id.typing_indicator);
        this.mUnreadNotificationView = (TextView) this.mWidgetView.findViewById(C1122R.C1125id.unread_notification);
        this.mWidgetBackground = (ImageView) this.mWidgetView.findViewById(C1122R.C1125id.background);
        if (VERSION.SDK_INT >= 11) {
            AnimatorSet crossfade = AnimatorPack.crossfade(this.mWidgetBackground, this.mWidgetBackground);
            AnimatorSet crossfade2 = AnimatorPack.crossfade(this.mTypingIndicatorView, this.mUnreadNotificationView);
            this.mCrossfadeAnimator = new AnimatorSet();
            this.mCrossfadeAnimator.play(crossfade).with(crossfade2);
            this.mCrossfadeAnimator.addListener(new C1272a(this));
            long integer = (long) getResources().getInteger(C1122R.integer.crossfade_duration);
            if (integer > 0) {
                this.mCrossfadeAnimator.setDuration(integer);
            }
        }
        this.mWidgetView.setOnTouchListener(new C1271b(this, null));
        int convertDpToPixel = Dimensions.convertDpToPixel(this, 50.0f);
        int convertDpToPixel2 = Dimensions.convertDpToPixel(this, 50.0f);
        try {
            convertDpToPixel = getResources().getDimensionPixelSize(C1122R.dimen.widget_width);
            convertDpToPixel2 = getResources().getDimensionPixelSize(C1122R.dimen.widget_height);
        } catch (NotFoundException e2) {
            Log.w(LOG_TAG, "Could not find widget size resources. Will use default size.", e2);
        }
        this.mRootLayoutParams = new LayoutParams(convertDpToPixel, convertDpToPixel2, GamesStatusCodes.STATUS_REQUEST_TOO_MANY_RECIPIENTS, 520, -3);
        this.mRootLayoutParams.gravity = 51;
        this.mWindowManager.addView(this.mWidgetView, this.mRootLayoutParams);
        this.mWidgetView.postDelayed(new C1273b(this, i2, i), 150);
    }

    public void onDestroy() {
        Logger.m577v(LOG_TAG, "Destroying Widget UI");
        if (this.mWidgetView != null) {
            this.mWindowManager.removeView(this.mWidgetView);
        }
        ZopimChat.getDataSource().deleteAgentsObserver(this.mAgentsObserver);
        ZopimChat.getDataSource().deleteChatLogObserver(this.mChannelLogObserver);
    }

    public int onStartCommand(Intent intent, int i, int i2) {
        Logger.m577v(LOG_TAG, "Starting Widget UI");
        if (ChatActions.ACTION_STOP_WIDGET_SERVICE.equals(intent.getAction())) {
            stopSelf();
        } else {
            this.mUnreadCount = 0;
            this.mInitialAgentMessageCount = LivechatChatLogPath.getInstance().countMessages(Type.CHAT_MSG_AGENT);
            ZopimChat.getDataSource().addAgentsObserver(this.mAgentsObserver);
            ZopimChat.getDataSource().addChatLogObserver(this.mChannelLogObserver);
        }
        return 2;
    }
}
