package im.getsocial.sdk.ui.internal.p128a;

import android.annotation.SuppressLint;
import android.text.Layout;
import android.text.Spanned;
import android.text.style.ClickableSpan;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.TextView;

/* renamed from: im.getsocial.sdk.ui.internal.a.upgqDBbsrL */
public class upgqDBbsrL implements OnTouchListener {
    @SuppressLint({"ClickableViewAccessibility"})
    public boolean onTouch(View view, MotionEvent motionEvent) {
        TextView textView = (TextView) view;
        CharSequence text = textView.getText();
        if (text instanceof Spanned) {
            int i;
            Spanned spanned = (Spanned) text;
            if (motionEvent.getAction() == 1) {
                int x = (int) motionEvent.getX();
                int y = (int) motionEvent.getY();
                int totalPaddingLeft = textView.getTotalPaddingLeft();
                int totalPaddingTop = textView.getTotalPaddingTop();
                int scrollX = textView.getScrollX();
                int scrollY = textView.getScrollY();
                Layout layout = textView.getLayout();
                x = layout.getOffsetForHorizontal(layout.getLineForVertical((y - totalPaddingTop) + scrollY), (float) ((x - totalPaddingLeft) + scrollX));
                ClickableSpan[] clickableSpanArr = (ClickableSpan[]) spanned.getSpans(x, x, ClickableSpan.class);
                if (clickableSpanArr.length != 0) {
                    clickableSpanArr[0].onClick(textView);
                    i = 1;
                    if (i != 0) {
                        return true;
                    }
                }
            }
            i = 0;
            if (i != 0) {
                return true;
            }
        }
        return false;
    }
}
