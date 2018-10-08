package net.gogame.gowrap.ui.dpro.view;

import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.graphics.Path;
import android.graphics.PointF;
import android.graphics.PorterDuff.Mode;
import android.graphics.PorterDuffXfermode;
import android.graphics.Rect;
import android.graphics.RectF;
import android.graphics.Typeface;
import android.graphics.drawable.Drawable;
import android.text.TextPaint;
import android.text.TextUtils;
import android.text.TextUtils.TruncateAt;
import android.util.AttributeSet;
import android.util.Log;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.ViewGroup;
import android.view.ViewGroup.MarginLayoutParams;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.ui.dpro.C1471R;
import net.gogame.gowrap.ui.utils.DisplayUtils;

public class CustomTabbelPanel extends ViewGroup {
    private static final float DEFAULT_CHAMFER_SLOPE = 45.0f;
    private static final float DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM = 2.0f;
    private static final float DEFAULT_CLOSE_BUTTON_MARGIN_LEFT = 4.0f;
    private static final float DEFAULT_CLOSE_BUTTON_MARGIN_RIGHT = 0.0f;
    private static final float DEFAULT_CLOSE_BUTTON_MARGIN_TOP = 0.0f;
    private static final float DEFAULT_CLOSE_BUTTON_PADDING_BOTTOM = 0.0f;
    private static final float DEFAULT_CLOSE_BUTTON_PADDING_LEFT = 0.0f;
    private static final float DEFAULT_CLOSE_BUTTON_PADDING_RIGHT = 0.0f;
    private static final float DEFAULT_CLOSE_BUTTON_PADDING_TOP = 0.0f;
    private static final float DEFAULT_EDGE_SLOPE_HEIGHT_RATIO = 0.5f;
    private static final int DEFAULT_FILL_COLOR = -1071245480;
    private static final String DEFAULT_FONT_FAMILY = "sans-serif";
    private static final int DEFAULT_INNER_LINE_COLOR = -13659232;
    private static final float DEFAULT_INNER_LINE_WIDTH = 1.0f;
    private static final int DEFAULT_OUTER_LINE_CHAMFER = 8;
    private static final int DEFAULT_OUTER_LINE_COLOR = -11806733;
    private static final float DEFAULT_OUTER_LINE_WIDTH = 1.0f;
    private static final float DEFAULT_PATH_OFFSET = 6.0f;
    private static final int DEFAULT_SELECTED_TEXT_COLOR = -1;
    private static final int DEFAULT_SELECTED_TEXT_STYLE = 1;
    private static final int DEFAULT_TAB_FILL_COLOR = -1071245480;
    private static final float DEFAULT_TAB_LABEL_MARGIN_BOTTOM = 16.0f;
    private static final float DEFAULT_TAB_LABEL_MARGIN_LEFT = 8.0f;
    private static final float DEFAULT_TAB_LABEL_MARGIN_RIGHT = 8.0f;
    private static final float DEFAULT_TAB_LABEL_MARGIN_TOP = 16.0f;
    private static final float DEFAULT_TAB_LABEL_PADDING_BOTTOM = 0.0f;
    private static final float DEFAULT_TAB_LABEL_PADDING_LEFT = 0.0f;
    private static final float DEFAULT_TAB_LABEL_PADDING_RIGHT = 0.0f;
    private static final float DEFAULT_TAB_LABEL_PADDING_TOP = 0.0f;
    private static final float DEFAULT_TAB_SLOPE = 58.0f;
    private static final int DEFAULT_TEXT_COLOR = -1;
    private static final float DEFAULT_TEXT_SIZE = 12.0f;
    private static final int DEFAULT_TEXT_STYLE = 0;
    private static final String TAG = "UI-2017-2-DPRO";
    private float chamferSlope = DEFAULT_CHAMFER_SLOPE;
    private Listener listener;
    private RectF mBounds = new RectF();
    private Paint mClearPaint;
    private RectF mCloseButtonBounds;
    private Drawable mCloseButtonDrawable;
    private float mCloseButtonMarginBottom = DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM;
    private float mCloseButtonMarginLeft = DEFAULT_CLOSE_BUTTON_MARGIN_LEFT;
    private float mCloseButtonMarginRight = 0.0f;
    private float mCloseButtonMarginTop = 0.0f;
    private Margins mCloseButtonMargins;
    private Margins mCloseButtonPadding;
    private float mCloseButtonPaddingBottom = 0.0f;
    private float mCloseButtonPaddingLeft = 0.0f;
    private float mCloseButtonPaddingRight = 0.0f;
    private float mCloseButtonPaddingTop = 0.0f;
    private RectF mContentBounds;
    private float mEdgeSlopeHeightRatio = DEFAULT_EDGE_SLOPE_HEIGHT_RATIO;
    private int mFillColor = -1071245480;
    private Paint mFillPaint;
    private String mFontFamily = DEFAULT_FONT_FAMILY;
    private int mInnerLineColor = DEFAULT_INNER_LINE_COLOR;
    private Paint mInnerLinePaint;
    private Path mInnerLinePathBottom;
    private float mInnerLineWidth = 1.0f;
    private PointF mOuterChamfer;
    private float mOuterLineChamfer = 8.0f;
    private int mOuterLineColor = DEFAULT_OUTER_LINE_COLOR;
    private Paint mOuterLinePaint;
    private Path mOuterLinePathBottom;
    private float mOuterLineWidth = 1.0f;
    private float mPathOffset = DEFAULT_PATH_OFFSET;
    private int mSelectedTabIndex = 0;
    private Paint mSelectedTabLabelPaint;
    private int mSelectedTextColor = -1;
    private int mSelectedTextStyle = 1;
    private int mTabFillColor = -1071245480;
    private Paint mTabFillPaint;
    private float mTabLabelMarginBottom = 16.0f;
    private float mTabLabelMarginLeft = 8.0f;
    private float mTabLabelMarginRight = 8.0f;
    private float mTabLabelMarginTop = 16.0f;
    private Margins mTabLabelMargins;
    private Margins mTabLabelPadding;
    private float mTabLabelPaddingBottom = 0.0f;
    private float mTabLabelPaddingLeft = 0.0f;
    private float mTabLabelPaddingRight = 0.0f;
    private float mTabLabelPaddingTop = 0.0f;
    private Paint mTabLabelPaint;
    private float mTabSlope = DEFAULT_TAB_SLOPE;
    private int mTextColor = -1;
    private float mTextSize = DEFAULT_TEXT_SIZE;
    private int mTextStyle = 0;
    private final Rect mTmpChildRect = new Rect();
    private final Rect mTmpContainerRect = new Rect();
    private final Rect mTmpTabLabelBounds = new Rect();
    private final List<TabHolder> tabHolderList = new ArrayList();
    private final List<String> tabLabels = new ArrayList();

