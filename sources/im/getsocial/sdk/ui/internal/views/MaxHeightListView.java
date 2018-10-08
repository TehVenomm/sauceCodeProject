package im.getsocial.sdk.ui.internal.views;

import android.annotation.TargetApi;
import android.content.Context;
import android.util.AttributeSet;
import android.view.View.MeasureSpec;
import android.widget.ListView;

public class MaxHeightListView extends ListView {
    /* renamed from: a */
    private int f3141a = 0;

    public MaxHeightListView(Context context) {
        super(context);
    }

    public MaxHeightListView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public MaxHeightListView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    @TargetApi(21)
    public MaxHeightListView(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
    }

    /* renamed from: a */
    public final void m3520a(int i) {
        this.f3141a = i;
    }

    protected void onMeasure(int i, int i2) {
        if (this.f3141a > 0) {
            i2 = MeasureSpec.makeMeasureSpec(this.f3141a, Integer.MIN_VALUE);
        }
        super.onMeasure(i, i2);
    }
}
