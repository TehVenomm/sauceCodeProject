package jp.colopl.libs;

import android.text.style.ClickableSpan;
import android.view.View;

public class CSpan extends ClickableSpan {
    public static final int TYPE_WIFI_SETTING = 1;
    private OnClickListener mListener;
    private int mType;

    public interface OnClickListener {
        void onClick(View view, int i);
    }

    public CSpan(int i) {
        this.mType = i;
    }

    public void onClick(View view) {
        if (this.mListener != null) {
            this.mListener.onClick(view, this.mType);
        }
    }

    public void setOnClickListener(OnClickListener onClickListener) {
        this.mListener = onClickListener;
    }
}
