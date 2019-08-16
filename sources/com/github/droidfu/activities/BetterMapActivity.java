package com.github.droidfu.activities;

import android.app.AlertDialog;
import android.app.Application;
import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.os.Bundle;
import android.view.GestureDetector;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.ZoomControls;
import com.github.droidfu.DroidFuApplication;
import com.github.droidfu.dialogs.DialogClickListener;
import com.github.droidfu.listeners.MapGestureListener;
import com.google.android.maps.MapActivity;
import com.google.android.maps.MapView;
import com.google.android.maps.MyLocationOverlay;
import java.util.List;

public class BetterMapActivity extends MapActivity implements BetterActivity {
    private Intent currentIntent;
    private MapView mapView;
    private MyLocationOverlay myLocationOverlay;
    private int progressDialogMsgId;
    private int progressDialogTitleId;
    /* access modifiers changed from: private */
    public GestureDetector tapDetector;
    private OnTouchListener tapListener;
    private boolean wasCreated;
    private boolean wasInterrupted;

    public Intent getCurrentIntent() {
        return this.currentIntent;
    }

    public MapView getMapView() {
        return this.mapView;
    }

    public MyLocationOverlay getMyLocationOverlay() {
        return this.myLocationOverlay;
    }

    /* JADX WARNING: type inference failed for: r1v0, types: [com.github.droidfu.activities.BetterMapActivity, android.app.Activity] */
    public int getWindowFeatures() {
        return BetterActivityHelper.getWindowFeatures(this);
    }

    /* JADX WARNING: type inference failed for: r1v0, types: [android.content.Context, com.github.droidfu.activities.BetterMapActivity] */
    public boolean isApplicationBroughtToBackground() {
        return BetterActivityHelper.isApplicationBroughtToBackground(this);
    }

    public boolean isLandscapeMode() {
        return getWindowManager().getDefaultDisplay().getOrientation() == 1;
    }

    public boolean isLaunching() {
        return !this.wasInterrupted && this.wasCreated;
    }

    public boolean isPortraitMode() {
        return !isLandscapeMode();
    }

    public boolean isRestoring() {
        return this.wasInterrupted;
    }

    public boolean isResuming() {
        return !this.wasCreated;
    }

    /* access modifiers changed from: protected */
    public boolean isRouteDisplayed() {
        return false;
    }

    /* JADX WARNING: type inference failed for: r3v0, types: [android.content.Context, com.github.droidfu.activities.BetterMapActivity] */
    public AlertDialog newAlertDialog(int i, int i2) {
        return BetterActivityHelper.newMessageDialog(this, getString(i), getString(i2), 17301543);
    }

    /* JADX WARNING: type inference failed for: r1v0, types: [com.github.droidfu.activities.BetterMapActivity, android.app.Activity] */
    public AlertDialog newErrorHandlerDialog(int i, Exception exc) {
        return BetterActivityHelper.newErrorHandlerDialog(this, getString(i), exc);
    }

    public AlertDialog newErrorHandlerDialog(Exception exc) {
        return newErrorHandlerDialog(getResources().getIdentifier(BetterActivityHelper.ERROR_DIALOG_TITLE_RESOURCE, "string", getPackageName()), exc);
    }

    /* JADX WARNING: type inference failed for: r3v0, types: [android.content.Context, com.github.droidfu.activities.BetterMapActivity] */
    public AlertDialog newInfoDialog(int i, int i2) {
        return BetterActivityHelper.newMessageDialog(this, getString(i), getString(i2), 17301659);
    }

    /* JADX WARNING: type inference failed for: r1v0, types: [com.github.droidfu.activities.BetterMapActivity, android.app.Activity] */
    public <T> Dialog newListDialog(String str, List<T> list, DialogClickListener<T> dialogClickListener, boolean z) {
        return BetterActivityHelper.newListDialog(this, str, list, dialogClickListener, z);
    }

