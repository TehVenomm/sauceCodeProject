package net.gogame.gowrap.ui.fab;

import android.animation.Animator;
import android.animation.Animator.AnimatorListener;
import android.animation.AnimatorSet;
import android.animation.ObjectAnimator;
import android.animation.ValueAnimator;
import android.animation.ValueAnimator.AnimatorUpdateListener;
import android.annotation.TargetApi;
import android.app.Activity;
import android.graphics.Rect;
import android.os.Build.VERSION;
import android.os.Handler;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.PopupWindow;
import android.widget.RelativeLayout;
import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;
import net.gogame.gowrap.C1110R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.ui.utils.DisplayUtils;
import net.gogame.gowrap.wrapper.OverlayUIHelper;

public class PopupWindowFab extends AbstractFab {
    private static final int MAX_RETRIES = 4;
    private static final int RETRY_MS = 500;
    private AnimatorSet animatorSet = null;
    private int fabEndPositionX = 0;
    private int fabStartPositionX = 0;
    private boolean fixX;
    private boolean fixY;
    private Handler handler = null;
    private boolean isFabLeft = true;
    private boolean isFirstTime = false;
    private Integer mPosX = null;
    private Integer mPosY = null;
    private int offsetX = 0;
    private int offsetY = 0;
    private PopupWindow popupWindow = null;
    private int retries = 0;
    private int screenBottomHeightLimit = 0;
    private int screenHeight = 0;
    private int screenTopHeightLimit = 0;
    private int screenWidth = 0;
    private final PopupWindowFab self = this;
    private boolean slideIn = false;
    private boolean slideOut = false;
    private Timer timer;
    private TimerTask timerTask;
    private final Runnable updateFab = new C11721();
    private final Runnable updatePopupWindowRunnable = new C11732();
    private View view = null;

    /* renamed from: net.gogame.gowrap.ui.fab.PopupWindowFab$1 */
    class C11721 implements Runnable {
        C11721() {
        }

