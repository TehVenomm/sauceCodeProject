package com.zopim.android.sdk.widget;

import android.animation.Animator;
import android.animation.AnimatorSet;
import android.annotation.TargetApi;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.content.res.Resources;
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
import com.zopim.android.sdk.C0785R;
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
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import java.util.Timer;
import java.util.TimerTask;

public class ChatWidgetService extends Service {
    private static final int ANIMATION_FRAME_RATE = 30;
    private static final int DEFAULT_WIDGET_HEIGHT_DP = 50;
    private static final int DEFAULT_WIDGET_WIDTH_DP = 50;
    private static final String LOG_TAG = ChatWidgetService.class.getSimpleName();
    private static final int WIDGET_INIT_DELAY = 150;
    AgentsObserver mAgentsObserver = new C0905c(this);
    private Handler mAnimationHandler = new Handler(Looper.getMainLooper());
    private final IBinder mBinder = new LocalBinder();
    ChatLogObserver mChannelLogObserver = new C0908f(this);
    private AnimatorSet mCrossfadeAnimator;
    private int mHorizontalMargin;
    private int mInitialAgentMessageCount;
    private double mOffsetX;
    private double mOffsetY;
    private LayoutParams mRootLayoutParams;
    private Timer mTimer;
    private TypingIndicatorView mTypingIndicatorView;
    private int mUnreadCount;
    private TextView mUnreadNotificationView;
    private int mVerticalMargin;
    private C0901a mWidgetAnimatorTask;
    private ImageView mWidgetBackground;
    private WidgetView mWidgetView;
    private WindowManager mWindowManager;

    public class LocalBinder extends Binder {
        public ChatWidgetService getService() {
            return ChatWidgetService.this;
        }
    }

    /* renamed from: com.zopim.android.sdk.widget.ChatWidgetService$a */
    private class C0901a extends TimerTask {
        /* renamed from: a */
        int f917a;
        /* renamed from: b */
        int f918b;
        /* renamed from: c */
        int f919c;
        /* renamed from: d */
        int f920d;
        /* renamed from: e */
        final /* synthetic */ ChatWidgetService f921e;

        public C0901a(ChatWidgetService chatWidgetService, int i, int i2) {
            int i3 = 0;
            this.f921e = chatWidgetService;
            this.f917a = i < 0 ? 0 : i;
            if (i2 >= 0) {
                i3 = i2;
            }
            this.f918b = i3;
            int i4 = chatWidgetService.getApplicationContext().getResources().getDisplayMetrics().heightPixels;
            i3 = chatWidgetService.getApplicationContext().getResources().getDisplayMetrics().widthPixels;
            int width = (i3 - chatWidgetService.mWidgetView.getWidth()) - i;
            int a = (((m709a() + i4) - m710b()) - chatWidgetService.mWidgetView.getHeight()) - i2;
            int abs = Math.abs(chatWidgetService.mRootLayoutParams.x - i);
            int abs2 = Math.abs(chatWidgetService.mRootLayoutParams.x - width);
            int abs3 = Math.abs(chatWidgetService.mRootLayoutParams.y - i2);
            int abs4 = Math.abs(chatWidgetService.mRootLayoutParams.y - a);
            if (Math.min(abs3, abs4) < Math.min(abs, abs2)) {
                if (abs3 < abs4) {
                    this.f920d = i2;
                } else {
                    this.f920d = a;
                }
                if (chatWidgetService.mRootLayoutParams.x < i) {
                    this.f919c = i;
                } else if (chatWidgetService.mRootLayoutParams.x > width) {
                    this.f919c = width;
                } else {
                    this.f919c = chatWidgetService.mRootLayoutParams.x;
                }
            } else {
                if (abs < abs2) {
                    this.f919c = i;
                } else {
                    this.f919c = width;
                }
                if (chatWidgetService.mRootLayoutParams.y < i2) {
                    this.f920d = i2;
                } else if (chatWidgetService.mRootLayoutParams.y > a) {
                    this.f920d = a;
                } else {
                    this.f920d = chatWidgetService.mRootLayoutParams.y;
                }
            }
            chatWidgetService.mOffsetX = (double) ((this.f919c * 100) / i3);
            chatWidgetService.mOffsetY = (double) ((this.f920d * 100) / i4);
        }

