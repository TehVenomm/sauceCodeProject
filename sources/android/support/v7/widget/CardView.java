package android.support.p003v7.widget;

import android.content.Context;
import android.content.res.ColorStateList;
import android.content.res.TypedArray;
import android.graphics.Color;
import android.graphics.Rect;
import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.support.annotation.ColorInt;
import android.support.annotation.Nullable;
import android.support.p003v7.cardview.C0265R;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.MeasureSpec;
import android.widget.FrameLayout;

/* renamed from: android.support.v7.widget.CardView */
public class CardView extends FrameLayout {
    private static final int[] COLOR_BACKGROUND_ATTR = {16842801};
    private static final CardViewImpl IMPL;
    private final CardViewDelegate mCardViewDelegate = new CardViewDelegate() {
        private Drawable mCardBackground;

        public Drawable getCardBackground() {
            return this.mCardBackground;
        }

        public View getCardView() {
            return CardView.this;
        }

        public boolean getPreventCornerOverlap() {
            return CardView.this.getPreventCornerOverlap();
        }

        public boolean getUseCompatPadding() {
            return CardView.this.getUseCompatPadding();
        }

        public void setCardBackground(Drawable drawable) {
            this.mCardBackground = drawable;
            CardView.this.setBackgroundDrawable(drawable);
        }

        public void setMinWidthHeightInternal(int i, int i2) {
            if (i > CardView.this.mUserSetMinWidth) {
                CardView.super.setMinimumWidth(i);
            }
            if (i2 > CardView.this.mUserSetMinHeight) {
                CardView.super.setMinimumHeight(i2);
            }
        }

        public void setShadowPadding(int i, int i2, int i3, int i4) {
            CardView.this.mShadowBounds.set(i, i2, i3, i4);
            CardView.super.setPadding(CardView.this.mContentPadding.left + i, CardView.this.mContentPadding.top + i2, CardView.this.mContentPadding.right + i3, CardView.this.mContentPadding.bottom + i4);
        }
    };
    private boolean mCompatPadding;
    final Rect mContentPadding = new Rect();
    private boolean mPreventCornerOverlap;
    final Rect mShadowBounds = new Rect();
    int mUserSetMinHeight;
    int mUserSetMinWidth;

    static {
        if (VERSION.SDK_INT >= 21) {
            IMPL = new CardViewApi21Impl();
        } else if (VERSION.SDK_INT >= 17) {
            IMPL = new CardViewApi17Impl();
        } else {
            IMPL = new CardViewBaseImpl();
        }
        IMPL.initStatic();
    }

    public CardView(Context context) {
        super(context);
        initialize(context, null, 0);
    }