    public interface Listener {
        void onClose();

        void onTabSelected(int i);
    }

    public static class LayoutParams extends android.widget.FrameLayout.LayoutParams {
        public LayoutParams(Context context, AttributeSet attributeSet) {
            super(context, attributeSet);
            context.obtainStyledAttributes(attributeSet, C1471R.styleable.CustomTabbedPanel_Layout).recycle();
        }

        public LayoutParams(int i, int i2) {
            super(i, i2);
        }

        public LayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
            super(layoutParams);
        }
    }

    private static class TabGeometry {
        private final float contentTop;
        private final RectF labelBounds;
        private final Path path;
        private final RectF tabBounds;

        public TabGeometry(Path path, RectF rectF, RectF rectF2, float f) {
            this.path = path;
            this.tabBounds = rectF;
            this.labelBounds = rectF2;
            this.contentTop = f;
        }

        public Path getPath() {
            return this.path;
        }

        public RectF getTabBounds() {
            return this.tabBounds;
        }

        public RectF getLabelBounds() {
            return this.labelBounds;
        }

        public float getContentTop() {
            return this.contentTop;
        }
    }

    private static class TabHolder {
        private final float contentTop;
        private final Path innerPath;
        private final RectF labelBounds;
        private final Path outerPath;
        private final RectF tabBounds;

        public TabHolder(Path path, Path path2, RectF rectF, RectF rectF2, float f) {
            this.outerPath = path;
            this.innerPath = path2;
            this.tabBounds = rectF;
            this.labelBounds = rectF2;
            this.contentTop = f;
        }

        public Path getOuterPath() {
            return this.outerPath;
        }

        public Path getInnerPath() {
            return this.innerPath;
        }

        public RectF getTabBounds() {
            return this.tabBounds;
        }

        public RectF getLabelBounds() {
            return this.labelBounds;
        }

        public float getContentTop() {
            return this.contentTop;
        }
    }

    public CustomTabbelPanel(Context context) {
        super(context);
        init();
    }

    public CustomTabbelPanel(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        TypedArray obtainStyledAttributes = context.getTheme().obtainStyledAttributes(attributeSet, C1471R.styleable.CustomTabbelPanel, 0, 0);
        try {
            this.mCloseButtonDrawable = obtainStyledAttributes.getDrawable(C1471R.styleable.CustomTabbelPanel_closeButton);
            this.mPathOffset = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_pathOffset, fromDp(DEFAULT_PATH_OFFSET));
            this.mTabSlope = obtainStyledAttributes.getFloat(C1471R.styleable.CustomTabbelPanel_tabSlope, DEFAULT_TAB_SLOPE);
            this.mTabFillColor = obtainStyledAttributes.getColor(C1471R.styleable.CustomTabbelPanel_tabFillColor, -1071245480);
            this.mFillColor = obtainStyledAttributes.getColor(C1471R.styleable.CustomTabbelPanel_fillColor, -1071245480);
            this.mOuterLineColor = obtainStyledAttributes.getColor(C1471R.styleable.CustomTabbelPanel_outerLineColor, DEFAULT_OUTER_LINE_COLOR);
            this.mOuterLineWidth = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_outerLineWidth, fromDp(1.0f));
            this.mOuterLineChamfer = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_outerLineChamfer, fromDp(8.0f));
            this.mInnerLineColor = obtainStyledAttributes.getColor(C1471R.styleable.CustomTabbelPanel_innerLineColor, DEFAULT_INNER_LINE_COLOR);
            this.mInnerLineWidth = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_innerLineWidth, fromDp(1.0f));
            this.mFontFamily = obtainStyledAttributes.getString(C1471R.styleable.CustomTabbelPanel_fontFamily);
            this.mTextSize = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_textSize, fromSp(DEFAULT_TEXT_SIZE));
            this.mTextStyle = obtainStyledAttributes.getInteger(C1471R.styleable.CustomTabbelPanel_textStyle, 0);
            this.mTextColor = obtainStyledAttributes.getColor(C1471R.styleable.CustomTabbelPanel_textColor, -1);
            this.mSelectedTextStyle = obtainStyledAttributes.getInteger(C1471R.styleable.CustomTabbelPanel_selectedTextStyle, 1);
            this.mSelectedTextColor = obtainStyledAttributes.getColor(C1471R.styleable.CustomTabbelPanel_selectedTextColor, -1);
            this.mTabLabelMarginLeft = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelMarginLeft, fromDp(8.0f));
            this.mTabLabelMarginTop = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelMarginTop, fromDp(16.0f));
            this.mTabLabelMarginRight = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelMarginRight, fromDp(8.0f));
            this.mTabLabelMarginBottom = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelMarginBottom, fromDp(16.0f));
            this.mTabLabelPaddingLeft = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelPaddingLeft, fromDp(0.0f));
            this.mTabLabelPaddingTop = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelPaddingTop, fromDp(0.0f));
            this.mTabLabelPaddingRight = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelPaddingRight, fromDp(0.0f));
            this.mTabLabelPaddingBottom = obtainStyledAttributes.getDimension(C1471R.styleable.CustomTabbelPanel_tabLabelPaddingBottom, fromDp(0.0f));
            init();
        } finally {
            obtainStyledAttributes.recycle();
        }
    }

    public Listener getListener() {
        return this.listener;
    }

    public void setListener(Listener listener) {
        this.listener = listener;
    }

    public void selectTab(int i) {
        if (i >= 0 && i < this.tabHolderList.size() && i != this.mSelectedTabIndex) {
            if (this.listener != null) {
                try {
                    this.listener.onTabSelected(i);
                } catch (Throwable e) {
                    Log.e(TAG, "Exception", e);
                }
            }
            this.mSelectedTabIndex = i;
            calculatePaths();
            invalidate();
        }
    }

    public boolean dispatchTouchEvent(MotionEvent motionEvent) {
        if (!isEnabled()) {
            return super.dispatchTouchEvent(motionEvent);
        }
        if (motionEvent.getAction() == 1) {
            Integer componentId = getComponentId(motionEvent);
            if (componentId != null) {
                if (componentId.intValue() != -1) {
                    selectTab(componentId.intValue());
                } else if (this.listener != null) {
                    try {
                        this.listener.onClose();
                    } catch (Throwable e) {
                        Log.e(TAG, "Error", e);
                    }
                }
            }
        }
        super.dispatchTouchEvent(motionEvent);
        return true;
    }

    public LayoutParams generateLayoutParams(AttributeSet attributeSet) {
        return new LayoutParams(getContext(), attributeSet);
    }

    protected LayoutParams generateDefaultLayoutParams() {
        return new LayoutParams(-1, -2);
    }

    protected android.view.ViewGroup.LayoutParams generateLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return new LayoutParams(layoutParams);
    }

    protected boolean checkLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return layoutParams instanceof LayoutParams;
    }

    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        canvas.drawPath(this.mOuterLinePathBottom, this.mFillPaint);
        canvas.drawPath(this.mOuterLinePathBottom, this.mOuterLinePaint);
        canvas.drawPath(this.mInnerLinePathBottom, this.mInnerLinePaint);
        for (int size = this.tabHolderList.size() - 1; size >= 0; size--) {
            if (size != this.mSelectedTabIndex) {
                drawTab(canvas, size);
            }
        }
        drawTab(canvas, this.mSelectedTabIndex);
        if (this.mCloseButtonDrawable != null && this.mCloseButtonBounds != null) {
            this.mCloseButtonDrawable.setBounds((int) this.mCloseButtonBounds.left, (int) this.mCloseButtonBounds.top, (int) this.mCloseButtonBounds.right, (int) this.mCloseButtonBounds.bottom);
            this.mCloseButtonDrawable.draw(canvas);
        }
    }

    protected void onLayout(boolean z, int i, int i2, int i3, int i4) {
        this.mTmpContainerRect.left = getPaddingLeft() + ((int) this.mPathOffset);
        this.mTmpContainerRect.top = getPaddingTop() + ((int) ((((calculateMaxTabLabelHeight() + this.mTabLabelMargins.top) + this.mTabLabelMargins.bottom) + this.mTabLabelPadding.top) + this.mTabLabelPadding.bottom));
        this.mTmpContainerRect.right = (getMeasuredWidth() - getPaddingRight()) - ((int) this.mPathOffset);
        this.mTmpContainerRect.bottom = (getMeasuredHeight() - getPaddingBottom()) - ((int) this.mPathOffset);
        for (int i5 = 0; i5 < getChildCount(); i5++) {
            View childAt = getChildAt(i5);
            if (childAt.getVisibility() != 8) {
                LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
                Gravity.apply(layoutParams.gravity, childAt.getMeasuredWidth(), childAt.getMeasuredHeight(), this.mTmpContainerRect, this.mTmpChildRect);
                childAt.layout(this.mTmpChildRect.left, this.mTmpChildRect.top, this.mTmpChildRect.right, this.mTmpChildRect.bottom);
            }
        }
    }

    protected void onMeasure(int i, int i2) {
        int makeMeasureSpec;
        int mode = MeasureSpec.getMode(i);
        int size = MeasureSpec.getSize(i);
        int mode2 = MeasureSpec.getMode(i2);
        int size2 = MeasureSpec.getSize(i2);
        switch (mode) {
            case Integer.MIN_VALUE:
            case 1073741824:
                makeMeasureSpec = MeasureSpec.makeMeasureSpec(Math.max(((size - getPaddingLeft()) - getPaddingRight()) - ((int) (this.mPathOffset * DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM)), 0), mode);
                break;
            default:
                makeMeasureSpec = MeasureSpec.makeMeasureSpec(0, 0);
                break;
        }
        switch (mode2) {
            case Integer.MIN_VALUE:
            case 1073741824:
                mode2 = MeasureSpec.makeMeasureSpec(Math.max((((size2 - getPaddingTop()) - getPaddingBottom()) - ((int) (this.mPathOffset * DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM))) - ((int) ((((this.mTabLabelMargins.top + this.mTabLabelMargins.bottom) + this.mTabLabelPadding.top) + this.mTabLabelPadding.bottom) + calculateMaxTabLabelHeight())), 0), mode2);
                break;
            default:
                mode2 = MeasureSpec.makeMeasureSpec(0, 0);
                break;
        }
        int i3 = 0;
        int i4 = 0;
        int i5 = 0;
        int i6 = 0;
        while (i3 < getChildCount()) {
            View childAt = getChildAt(i3);
            if (childAt.getVisibility() != 8) {
                measureChildWithMargins(childAt, makeMeasureSpec, 0, mode2, 0);
                MarginLayoutParams marginLayoutParams = (MarginLayoutParams) childAt.getLayoutParams();
                size2 = Math.max(i5, (childAt.getMeasuredWidth() + marginLayoutParams.leftMargin) + marginLayoutParams.rightMargin);
                size = Math.max(i4, marginLayoutParams.bottomMargin + (childAt.getMeasuredHeight() + marginLayoutParams.topMargin));
                i4 = combineMeasuredStates(i6, childAt.getMeasuredState());
            } else {
                size = i4;
                size2 = i5;
                i4 = i6;
            }
            i3++;
            i5 = size2;
            i6 = i4;
            i4 = size;
        }
        setMeasuredDimension(resolveSizeAndState(i5, i, i6), resolveSizeAndState(i4, i2, i6 << 16));
    }

    protected void onSizeChanged(int i, int i2, int i3, int i4) {
        super.onSizeChanged(i, i2, i3, i4);
        calculateBounds(i, i2);
    }

    private void init() {
        setWillNotDraw(false);
        this.mClearPaint = new Paint(1);
        this.mClearPaint.setColor(0);
        this.mClearPaint.setXfermode(new PorterDuffXfermode(Mode.CLEAR));
        this.mClearPaint.setAntiAlias(true);
        this.mOuterLinePaint = new Paint(1);
        this.mOuterLinePaint.setColor(this.mOuterLineColor);
        this.mOuterLinePaint.setStyle(Style.STROKE);
        this.mOuterLinePaint.setStrokeWidth(this.mOuterLineWidth);
        this.mTabFillPaint = new Paint(1);
        this.mTabFillPaint.setColor(this.mTabFillColor);
        this.mTabFillPaint.setStyle(Style.FILL);
        this.mFillPaint = new Paint(1);
        this.mFillPaint.setColor(this.mFillColor);
        this.mFillPaint.setStyle(Style.FILL);
        this.mInnerLinePaint = new Paint(1);
        this.mInnerLinePaint.setColor(this.mInnerLineColor);
        this.mInnerLinePaint.setStyle(Style.STROKE);
        this.mInnerLinePaint.setStrokeWidth(this.mInnerLineWidth);
        this.mOuterChamfer = new PointF(this.mOuterLineChamfer, this.mOuterLineChamfer);
        this.mTabLabelPaint = new Paint(1);
        this.mTabLabelPaint.setTypeface(Typeface.create(this.mFontFamily, this.mTextStyle));
        this.mTabLabelPaint.setTextSize(this.mTextSize);
        this.mTabLabelPaint.setColor(this.mTextColor);
        this.mSelectedTabLabelPaint = new Paint(1);
        this.mSelectedTabLabelPaint.setTypeface(Typeface.create(this.mFontFamily, this.mSelectedTextStyle));
        this.mSelectedTabLabelPaint.setTextSize(this.mTextSize);
        this.mSelectedTabLabelPaint.setColor(this.mSelectedTextColor);
        this.mTabLabelMargins = new Margins(this.mTabLabelMarginLeft, this.mTabLabelMarginTop, this.mTabLabelMarginRight, this.mTabLabelMarginBottom);
        this.mTabLabelPadding = new Margins(this.mTabLabelPaddingLeft, this.mTabLabelPaddingTop, this.mTabLabelPaddingRight, this.mTabLabelPaddingBottom);
        this.mCloseButtonMargins = new Margins(this.mCloseButtonMarginLeft, this.mCloseButtonMarginTop, this.mCloseButtonMarginRight, this.mCloseButtonMarginBottom);
        this.mCloseButtonPadding = new Margins(this.mCloseButtonPaddingLeft, this.mCloseButtonPaddingTop, this.mCloseButtonPaddingRight, this.mCloseButtonPaddingBottom);
        if (isInEditMode()) {
            this.tabLabels.add("News");
            this.tabLabels.add("Tips");
            this.tabLabels.add("Ranking");
            this.tabLabels.add("Help");
            return;
        }
        this.tabLabels.add("News");
        this.tabLabels.add("Tips");
        this.tabLabels.add("Ranking");
        this.tabLabels.add("Help");
    }

    private Integer getComponentId(MotionEvent motionEvent) {
        if (this.mCloseButtonBounds != null && this.mCloseButtonBounds.contains(motionEvent.getX(), motionEvent.getY())) {
            return Integer.valueOf(-1);
        }
        if (this.tabHolderList != null) {
            for (int i = 0; i < this.tabHolderList.size(); i++) {
                TabHolder tabHolder = (TabHolder) this.tabHolderList.get(i);
                if (tabHolder != null && tabHolder.getLabelBounds() != null && tabHolder.getLabelBounds().contains(motionEvent.getX(), motionEvent.getY())) {
                    return Integer.valueOf(i);
                }
            }
        }
        return null;
    }

    private float calculateMaxTabLabelHeight() {
        Rect rect = new Rect();
        float f = 0.0f;
        for (int i = 0; i < this.tabLabels.size(); i++) {
            String str = (String) this.tabLabels.get(i);
            if (i == this.mSelectedTabIndex) {
                this.mSelectedTabLabelPaint.getTextBounds(str, 0, str.length(), rect);
            } else {
                this.mTabLabelPaint.getTextBounds(str, 0, str.length(), rect);
            }
            if (((float) rect.height()) > f) {
                f = (float) rect.height();
            }
        }
        return f;
    }

    private void calculateBounds(int i, int i2) {
        this.mBounds = new RectF(0.0f, 0.0f, ((float) i) - ((float) (getPaddingLeft() + getPaddingRight())), ((float) i2) - ((float) (getPaddingTop() + getPaddingBottom())));
        this.mBounds.offsetTo((float) getPaddingLeft(), (float) getPaddingTop());
        calculatePaths();
    }

    private void calculatePaths() {
        float calculateMaxTabLabelHeight = ((((calculateMaxTabLabelHeight() + this.mTabLabelMargins.top) + this.mTabLabelMargins.bottom) + this.mTabLabelPadding.top) + this.mTabLabelPadding.bottom) + this.mPathOffset;
        int i = 0;
        float f = 0.0f;
        while (i < 2) {
            this.tabHolderList.clear();
            float f2 = 0.0f;
            RectF rectF = null;
            int i2 = 0;
            while (i2 < this.tabLabels.size()) {
                RectF labelBounds;
                String str = (String) this.tabLabels.get(i2);
                if (i2 == this.mSelectedTabIndex) {
                    this.mSelectedTabLabelPaint.getTextBounds(str, 0, str.length(), this.mTmpTabLabelBounds);
                } else {
                    this.mTabLabelPaint.getTextBounds(str, 0, str.length(), this.mTmpTabLabelBounds);
                }
                Rect rect;
                if (f < 0.0f) {
                    if (i2 != this.mSelectedTabIndex) {
                        rect = this.mTmpTabLabelBounds;
                        rect.right = (int) (((float) rect.right) + f);
                    }
                } else if (f > 0.0f) {
                    rect = this.mTmpTabLabelBounds;
                    rect.right = (int) (((float) rect.right) + f);
                }
                float width = (((((float) this.mTmpTabLabelBounds.width()) + this.mTabLabelMargins.left) + this.mTabLabelMargins.right) + this.mTabLabelPadding.left) + this.mTabLabelPadding.right;
                boolean z = i2 == this.tabLabels.size() + -1;
                TabGeometry newTopOutlineGeometry = newTopOutlineGeometry(this.mBounds, this.mOuterChamfer, calculateMaxTabLabelHeight, f2, width + this.mPathOffset, this.mPathOffset, 0.0f, z);
                TabGeometry newTopOutlineGeometry2 = newTopOutlineGeometry(this.mBounds, this.mOuterChamfer, calculateMaxTabLabelHeight, f2, width + this.mPathOffset, this.mPathOffset, this.mPathOffset, z);
                TabHolder tabHolder = new TabHolder(newTopOutlineGeometry.getPath(), newTopOutlineGeometry2.getPath(), newTopOutlineGeometry.getTabBounds(), newTopOutlineGeometry2.getLabelBounds(), newTopOutlineGeometry2.getContentTop());
                this.tabHolderList.add(tabHolder);
                f2 = tabHolder.getLabelBounds().right - this.mBounds.left;
                if (rectF == null) {
                    labelBounds = newTopOutlineGeometry.getLabelBounds();
                } else {
                    labelBounds = rectF;
                }
                i2++;
                rectF = labelBounds;
            }
            if (!(rectF == null || this.mCloseButtonDrawable == null)) {
                this.mCloseButtonBounds = new RectF(this.mBounds.right - ((((((((((rectF.bottom - rectF.top) - this.mCloseButtonMargins.top) - this.mCloseButtonMargins.bottom) - this.mCloseButtonPaddingTop) - this.mCloseButtonPaddingBottom) * (((float) this.mCloseButtonDrawable.getIntrinsicWidth()) / ((float) this.mCloseButtonDrawable.getIntrinsicHeight()))) + this.mCloseButtonMargins.left) + this.mCloseButtonMargins.right) + this.mCloseButtonPadding.left) + this.mCloseButtonPadding.right), rectF.top, this.mBounds.right, rectF.bottom);
            }
            RectF tabBounds = ((TabHolder) this.tabHolderList.get(this.tabHolderList.size() - 1)).getTabBounds();
            float f3 = this.mCloseButtonBounds != null ? this.mCloseButtonBounds.left : this.mBounds.right;
            if (tabBounds.right == f3) {
                break;
            }
            if (tabBounds.right < f3) {
                f3 = (f3 - tabBounds.right) / ((float) this.tabLabels.size());
            } else {
                f3 = (f3 - tabBounds.right) / ((float) (this.tabLabels.size() - 1));
            }
            i++;
            f = f3;
        }
        this.mOuterLinePathBottom = newBottomOutlinePath(this.mBounds, this.mOuterChamfer, calculateMaxTabLabelHeight, 0.0f);
        this.mInnerLinePathBottom = newBottomOutlinePath(this.mBounds, this.mOuterChamfer, calculateMaxTabLabelHeight, this.mPathOffset);
        this.mContentBounds = new RectF();
        this.mInnerLinePathBottom.computeBounds(this.mContentBounds, false);
        if (!this.tabHolderList.isEmpty()) {
            this.mContentBounds.top = ((TabHolder) this.tabHolderList.get(0)).getContentTop();
        }
    }

    private TabGeometry newTopOutlineGeometry(RectF rectF, PointF pointF, float f, float f2, float f3, float f4, float f5, boolean z) {
        float f6;
        PointF lineTo;
        PointF lineTo2;
        Object obj = f2 == 0.0f ? 1 : null;
        float f7 = rectF.top + f;
        float f8 = f7 - f4;
        float f9 = f8 - pointF.y;
        float f10 = rectF.top;
        float f11 = f9 - f10;
        float f12 = this.mEdgeSlopeHeightRatio * f11;
        float f13 = f10 + f12;
        float tan = (float) Math.tan(Math.toRadians((double) this.mTabSlope));
        float f14 = rectF.left;
        float f15 = obj != null ? f14 : pointF.x + f14;
        float max = obj != null ? f14 : Math.max(f2, pointF.x) + f14;
        if (obj != null) {
            f6 = f12;
        } else {
            f6 = f11;
        }
        f6 = (f6 / tan) + max;
        float f16 = f6 + f3;
        if (!z) {
            f12 = f11;
        }
        f12 = (f12 / tan) + f16;
        f11 = rectF.right;
        tan = f11 - pointF.x;
        Path path = new Path();
        moveTo(path, new PointF(f14, f7), 0.0f, f5);
        if (obj != null) {
            lineTo = lineTo(path, new PointF(max, f13), 90.0f - ((90.0f + this.mTabSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f5);
        } else {
            lineTo(path, new PointF(f14, f8), 90.0f - ((90.0f + this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f5);
            lineTo(path, new PointF(f15, f9), (180.0f - this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM, f5);
            lineTo = lineTo(path, new PointF(max, f9), 180.0f - ((180.0f + this.mTabSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f5);
        }
        PointF lineTo3 = lineTo(path, new PointF(f6, f10), (180.0f - this.mTabSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM, f5);
        PointF lineTo4 = lineTo(path, new PointF(f16, f10), 180.0f - ((180.0f - this.mTabSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f5);
        if (z) {
            lineTo(path, new PointF(f12, f13), 90.0f + ((90.0f + this.mTabSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f5);
            lineTo2 = lineTo(path, new PointF(f12, f9), 135.0f, f5);
        } else {
            lineTo2 = lineTo(path, new PointF(f12, f9), (180.0f + this.mTabSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM, f5);
        }
        lineTo(path, new PointF(tan, f9), 180.0f - ((180.0f - this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f5);
        lineTo(path, new PointF(f11, f8), 90.0f + ((90.0f + this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f5);
        lineTo(path, new PointF(f11, f7), 180.0f, f5);
        moveTo(path, new PointF(f14, f7), 0.0f, f5);
        path.close();
        return new TabGeometry(path, new RectF(lineTo.x, lineTo3.y, lineTo2.x, lineTo2.y), new RectF(lineTo3.x, lineTo3.y, lineTo4.x, lineTo2.y), lineTo2.y);
    }

    private Path newBottomOutlinePath(RectF rectF, PointF pointF, float f, float f2) {
        float f3 = rectF.left;
        float f4 = pointF.x + f3;
        float f5 = rectF.right;
        float f6 = f5 - pointF.x;
        float f7 = rectF.top + f;
        float f8 = rectF.bottom;
        float f9 = f8 - pointF.y;
        Path path = new Path();
        moveTo(path, new PointF(f5, f7), 180.0f, f2);
        lineTo(path, new PointF(f5, f9), 270.0f - ((90.0f + this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f2);
        lineTo(path, new PointF(f6, f8), 180.0f + ((180.0f - this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f2);
        lineTo(path, new PointF(f4, f8), 0.0f - ((180.0f - this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f2);
        lineTo(path, new PointF(f3, f9), 270.0f + ((90.0f + this.chamferSlope) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f2);
        lineTo(path, new PointF(f3, f7), 0.0f, f2);
        moveTo(path, new PointF(f5, f7), 180.0f, f2);
        path.close();
        return path;
    }

    private void drawTab(Canvas canvas, int i) {
        String str = (String) this.tabLabels.get(i);
        TabHolder tabHolder = (TabHolder) this.tabHolderList.get(i);
        canvas.drawPath(tabHolder.getOuterPath(), this.mClearPaint);
        canvas.drawPath(tabHolder.getOuterPath(), this.mTabFillPaint);
        canvas.drawPath(tabHolder.getOuterPath(), this.mOuterLinePaint);
        canvas.drawPath(tabHolder.getInnerPath(), this.mInnerLinePaint);
        RectF labelBounds = tabHolder.getLabelBounds();
        float f = (labelBounds.left + this.mTabLabelMargins.left) + this.mTabLabelPadding.left;
        float f2 = (labelBounds.bottom - this.mTabLabelMargins.bottom) - this.mTabLabelPadding.bottom;
        float width = (((labelBounds.width() - this.mTabLabelMargins.left) - this.mTabLabelMargins.right) - this.mTabLabelPadding.left) - this.mTabLabelPadding.right;
        if (this.mSelectedTabIndex != i) {
            str = TextUtils.ellipsize(str, new TextPaint(this.mTabLabelPaint), width, TruncateAt.END).toString();
        }
        Paint paint = i == this.mSelectedTabIndex ? this.mSelectedTabLabelPaint : this.mTabLabelPaint;
        paint.getTextBounds(str, 0, str.length(), this.mTmpTabLabelBounds);
        canvas.drawText(str, f + ((width - ((float) this.mTmpTabLabelBounds.width())) / DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM), f2, paint);
    }

    private RectF adjustMargins(RectF rectF, Margins... marginsArr) {
        for (Margins doAdjustMargins : marginsArr) {
            rectF = doAdjustMargins(rectF, doAdjustMargins);
        }
        return rectF;
    }

    private RectF doAdjustMargins(RectF rectF, Margins margins) {
        return new RectF(rectF.left + margins.left, rectF.top + margins.top, rectF.right - margins.right, rectF.bottom - margins.bottom);
    }

    private PointF moveTo(Path path, PointF pointF, float f, float f2) {
        PointF adjust = adjust(pointF, f, f2);
        path.moveTo(adjust.x, adjust.y);
        return adjust;
    }

    private PointF lineTo(Path path, PointF pointF, float f, float f2) {
        PointF adjust = adjust(pointF, f, f2);
        path.lineTo(adjust.x, adjust.y);
        return adjust;
    }

    private PointF adjust(PointF pointF, float f, float f2) {
        if (f2 == 0.0f) {
            return pointF;
        }
        double toRadians = Math.toRadians((double) f);
        return new PointF((float) (((double) pointF.x) + (Math.cos(toRadians) * ((double) f2))), (float) ((Math.sin(toRadians) * ((double) f2)) + ((double) pointF.y)));
    }

    private float fromDp(float f) {
        return (float) DisplayUtils.pxFromDp(getContext(), f);
    }

    private float fromSp(float f) {
        return (float) DisplayUtils.pxFromSp(getContext(), f);
    }
}
