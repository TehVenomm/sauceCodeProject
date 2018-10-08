package net.gogame.gowrap.ui.v2017_1;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.drawable.Drawable;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.view.ViewTreeObserver.OnGlobalLayoutListener;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.TextView;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import net.gogame.gowrap.C1426R;

public abstract class AbstractNewsFeedItemView extends FrameLayout {
    public static final String DEFAULT_TIMESTAMP_DATE_FORMAT = "MMM d, yy h:mm a";
    private Double aspectRatio;
    private Integer availableWidth;
    private int contentBackgroundCount = 1;
    private View resizingView;

    /* renamed from: net.gogame.gowrap.ui.v2017_1.AbstractNewsFeedItemView$1 */
    class C15021 implements OnGlobalLayoutListener {
        C15021() {
        }

        public void onGlobalLayout() {
            if (AbstractNewsFeedItemView.this.availableWidth == null) {
                AbstractNewsFeedItemView.this.availableWidth = Integer.valueOf(AbstractNewsFeedItemView.this.resizingView.getWidth());
                AbstractNewsFeedItemView.this.resizeView(AbstractNewsFeedItemView.this.resizingView, AbstractNewsFeedItemView.this.availableWidth, AbstractNewsFeedItemView.this.aspectRatio);
                AbstractNewsFeedItemView.this.onLayoutCompleted();
            }
        }
    }

    protected abstract void customInit(Context context);

    protected abstract int[] getButtonClickResourceIds();

    protected abstract int[] getClickResourceIds();

    protected abstract Integer getResizingViewResourceId();

    protected abstract int getViewResourceId();

    protected abstract void onLayoutCompleted();

    public AbstractNewsFeedItemView(Context context) {
        super(context);
        init(context, null);
    }

    public AbstractNewsFeedItemView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        init(context, attributeSet);
    }

    public AbstractNewsFeedItemView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        init(context, attributeSet);
    }

    @TargetApi(21)
    public AbstractNewsFeedItemView(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        init(context, attributeSet);
    }

    protected boolean isLayoutCompleted() {
        return this.availableWidth != null;
    }

    private void init(Context context, AttributeSet attributeSet) {
        View inflate = ((LayoutInflater) getContext().getSystemService("layout_inflater")).inflate(getViewResourceId(), this, false);
        if (inflate != null) {
            addView(inflate);
        }
        this.contentBackgroundCount = getResources().getInteger(C1426R.integer.net_gogame_gowrap_newsfeed_item_content_backgrounds);
        customInit(context);
        if (getResizingViewResourceId() != null) {
            this.resizingView = findViewById(getResizingViewResourceId().intValue());
            if (this.resizingView != null) {
                this.resizingView.getViewTreeObserver().addOnGlobalLayoutListener(new C15021());
            }
        }
    }

    public void setPosition(int i) {
        ((ImageView) findViewById(C1426R.id.net_gogame_gowrap_newsfeed_item_content_background)).setImageLevel(i % this.contentBackgroundCount);
    }

    public void setButtonImage(Drawable drawable) {
        ((ImageView) findViewById(C1426R.id.net_gogame_gowrap_newsfeed_item_button)).setImageDrawable(drawable);
    }

    public void setTimestamp(Long l) {
        if (l == null) {
            setTimestamp("");
        } else {
            setTimestamp(new Date(l.longValue()));
        }
    }

    public void setTimestamp(Date date) {
        if (date == null) {
            setTimestamp("");
        } else {
            setTimestamp(new SimpleDateFormat(DEFAULT_TIMESTAMP_DATE_FORMAT, Locale.getDefault()).format(date));
        }
    }

    public void setTimestamp(String str) {
        TextView textView = (TextView) findViewById(C1426R.id.net_gogame_gowrap_newsfeed_item_timestamp);
        if (str != null) {
            textView.setText(str);
            textView.setVisibility(0);
            return;
        }
        textView.setVisibility(8);
    }

    public void setMessage(String str) {
        TextView textView = (TextView) findViewById(C1426R.id.net_gogame_gowrap_newsfeed_item_message);
        if (str != null) {
            textView.setText(str);
            textView.setVisibility(0);
            return;
        }
        textView.setVisibility(8);
    }

    private void setOnClickListener(int[] iArr, OnClickListener onClickListener) {
        for (int findViewById : iArr) {
            View findViewById2 = findViewById(findViewById);
            if (findViewById2 != null) {
                findViewById2.setOnClickListener(onClickListener);
            }
        }
    }

    public void setOnClickListener(OnClickListener onClickListener) {
        setOnClickListener(getClickResourceIds(), onClickListener);
    }

    public void setButtonOnClickListener(OnClickListener onClickListener) {
        setOnClickListener(getButtonClickResourceIds(), onClickListener);
    }

    public void setAspectRatio(double d) {
        this.aspectRatio = Double.valueOf(d);
        resizeView(this.resizingView, this.availableWidth, Double.valueOf(d));
    }

    protected void resizeView(View view, Integer num, Double d) {
        if (view != null && num != null && d != null) {
            int intValue = num.intValue();
            int doubleValue = (int) (num.doubleValue() / d.doubleValue());
            LayoutParams layoutParams = view.getLayoutParams();
            layoutParams.width = intValue;
            layoutParams.height = doubleValue;
            view.setMinimumWidth(intValue);
            view.setMinimumHeight(doubleValue);
            if (view.getParent() instanceof ViewGroup) {
                ((ViewGroup) view.getParent()).requestLayout();
            }
        }
    }

    public View getResizingView() {
        return this.resizingView;
    }
}