    public CardView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        initialize(context, attributeSet, 0);
    }

    public CardView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        initialize(context, attributeSet, i);
    }

    private void initialize(Context context, AttributeSet attributeSet, int i) {
        ColorStateList valueOf;
        TypedArray obtainStyledAttributes = context.obtainStyledAttributes(attributeSet, C0265R.styleable.CardView, i, C0265R.style.CardView);
        if (obtainStyledAttributes.hasValue(C0265R.styleable.CardView_cardBackgroundColor)) {
            valueOf = obtainStyledAttributes.getColorStateList(C0265R.styleable.CardView_cardBackgroundColor);
        } else {
            TypedArray obtainStyledAttributes2 = getContext().obtainStyledAttributes(COLOR_BACKGROUND_ATTR);
            int color = obtainStyledAttributes2.getColor(0, 0);
            obtainStyledAttributes2.recycle();
            float[] fArr = new float[3];
            Color.colorToHSV(color, fArr);
            valueOf = ColorStateList.valueOf(fArr[2] > 0.5f ? getResources().getColor(C0265R.color.cardview_light_background) : getResources().getColor(C0265R.color.cardview_dark_background));
        }
        float dimension = obtainStyledAttributes.getDimension(C0265R.styleable.CardView_cardCornerRadius, 0.0f);
        float dimension2 = obtainStyledAttributes.getDimension(C0265R.styleable.CardView_cardElevation, 0.0f);
        float dimension3 = obtainStyledAttributes.getDimension(C0265R.styleable.CardView_cardMaxElevation, 0.0f);
        this.mCompatPadding = obtainStyledAttributes.getBoolean(C0265R.styleable.CardView_cardUseCompatPadding, false);
        this.mPreventCornerOverlap = obtainStyledAttributes.getBoolean(C0265R.styleable.CardView_cardPreventCornerOverlap, true);
        int dimensionPixelSize = obtainStyledAttributes.getDimensionPixelSize(C0265R.styleable.CardView_contentPadding, 0);
        this.mContentPadding.left = obtainStyledAttributes.getDimensionPixelSize(C0265R.styleable.CardView_contentPaddingLeft, dimensionPixelSize);
        this.mContentPadding.top = obtainStyledAttributes.getDimensionPixelSize(C0265R.styleable.CardView_contentPaddingTop, dimensionPixelSize);
        this.mContentPadding.right = obtainStyledAttributes.getDimensionPixelSize(C0265R.styleable.CardView_contentPaddingRight, dimensionPixelSize);
        this.mContentPadding.bottom = obtainStyledAttributes.getDimensionPixelSize(C0265R.styleable.CardView_contentPaddingBottom, dimensionPixelSize);
        if (dimension2 > dimension3) {
            dimension3 = dimension2;
        }
        this.mUserSetMinWidth = obtainStyledAttributes.getDimensionPixelSize(C0265R.styleable.CardView_android_minWidth, 0);
        this.mUserSetMinHeight = obtainStyledAttributes.getDimensionPixelSize(C0265R.styleable.CardView_android_minHeight, 0);
        obtainStyledAttributes.recycle();
        IMPL.initialize(this.mCardViewDelegate, context, valueOf, dimension, dimension2, dimension3);
    }

    public ColorStateList getCardBackgroundColor() {
        return IMPL.getBackgroundColor(this.mCardViewDelegate);
    }

    public float getCardElevation() {
        return IMPL.getElevation(this.mCardViewDelegate);
    }

    public int getContentPaddingBottom() {
        return this.mContentPadding.bottom;
    }

    public int getContentPaddingLeft() {
        return this.mContentPadding.left;
    }

    public int getContentPaddingRight() {
        return this.mContentPadding.right;
    }

    public int getContentPaddingTop() {
        return this.mContentPadding.top;
    }

    public float getMaxCardElevation() {
        return IMPL.getMaxElevation(this.mCardViewDelegate);
    }

    public boolean getPreventCornerOverlap() {
        return this.mPreventCornerOverlap;
    }

    public float getRadius() {
        return IMPL.getRadius(this.mCardViewDelegate);
    }

    public boolean getUseCompatPadding() {
        return this.mCompatPadding;
    }

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        if (!(IMPL instanceof CardViewApi21Impl)) {
            int mode = MeasureSpec.getMode(i);
            switch (mode) {
                case Integer.MIN_VALUE:
                case 1073741824:
                    i = MeasureSpec.makeMeasureSpec(Math.max((int) Math.ceil((double) IMPL.getMinWidth(this.mCardViewDelegate)), MeasureSpec.getSize(i)), mode);
                    break;
            }
            int mode2 = MeasureSpec.getMode(i2);
            switch (mode2) {
                case Integer.MIN_VALUE:
                case 1073741824:
                    i2 = MeasureSpec.makeMeasureSpec(Math.max((int) Math.ceil((double) IMPL.getMinHeight(this.mCardViewDelegate)), MeasureSpec.getSize(i2)), mode2);
                    break;
            }
            super.onMeasure(i, i2);
            return;
        }
        super.onMeasure(i, i2);
    }

    public void setCardBackgroundColor(@ColorInt int i) {
        IMPL.setBackgroundColor(this.mCardViewDelegate, ColorStateList.valueOf(i));
    }

    public void setCardBackgroundColor(@Nullable ColorStateList colorStateList) {
        IMPL.setBackgroundColor(this.mCardViewDelegate, colorStateList);
    }

    public void setCardElevation(float f) {
        IMPL.setElevation(this.mCardViewDelegate, f);
    }

    public void setContentPadding(int i, int i2, int i3, int i4) {
        this.mContentPadding.set(i, i2, i3, i4);
        IMPL.updatePadding(this.mCardViewDelegate);
    }

    public void setMaxCardElevation(float f) {
        IMPL.setMaxElevation(this.mCardViewDelegate, f);
    }

    public void setMinimumHeight(int i) {
        this.mUserSetMinHeight = i;
        super.setMinimumHeight(i);
    }

    public void setMinimumWidth(int i) {
        this.mUserSetMinWidth = i;
        super.setMinimumWidth(i);
    }

    public void setPadding(int i, int i2, int i3, int i4) {
    }

    public void setPaddingRelative(int i, int i2, int i3, int i4) {
    }

    public void setPreventCornerOverlap(boolean z) {
        if (z != this.mPreventCornerOverlap) {
            this.mPreventCornerOverlap = z;
            IMPL.onPreventCornerOverlapChanged(this.mCardViewDelegate);
        }
    }

    public void setRadius(float f) {
        IMPL.setRadius(this.mCardViewDelegate, f);
    }

    public void setUseCompatPadding(boolean z) {
        if (this.mCompatPadding != z) {
            this.mCompatPadding = z;
            IMPL.onCompatPaddingChanged(this.mCardViewDelegate);
        }
    }
}
