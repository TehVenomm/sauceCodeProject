package com.github.droidfu.listeners;

import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;
import com.github.droidfu.activities.BetterMapActivity;

public class MapGestureListener extends SimpleOnGestureListener {
    protected BetterMapActivity mapActivity;

    public MapGestureListener(BetterMapActivity betterMapActivity) {
        this.mapActivity = betterMapActivity;
    }

    public boolean onDoubleTap(MotionEvent motionEvent) {
        this.mapActivity.getMapView().getController().zoomInFixing((int) motionEvent.getX(), (int) motionEvent.getY());
        return true;
    }
}
