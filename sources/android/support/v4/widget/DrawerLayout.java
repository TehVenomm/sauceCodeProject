package android.support.p000v4.widget;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Rect;
import android.graphics.drawable.ColorDrawable;
import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.ClassLoaderCreator;
import android.os.Parcelable.Creator;
import android.os.SystemClock;
import android.support.annotation.ColorInt;
import android.support.annotation.DrawableRes;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.content.ContextCompat;
import android.support.p000v4.graphics.drawable.DrawableCompat;
import android.support.p000v4.view.AbsSavedState;
import android.support.p000v4.view.AccessibilityDelegateCompat;
import android.support.p000v4.view.GravityCompat;
import android.support.p000v4.view.ViewCompat;
import android.support.p000v4.view.ViewGroupCompat;
import android.support.p000v4.view.accessibility.AccessibilityNodeInfoCompat;
import android.support.p000v4.view.accessibility.AccessibilityNodeInfoCompat.AccessibilityActionCompat;
import android.support.p000v4.widget.ViewDragHelper.Callback;
import android.util.AttributeSet;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnApplyWindowInsetsListener;
import android.view.ViewGroup;
import android.view.ViewGroup.MarginLayoutParams;
import android.view.ViewParent;
import android.view.WindowInsets;
import android.view.accessibility.AccessibilityEvent;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.util.ArrayList;
import java.util.List;

/* renamed from: android.support.v4.widget.DrawerLayout */
public class DrawerLayout extends ViewGroup {
    private static final boolean ALLOW_EDGE_LOCK = false;
    static final boolean CAN_HIDE_DESCENDANTS = (VERSION.SDK_INT >= 19);
    private static final boolean CHILDREN_DISALLOW_INTERCEPT = true;
    private static final int DEFAULT_SCRIM_COLOR = -1728053248;
    private static final int DRAWER_ELEVATION = 10;
    static final int[] LAYOUT_ATTRS = {16842931};
    public static final int LOCK_MODE_LOCKED_CLOSED = 1;
    public static final int LOCK_MODE_LOCKED_OPEN = 2;
    public static final int LOCK_MODE_UNDEFINED = 3;
    public static final int LOCK_MODE_UNLOCKED = 0;
    private static final int MIN_DRAWER_MARGIN = 64;
    private static final int MIN_FLING_VELOCITY = 400;
    private static final int PEEK_DELAY = 160;
    private static final boolean SET_DRAWER_SHADOW_FROM_ELEVATION;
    public static final int STATE_DRAGGING = 1;
    public static final int STATE_IDLE = 0;
    public static final int STATE_SETTLING = 2;
    private static final String TAG = "DrawerLayout";
    private static final int[] THEME_ATTRS = {16843828};
    private static final float TOUCH_SLOP_SENSITIVITY = 1.0f;
    private final ChildAccessibilityDelegate mChildAccessibilityDelegate;
    private boolean mChildrenCanceledTouch;
    private boolean mDisallowInterceptRequested;
    private boolean mDrawStatusBarBackground;
    private float mDrawerElevation;
    private int mDrawerState;
    private boolean mFirstLayout;
    private boolean mInLayout;
    private float mInitialMotionX;
    private float mInitialMotionY;
    private Object mLastInsets;
    private final ViewDragCallback mLeftCallback;
    private final ViewDragHelper mLeftDragger;
    @Nullable
    private DrawerListener mListener;
    private List<DrawerListener> mListeners;
    private int mLockModeEnd;
    private int mLockModeLeft;
    private int mLockModeRight;
    private int mLockModeStart;
    private int mMinDrawerMargin;
    private final ArrayList<View> mNonDrawerViews;
    private final ViewDragCallback mRightCallback;
    private final ViewDragHelper mRightDragger;
    private int mScrimColor;
    private float mScrimOpacity;
    private Paint mScrimPaint;
    private Drawable mShadowEnd;
    private Drawable mShadowLeft;
    private Drawable mShadowLeftResolved;
    private Drawable mShadowRight;
    private Drawable mShadowRightResolved;
    private Drawable mShadowStart;
    private Drawable mStatusBarBackground;
    private CharSequence mTitleLeft;
    private CharSequence mTitleRight;

    /* renamed from: android.support.v4.widget.DrawerLayout$AccessibilityDelegate */
    class AccessibilityDelegate extends AccessibilityDelegateCompat {
        private final Rect mTmpRect = new Rect();

        AccessibilityDelegate() {
        }

        private void addChildrenForAccessibility(AccessibilityNodeInfoCompat accessibilityNodeInfoCompat, ViewGroup viewGroup) {
            int childCount = viewGroup.getChildCount();
            for (int i = 0; i < childCount; i++) {
                View childAt = viewGroup.getChildAt(i);
                if (DrawerLayout.includeChildForAccessibility(childAt)) {
                    accessibilityNodeInfoCompat.addChild(childAt);
                }
            }
        }

        private void copyNodeInfoNoChildren(AccessibilityNodeInfoCompat accessibilityNodeInfoCompat, AccessibilityNodeInfoCompat accessibilityNodeInfoCompat2) {
            Rect rect = this.mTmpRect;
            accessibilityNodeInfoCompat2.getBoundsInParent(rect);
            accessibilityNodeInfoCompat.setBoundsInParent(rect);
            accessibilityNodeInfoCompat2.getBoundsInScreen(rect);
            accessibilityNodeInfoCompat.setBoundsInScreen(rect);
            accessibilityNodeInfoCompat.setVisibleToUser(accessibilityNodeInfoCompat2.isVisibleToUser());
            accessibilityNodeInfoCompat.setPackageName(accessibilityNodeInfoCompat2.getPackageName());
            accessibilityNodeInfoCompat.setClassName(accessibilityNodeInfoCompat2.getClassName());
            accessibilityNodeInfoCompat.setContentDescription(accessibilityNodeInfoCompat2.getContentDescription());
            accessibilityNodeInfoCompat.setEnabled(accessibilityNodeInfoCompat2.isEnabled());
            accessibilityNodeInfoCompat.setClickable(accessibilityNodeInfoCompat2.isClickable());
            accessibilityNodeInfoCompat.setFocusable(accessibilityNodeInfoCompat2.isFocusable());
            accessibilityNodeInfoCompat.setFocused(accessibilityNodeInfoCompat2.isFocused());
            accessibilityNodeInfoCompat.setAccessibilityFocused(accessibilityNodeInfoCompat2.isAccessibilityFocused());
            accessibilityNodeInfoCompat.setSelected(accessibilityNodeInfoCompat2.isSelected());
            accessibilityNodeInfoCompat.setLongClickable(accessibilityNodeInfoCompat2.isLongClickable());
            accessibilityNodeInfoCompat.addAction(accessibilityNodeInfoCompat2.getActions());
        }

        public boolean dispatchPopulateAccessibilityEvent(View view, AccessibilityEvent accessibilityEvent) {
            if (accessibilityEvent.getEventType() != 32) {
                return super.dispatchPopulateAccessibilityEvent(view, accessibilityEvent);
            }
            List text = accessibilityEvent.getText();
            View findVisibleDrawer = DrawerLayout.this.findVisibleDrawer();
            if (findVisibleDrawer != null) {
                CharSequence drawerTitle = DrawerLayout.this.getDrawerTitle(DrawerLayout.this.getDrawerViewAbsoluteGravity(findVisibleDrawer));
                if (drawerTitle != null) {
                    text.add(drawerTitle);
                }
            }
            return true;
        }

        public void onInitializeAccessibilityEvent(View view, AccessibilityEvent accessibilityEvent) {
            super.onInitializeAccessibilityEvent(view, accessibilityEvent);
            accessibilityEvent.setClassName(DrawerLayout.class.getName());
        }

        public void onInitializeAccessibilityNodeInfo(View view, AccessibilityNodeInfoCompat accessibilityNodeInfoCompat) {
            if (DrawerLayout.CAN_HIDE_DESCENDANTS) {
                super.onInitializeAccessibilityNodeInfo(view, accessibilityNodeInfoCompat);
            } else {
                AccessibilityNodeInfoCompat obtain = AccessibilityNodeInfoCompat.obtain(accessibilityNodeInfoCompat);
                super.onInitializeAccessibilityNodeInfo(view, obtain);
                accessibilityNodeInfoCompat.setSource(view);
                ViewParent parentForAccessibility = ViewCompat.getParentForAccessibility(view);
                if (parentForAccessibility instanceof View) {
                    accessibilityNodeInfoCompat.setParent((View) parentForAccessibility);
                }
                copyNodeInfoNoChildren(accessibilityNodeInfoCompat, obtain);
                obtain.recycle();
                addChildrenForAccessibility(accessibilityNodeInfoCompat, (ViewGroup) view);
            }
            accessibilityNodeInfoCompat.setClassName(DrawerLayout.class.getName());
            accessibilityNodeInfoCompat.setFocusable(false);
            accessibilityNodeInfoCompat.setFocused(false);
            accessibilityNodeInfoCompat.removeAction(AccessibilityActionCompat.ACTION_FOCUS);
            accessibilityNodeInfoCompat.removeAction(AccessibilityActionCompat.ACTION_CLEAR_FOCUS);
        }

