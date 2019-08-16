package net.gogame.gowrap.p019ui.fab;

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
import java.util.Timer;
import java.util.TimerTask;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.p019ui.utils.DisplayUtils;
import net.gogame.gowrap.wrapper.OverlayUIHelper;

/* renamed from: net.gogame.gowrap.ui.fab.PopupWindowFab */
public class PopupWindowFab extends AbstractFab {
    private static final int MAX_RETRIES = 4;
    private static final int RETRY_MS = 500;
    /* access modifiers changed from: private */
    public AnimatorSet animatorSet = null;
    /* access modifiers changed from: private */
    public int fabEndPositionX = 0;
    /* access modifiers changed from: private */
    public int fabStartPositionX = 0;
    /* access modifiers changed from: private */
    public boolean fixX;
    /* access modifiers changed from: private */
    public boolean fixY;
    private Handler handler = null;
    /* access modifiers changed from: private */
    public boolean isFabLeft = true;
    private boolean isFirstTime = false;
    /* access modifiers changed from: private */
    public Integer mPosX = null;
    /* access modifiers changed from: private */
    public Integer mPosY = null;
    /* access modifiers changed from: private */
    public int offsetX = 0;
    /* access modifiers changed from: private */
    public int offsetY = 0;
    /* access modifiers changed from: private */
    public PopupWindow popupWindow = null;
    private int retries = 0;
    /* access modifiers changed from: private */
    public int screenBottomHeightLimit = 0;
    /* access modifiers changed from: private */
    public int screenHeight = 0;
    /* access modifiers changed from: private */
    public int screenTopHeightLimit = 0;
    /* access modifiers changed from: private */
    public int screenWidth = 0;
    /* access modifiers changed from: private */
    public final PopupWindowFab self = this;
    private boolean slideIn = false;
    private boolean slideOut = false;
    private Timer timer;
    private TimerTask timerTask;
    private final Runnable updateFab = new Runnable() {
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
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    };
    private final Runnable updatePopupWindowRunnable = new Runnable() {
        public void run() {
            try {
                if (PopupWindowFab.this.popupWindow != null) {
                    PopupWindowFab.this.popupWindow.update(PopupWindowFab.this.getX(), PopupWindowFab.this.getY(), -1, -1);
                }
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    };
    /* access modifiers changed from: private */
    public View view = null;

    public PopupWindowFab(boolean z, boolean z2) {
        this.fixX = z;
        this.fixY = z2;
    }

    /* access modifiers changed from: private */
    public int getX() {
        return this.mPosX.intValue() + this.offsetX;
    }

    /* access modifiers changed from: private */
    public int getY() {
        return this.mPosY.intValue() + this.offsetY;
    }

    private Handler getHandler(Activity activity) {
        if (this.handler == null || this.handler.getLooper() != activity.getMainLooper()) {
            this.handler = new Handler(activity.getMainLooper());
        }
        return this.handler;
    }

    /* access modifiers changed from: private */
    public void post(Activity activity, Runnable runnable) {
        getHandler(activity).post(runnable);
    }

    /* access modifiers changed from: private */
    public void postDelayed(Activity activity, Runnable runnable, long j) {
        getHandler(activity).postDelayed(runnable, j);
    }

    public boolean handleTouchEvent(MotionEvent motionEvent) {
        if (!new Rect(getX(), getY(), getX() + this.view.getWidth(), getY() + this.view.getHeight()).contains((int) motionEvent.getX(), (int) motionEvent.getY())) {
            return false;
        }
        return this.view.dispatchTouchEvent(motionEvent);
    }

    public void update(Activity activity) {
        activity.runOnUiThread(this.updateFab);
    }

    /* access modifiers changed from: private */
    public void setTimerForFab(final Activity activity) {
        this.timer = new Timer();
        this.timerTask = new TimerTask() {
            public void run() {
                PopupWindowFab.this.post(activity, new Runnable() {
                    public void run() {
                        PopupWindowFab.this.postDelayed(activity, new Runnable() {
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
                                } catch (Exception e) {
                                    Log.e(Constants.TAG, "Exception", e);
                                }
                            }
                        }, 100);
                    }
                });
            }
        };
        this.timer.schedule(this.timerTask, 60000, 60000);
    }

