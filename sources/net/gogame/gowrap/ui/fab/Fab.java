package net.gogame.gowrap.ui.fab;

import android.app.Activity;
import android.view.MotionEvent;

public interface Fab {

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
