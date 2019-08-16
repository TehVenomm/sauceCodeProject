package net.gogame.gowrap.p019ui.fab;

import android.app.Activity;
import android.view.MotionEvent;

/* renamed from: net.gogame.gowrap.ui.fab.Fab */
public interface Fab {

    /* renamed from: net.gogame.gowrap.ui.fab.Fab$ClickListener */
    public interface ClickListener {
        void onClick(Fab fab, MotionEvent motionEvent);
    }

    void destroy(Activity activity);

    boolean handleTouchEvent(MotionEvent motionEvent);

    void hide(Activity activity);

    void setClickListener(ClickListener clickListener);

    void show(Activity activity);

    void update(Activity activity);
}