        /* renamed from: a */
        public int m709a() {
            int identifier = this.f921e.getResources().getIdentifier("status_bar_height", "dimen", AbstractSpiCall.ANDROID_CLIENT_TYPE);
            return identifier > 0 ? this.f921e.getResources().getDimensionPixelSize(identifier) : 0;
        }

        /* renamed from: b */
        int m710b() {
            Resources resources = this.f921e.getBaseContext().getResources();
            int identifier = resources.getIdentifier(this.f921e.getResources().getConfiguration().orientation == 1 ? "navigation_bar_height" : "navigation_bar_height_landscape", "dimen", AbstractSpiCall.ANDROID_CLIENT_TYPE);
            return identifier > 0 ? resources.getDimensionPixelSize(identifier) : 0;
        }

        public void run() {
            this.f921e.mAnimationHandler.post(new C0911i(this));
        }
    }

    /* renamed from: com.zopim.android.sdk.widget.ChatWidgetService$b */
    private class C0902b implements OnTouchListener {
        /* renamed from: a */
        long f922a;
        /* renamed from: b */
        final /* synthetic */ ChatWidgetService f923b;
        /* renamed from: c */
        private final int f924c;
        /* renamed from: d */
        private float f925d;
        /* renamed from: e */
        private float f926e;
        /* renamed from: f */
        private float f927f;
        /* renamed from: g */
        private float f928g;

        private C0902b(ChatWidgetService chatWidgetService) {
            this.f923b = chatWidgetService;
            this.f924c = ViewConfiguration.get(this.f923b.getApplicationContext()).getScaledTouchSlop();
        }

        /* renamed from: a */
        void m711a() {
            Logger.m564v(ChatWidgetService.LOG_TAG, "onClick() chat widget");
            Log.i(ChatWidgetService.LOG_TAG, "Broadcasting intent action zopim.action.RESUME_CHAT to resume a chat activity");
            Intent intent = new Intent();
            intent.setAction(ChatActions.ACTION_RESUME_CHAT);
            intent.addCategory("android.intent.category.DEFAULT");
            intent.addFlags(DriveFile.MODE_READ_WRITE);
            intent.setPackage(this.f923b.getApplicationContext().getPackageName());
            this.f923b.startActivity(intent);
            this.f923b.stopSelf();
        }