    /* JADX WARNING: type inference failed for: r3v0, types: [android.content.Context, com.github.droidfu.activities.BetterMapActivity] */
    public AlertDialog newYesNoDialog(int i, int i2, OnClickListener onClickListener) {
        return BetterActivityHelper.newYesNoDialog(this, getString(i), getString(i2), 17301659, onClickListener);
    }

    /* JADX WARNING: type inference failed for: r2v0, types: [android.content.Context, java.lang.Object, com.github.droidfu.activities.BetterMapActivity, com.google.android.maps.MapActivity] */
    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        BetterMapActivity.super.onCreate(bundle);
        this.wasCreated = true;
        this.currentIntent = getIntent();
        Application application = getApplication();
        if (application instanceof DroidFuApplication) {
            ((DroidFuApplication) application).setActiveContext(getClass().getCanonicalName(), this);
        }
    }

    /* JADX WARNING: type inference failed for: r2v0, types: [com.github.droidfu.activities.BetterMapActivity, android.app.Activity] */
    /* access modifiers changed from: protected */
    public Dialog onCreateDialog(int i) {
        return BetterActivityHelper.createProgressDialog(this, this.progressDialogTitleId, this.progressDialogMsgId);
    }

    /* JADX WARNING: type inference failed for: r1v0, types: [android.content.Context, com.github.droidfu.activities.BetterMapActivity, com.google.android.maps.MapActivity] */
    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        BetterActivityHelper.handleApplicationClosing(this, i);
        return BetterMapActivity.super.onKeyDown(i, keyEvent);
    }

    public void onNewIntent(Intent intent) {
        BetterMapActivity.super.onNewIntent(intent);
        this.currentIntent = intent;
    }

    /* access modifiers changed from: protected */
    public void onPause() {
        BetterMapActivity.super.onPause();
        this.wasInterrupted = false;
        this.wasCreated = false;
        if (this.myLocationOverlay != null) {
            this.myLocationOverlay.disableMyLocation();
        }
    }

    /* access modifiers changed from: protected */
    public void onRestoreInstanceState(Bundle bundle) {
        BetterMapActivity.super.onRestoreInstanceState(bundle);
        this.wasInterrupted = true;
    }

    /* access modifiers changed from: protected */
    public void onResume() {
        BetterMapActivity.super.onResume();
        if (this.myLocationOverlay != null) {
            this.myLocationOverlay.enableMyLocation();
        }
    }

    /* access modifiers changed from: protected */
    public void setMapGestureListener(MapGestureListener mapGestureListener) {
        this.tapDetector = new GestureDetector(mapGestureListener);
        this.tapListener = new OnTouchListener() {
            public boolean onTouch(View view, MotionEvent motionEvent) {
                return BetterMapActivity.this.tapDetector.onTouchEvent(motionEvent);
            }
        };
        this.mapView.setOnTouchListener(this.tapListener);
    }

    public void setMapView(int i) {
        this.mapView = findViewById(i);
    }

    public void setMapViewWithZoom(int i, int i2) {
        this.mapView = findViewById(i);
        ZoomControls zoomControls = (ZoomControls) findViewById(i2);
        zoomControls.setOnZoomInClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                BetterMapActivity.this.getMapView().getController().zoomInFixing(BetterMapActivity.this.getMapView().getWidth() / 2, BetterMapActivity.this.getMapView().getHeight() / 2);
            }
        });
        zoomControls.setOnZoomOutClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                BetterMapActivity.this.getMapView().getController().zoomOut();
            }
        });
    }

    public void setMyLocationOverlay(MyLocationOverlay myLocationOverlay2) {
        this.myLocationOverlay = myLocationOverlay2;
        this.mapView.getOverlays().add(this.myLocationOverlay);
    }

    public void setProgressDialogMsgId(int i) {
        this.progressDialogMsgId = i;
    }

    public void setProgressDialogTitleId(int i) {
        this.progressDialogTitleId = i;
    }
}