        public boolean onRequestSendAccessibilityEvent(ViewGroup viewGroup, View view, AccessibilityEvent accessibilityEvent) {
            if (DrawerLayout.CAN_HIDE_DESCENDANTS || DrawerLayout.includeChildForAccessibility(view)) {
                return super.onRequestSendAccessibilityEvent(viewGroup, view, accessibilityEvent);
            }
            return false;
        }
    }

    /* renamed from: android.support.v4.widget.DrawerLayout$ChildAccessibilityDelegate */
    static final class ChildAccessibilityDelegate extends AccessibilityDelegateCompat {
        ChildAccessibilityDelegate() {
        }

        public void onInitializeAccessibilityNodeInfo(View view, AccessibilityNodeInfoCompat accessibilityNodeInfoCompat) {
            super.onInitializeAccessibilityNodeInfo(view, accessibilityNodeInfoCompat);
            if (!DrawerLayout.includeChildForAccessibility(view)) {
                accessibilityNodeInfoCompat.setParent(null);
            }
        }
    }

    /* renamed from: android.support.v4.widget.DrawerLayout$DrawerListener */
    public interface DrawerListener {
        void onDrawerClosed(View view);

        void onDrawerOpened(View view);

        void onDrawerSlide(View view, float f);

        void onDrawerStateChanged(int i);
    }

    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: android.support.v4.widget.DrawerLayout$EdgeGravity */
    private @interface EdgeGravity {
    }

    /* renamed from: android.support.v4.widget.DrawerLayout$LayoutParams */
    public static class LayoutParams extends MarginLayoutParams {
        private static final int FLAG_IS_CLOSING = 4;
        private static final int FLAG_IS_OPENED = 1;
        private static final int FLAG_IS_OPENING = 2;
        public int gravity;
        boolean isPeeking;
        float onScreen;
        int openState;

        public LayoutParams(int i, int i2) {
            super(i, i2);
            this.gravity = 0;
        }

        public LayoutParams(int i, int i2, int i3) {
            this(i, i2);
            this.gravity = i3;
        }

        public LayoutParams(Context context, AttributeSet attributeSet) {
            super(context, attributeSet);
            this.gravity = 0;
            TypedArray obtainStyledAttributes = context.obtainStyledAttributes(attributeSet, DrawerLayout.LAYOUT_ATTRS);
            this.gravity = obtainStyledAttributes.getInt(0, 0);
            obtainStyledAttributes.recycle();
        }

        public LayoutParams(LayoutParams layoutParams) {
            super(layoutParams);
            this.gravity = 0;
            this.gravity = layoutParams.gravity;
        }

        public LayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
            super(layoutParams);
            this.gravity = 0;
        }