        public boolean onTouch(View view, MotionEvent motionEvent) {
            int actionMasked = motionEvent.getActionMasked();
            float rawX = motionEvent.getRawX();
            float rawY = motionEvent.getRawY();
            float abs;
            switch (actionMasked) {
                case 0:
                    this.f922a = SystemClock.elapsedRealtime();
                    if (this.f923b.mWidgetAnimatorTask != null) {
                        this.f923b.mWidgetAnimatorTask.cancel();
                        this.f923b.mTimer.cancel();
                    }
                    this.f925d = rawX;
                    this.f926e = rawY;
                    this.f927f = rawX;
                    this.f928g = rawY;
                    return true;
                case 1:
                case 3:
                    if (SystemClock.elapsedRealtime() - this.f922a < 200) {
                        abs = Math.abs(rawX - this.f925d);
                        rawX = Math.abs(rawY - this.f926e);
                        if (((int) Math.sqrt((double) ((abs * abs) + (rawX * rawX)))) < this.f924c) {
                            m711a();
                            return false;
                        }
                    }
                    this.f923b.mWidgetAnimatorTask = new C0901a(this.f923b, this.f923b.mHorizontalMargin, this.f923b.mVerticalMargin);
                    this.f923b.mTimer = new Timer();
                    this.f923b.mTimer.schedule(this.f923b.mWidgetAnimatorTask, 0, 30);
                    return true;
                case 2:
                    float f = rawX - this.f927f;
                    abs = rawY - this.f928g;
                    LayoutParams access$400 = this.f923b.mRootLayoutParams;
                    access$400.x = (int) (f + ((float) access$400.x));
                    LayoutParams access$4002 = this.f923b.mRootLayoutParams;
                    access$4002.y = (int) (abs + ((float) access$4002.y));
                    this.f927f = rawX;
                    this.f928g = rawY;
                    this.f923b.mWindowManager.updateViewLayout(this.f923b.mWidgetView, this.f923b.mRootLayoutParams);
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

    @TargetApi(11)
    private void showUnreadNotification() {
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
        this.mWidgetAnimatorTask = new C0901a(this, this.mHorizontalMargin, this.mVerticalMargin);
        this.mTimer = new Timer();
        this.mTimer.schedule(this.mWidgetAnimatorTask, 0, 30);
    }

    @TargetApi(11)
    public void onCreate() {
        if (hasSystemAlertWindowPermission(this)) {
            int i = getApplicationContext().getResources().getDisplayMetrics().heightPixels;
            int i2 = getApplicationContext().getResources().getDisplayMetrics().widthPixels;
            try {
                this.mHorizontalMargin = getResources().getDimensionPixelSize(C0785R.dimen.widget_horizontal_margin);
                this.mVerticalMargin = getResources().getDimensionPixelSize(C0785R.dimen.widget_vertical_margin);
            } catch (Throwable e) {
                Log.w(LOG_TAG, "Could not find margin resources. Will use zero margin", e);
                this.mHorizontalMargin = 0;
                this.mVerticalMargin = 0;
            }
            this.mWindowManager = (WindowManager) getSystemService("window");
            this.mWidgetView = (WidgetView) LayoutInflater.from(this).inflate(C0785R.layout.chat_widget, null);
            this.mTypingIndicatorView = (TypingIndicatorView) this.mWidgetView.findViewById(C0785R.id.typing_indicator);
            this.mUnreadNotificationView = (TextView) this.mWidgetView.findViewById(C0785R.id.unread_notification);
            this.mWidgetBackground = (ImageView) this.mWidgetView.findViewById(C0785R.id.background);
            if (VERSION.SDK_INT >= 11) {
                Animator crossfade = AnimatorPack.crossfade(this.mWidgetBackground, this.mWidgetBackground);
                Animator crossfade2 = AnimatorPack.crossfade(this.mTypingIndicatorView, this.mUnreadNotificationView);
                this.mCrossfadeAnimator = new AnimatorSet();
                this.mCrossfadeAnimator.play(crossfade).with(crossfade2);
                this.mCrossfadeAnimator.addListener(new C0903a(this));
                long integer = (long) getResources().getInteger(C0785R.integer.crossfade_duration);
                if (integer > 0) {
                    this.mCrossfadeAnimator.setDuration(integer);
                }
            }
            this.mWidgetView.setOnTouchListener(new C0902b());
            int convertDpToPixel = Dimensions.convertDpToPixel(this, 50.0f);
            int convertDpToPixel2 = Dimensions.convertDpToPixel(this, 50.0f);
            try {
                convertDpToPixel = getResources().getDimensionPixelSize(C0785R.dimen.widget_width);
                convertDpToPixel2 = getResources().getDimensionPixelSize(C0785R.dimen.widget_height);
            } catch (Throwable e2) {
                Log.w(LOG_TAG, "Could not find widget size resources. Will use default size.", e2);
            }
            this.mRootLayoutParams = new LayoutParams(convertDpToPixel, convertDpToPixel2, 2002, 520, -3);
            this.mRootLayoutParams.gravity = 51;
            this.mWindowManager.addView(this.mWidgetView, this.mRootLayoutParams);
            this.mWidgetView.postDelayed(new C0904b(this, i2, i), 150);
            return;
        }
        Log.i(LOG_TAG, "Not presenting chat widget. Missing permission SYSTEM_ALERT_WINDOW");
        stopSelf();
    }

    public void onDestroy() {
        Logger.m564v(LOG_TAG, "Destroying Widget UI");
        if (this.mWidgetView != null) {
            this.mWindowManager.removeView(this.mWidgetView);
        }
        ZopimChat.getDataSource().deleteAgentsObserver(this.mAgentsObserver);
        ZopimChat.getDataSource().deleteChatLogObserver(this.mChannelLogObserver);
    }

    public int onStartCommand(Intent intent, int i, int i2) {
        Logger.m564v(LOG_TAG, "Starting Widget UI");
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