        public void run() {
            try {
                if (PopupWindowFab.this.view == null) {
                    return;
                }
                if (Wrapper.INSTANCE.isServerDown()) {
                    ((RelativeLayout) PopupWindowFab.this.view).getChildAt(1).setVisibility(0);
                    PopupWindowFab.this.self.fixY = true;
                    PopupWindowFab.this.self.fixX = true;
                    return;
                }
                ((RelativeLayout) PopupWindowFab.this.view).getChildAt(1).setVisibility(8);
                PopupWindowFab.this.self.fixY = false;
                PopupWindowFab.this.self.fixX = false;
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.fab.PopupWindowFab$2 */
    class C11732 implements Runnable {
        C11732() {
        }

        public void run() {
            try {
                if (PopupWindowFab.this.popupWindow != null) {
                    PopupWindowFab.this.popupWindow.update(PopupWindowFab.this.getX(), PopupWindowFab.this.getY(), -1, -1);
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public PopupWindowFab(boolean z, boolean z2) {
        this.fixX = z;
        this.fixY = z2;
    }

    private int getX() {
        return this.mPosX.intValue() + this.offsetX;
    }

    private int getY() {
        return this.mPosY.intValue() + this.offsetY;
    }

    private Handler getHandler(Activity activity) {
        if (this.handler == null || this.handler.getLooper() != activity.getMainLooper()) {
            this.handler = new Handler(activity.getMainLooper());
        }
        return this.handler;
    }

    private void post(Activity activity, Runnable runnable) {
        getHandler(activity).post(runnable);
    }

    private void postDelayed(Activity activity, Runnable runnable, long j) {
        getHandler(activity).postDelayed(runnable, j);
    }

    public boolean handleTouchEvent(MotionEvent motionEvent) {
        if (new Rect(getX(), getY(), getX() + this.view.getWidth(), getY() + this.view.getHeight()).contains((int) motionEvent.getX(), (int) motionEvent.getY())) {
            return this.view.dispatchTouchEvent(motionEvent);
        }
        return false;
    }

    public void update(Activity activity) {
        activity.runOnUiThread(this.updateFab);
    }

    private void setTimerForFab(final Activity activity) {
        this.timer = new Timer();
        this.timerTask = new TimerTask() {

            /* renamed from: net.gogame.gowrap.ui.fab.PopupWindowFab$3$1 */
            class C11751 implements Runnable {

                /* renamed from: net.gogame.gowrap.ui.fab.PopupWindowFab$3$1$1 */
                class C11741 implements Runnable {
                    C11741() {
                    }

                    public void run() {
                        try {
                            if (PopupWindowFab.this.view != null) {
                                if (PopupWindowFab.this.view.getWidth() > 0) {
                                    PopupWindowFab.this.animatorSet = PopupWindowFab.this.getBlinkingAnimation();
                                    PopupWindowFab.this.animatorSet.start();
                                    return;
                                }
                                PopupWindowFab.this.postDelayed(activity, this, 100);
                            }
                        } catch (Throwable e) {
                            Log.e(Constants.TAG, "Exception", e);
                        }
                    }
                }

                C11751() {
                }

                public void run() {
                    PopupWindowFab.this.postDelayed(activity, new C11741(), 100);
                }
            }

            public void run() {
                PopupWindowFab.this.post(activity, new C11751());
            }
        };
        this.timer.schedule(this.timerTask, 60000, 60000);
    }

    public void show(final Activity activity) {
        if (this.popupWindow == null) {
            View view;
            cancelTimer();
            DisplayMetrics displayMetrics = new DisplayMetrics();
            activity.getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);
            this.screenHeight = displayMetrics.heightPixels;
            this.screenWidth = displayMetrics.widthPixels;
            this.screenTopHeightLimit = DisplayUtils.pxFromDp(activity, 40.0f);
            this.screenBottomHeightLimit = DisplayUtils.pxFromDp(activity, 50.0f);
            View rootView = OverlayUIHelper.getRootView(activity);
            View leafView = OverlayUIHelper.getLeafView(rootView);
            if (leafView == null) {
                this.retries++;
                if (this.retries <= 4) {
                    postDelayed(activity, new Runnable() {
                        public void run() {
                            try {
                                PopupWindowFab.this.show(activity);
                            } catch (Throwable e) {
                                Log.e(Constants.TAG, "Exception", e);
                            }
                        }
                    }, 500);
                    return;
                }
                view = rootView;
            } else {
                view = leafView;
            }
            this.retries = 0;
            OnTouchListener c11795 = new OnTouchListener() {
                private float initialTouchX;
                private float initialTouchY;
                private int initialX;
                private int initialY;
                private boolean isDrag;

                /* renamed from: net.gogame.gowrap.ui.fab.PopupWindowFab$5$1 */
                class C11781 implements Runnable {
                    C11781() {
                    }

                    public void run() {
                        try {
                            if (PopupWindowFab.this.view != null) {
                                if (PopupWindowFab.this.view.getWidth() > 0) {
                                    PopupWindowFab.this.animatorSet = PopupWindowFab.this.getInsideAnimation(activity, PopupWindowFab.this.view.getWidth());
                                    PopupWindowFab.this.animatorSet.start();
                                    return;
                                }
                                PopupWindowFab.this.postDelayed(activity, this, 100);
                            }
                        } catch (Throwable e) {
                            Log.e(Constants.TAG, "Exception", e);
                        }
                    }
                }

                public boolean onTouch(View view, MotionEvent motionEvent) {
                    int access$1100;
                    switch (motionEvent.getAction()) {
                        case 0:
                            this.initialX = PopupWindowFab.this.getX();
                            this.initialY = PopupWindowFab.this.getY();
                            this.initialTouchX = motionEvent.getRawX();
                            this.initialTouchY = motionEvent.getRawY();
                            this.isDrag = false;
                            PopupWindowFab.this.cancelTimer();
                            PopupWindowFab.this.cancelAnimation(activity);
                            if (PopupWindowFab.this.view != null && VERSION.SDK_INT >= 11) {
                                PopupWindowFab.this.view.setAlpha(1.0f);
                                break;
                            }
                        case 1:
                            access$1100 = PopupWindowFab.this.screenWidth / 2;
                            if (PopupWindowFab.this.mPosX.intValue() >= access$1100) {
                                PopupWindowFab.this.mPosX = Integer.valueOf(PopupWindowFab.this.screenWidth - PopupWindowFab.this.fabEndPositionX);
                                PopupWindowFab.this.isFabLeft = false;
                            } else if (PopupWindowFab.this.mPosX.intValue() < access$1100) {
                                PopupWindowFab.this.mPosX = Integer.valueOf(PopupWindowFab.this.fabStartPositionX);
                                PopupWindowFab.this.isFabLeft = true;
                            }
                            if (Wrapper.INSTANCE.isSlideOut() || Wrapper.INSTANCE.isSlideIn()) {
                                PopupWindowFab.this.cancelAnimation(activity);
                                PopupWindowFab.this.postDelayed(activity, new C11781(), 100);
                            }
                            PopupWindowFab.this.updateFabLocation(activity);
                            if (!this.isDrag) {
                                PopupWindowFab.this.fireClickListener(motionEvent);
                                break;
                            }
                            break;
                        case 2:
                            if (!PopupWindowFab.this.self.fixX) {
                                PopupWindowFab.this.mPosX = Integer.valueOf((this.initialX + ((int) (motionEvent.getRawX() - this.initialTouchX))) - PopupWindowFab.this.offsetX);
                            }
                            if (!PopupWindowFab.this.self.fixY) {
                                access$1100 = (this.initialY + ((int) (motionEvent.getRawY() - this.initialTouchY))) - PopupWindowFab.this.offsetY;
                                if (access$1100 > PopupWindowFab.this.screenTopHeightLimit && access$1100 < PopupWindowFab.this.screenHeight - PopupWindowFab.this.screenBottomHeightLimit) {
                                    PopupWindowFab.this.mPosY = Integer.valueOf(access$1100);
                                }
                            }
                            PopupWindowFab.this.updateFabLocation(activity);
                            access$1100 = PopupWindowFab.this.getX() - this.initialX;
                            int access$600 = PopupWindowFab.this.getY() - this.initialY;
                            if (!this.isDrag && (Math.abs(access$1100) > 10 || Math.abs(access$600) > 10)) {
                                this.isDrag = true;
                                break;
                            }
                    }
                    return false;
                }
            };
            this.view = createImageView(activity, C1110R.drawable.net_gogame_gowrap_fab);
            this.view.setOnTouchListener(c11795);
            int intrinsicWidth = activity.getResources().getDrawable(C1110R.drawable.net_gogame_gowrap_fab).getIntrinsicWidth();
            if (Wrapper.INSTANCE.isSlideOut() || Wrapper.INSTANCE.isSlideIn()) {
                this.fabStartPositionX = -((int) Math.round(((double) intrinsicWidth) * 0.65d));
                this.fabEndPositionX = (int) Math.round(((double) intrinsicWidth) * 1.3d);
            } else {
                this.fabStartPositionX = -((int) Math.round(((double) intrinsicWidth) * 0.2d));
                this.fabEndPositionX = (int) Math.round(((double) intrinsicWidth) * 0.8d);
            }
            WindowManager windowManager = (WindowManager) activity.getSystemService("window");
            final DisplayMetrics displayMetrics2 = new DisplayMetrics();
            windowManager.getDefaultDisplay().getMetrics(displayMetrics2);
            if (this.mPosX == null || this.mPosY == null) {
                this.mPosX = Integer.valueOf(this.fabStartPositionX);
                this.mPosY = Integer.valueOf(displayMetrics2.heightPixels / 2);
            }
            this.popupWindow = new PopupWindow(this.view, -2, -2, false);
            this.popupWindow.setClippingEnabled(false);
            final ViewGroup viewGroup = (ViewGroup) view.getParent();
            rootView.post(new Runnable() {

                /* renamed from: net.gogame.gowrap.ui.fab.PopupWindowFab$6$1 */
                class C11801 implements Runnable {
                    C11801() {
                    }

                    public void run() {
                        try {
                            if (PopupWindowFab.this.view != null) {
                                if (PopupWindowFab.this.view.getWidth() > 0) {
                                    PopupWindowFab.this.animatorSet = PopupWindowFab.this.getAnimation(activity, PopupWindowFab.this.view.getWidth());
                                    PopupWindowFab.this.animatorSet.start();
                                    return;
                                }
                                PopupWindowFab.this.postDelayed(activity, this, 100);
                            }
                        } catch (Throwable e) {
                            Log.e(Constants.TAG, "Exception", e);
                        }
                    }
                }

                public void run() {
                    try {
                        if (PopupWindowFab.this.popupWindow != null) {
                            if (Wrapper.INSTANCE.isSlideOut()) {
                                PopupWindowFab.this.offsetX = -displayMetrics2.widthPixels;
                            }
                            PopupWindowFab.this.popupWindow.showAtLocation(viewGroup, 0, PopupWindowFab.this.getX(), PopupWindowFab.this.getY());
                            if (Wrapper.INSTANCE.isSlideOut() || Wrapper.INSTANCE.isSlideIn()) {
                                PopupWindowFab.this.postDelayed(activity, new C11801(), 100);
                            }
                        }
                    } catch (Throwable e) {
                        Log.e(Constants.TAG, "Exception", e);
                    }
                }
            });
            this.popupWindow.setTouchInterceptor(c11795);
        }
    }

    private AnimatorListener getAnimationListener(final Activity activity) {
        return new AnimatorListener() {
            public void onAnimationStart(Animator animator) {
            }

            public void onAnimationEnd(Animator animator) {
                PopupWindowFab.this.setTimerForFab(activity);
            }

            public void onAnimationCancel(Animator animator) {
            }

            public void onAnimationRepeat(Animator animator) {
            }
        };
    }

    @TargetApi(11)
    private AnimatorSet getAnimation(final Activity activity, final int i) {
        int i2;
        AnimatorUpdateListener c11838 = new AnimatorUpdateListener() {
            public void onAnimationUpdate(ValueAnimator valueAnimator) {
                PopupWindowFab.this.offsetX = ((Integer) valueAnimator.getAnimatedValue()).intValue();
                if (PopupWindowFab.this.view != null) {
                    PopupWindowFab.this.view.setPadding(i / 2, 0, i / 2, 0);
                }
                PopupWindowFab.this.updateFabLocation(activity);
            }
        };
        List arrayList = new ArrayList();
        if (Wrapper.INSTANCE.isSlideOut()) {
            if (this.isFabLeft) {
                i2 = -i;
            } else {
                i2 = i;
            }
            ValueAnimator valueAnimator = new ValueAnimator();
            valueAnimator.setDuration(500);
            valueAnimator.setIntValues(new int[]{i2, 0});
            valueAnimator.addUpdateListener(c11838);
            arrayList.add(valueAnimator);
        }
        ValueAnimator valueAnimator2 = new ValueAnimator();
        valueAnimator2.setDuration(5000);
        valueAnimator2.setIntValues(new int[]{0, 0});
        valueAnimator2.addUpdateListener(c11838);
        arrayList.add(valueAnimator2);
        if (Wrapper.INSTANCE.isSlideIn()) {
            if (this.isFabLeft) {
                i2 = -((int) Math.ceil(((double) i) * 0.6d));
            } else {
                i2 = (int) Math.ceil(((double) i) * 0.6d);
            }
            valueAnimator = new ValueAnimator();
            valueAnimator.setDuration(500);
            valueAnimator.setIntValues(new int[]{0, i2});
            valueAnimator.addUpdateListener(c11838);
            valueAnimator.addListener(getAnimationListener(activity));
            arrayList.add(valueAnimator);
        }
        AnimatorSet animatorSet = new AnimatorSet();
        animatorSet.playSequentially((Animator[]) arrayList.toArray(new ValueAnimator[arrayList.size()]));
        return animatorSet;
    }

    @TargetApi(11)
    private AnimatorSet getBlinkingAnimation() {
        List arrayList = new ArrayList();
        if (VERSION.SDK_INT >= 14) {
            ValueAnimator ofFloat = ObjectAnimator.ofFloat(this.view, View.ALPHA, new float[]{0.0f, 1.0f});
            ofFloat.setDuration(250);
            ofFloat.setRepeatCount(10);
            ofFloat.setRepeatMode(2);
            arrayList.add(ofFloat);
        }
        AnimatorSet animatorSet = new AnimatorSet();
        animatorSet.playSequentially((Animator[]) arrayList.toArray(new ValueAnimator[arrayList.size()]));
        return animatorSet;
    }

    @TargetApi(11)
    private AnimatorSet getInsideAnimation(final Activity activity, int i) {
        AnimatorUpdateListener c11849 = new AnimatorUpdateListener() {
            public void onAnimationUpdate(ValueAnimator valueAnimator) {
                PopupWindowFab.this.offsetX = ((Integer) valueAnimator.getAnimatedValue()).intValue();
                PopupWindowFab.this.updateFabLocation(activity);
            }
        };
        List arrayList = new ArrayList();
        ValueAnimator valueAnimator = new ValueAnimator();
        valueAnimator.setDuration(3000);
        valueAnimator.setIntValues(new int[]{0, 0});
        valueAnimator.addUpdateListener(c11849);
        arrayList.add(valueAnimator);
        if (Wrapper.INSTANCE.isSlideIn()) {
            int i2;
            if (this.isFabLeft) {
                i2 = -((int) Math.ceil(((double) i) * 0.3d));
            } else {
                i2 = (int) Math.ceil(((double) i) * 0.3d);
            }
            ValueAnimator valueAnimator2 = new ValueAnimator();
            valueAnimator2.setDuration(500);
            valueAnimator2.setIntValues(new int[]{0, i2});
            valueAnimator2.addUpdateListener(c11849);
            valueAnimator2.addListener(getAnimationListener(activity));
            arrayList.add(valueAnimator2);
        }
        AnimatorSet animatorSet = new AnimatorSet();
        animatorSet.playSequentially((Animator[]) arrayList.toArray(new ValueAnimator[arrayList.size()]));
        return animatorSet;
    }

    private void updateFabLocation(Activity activity) {
        post(activity, this.updatePopupWindowRunnable);
    }

    public void hide(Activity activity) {
        cancelAnimation(activity);
        post(activity, new Runnable() {
            public void run() {
                try {
                    if (PopupWindowFab.this.popupWindow != null) {
                        PopupWindowFab.this.popupWindow.dismiss();
                        PopupWindowFab.this.popupWindow = null;
                    }
                } catch (Throwable e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        });
        if (this.view != null) {
            this.view = null;
        }
    }

    public void cancelAnimation(Activity activity) {
        if (this.animatorSet != null) {
            final AnimatorSet animatorSet = this.animatorSet;
            post(activity, new Runnable() {
                public void run() {
                    try {
                        animatorSet.cancel();
                    } catch (Throwable e) {
                        Log.e(Constants.TAG, "Exception", e);
                    }
                }
            });
            this.animatorSet = null;
        }
    }

    public void cancelTimer() {
        if (this.timer != null) {
            this.timer.cancel();
        }
        if (this.timerTask != null) {
            this.timerTask.cancel();
        }
    }

    public void destroy(Activity activity) {
        hide(activity);
    }
}
