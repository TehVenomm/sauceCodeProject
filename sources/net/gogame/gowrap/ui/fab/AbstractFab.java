package net.gogame.gowrap.p019ui.fab;

import android.content.Context;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.RelativeLayout.LayoutParams;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.p019ui.fab.Fab.ClickListener;
import net.gogame.gowrap.p019ui.utils.DisplayUtils;

/* renamed from: net.gogame.gowrap.ui.fab.AbstractFab */
public abstract class AbstractFab implements Fab {
    private ClickListener clickListener;

    /* access modifiers changed from: protected */
    public void fireClickListener(MotionEvent motionEvent) {
        if (this.clickListener != null) {
            this.clickListener.onClick(this, motionEvent);
        }
    }

    /* access modifiers changed from: protected */
    public View createImageView(Context context, int i) {
        RelativeLayout relativeLayout = new RelativeLayout(context);
        relativeLayout.setPadding(0, DisplayUtils.pxFromDp(context, 8.0f), 0, 0);
        relativeLayout.setClipToPadding(false);
        relativeLayout.setLayoutParams(new LayoutParams(-2, DisplayUtils.pxFromDp(context, 200.0f)));
        ImageView imageView = new ImageView(context);
        imageView.setImageDrawable(context.getResources().getDrawable(i));
        relativeLayout.addView(imageView);
        ImageView imageView2 = new ImageView(context);
        imageView2.setImageResource(C1423R.C1427drawable.net_gogame_gowrap_server_down_fab_icon);
        LayoutParams layoutParams = new LayoutParams(-2, -2);
        layoutParams.setMargins(DisplayUtils.pxFromDp(context, 20.0f), DisplayUtils.pxFromDp(context, 5.0f), 0, 0);
        imageView2.setLayoutParams(layoutParams);
        imageView2.setVisibility(8);
        relativeLayout.addView(imageView2);
        return relativeLayout;
    }

    public void setClickListener(ClickListener clickListener2) {
        this.clickListener = clickListener2;
    }
}
