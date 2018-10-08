package com.google.android.gms.plus;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.FrameLayout;
import com.google.android.gms.plus.internal.zzg;

public final class PlusOneButton extends FrameLayout {
    public static final int ANNOTATION_BUBBLE = 1;
    public static final int ANNOTATION_INLINE = 2;
    public static final int ANNOTATION_NONE = 0;
    public static final int DEFAULT_ACTIVITY_REQUEST_CODE = -1;
    public static final int SIZE_MEDIUM = 1;
    public static final int SIZE_SMALL = 0;
    public static final int SIZE_STANDARD = 3;
    public static final int SIZE_TALL = 2;
    private int mSize;
    private String zzAX;
    private View zzayP;
    private int zzayQ;
    private int zzayR;
    private OnPlusOneClickListener zzayS;

    public interface OnPlusOneClickListener {
        void onPlusOneClick(Intent intent);
    }

    protected class DefaultOnPlusOneClickListener implements OnClickListener, OnPlusOneClickListener {
        private final OnPlusOneClickListener zzayT;
        final /* synthetic */ PlusOneButton zzayU;

        public DefaultOnPlusOneClickListener(PlusOneButton plusOneButton, OnPlusOneClickListener onPlusOneClickListener) {
            this.zzayU = plusOneButton;
            this.zzayT = onPlusOneClickListener;
        }

        public void onClick(View view) {
            Intent intent = (Intent) this.zzayU.zzayP.getTag();
            if (this.zzayT != null) {
                this.zzayT.onPlusOneClick(intent);
            } else {
                onPlusOneClick(intent);
            }
        }

        public void onPlusOneClick(Intent intent) {
            Context context = this.zzayU.getContext();
            if ((context instanceof Activity) && intent != null) {
                ((Activity) context).startActivityForResult(intent, this.zzayU.zzayR);
            }
        }
    }

    public PlusOneButton(Context context) {
        this(context, null);
    }

    public PlusOneButton(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        this.mSize = getSize(context, attributeSet);
        this.zzayQ = getAnnotation(context, attributeSet);
        this.zzayR = -1;
        zzS(getContext());
        if (!isInEditMode()) {
        }
    }

    protected static int getAnnotation(Context context, AttributeSet attributeSet) {
        String zza = zzac.zza("http://schemas.android.com/apk/lib/com.google.android.gms.plus", "annotation", context, attributeSet, true, false, "PlusOneButton");
        return "INLINE".equalsIgnoreCase(zza) ? 2 : "NONE".equalsIgnoreCase(zza) ? 0 : 1;
    }

    protected static int getSize(Context context, AttributeSet attributeSet) {
        String zza = zzac.zza("http://schemas.android.com/apk/lib/com.google.android.gms.plus", "size", context, attributeSet, true, false, "PlusOneButton");
        return "SMALL".equalsIgnoreCase(zza) ? 0 : !"MEDIUM".equalsIgnoreCase(zza) ? "TALL".equalsIgnoreCase(zza) ? 2 : 3 : 1;
    }

    private void zzS(Context context) {
        if (this.zzayP != null) {
            removeView(this.zzayP);
        }
        this.zzayP = zzg.zza(context, this.mSize, this.zzayQ, this.zzAX, this.zzayR);
        setOnPlusOneClickListener(this.zzayS);
        addView(this.zzayP);
    }

    public void initialize(String str, int i) {
        zzv.zza(getContext() instanceof Activity, "To use this method, the PlusOneButton must be placed in an Activity. Use initialize(String, OnPlusOneClickListener).");
        this.zzAX = str;
        this.zzayR = i;
        zzS(getContext());
    }

    public void initialize(String str, OnPlusOneClickListener onPlusOneClickListener) {
        this.zzAX = str;
        this.zzayR = 0;
        zzS(getContext());
        setOnPlusOneClickListener(onPlusOneClickListener);
    }

    protected void onLayout(boolean z, int i, int i2, int i3, int i4) {
        this.zzayP.layout(0, 0, i3 - i, i4 - i2);
    }

    protected void onMeasure(int i, int i2) {
        View view = this.zzayP;
        measureChild(view, i, i2);
        setMeasuredDimension(view.getMeasuredWidth(), view.getMeasuredHeight());
    }

    public void setAnnotation(int i) {
        this.zzayQ = i;
        zzS(getContext());
    }

    public void setOnPlusOneClickListener(OnPlusOneClickListener onPlusOneClickListener) {
        this.zzayS = onPlusOneClickListener;
        this.zzayP.setOnClickListener(new DefaultOnPlusOneClickListener(this, onPlusOneClickListener));
    }

    public void setSize(int i) {
        this.mSize = i;
        zzS(getContext());
    }
}