        public LayoutParams(MarginLayoutParams marginLayoutParams) {
            super(marginLayoutParams);
            this.gravity = 0;
        }
    }

    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: android.support.v4.widget.DrawerLayout$LockMode */
    private @interface LockMode {
    }

    /* renamed from: android.support.v4.widget.DrawerLayout$SavedState */
    protected static class SavedState extends AbsSavedState {
        public static final Creator<SavedState> CREATOR = new ClassLoaderCreator<SavedState>() {
            public SavedState createFromParcel(Parcel parcel) {
                return new SavedState(parcel, null);
            }

            public SavedState createFromParcel(Parcel parcel, ClassLoader classLoader) {
                return new SavedState(parcel, classLoader);
            }

            public SavedState[] newArray(int i) {
                return new SavedState[i];
            }
        };
        int lockModeEnd;
        int lockModeLeft;
        int lockModeRight;
        int lockModeStart;
        int openDrawerGravity = 0;

        public SavedState(Parcel parcel, ClassLoader classLoader) {
            super(parcel, classLoader);
            this.openDrawerGravity = parcel.readInt();
            this.lockModeLeft = parcel.readInt();
            this.lockModeRight = parcel.readInt();
            this.lockModeStart = parcel.readInt();
            this.lockModeEnd = parcel.readInt();
        }

        public SavedState(Parcelable parcelable) {
            super(parcelable);
        }

        public void writeToParcel(Parcel parcel, int i) {
            super.writeToParcel(parcel, i);
            parcel.writeInt(this.openDrawerGravity);
            parcel.writeInt(this.lockModeLeft);
            parcel.writeInt(this.lockModeRight);
            parcel.writeInt(this.lockModeStart);
            parcel.writeInt(this.lockModeEnd);
        }
    }

    /* renamed from: android.support.v4.widget.DrawerLayout$SimpleDrawerListener */
    public static abstract class SimpleDrawerListener implements DrawerListener {
        public void onDrawerClosed(View view) {
        }

        public void onDrawerOpened(View view) {
        }

        public void onDrawerSlide(View view, float f) {
        }

        public void onDrawerStateChanged(int i) {
        }
    }

    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: android.support.v4.widget.DrawerLayout$State */
    private @interface State {
    }

    /* renamed from: android.support.v4.widget.DrawerLayout$ViewDragCallback */
    private class ViewDragCallback extends Callback {
        private final int mAbsGravity;
        private ViewDragHelper mDragger;
        private final Runnable mPeekRunnable = new Runnable() {
            public void run() {
                ViewDragCallback.this.peekDrawer();
            }
        };

        ViewDragCallback(int i) {
            this.mAbsGravity = i;
        }

        private void closeOtherDrawer() {
            int i = 3;
            if (this.mAbsGravity == 3) {
                i = 5;
            }
            View findDrawerWithGravity = DrawerLayout.this.findDrawerWithGravity(i);
            if (findDrawerWithGravity != null) {
                DrawerLayout.this.closeDrawer(findDrawerWithGravity);
            }
        }

        public int clampViewPositionHorizontal(View view, int i, int i2) {
            if (DrawerLayout.this.checkDrawerViewAbsoluteGravity(view, 3)) {
                return Math.max(-view.getWidth(), Math.min(i, 0));
            }
            int width = DrawerLayout.this.getWidth();
            return Math.max(width - view.getWidth(), Math.min(i, width));
        }

        public int clampViewPositionVertical(View view, int i, int i2) {
            return view.getTop();
        }

        public int getViewHorizontalDragRange(View view) {
            if (DrawerLayout.this.isDrawerView(view)) {
                return view.getWidth();
            }
            return 0;
        }

        public void onEdgeDragStarted(int i, int i2) {
            View findDrawerWithGravity = (i & 1) == 1 ? DrawerLayout.this.findDrawerWithGravity(3) : DrawerLayout.this.findDrawerWithGravity(5);
            if (findDrawerWithGravity != null && DrawerLayout.this.getDrawerLockMode(findDrawerWithGravity) == 0) {
                this.mDragger.captureChildView(findDrawerWithGravity, i2);
            }
        }

        public boolean onEdgeLock(int i) {
            return false;
        }

        public void onEdgeTouched(int i, int i2) {
            DrawerLayout.this.postDelayed(this.mPeekRunnable, 160);
        }

        public void onViewCaptured(View view, int i) {
            ((LayoutParams) view.getLayoutParams()).isPeeking = false;
            closeOtherDrawer();
        }

        public void onViewDragStateChanged(int i) {
            DrawerLayout.this.updateDrawerState(this.mAbsGravity, i, this.mDragger.getCapturedView());
        }

        public void onViewPositionChanged(View view, int i, int i2, int i3, int i4) {
            int width = view.getWidth();
            float width2 = DrawerLayout.this.checkDrawerViewAbsoluteGravity(view, 3) ? ((float) (width + i)) / ((float) width) : ((float) (DrawerLayout.this.getWidth() - i)) / ((float) width);
            DrawerLayout.this.setDrawerViewOffset(view, width2);
            view.setVisibility(width2 == 0.0f ? 4 : 0);
            DrawerLayout.this.invalidate();
        }

        public void onViewReleased(View view, float f, float f2) {
            int width;
            float drawerViewOffset = DrawerLayout.this.getDrawerViewOffset(view);
            int width2 = view.getWidth();
            if (DrawerLayout.this.checkDrawerViewAbsoluteGravity(view, 3)) {
                width = (f > 0.0f || (f == 0.0f && drawerViewOffset > 0.5f)) ? 0 : -width2;
            } else {
                width = DrawerLayout.this.getWidth();
                if (f < 0.0f || (f == 0.0f && drawerViewOffset > 0.5f)) {
                    width -= width2;
                }
            }
            this.mDragger.settleCapturedViewAt(width, view.getTop());
            DrawerLayout.this.invalidate();
        }

        /* access modifiers changed from: 0000 */
        public void peekDrawer() {
            int width;
            View findDrawerWithGravity;
            int i = 0;
            int edgeSize = this.mDragger.getEdgeSize();
            boolean z = this.mAbsGravity == 3;
            if (z) {
                findDrawerWithGravity = DrawerLayout.this.findDrawerWithGravity(3);
                if (findDrawerWithGravity != null) {
                    i = -findDrawerWithGravity.getWidth();
                }
                width = i + edgeSize;
            } else {
                width = DrawerLayout.this.getWidth() - edgeSize;
                findDrawerWithGravity = DrawerLayout.this.findDrawerWithGravity(5);
            }
            if (findDrawerWithGravity == null) {
                return;
            }
            if (((z && findDrawerWithGravity.getLeft() < width) || (!z && findDrawerWithGravity.getLeft() > width)) && DrawerLayout.this.getDrawerLockMode(findDrawerWithGravity) == 0) {
                LayoutParams layoutParams = (LayoutParams) findDrawerWithGravity.getLayoutParams();
                this.mDragger.smoothSlideViewTo(findDrawerWithGravity, width, findDrawerWithGravity.getTop());
                layoutParams.isPeeking = true;
                DrawerLayout.this.invalidate();
                closeOtherDrawer();
                DrawerLayout.this.cancelChildViewTouch();
            }
        }

        public void removeCallbacks() {
            DrawerLayout.this.removeCallbacks(this.mPeekRunnable);
        }

        public void setDragger(ViewDragHelper viewDragHelper) {
            this.mDragger = viewDragHelper;
        }

        public boolean tryCaptureView(View view, int i) {
            return DrawerLayout.this.isDrawerView(view) && DrawerLayout.this.checkDrawerViewAbsoluteGravity(view, this.mAbsGravity) && DrawerLayout.this.getDrawerLockMode(view) == 0;
        }
    }

    static {
        boolean z = true;
        if (VERSION.SDK_INT < 21) {
            z = false;
        }
        SET_DRAWER_SHADOW_FROM_ELEVATION = z;
    }

    public DrawerLayout(Context context) {
        this(context, null);
    }

    public DrawerLayout(Context context, AttributeSet attributeSet) {
        this(context, attributeSet, 0);
    }

    public DrawerLayout(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        this.mChildAccessibilityDelegate = new ChildAccessibilityDelegate();
        this.mScrimColor = DEFAULT_SCRIM_COLOR;
        this.mScrimPaint = new Paint();
        this.mFirstLayout = true;
        this.mLockModeLeft = 3;
        this.mLockModeRight = 3;
        this.mLockModeStart = 3;
        this.mLockModeEnd = 3;
        this.mShadowStart = null;
        this.mShadowEnd = null;
        this.mShadowLeft = null;
        this.mShadowRight = null;
        setDescendantFocusability(262144);
        float f = getResources().getDisplayMetrics().density;
        this.mMinDrawerMargin = (int) ((64.0f * f) + 0.5f);
        float f2 = 400.0f * f;
        this.mLeftCallback = new ViewDragCallback(3);
        this.mRightCallback = new ViewDragCallback(5);
        this.mLeftDragger = ViewDragHelper.create(this, TOUCH_SLOP_SENSITIVITY, this.mLeftCallback);
        this.mLeftDragger.setEdgeTrackingEnabled(1);
        this.mLeftDragger.setMinVelocity(f2);
        this.mLeftCallback.setDragger(this.mLeftDragger);
        this.mRightDragger = ViewDragHelper.create(this, TOUCH_SLOP_SENSITIVITY, this.mRightCallback);
        this.mRightDragger.setEdgeTrackingEnabled(2);
        this.mRightDragger.setMinVelocity(f2);
        this.mRightCallback.setDragger(this.mRightDragger);
        setFocusableInTouchMode(true);
        ViewCompat.setImportantForAccessibility(this, 1);
        ViewCompat.setAccessibilityDelegate(this, new AccessibilityDelegate());
        ViewGroupCompat.setMotionEventSplittingEnabled(this, false);
        if (ViewCompat.getFitsSystemWindows(this)) {
            if (VERSION.SDK_INT >= 21) {
                setOnApplyWindowInsetsListener(new OnApplyWindowInsetsListener() {
                    @TargetApi(21)
                    public WindowInsets onApplyWindowInsets(View view, WindowInsets windowInsets) {
                        ((DrawerLayout) view).setChildInsets(windowInsets, windowInsets.getSystemWindowInsetTop() > 0);
                        return windowInsets.consumeSystemWindowInsets();
                    }
                });
                setSystemUiVisibility(1280);
                TypedArray obtainStyledAttributes = context.obtainStyledAttributes(THEME_ATTRS);
                try {
                    this.mStatusBarBackground = obtainStyledAttributes.getDrawable(0);
                } finally {
                    obtainStyledAttributes.recycle();
                }
            } else {
                this.mStatusBarBackground = null;
            }
        }
        this.mDrawerElevation = f * 10.0f;
        this.mNonDrawerViews = new ArrayList<>();
    }

    static String gravityToString(int i) {
        return (i & 3) == 3 ? "LEFT" : (i & 5) == 5 ? "RIGHT" : Integer.toHexString(i);
    }

    private static boolean hasOpaqueBackground(View view) {
        Drawable background = view.getBackground();
        return background != null && background.getOpacity() == -1;
    }

    private boolean hasPeekingDrawer() {
        int childCount = getChildCount();
        for (int i = 0; i < childCount; i++) {
            if (((LayoutParams) getChildAt(i).getLayoutParams()).isPeeking) {
                return true;
            }
        }
        return false;
    }

    private boolean hasVisibleDrawer() {
        return findVisibleDrawer() != null;
    }

    static boolean includeChildForAccessibility(View view) {
        return (ViewCompat.getImportantForAccessibility(view) == 4 || ViewCompat.getImportantForAccessibility(view) == 2) ? false : true;
    }

    private boolean mirror(Drawable drawable, int i) {
        if (drawable == null || !DrawableCompat.isAutoMirrored(drawable)) {
            return false;
        }
        DrawableCompat.setLayoutDirection(drawable, i);
        return true;
    }

    private Drawable resolveLeftShadow() {
        int layoutDirection = ViewCompat.getLayoutDirection(this);
        if (layoutDirection == 0) {
            if (this.mShadowStart != null) {
                mirror(this.mShadowStart, layoutDirection);
                return this.mShadowStart;
            }
        } else if (this.mShadowEnd != null) {
            mirror(this.mShadowEnd, layoutDirection);
            return this.mShadowEnd;
        }
        return this.mShadowLeft;
    }

    private Drawable resolveRightShadow() {
        int layoutDirection = ViewCompat.getLayoutDirection(this);
        if (layoutDirection == 0) {
            if (this.mShadowEnd != null) {
                mirror(this.mShadowEnd, layoutDirection);
                return this.mShadowEnd;
            }
        } else if (this.mShadowStart != null) {
            mirror(this.mShadowStart, layoutDirection);
            return this.mShadowStart;
        }
        return this.mShadowRight;
    }

    private void resolveShadowDrawables() {
        if (!SET_DRAWER_SHADOW_FROM_ELEVATION) {
            this.mShadowLeftResolved = resolveLeftShadow();
            this.mShadowRightResolved = resolveRightShadow();
        }
    }

    private void updateChildrenImportantForAccessibility(View view, boolean z) {
        int childCount = getChildCount();
        for (int i = 0; i < childCount; i++) {
            View childAt = getChildAt(i);
            if ((z || isDrawerView(childAt)) && (!z || childAt != view)) {
                ViewCompat.setImportantForAccessibility(childAt, 4);
            } else {
                ViewCompat.setImportantForAccessibility(childAt, 1);
            }
        }
    }

    public void addDrawerListener(@NonNull DrawerListener drawerListener) {
        if (drawerListener != null) {
            if (this.mListeners == null) {
                this.mListeners = new ArrayList();
            }
            this.mListeners.add(drawerListener);
        }
    }

    public void addFocusables(ArrayList<View> arrayList, int i, int i2) {
        if (getDescendantFocusability() != 393216) {
            int childCount = getChildCount();
            boolean z = false;
            for (int i3 = 0; i3 < childCount; i3++) {
                View childAt = getChildAt(i3);
                if (!isDrawerView(childAt)) {
                    this.mNonDrawerViews.add(childAt);
                } else if (isDrawerOpen(childAt)) {
                    z = true;
                    childAt.addFocusables(arrayList, i, i2);
                }
            }
            if (!z) {
                int size = this.mNonDrawerViews.size();
                for (int i4 = 0; i4 < size; i4++) {
                    View view = (View) this.mNonDrawerViews.get(i4);
                    if (view.getVisibility() == 0) {
                        view.addFocusables(arrayList, i, i2);
                    }
                }
            }
            this.mNonDrawerViews.clear();
        }
    }

    public void addView(View view, int i, android.view.ViewGroup.LayoutParams layoutParams) {
        super.addView(view, i, layoutParams);
        if (findOpenDrawer() != null || isDrawerView(view)) {
            ViewCompat.setImportantForAccessibility(view, 4);
        } else {
            ViewCompat.setImportantForAccessibility(view, 1);
        }
        if (!CAN_HIDE_DESCENDANTS) {
            ViewCompat.setAccessibilityDelegate(view, this.mChildAccessibilityDelegate);
        }
    }

    /* access modifiers changed from: 0000 */
    public void cancelChildViewTouch() {
        if (!this.mChildrenCanceledTouch) {
            long uptimeMillis = SystemClock.uptimeMillis();
            MotionEvent obtain = MotionEvent.obtain(uptimeMillis, uptimeMillis, 3, 0.0f, 0.0f, 0);
            int childCount = getChildCount();
            for (int i = 0; i < childCount; i++) {
                getChildAt(i).dispatchTouchEvent(obtain);
            }
            obtain.recycle();
            this.mChildrenCanceledTouch = true;
        }
    }

    /* access modifiers changed from: 0000 */
    public boolean checkDrawerViewAbsoluteGravity(View view, int i) {
        return (getDrawerViewAbsoluteGravity(view) & i) == i;
    }

    /* access modifiers changed from: protected */
    public boolean checkLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return (layoutParams instanceof LayoutParams) && super.checkLayoutParams(layoutParams);
    }

    public void closeDrawer(int i) {
        closeDrawer(i, true);
    }

    public void closeDrawer(int i, boolean z) {
        View findDrawerWithGravity = findDrawerWithGravity(i);
        if (findDrawerWithGravity == null) {
            throw new IllegalArgumentException("No drawer view found with gravity " + gravityToString(i));
        }
        closeDrawer(findDrawerWithGravity, z);
    }

    public void closeDrawer(View view) {
        closeDrawer(view, true);
    }

    public void closeDrawer(View view, boolean z) {
        if (!isDrawerView(view)) {
            throw new IllegalArgumentException("View " + view + " is not a sliding drawer");
        }
        LayoutParams layoutParams = (LayoutParams) view.getLayoutParams();
        if (this.mFirstLayout) {
            layoutParams.onScreen = 0.0f;
            layoutParams.openState = 0;
        } else if (z) {
            layoutParams.openState |= 4;
            if (checkDrawerViewAbsoluteGravity(view, 3)) {
                this.mLeftDragger.smoothSlideViewTo(view, -view.getWidth(), view.getTop());
            } else {
                this.mRightDragger.smoothSlideViewTo(view, getWidth(), view.getTop());
            }
        } else {
            moveDrawerToOffset(view, 0.0f);
            updateDrawerState(layoutParams.gravity, 0, view);
            view.setVisibility(4);
        }
        invalidate();
    }

    public void closeDrawers() {
        closeDrawers(false);
    }

    /* access modifiers changed from: 0000 */
    public void closeDrawers(boolean z) {
        boolean z2;
        int childCount = getChildCount();
        int i = 0;
        boolean z3 = false;
        while (i < childCount) {
            View childAt = getChildAt(i);
            LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
            if (isDrawerView(childAt) && (!z || layoutParams.isPeeking)) {
                z2 = checkDrawerViewAbsoluteGravity(childAt, 3) ? z2 | this.mLeftDragger.smoothSlideViewTo(childAt, -childAt.getWidth(), childAt.getTop()) : z2 | this.mRightDragger.smoothSlideViewTo(childAt, getWidth(), childAt.getTop());
                layoutParams.isPeeking = false;
            }
            i++;
            z3 = z2;
        }
        this.mLeftCallback.removeCallbacks();
        this.mRightCallback.removeCallbacks();
        if (z2) {
            invalidate();
        }
    }

    public void computeScroll() {
        int childCount = getChildCount();
        float f = 0.0f;
        for (int i = 0; i < childCount; i++) {
            f = Math.max(f, ((LayoutParams) getChildAt(i).getLayoutParams()).onScreen);
        }
        this.mScrimOpacity = f;
        boolean continueSettling = this.mLeftDragger.continueSettling(true);
        boolean continueSettling2 = this.mRightDragger.continueSettling(true);
        if (continueSettling || continueSettling2) {
            ViewCompat.postInvalidateOnAnimation(this);
        }
    }

    /* access modifiers changed from: 0000 */
    public void dispatchOnDrawerClosed(View view) {
        LayoutParams layoutParams = (LayoutParams) view.getLayoutParams();
        if ((layoutParams.openState & 1) == 1) {
            layoutParams.openState = 0;
            if (this.mListeners != null) {
                for (int size = this.mListeners.size() - 1; size >= 0; size--) {
                    ((DrawerListener) this.mListeners.get(size)).onDrawerClosed(view);
                }
            }
            updateChildrenImportantForAccessibility(view, false);
            if (hasWindowFocus()) {
                View rootView = getRootView();
                if (rootView != null) {
                    rootView.sendAccessibilityEvent(32);
                }
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public void dispatchOnDrawerOpened(View view) {
        LayoutParams layoutParams = (LayoutParams) view.getLayoutParams();
        if ((layoutParams.openState & 1) == 0) {
            layoutParams.openState = 1;
            if (this.mListeners != null) {
                for (int size = this.mListeners.size() - 1; size >= 0; size--) {
                    ((DrawerListener) this.mListeners.get(size)).onDrawerOpened(view);
                }
            }
            updateChildrenImportantForAccessibility(view, true);
            if (hasWindowFocus()) {
                sendAccessibilityEvent(32);
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public void dispatchOnDrawerSlide(View view, float f) {
        if (this.mListeners != null) {
            for (int size = this.mListeners.size() - 1; size >= 0; size--) {
                ((DrawerListener) this.mListeners.get(size)).onDrawerSlide(view, f);
            }
        }
    }

    /* access modifiers changed from: protected */
    public boolean drawChild(Canvas canvas, View view, long j) {
        int height = getHeight();
        boolean isContentView = isContentView(view);
        int i = 0;
        int width = getWidth();
        int save = canvas.save();
        if (isContentView) {
            int childCount = getChildCount();
            i = 0;
            for (int i2 = 0; i2 < childCount; i2++) {
                View childAt = getChildAt(i2);
                if (childAt != view && childAt.getVisibility() == 0 && hasOpaqueBackground(childAt) && isDrawerView(childAt) && childAt.getHeight() >= height) {
                    if (checkDrawerViewAbsoluteGravity(childAt, 3)) {
                        int right = childAt.getRight();
                        if (right > i) {
                            i = right;
                        }
                    } else {
                        int left = childAt.getLeft();
                        if (left < width) {
                            width = left;
                        }
                    }
                }
            }
            canvas.clipRect(i, 0, width, getHeight());
        }
        boolean drawChild = super.drawChild(canvas, view, j);
        canvas.restoreToCount(save);
        if (this.mScrimOpacity > 0.0f && isContentView) {
            this.mScrimPaint.setColor((((int) (((float) ((this.mScrimColor & ViewCompat.MEASURED_STATE_MASK) >>> 24)) * this.mScrimOpacity)) << 24) | (this.mScrimColor & ViewCompat.MEASURED_SIZE_MASK));
            canvas.drawRect((float) i, 0.0f, (float) width, (float) getHeight(), this.mScrimPaint);
        } else if (this.mShadowLeftResolved != null && checkDrawerViewAbsoluteGravity(view, 3)) {
            int intrinsicWidth = this.mShadowLeftResolved.getIntrinsicWidth();
            int right2 = view.getRight();
            float max = Math.max(0.0f, Math.min(((float) right2) / ((float) this.mLeftDragger.getEdgeSize()), TOUCH_SLOP_SENSITIVITY));
            this.mShadowLeftResolved.setBounds(right2, view.getTop(), intrinsicWidth + right2, view.getBottom());
            this.mShadowLeftResolved.setAlpha((int) (255.0f * max));
            this.mShadowLeftResolved.draw(canvas);
        } else if (this.mShadowRightResolved != null && checkDrawerViewAbsoluteGravity(view, 5)) {
            int intrinsicWidth2 = this.mShadowRightResolved.getIntrinsicWidth();
            int left2 = view.getLeft();
            float max2 = Math.max(0.0f, Math.min(((float) (getWidth() - left2)) / ((float) this.mRightDragger.getEdgeSize()), TOUCH_SLOP_SENSITIVITY));
            this.mShadowRightResolved.setBounds(left2 - intrinsicWidth2, view.getTop(), left2, view.getBottom());
            this.mShadowRightResolved.setAlpha((int) (255.0f * max2));
            this.mShadowRightResolved.draw(canvas);
        }
        return drawChild;
    }

    /* access modifiers changed from: 0000 */
    public View findDrawerWithGravity(int i) {
        int absoluteGravity = GravityCompat.getAbsoluteGravity(i, ViewCompat.getLayoutDirection(this));
        int childCount = getChildCount();
        for (int i2 = 0; i2 < childCount; i2++) {
            View childAt = getChildAt(i2);
            if ((getDrawerViewAbsoluteGravity(childAt) & 7) == (absoluteGravity & 7)) {
                return childAt;
            }
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public View findOpenDrawer() {
        int childCount = getChildCount();
        for (int i = 0; i < childCount; i++) {
            View childAt = getChildAt(i);
            if ((((LayoutParams) childAt.getLayoutParams()).openState & 1) == 1) {
                return childAt;
            }
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public View findVisibleDrawer() {
        int childCount = getChildCount();
        for (int i = 0; i < childCount; i++) {
            View childAt = getChildAt(i);
            if (isDrawerView(childAt) && isDrawerVisible(childAt)) {
                return childAt;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public android.view.ViewGroup.LayoutParams generateDefaultLayoutParams() {
        return new LayoutParams(-1, -1);
    }

    public android.view.ViewGroup.LayoutParams generateLayoutParams(AttributeSet attributeSet) {
        return new LayoutParams(getContext(), attributeSet);
    }

    /* access modifiers changed from: protected */
    public android.view.ViewGroup.LayoutParams generateLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return layoutParams instanceof LayoutParams ? new LayoutParams((LayoutParams) layoutParams) : layoutParams instanceof MarginLayoutParams ? new LayoutParams((MarginLayoutParams) layoutParams) : new LayoutParams(layoutParams);
    }

    public float getDrawerElevation() {
        if (SET_DRAWER_SHADOW_FROM_ELEVATION) {
            return this.mDrawerElevation;
        }
        return 0.0f;
    }

    public int getDrawerLockMode(int i) {
        int layoutDirection = ViewCompat.getLayoutDirection(this);
        switch (i) {
            case 3:
                if (this.mLockModeLeft != 3) {
                    return this.mLockModeLeft;
                }
                int i2 = layoutDirection == 0 ? this.mLockModeStart : this.mLockModeEnd;
                if (i2 != 3) {
                    return i2;
                }
                break;
            case 5:
                if (this.mLockModeRight != 3) {
                    return this.mLockModeRight;
                }
                int i3 = layoutDirection == 0 ? this.mLockModeEnd : this.mLockModeStart;
                if (i3 != 3) {
                    return i3;
                }
                break;
            case GravityCompat.START /*8388611*/:
                if (this.mLockModeStart != 3) {
                    return this.mLockModeStart;
                }
                int i4 = layoutDirection == 0 ? this.mLockModeLeft : this.mLockModeRight;
                if (i4 != 3) {
                    return i4;
                }
                break;
            case GravityCompat.END /*8388613*/:
                if (this.mLockModeEnd != 3) {
                    return this.mLockModeEnd;
                }
                int i5 = layoutDirection == 0 ? this.mLockModeRight : this.mLockModeLeft;
                if (i5 != 3) {
                    return i5;
                }
                break;
        }
        return 0;
    }

    public int getDrawerLockMode(View view) {
        if (isDrawerView(view)) {
            return getDrawerLockMode(((LayoutParams) view.getLayoutParams()).gravity);
        }
        throw new IllegalArgumentException("View " + view + " is not a drawer");
    }

    @Nullable
    public CharSequence getDrawerTitle(int i) {
        int absoluteGravity = GravityCompat.getAbsoluteGravity(i, ViewCompat.getLayoutDirection(this));
        if (absoluteGravity == 3) {
            return this.mTitleLeft;
        }
        if (absoluteGravity == 5) {
            return this.mTitleRight;
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public int getDrawerViewAbsoluteGravity(View view) {
        return GravityCompat.getAbsoluteGravity(((LayoutParams) view.getLayoutParams()).gravity, ViewCompat.getLayoutDirection(this));
    }

    /* access modifiers changed from: 0000 */
    public float getDrawerViewOffset(View view) {
        return ((LayoutParams) view.getLayoutParams()).onScreen;
    }

    public Drawable getStatusBarBackgroundDrawable() {
        return this.mStatusBarBackground;
    }

    /* access modifiers changed from: 0000 */
    public boolean isContentView(View view) {
        return ((LayoutParams) view.getLayoutParams()).gravity == 0;
    }

    public boolean isDrawerOpen(int i) {
        View findDrawerWithGravity = findDrawerWithGravity(i);
        if (findDrawerWithGravity != null) {
            return isDrawerOpen(findDrawerWithGravity);
        }
        return false;
    }

    public boolean isDrawerOpen(View view) {
        if (isDrawerView(view)) {
            return (((LayoutParams) view.getLayoutParams()).openState & 1) == 1;
        }
        throw new IllegalArgumentException("View " + view + " is not a drawer");
    }

    /* access modifiers changed from: 0000 */
    public boolean isDrawerView(View view) {
        int absoluteGravity = GravityCompat.getAbsoluteGravity(((LayoutParams) view.getLayoutParams()).gravity, ViewCompat.getLayoutDirection(view));
        if ((absoluteGravity & 3) != 0) {
            return true;
        }
        return (absoluteGravity & 5) != 0;
    }

    public boolean isDrawerVisible(int i) {
        View findDrawerWithGravity = findDrawerWithGravity(i);
        if (findDrawerWithGravity != null) {
            return isDrawerVisible(findDrawerWithGravity);
        }
        return false;
    }

    public boolean isDrawerVisible(View view) {
        if (isDrawerView(view)) {
            return ((LayoutParams) view.getLayoutParams()).onScreen > 0.0f;
        }
        throw new IllegalArgumentException("View " + view + " is not a drawer");
    }

    /* access modifiers changed from: 0000 */
    public void moveDrawerToOffset(View view, float f) {
        float drawerViewOffset = getDrawerViewOffset(view);
        int width = view.getWidth();
        int i = ((int) (((float) width) * f)) - ((int) (drawerViewOffset * ((float) width)));
        if (!checkDrawerViewAbsoluteGravity(view, 3)) {
            i = -i;
        }
        view.offsetLeftAndRight(i);
        setDrawerViewOffset(view, f);
    }

    /* access modifiers changed from: protected */
    public void onAttachedToWindow() {
        super.onAttachedToWindow();
        this.mFirstLayout = true;
    }

    /* access modifiers changed from: protected */
    public void onDetachedFromWindow() {
        super.onDetachedFromWindow();
        this.mFirstLayout = true;
    }

    public void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        if (this.mDrawStatusBarBackground && this.mStatusBarBackground != null) {
            int i = VERSION.SDK_INT >= 21 ? this.mLastInsets != null ? ((WindowInsets) this.mLastInsets).getSystemWindowInsetTop() : 0 : 0;
            if (i > 0) {
                this.mStatusBarBackground.setBounds(0, 0, getWidth(), i);
                this.mStatusBarBackground.draw(canvas);
            }
        }
    }

    public boolean onInterceptTouchEvent(MotionEvent motionEvent) {
        boolean z;
        int actionMasked = motionEvent.getActionMasked();
        boolean shouldInterceptTouchEvent = this.mLeftDragger.shouldInterceptTouchEvent(motionEvent);
        boolean shouldInterceptTouchEvent2 = this.mRightDragger.shouldInterceptTouchEvent(motionEvent);
        switch (actionMasked) {
            case 0:
                float x = motionEvent.getX();
                float y = motionEvent.getY();
                this.mInitialMotionX = x;
                this.mInitialMotionY = y;
                if (this.mScrimOpacity > 0.0f) {
                    View findTopChildUnder = this.mLeftDragger.findTopChildUnder((int) x, (int) y);
                    if (findTopChildUnder != null && isContentView(findTopChildUnder)) {
                        z = true;
                        this.mDisallowInterceptRequested = false;
                        this.mChildrenCanceledTouch = false;
                        break;
                    }
                }
                z = false;
                this.mDisallowInterceptRequested = false;
                this.mChildrenCanceledTouch = false;
            case 1:
            case 3:
                closeDrawers(true);
                this.mDisallowInterceptRequested = false;
                this.mChildrenCanceledTouch = false;
                z = false;
                break;
            case 2:
                if (!this.mLeftDragger.checkTouchSlop(3)) {
                    z = false;
                    break;
                } else {
                    this.mLeftCallback.removeCallbacks();
                    this.mRightCallback.removeCallbacks();
                    z = false;
                    break;
                }
            default:
                z = false;
                break;
        }
        return shouldInterceptTouchEvent || shouldInterceptTouchEvent2 || z || hasPeekingDrawer() || this.mChildrenCanceledTouch;
    }

    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        if (i != 4 || !hasVisibleDrawer()) {
            return super.onKeyDown(i, keyEvent);
        }
        keyEvent.startTracking();
        return true;
    }

    public boolean onKeyUp(int i, KeyEvent keyEvent) {
        if (i != 4) {
            return super.onKeyUp(i, keyEvent);
        }
        View findVisibleDrawer = findVisibleDrawer();
        if (findVisibleDrawer != null && getDrawerLockMode(findVisibleDrawer) == 0) {
            closeDrawers();
        }
        return findVisibleDrawer != null;
    }

    /* access modifiers changed from: protected */
    public void onLayout(boolean z, int i, int i2, int i3, int i4) {
        int i5;
        float f;
        this.mInLayout = true;
        int i6 = i3 - i;
        int childCount = getChildCount();
        for (int i7 = 0; i7 < childCount; i7++) {
            View childAt = getChildAt(i7);
            if (childAt.getVisibility() != 8) {
                LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
                if (isContentView(childAt)) {
                    childAt.layout(layoutParams.leftMargin, layoutParams.topMargin, layoutParams.leftMargin + childAt.getMeasuredWidth(), layoutParams.topMargin + childAt.getMeasuredHeight());
                } else {
                    int measuredWidth = childAt.getMeasuredWidth();
                    int measuredHeight = childAt.getMeasuredHeight();
                    if (checkDrawerViewAbsoluteGravity(childAt, 3)) {
                        i5 = (-measuredWidth) + ((int) (((float) measuredWidth) * layoutParams.onScreen));
                        f = ((float) (measuredWidth + i5)) / ((float) measuredWidth);
                    } else {
                        i5 = i6 - ((int) (((float) measuredWidth) * layoutParams.onScreen));
                        f = ((float) (i6 - i5)) / ((float) measuredWidth);
                    }
                    boolean z2 = f != layoutParams.onScreen;
                    switch (layoutParams.gravity & 112) {
                        case 16:
                            int i8 = i4 - i2;
                            int i9 = (i8 - measuredHeight) / 2;
                            if (i9 < layoutParams.topMargin) {
                                i9 = layoutParams.topMargin;
                            } else if (i9 + measuredHeight > i8 - layoutParams.bottomMargin) {
                                i9 = (i8 - layoutParams.bottomMargin) - measuredHeight;
                            }
                            childAt.layout(i5, i9, measuredWidth + i5, measuredHeight + i9);
                            break;
                        case 80:
                            int i10 = i4 - i2;
                            childAt.layout(i5, (i10 - layoutParams.bottomMargin) - childAt.getMeasuredHeight(), measuredWidth + i5, i10 - layoutParams.bottomMargin);
                            break;
                        default:
                            childAt.layout(i5, layoutParams.topMargin, measuredWidth + i5, measuredHeight + layoutParams.topMargin);
                            break;
                    }
                    if (z2) {
                        setDrawerViewOffset(childAt, f);
                    }
                    int i11 = layoutParams.onScreen > 0.0f ? 0 : 4;
                    if (childAt.getVisibility() != i11) {
                        childAt.setVisibility(i11);
                    }
                }
            }
        }
        this.mInLayout = false;
        this.mFirstLayout = false;
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:18:0x0049  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void onMeasure(int r17, int r18) {
        /*
            r16 = this;
            int r3 = android.view.View.MeasureSpec.getMode(r17)
            int r4 = android.view.View.MeasureSpec.getMode(r18)
            int r1 = android.view.View.MeasureSpec.getSize(r17)
            int r2 = android.view.View.MeasureSpec.getSize(r18)
            r5 = 1073741824(0x40000000, float:2.0)
            if (r3 != r5) goto L_0x0018
            r5 = 1073741824(0x40000000, float:2.0)
            if (r4 == r5) goto L_0x01fb
        L_0x0018:
            boolean r5 = r16.isInEditMode()
            if (r5 == 0) goto L_0x0069
            r5 = -2147483648(0xffffffff80000000, float:-0.0)
            if (r3 != r5) goto L_0x005d
        L_0x0022:
            r3 = -2147483648(0xffffffff80000000, float:-0.0)
            if (r4 != r3) goto L_0x0062
            r3 = r1
            r4 = r2
        L_0x0028:
            r0 = r16
            r0.setMeasuredDimension(r3, r4)
            r0 = r16
            java.lang.Object r1 = r0.mLastInsets
            if (r1 == 0) goto L_0x0071
            boolean r1 = android.support.p000v4.view.ViewCompat.getFitsSystemWindows(r16)
            if (r1 == 0) goto L_0x0071
            r1 = 1
            r5 = r1
        L_0x003b:
            int r9 = android.support.p000v4.view.ViewCompat.getLayoutDirection(r16)
            r6 = 0
            r7 = 0
            int r10 = r16.getChildCount()
            r1 = 0
            r8 = r1
        L_0x0047:
            if (r8 >= r10) goto L_0x01fa
            r0 = r16
            android.view.View r11 = r0.getChildAt(r8)
            int r1 = r11.getVisibility()
            r2 = 8
            if (r1 != r2) goto L_0x0074
            r1 = r6
        L_0x0058:
            int r2 = r8 + 1
            r8 = r2
            r6 = r1
            goto L_0x0047
        L_0x005d:
            if (r3 != 0) goto L_0x0022
            r1 = 300(0x12c, float:4.2E-43)
            goto L_0x0022
        L_0x0062:
            if (r4 != 0) goto L_0x01fb
            r2 = 300(0x12c, float:4.2E-43)
            r3 = r1
            r4 = r2
            goto L_0x0028
        L_0x0069:
            java.lang.IllegalArgumentException r1 = new java.lang.IllegalArgumentException
            java.lang.String r2 = "DrawerLayout must be measured with MeasureSpec.EXACTLY."
            r1.<init>(r2)
            throw r1
        L_0x0071:
            r1 = 0
            r5 = r1
            goto L_0x003b
        L_0x0074:
            android.view.ViewGroup$LayoutParams r1 = r11.getLayoutParams()
            android.support.v4.widget.DrawerLayout$LayoutParams r1 = (android.support.p000v4.widget.DrawerLayout.LayoutParams) r1
            if (r5 == 0) goto L_0x00ab
            int r2 = r1.gravity
            int r12 = android.support.p000v4.view.GravityCompat.getAbsoluteGravity(r2, r9)
            boolean r2 = android.support.p000v4.view.ViewCompat.getFitsSystemWindows(r11)
            if (r2 == 0) goto L_0x00e8
            int r2 = android.os.Build.VERSION.SDK_INT
            r13 = 21
            if (r2 < r13) goto L_0x00ab
            r0 = r16
            java.lang.Object r2 = r0.mLastInsets
            android.view.WindowInsets r2 = (android.view.WindowInsets) r2
            r13 = 3
            if (r12 != r13) goto L_0x00d3
            int r12 = r2.getSystemWindowInsetLeft()
            int r13 = r2.getSystemWindowInsetTop()
            r14 = 0
            int r15 = r2.getSystemWindowInsetBottom()
            android.view.WindowInsets r2 = r2.replaceSystemWindowInsets(r12, r13, r14, r15)
        L_0x00a8:
            r11.dispatchApplyWindowInsets(r2)
        L_0x00ab:
            r0 = r16
            boolean r2 = r0.isContentView(r11)
            if (r2 == 0) goto L_0x0136
            int r2 = r1.leftMargin
            int r2 = r3 - r2
            int r12 = r1.rightMargin
            int r2 = r2 - r12
            r12 = 1073741824(0x40000000, float:2.0)
            int r2 = android.view.View.MeasureSpec.makeMeasureSpec(r2, r12)
            int r12 = r1.topMargin
            int r12 = r4 - r12
            int r1 = r1.bottomMargin
            int r1 = r12 - r1
            r12 = 1073741824(0x40000000, float:2.0)
            int r1 = android.view.View.MeasureSpec.makeMeasureSpec(r1, r12)
            r11.measure(r2, r1)
            r1 = r6
            goto L_0x0058
        L_0x00d3:
            r13 = 5
            if (r12 != r13) goto L_0x00a8
            r12 = 0
            int r13 = r2.getSystemWindowInsetTop()
            int r14 = r2.getSystemWindowInsetRight()
            int r15 = r2.getSystemWindowInsetBottom()
            android.view.WindowInsets r2 = r2.replaceSystemWindowInsets(r12, r13, r14, r15)
            goto L_0x00a8
        L_0x00e8:
            int r2 = android.os.Build.VERSION.SDK_INT
            r13 = 21
            if (r2 < r13) goto L_0x00ab
            r0 = r16
            java.lang.Object r2 = r0.mLastInsets
            android.view.WindowInsets r2 = (android.view.WindowInsets) r2
            r13 = 3
            if (r12 != r13) goto L_0x0121
            int r12 = r2.getSystemWindowInsetLeft()
            int r13 = r2.getSystemWindowInsetTop()
            r14 = 0
            int r15 = r2.getSystemWindowInsetBottom()
            android.view.WindowInsets r2 = r2.replaceSystemWindowInsets(r12, r13, r14, r15)
        L_0x0108:
            int r12 = r2.getSystemWindowInsetLeft()
            r1.leftMargin = r12
            int r12 = r2.getSystemWindowInsetTop()
            r1.topMargin = r12
            int r12 = r2.getSystemWindowInsetRight()
            r1.rightMargin = r12
            int r2 = r2.getSystemWindowInsetBottom()
            r1.bottomMargin = r2
            goto L_0x00ab
        L_0x0121:
            r13 = 5
            if (r12 != r13) goto L_0x0108
            r12 = 0
            int r13 = r2.getSystemWindowInsetTop()
            int r14 = r2.getSystemWindowInsetRight()
            int r15 = r2.getSystemWindowInsetBottom()
            android.view.WindowInsets r2 = r2.replaceSystemWindowInsets(r12, r13, r14, r15)
            goto L_0x0108
        L_0x0136:
            r0 = r16
            boolean r2 = r0.isDrawerView(r11)
            if (r2 == 0) goto L_0x01cb
            boolean r2 = SET_DRAWER_SHADOW_FROM_ELEVATION
            if (r2 == 0) goto L_0x0155
            float r2 = android.support.p000v4.view.ViewCompat.getElevation(r11)
            r0 = r16
            float r12 = r0.mDrawerElevation
            int r2 = (r2 > r12 ? 1 : (r2 == r12 ? 0 : -1))
            if (r2 == 0) goto L_0x0155
            r0 = r16
            float r2 = r0.mDrawerElevation
            android.support.p000v4.view.ViewCompat.setElevation(r11, r2)
        L_0x0155:
            r0 = r16
            int r2 = r0.getDrawerViewAbsoluteGravity(r11)
            r12 = r2 & 7
            r2 = 3
            if (r12 != r2) goto L_0x019e
            r2 = 1
        L_0x0161:
            if (r2 == 0) goto L_0x0165
            if (r6 != 0) goto L_0x0169
        L_0x0165:
            if (r2 != 0) goto L_0x01a0
            if (r7 == 0) goto L_0x01a0
        L_0x0169:
            java.lang.IllegalStateException r1 = new java.lang.IllegalStateException
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Child drawer has absolute gravity "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = gravityToString(r12)
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = " but this "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = "DrawerLayout"
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = " already has a "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = "drawer view along that edge"
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            r1.<init>(r2)
            throw r1
        L_0x019e:
            r2 = 0
            goto L_0x0161
        L_0x01a0:
            if (r2 == 0) goto L_0x01c8
            r2 = 1
        L_0x01a3:
            r0 = r16
            int r6 = r0.mMinDrawerMargin
            int r12 = r1.leftMargin
            int r6 = r6 + r12
            int r12 = r1.rightMargin
            int r6 = r6 + r12
            int r12 = r1.width
            r0 = r17
            int r6 = getChildMeasureSpec(r0, r6, r12)
            int r12 = r1.topMargin
            int r13 = r1.bottomMargin
            int r12 = r12 + r13
            int r1 = r1.height
            r0 = r18
            int r1 = getChildMeasureSpec(r0, r12, r1)
            r11.measure(r6, r1)
            r1 = r2
            goto L_0x0058
        L_0x01c8:
            r7 = 1
            r2 = r6
            goto L_0x01a3
        L_0x01cb:
            java.lang.IllegalStateException r1 = new java.lang.IllegalStateException
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Child "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.StringBuilder r2 = r2.append(r11)
            java.lang.String r3 = " at index "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.StringBuilder r2 = r2.append(r8)
            java.lang.String r3 = " does not have a valid layout_gravity - must be Gravity.LEFT, "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = "Gravity.RIGHT or Gravity.NO_GRAVITY"
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            r1.<init>(r2)
            throw r1
        L_0x01fa:
            return
        L_0x01fb:
            r3 = r1
            r4 = r2
            goto L_0x0028
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.widget.DrawerLayout.onMeasure(int, int):void");
    }

    /* access modifiers changed from: protected */
    public void onRestoreInstanceState(Parcelable parcelable) {
        if (!(parcelable instanceof SavedState)) {
            super.onRestoreInstanceState(parcelable);
            return;
        }
        SavedState savedState = (SavedState) parcelable;
        super.onRestoreInstanceState(savedState.getSuperState());
        if (savedState.openDrawerGravity != 0) {
            View findDrawerWithGravity = findDrawerWithGravity(savedState.openDrawerGravity);
            if (findDrawerWithGravity != null) {
                openDrawer(findDrawerWithGravity);
            }
        }
        if (savedState.lockModeLeft != 3) {
            setDrawerLockMode(savedState.lockModeLeft, 3);
        }
        if (savedState.lockModeRight != 3) {
            setDrawerLockMode(savedState.lockModeRight, 5);
        }
        if (savedState.lockModeStart != 3) {
            setDrawerLockMode(savedState.lockModeStart, (int) GravityCompat.START);
        }
        if (savedState.lockModeEnd != 3) {
            setDrawerLockMode(savedState.lockModeEnd, (int) GravityCompat.END);
        }
    }

    public void onRtlPropertiesChanged(int i) {
        resolveShadowDrawables();
    }

    /* access modifiers changed from: protected */
    public Parcelable onSaveInstanceState() {
        LayoutParams layoutParams;
        SavedState savedState = new SavedState(super.onSaveInstanceState());
        int childCount = getChildCount();
        int i = 0;
        while (true) {
            if (i >= childCount) {
                break;
            }
            layoutParams = (LayoutParams) getChildAt(i).getLayoutParams();
            boolean z = layoutParams.openState == 1;
            boolean z2 = layoutParams.openState == 2;
            if (z || z2) {
                savedState.openDrawerGravity = layoutParams.gravity;
            } else {
                i++;
            }
        }
        savedState.openDrawerGravity = layoutParams.gravity;
        savedState.lockModeLeft = this.mLockModeLeft;
        savedState.lockModeRight = this.mLockModeRight;
        savedState.lockModeStart = this.mLockModeStart;
        savedState.lockModeEnd = this.mLockModeEnd;
        return savedState;
    }

    public boolean onTouchEvent(MotionEvent motionEvent) {
        boolean z;
        this.mLeftDragger.processTouchEvent(motionEvent);
        this.mRightDragger.processTouchEvent(motionEvent);
        switch (motionEvent.getAction() & 255) {
            case 0:
                float x = motionEvent.getX();
                float y = motionEvent.getY();
                this.mInitialMotionX = x;
                this.mInitialMotionY = y;
                this.mDisallowInterceptRequested = false;
                this.mChildrenCanceledTouch = false;
                break;
            case 1:
                float x2 = motionEvent.getX();
                float y2 = motionEvent.getY();
                View findTopChildUnder = this.mLeftDragger.findTopChildUnder((int) x2, (int) y2);
                if (findTopChildUnder != null && isContentView(findTopChildUnder)) {
                    float f = x2 - this.mInitialMotionX;
                    float f2 = y2 - this.mInitialMotionY;
                    int touchSlop = this.mLeftDragger.getTouchSlop();
                    if ((f * f) + (f2 * f2) < ((float) (touchSlop * touchSlop))) {
                        View findOpenDrawer = findOpenDrawer();
                        if (findOpenDrawer != null) {
                            z = getDrawerLockMode(findOpenDrawer) == 2;
                            closeDrawers(z);
                            this.mDisallowInterceptRequested = false;
                            break;
                        }
                    }
                }
                z = true;
                closeDrawers(z);
                this.mDisallowInterceptRequested = false;
            case 3:
                closeDrawers(true);
                this.mDisallowInterceptRequested = false;
                this.mChildrenCanceledTouch = false;
                break;
        }
        return true;
    }

    public void openDrawer(int i) {
        openDrawer(i, true);
    }

    public void openDrawer(int i, boolean z) {
        View findDrawerWithGravity = findDrawerWithGravity(i);
        if (findDrawerWithGravity == null) {
            throw new IllegalArgumentException("No drawer view found with gravity " + gravityToString(i));
        }
        openDrawer(findDrawerWithGravity, z);
    }

    public void openDrawer(View view) {
        openDrawer(view, true);
    }

    public void openDrawer(View view, boolean z) {
        if (!isDrawerView(view)) {
            throw new IllegalArgumentException("View " + view + " is not a sliding drawer");
        }
        LayoutParams layoutParams = (LayoutParams) view.getLayoutParams();
        if (this.mFirstLayout) {
            layoutParams.onScreen = TOUCH_SLOP_SENSITIVITY;
            layoutParams.openState = 1;
            updateChildrenImportantForAccessibility(view, true);
        } else if (z) {
            layoutParams.openState |= 2;
            if (checkDrawerViewAbsoluteGravity(view, 3)) {
                this.mLeftDragger.smoothSlideViewTo(view, 0, view.getTop());
            } else {
                this.mRightDragger.smoothSlideViewTo(view, getWidth() - view.getWidth(), view.getTop());
            }
        } else {
            moveDrawerToOffset(view, TOUCH_SLOP_SENSITIVITY);
            updateDrawerState(layoutParams.gravity, 0, view);
            view.setVisibility(0);
        }
        invalidate();
    }

    public void removeDrawerListener(@NonNull DrawerListener drawerListener) {
        if (drawerListener != null && this.mListeners != null) {
            this.mListeners.remove(drawerListener);
        }
    }

    public void requestDisallowInterceptTouchEvent(boolean z) {
        super.requestDisallowInterceptTouchEvent(z);
        this.mDisallowInterceptRequested = z;
        if (z) {
            closeDrawers(true);
        }
    }

    public void requestLayout() {
        if (!this.mInLayout) {
            super.requestLayout();
        }
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public void setChildInsets(Object obj, boolean z) {
        this.mLastInsets = obj;
        this.mDrawStatusBarBackground = z;
        setWillNotDraw(!z && getBackground() == null);
        requestLayout();
    }

    public void setDrawerElevation(float f) {
        this.mDrawerElevation = f;
        for (int i = 0; i < getChildCount(); i++) {
            View childAt = getChildAt(i);
            if (isDrawerView(childAt)) {
                ViewCompat.setElevation(childAt, this.mDrawerElevation);
            }
        }
    }

    @Deprecated
    public void setDrawerListener(DrawerListener drawerListener) {
        if (this.mListener != null) {
            removeDrawerListener(this.mListener);
        }
        if (drawerListener != null) {
            addDrawerListener(drawerListener);
        }
        this.mListener = drawerListener;
    }

    public void setDrawerLockMode(int i) {
        setDrawerLockMode(i, 3);
        setDrawerLockMode(i, 5);
    }

    public void setDrawerLockMode(int i, int i2) {
        int absoluteGravity = GravityCompat.getAbsoluteGravity(i2, ViewCompat.getLayoutDirection(this));
        switch (i2) {
            case 3:
                this.mLockModeLeft = i;
                break;
            case 5:
                this.mLockModeRight = i;
                break;
            case GravityCompat.START /*8388611*/:
                this.mLockModeStart = i;
                break;
            case GravityCompat.END /*8388613*/:
                this.mLockModeEnd = i;
                break;
        }
        if (i != 0) {
            (absoluteGravity == 3 ? this.mLeftDragger : this.mRightDragger).cancel();
        }
        switch (i) {
            case 1:
                View findDrawerWithGravity = findDrawerWithGravity(absoluteGravity);
                if (findDrawerWithGravity != null) {
                    closeDrawer(findDrawerWithGravity);
                    return;
                }
                return;
            case 2:
                View findDrawerWithGravity2 = findDrawerWithGravity(absoluteGravity);
                if (findDrawerWithGravity2 != null) {
                    openDrawer(findDrawerWithGravity2);
                    return;
                }
                return;
            default:
                return;
        }
    }

    public void setDrawerLockMode(int i, View view) {
        if (!isDrawerView(view)) {
            throw new IllegalArgumentException("View " + view + " is not a " + "drawer with appropriate layout_gravity");
        }
        setDrawerLockMode(i, ((LayoutParams) view.getLayoutParams()).gravity);
    }

    public void setDrawerShadow(@DrawableRes int i, int i2) {
        setDrawerShadow(ContextCompat.getDrawable(getContext(), i), i2);
    }

    public void setDrawerShadow(Drawable drawable, int i) {
        if (!SET_DRAWER_SHADOW_FROM_ELEVATION) {
            if ((i & GravityCompat.START) == 8388611) {
                this.mShadowStart = drawable;
            } else if ((i & GravityCompat.END) == 8388613) {
                this.mShadowEnd = drawable;
            } else if ((i & 3) == 3) {
                this.mShadowLeft = drawable;
            } else if ((i & 5) == 5) {
                this.mShadowRight = drawable;
            } else {
                return;
            }
            resolveShadowDrawables();
            invalidate();
        }
    }

    public void setDrawerTitle(int i, CharSequence charSequence) {
        int absoluteGravity = GravityCompat.getAbsoluteGravity(i, ViewCompat.getLayoutDirection(this));
        if (absoluteGravity == 3) {
            this.mTitleLeft = charSequence;
        } else if (absoluteGravity == 5) {
            this.mTitleRight = charSequence;
        }
    }

    /* access modifiers changed from: 0000 */
    public void setDrawerViewOffset(View view, float f) {
        LayoutParams layoutParams = (LayoutParams) view.getLayoutParams();
        if (f != layoutParams.onScreen) {
            layoutParams.onScreen = f;
            dispatchOnDrawerSlide(view, f);
        }
    }

    public void setScrimColor(@ColorInt int i) {
        this.mScrimColor = i;
        invalidate();
    }

    public void setStatusBarBackground(int i) {
        this.mStatusBarBackground = i != 0 ? ContextCompat.getDrawable(getContext(), i) : null;
        invalidate();
    }

    public void setStatusBarBackground(Drawable drawable) {
        this.mStatusBarBackground = drawable;
        invalidate();
    }

    public void setStatusBarBackgroundColor(@ColorInt int i) {
        this.mStatusBarBackground = new ColorDrawable(i);
        invalidate();
    }

    /* access modifiers changed from: 0000 */
    public void updateDrawerState(int i, int i2, View view) {
        int viewDragState = this.mLeftDragger.getViewDragState();
        int viewDragState2 = this.mRightDragger.getViewDragState();
        int i3 = (viewDragState == 1 || viewDragState2 == 1) ? 1 : (viewDragState == 2 || viewDragState2 == 2) ? 2 : 0;
        if (view != null && i2 == 0) {
            LayoutParams layoutParams = (LayoutParams) view.getLayoutParams();
            if (layoutParams.onScreen == 0.0f) {
                dispatchOnDrawerClosed(view);
            } else if (layoutParams.onScreen == TOUCH_SLOP_SENSITIVITY) {
                dispatchOnDrawerOpened(view);
            }
        }
        if (i3 != this.mDrawerState) {
            this.mDrawerState = i3;
            if (this.mListeners != null) {
                for (int size = this.mListeners.size() - 1; size >= 0; size--) {
                    ((DrawerListener) this.mListeners.get(size)).onDrawerStateChanged(i3);
                }
            }
        }
    }
}