    public void show(final Activity activity) {
        View view2;
        if (this.popupWindow == null) {
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
                            } catch (Exception e) {
                                Log.e(Constants.TAG, "Exception", e);
                            }
                        }
                    }, 500);
                    return;
                }
                view2 = rootView;
            } else {
                view2 = leafView;
            }
            this.retries = 0;
            C17235 r3 = new OnTouchListener() {
                private float initialTouchX;
                private float initialTouchY;
                private int initialX;
                private int initialY;
                private boolean isDrag;

                public boolean onTouch(View view, MotionEvent motionEvent) {
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
                            int access$1100 = PopupWindowFab.this.screenWidth / 2;
                            if (PopupWindowFab.this.mPosX.intValue() >= access$1100) {
                                PopupWindowFab.this.mPosX = Integer.valueOf(PopupWindowFab.this.screenWidth - PopupWindowFab.this.fabEndPositionX);
                                PopupWindowFab.this.isFabLeft = false;
                            } else if (PopupWindowFab.this.mPosX.intValue() < access$1100) {
                                PopupWindowFab.this.mPosX = Integer.valueOf(PopupWindowFab.this.fabStartPositionX);
                                PopupWindowFab.this.isFabLeft = true;
                            }
                            if (Wrapper.INSTANCE.isSlideOut() || Wrapper.INSTANCE.isSlideIn()) {
                                PopupWindowFab.this.cancelAnimation(activity);
                                PopupWindowFab.this.postDelayed(activity, new Runnable() {
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
                                        } catch (Exception e) {
                                            Log.e(Constants.TAG, "Exception", e);
                                        }
                                    }
                                }, 100);
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
                                int rawY = (this.initialY + ((int) (motionEvent.getRawY() - this.initialTouchY))) - PopupWindowFab.this.offsetY;
                                if (rawY > PopupWindowFab.this.screenTopHeightLimit && rawY < PopupWindowFab.this.screenHeight - PopupWindowFab.this.screenBottomHeightLimit) {
                                    PopupWindowFab.this.mPosY = Integer.valueOf(rawY);
                                }
                            }
                            PopupWindowFab.this.updateFabLocation(activity);
                            int access$500 = PopupWindowFab.this.getX() - this.initialX;
                            int access$600 = PopupWindowFab.this.getY() - this.initialY;
                            if (!this.isDrag && (Math.abs(access$500) > 10 || Math.abs(access$600) > 10)) {
                                this.isDrag = true;
                                break;
                            }
                    }
                    return false;
                }
            };
            this.view = createImageView(activity, C1423R.C1427drawable.net_gogame_gowrap_fab);
            this.view.setOnTouchListener(r3);
            int intrinsicWidth = activity.getResources().getDrawable(C1423R.C1427drawable.net_gogame_gowrap_fab).getIntrinsicWidth();
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
            final ViewGroup viewGroup = (ViewGroup) view2.getParent();
            rootView.post(new Runnable() {
                public void run() {
                    try {
                        if (PopupWindowFab.this.popupWindow != null) {
                            if (Wrapper.INSTANCE.isSlideOut()) {
                                PopupWindowFab.this.offsetX = -displayMetrics2.widthPixels;
                            }
                            PopupWindowFab.this.popupWindow.showAtLocation(viewGroup, 0, PopupWindowFab.this.getX(), PopupWindowFab.this.getY());
                            if (Wrapper.INSTANCE.isSlideOut() || Wrapper.INSTANCE.isSlideIn()) {
                                PopupWindowFab.this.postDelayed(activity, new Runnable() {
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
                                        } catch (Exception e) {
                                            Log.e(Constants.TAG, "Exception", e);
                                        }
                                    }
                                }, 100);
                            }
                        }
                    } catch (Exception e) {
                        Log.e(Constants.TAG, "Exception", e);
                    }
                }
            });
            this.popupWindow.setTouchInterceptor(r3);
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

    /* access modifiers changed from: private */
    @TargetApi(11)
    public AnimatorSet getAnimation(final Activity activity, final int i) {
        int ceil;
        int i2;
        C17288 r1 = new AnimatorUpdateListener() {
            public void onAnimationUpdate(ValueAnimator valueAnimator) {
                PopupWindowFab.this.offsetX = ((Integer) valueAnimator.getAnimatedValue()).intValue();
                if (PopupWindowFab.this.view != null) {
                    PopupWindowFab.this.view.setPadding(i / 2, 0, i / 2, 0);
                }
                PopupWindowFab.this.updateFabLocation(activity);
            }
        };
        ArrayList arrayList = new ArrayList();
        if (Wrapper.INSTANCE.isSlideOut()) {
            if (this.isFabLeft) {
                i2 = -i;
            } else {
                i2 = i;
            }
            ValueAnimator valueAnimator = new ValueAnimator();
            valueAnimator.setDuration(500);
            valueAnimator.setIntValues(new int[]{i2, 0});
            valueAnimator.addUpdateListener(r1);
            arrayList.add(valueAnimator);
        }
        ValueAnimator valueAnimator2 = new ValueAnimator();
        valueAnimator2.setDuration(5000);
        valueAnimator2.setIntValues(new int[]{0, 0});
        valueAnimator2.addUpdateListener(r1);
        arrayList.add(valueAnimator2);
        if (Wrapper.INSTANCE.isSlideIn()) {
            if (this.isFabLeft) {
                ceil = -((int) Math.ceil(((double) i) * 0.6d));
            } else {
                ceil = (int) Math.ceil(((double) i) * 0.6d);
            }
            ValueAnimator valueAnimator3 = new ValueAnimator();
            valueAnimator3.setDuration(500);
            valueAnimator3.setIntValues(new int[]{0, ceil});
            valueAnimator3.addUpdateListener(r1);
            valueAnimator3.addListener(getAnimationListener(activity));
            arrayList.add(valueAnimator3);
        }
        AnimatorSet animatorSet2 = new AnimatorSet();
        animatorSet2.playSequentially((Animator[]) arrayList.toArray(new ValueAnimator[arrayList.size()]));
        return animatorSet2;
    }

    /* access modifiers changed from: private */
    @TargetApi(11)
    public AnimatorSet getBlinkingAnimation() {
        ArrayList arrayList = new ArrayList();
        if (VERSION.SDK_INT >= 14) {
            ObjectAnimator ofFloat = ObjectAnimator.ofFloat(this.view, View.ALPHA, new float[]{0.0f, 1.0f});
            ofFloat.setDuration(250);
            ofFloat.setRepeatCount(10);
            ofFloat.setRepeatMode(2);
            arrayList.add(ofFloat);
        }
        AnimatorSet animatorSet2 = new AnimatorSet();
        animatorSet2.playSequentially((Animator[]) arrayList.toArray(new ValueAnimator[arrayList.size()]));
        return animatorSet2;
    }

    /* access modifiers changed from: private */
    @TargetApi(11)
    public AnimatorSet getInsideAnimation(final Activity activity, int i) {
        int ceil;
        C17299 r1 = new AnimatorUpdateListener() {
            public void onAnimationUpdate(ValueAnimator valueAnimator) {
                PopupWindowFab.this.offsetX = ((Integer) valueAnimator.getAnimatedValue()).intValue();
                PopupWindowFab.this.updateFabLocation(activity);
            }
        };
        ArrayList arrayList = new ArrayList();
        ValueAnimator valueAnimator = new ValueAnimator();
        valueAnimator.setDuration(3000);
        valueAnimator.setIntValues(new int[]{0, 0});
        valueAnimator.addUpdateListener(r1);
        arrayList.add(valueAnimator);
        if (Wrapper.INSTANCE.isSlideIn()) {
            if (this.isFabLeft) {
                ceil = -((int) Math.ceil(((double) i) * 0.3d));
            } else {
                ceil = (int) Math.ceil(((double) i) * 0.3d);
            }
            ValueAnimator valueAnimator2 = new ValueAnimator();
            valueAnimator2.setDuration(500);
            valueAnimator2.setIntValues(new int[]{0, ceil});
            valueAnimator2.addUpdateListener(r1);
            valueAnimator2.addListener(getAnimationListener(activity));
            arrayList.add(valueAnimator2);
        }
        AnimatorSet animatorSet2 = new AnimatorSet();
        animatorSet2.playSequentially((Animator[]) arrayList.toArray(new ValueAnimator[arrayList.size()]));
        return animatorSet2;
    }

    /* access modifiers changed from: private */
    public void updateFabLocation(Activity activity) {
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
                } catch (Exception e) {
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
            final AnimatorSet animatorSet2 = this.animatorSet;
            post(activity, new Runnable() {
                public void run() {
                    try {
                        animatorSet2.cancel();
                    } catch (Exception e) {
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
