package com.github.droidfu.activities;

import android.app.AlertDialog;
import android.app.Application;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.GestureDetector;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
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
    private GestureDetector tapDetector;
    private OnTouchListener tapListener;
    private boolean wasCreated;
    private boolean wasInterrupted;

    /* renamed from: com.github.droidfu.activities.BetterMapActivity$1 */
    class C05911 implements OnClickListener {
        C05911() {
        }

        public void onClick(View view) {
            BetterMapActivity.this.getMapView().getController().zoomInFixing(BetterMapActivity.this.getMapView().getWidth() / 2, BetterMapActivity.this.getMapView().getHeight() / 2);
        }
    }

    /* renamed from: com.github.droidfu.activities.BetterMapActivity$2 */
    class C05922 implements OnClickListener {
        C05922() {
        }

        public void onClick(View view) {
            BetterMapActivity.this.getMapView().getController().zoomOut();
        }
    }

    /* renamed from: com.github.droidfu.activities.BetterMapActivity$3 */
    class C05933 implements OnTouchListener {
        C05933() {
        }

        public boolean onTouch(View view, MotionEvent motionEvent) {
            return BetterMapActivity.this.tapDetector.onTouchEvent(motionEvent);
        }
    }

    public Intent getCurrentIntent() {
        return this.currentIntent;
    }

    public MapView getMapView() {
        return this.mapView;
    }

    public MyLocationOverlay getMyLocationOverlay() {
        return this.myLocationOverlay;
    }

    public int getWindowFeatures() {
        return BetterActivityHelper.getWindowFeatures(this);
    }

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

    protected boolean isRouteDisplayed() {
        return false;
    }

    public AlertDialog newAlertDialog(int i, int i2) {
        return BetterActivityHelper.newMessageDialog(this, getString(i), getString(i2), 17301543);
    }

    public AlertDialog newErrorHandlerDialog(int i, Exception exception) {
        return BetterActivityHelper.newErrorHandlerDialog(this, getString(i), exception);
    }

    public AlertDialog newErrorHandlerDialog(Exception exception) {
        return newErrorHandlerDialog(getResources().getIdentifier(BetterActivityHelper.ERROR_DIALOG_TITLE_RESOURCE, "string", getPackageName()), exception);
    }

    public AlertDialog newInfoDialog(int i, int i2) {
        return BetterActivityHelper.newMessageDialog(this, getString(i), getString(i2), 17301659);
    }

    public <T> Dialog newListDialog(String str, List<T> list, DialogClickListener<T> dialogClickListener, boolean z) {
        return BetterActivityHelper.newListDialog(this, str, list, dialogClickListener, z);
    }

    public AlertDialog newYesNoDialog(int i, int i2, DialogInterface.OnClickListener onClickListener) {
        return BetterActivityHelper.newYesNoDialog(this, getString(i), getString(i2), 17301659, onClickListener);
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.wasCreated = true;
        this.currentIntent = getIntent();
        Application application = getApplication();
        if (application instanceof DroidFuApplication) {
            ((DroidFuApplication) application).setActiveContext(getClass().getCanonicalName(), this);
        }
    }

    protected Dialog onCreateDialog(int i) {
        return BetterActivityHelper.createProgressDialog(this, this.progressDialogTitleId, this.progressDialogMsgId);
    }

    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        BetterActivityHelper.handleApplicationClosing(this, i);
        return super.onKeyDown(i, keyEvent);
    }

    public void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        this.currentIntent = intent;
    }

    protected void onPause() {
        super.onPause();
        this.wasInterrupted = false;
        this.wasCreated = false;
        if (this.myLocationOverlay != null) {
            this.myLocationOverlay.disableMyLocation();
        }
    }

    protected void onRestoreInstanceState(Bundle bundle) {
        super.onRestoreInstanceState(bundle);
        this.wasInterrupted = true;
    }

    protected void onResume() {
        super.onResume();
        if (this.myLocationOverlay != null) {
            this.myLocationOverlay.enableMyLocation();
        }
    }

    protected void setMapGestureListener(MapGestureListener mapGestureListener) {
        this.tapDetector = new GestureDetector(mapGestureListener);
        this.tapListener = new C05933();
        this.mapView.setOnTouchListener(this.tapListener);
    }

    public void setMapView(int i) {
        this.mapView = (MapView) findViewById(i);
    }

    public void setMapViewWithZoom(int i, int i2) {
        this.mapView = (MapView) findViewById(i);
        ZoomControls zoomControls = (ZoomControls) findViewById(i2);
        zoomControls.setOnZoomInClickListener(new C05911());
        zoomControls.setOnZoomOutClickListener(new C05922());
    }

    public void setMyLocationOverlay(MyLocationOverlay myLocationOverlay) {
        this.myLocationOverlay = myLocationOverlay;
        this.mapView.getOverlays().add(this.myLocationOverlay);
    }

    public void setProgressDialogMsgId(int i) {
        this.progressDialogMsgId = i;
    }

    public void setProgressDialogTitleId(int i) {
        this.progressDialogTitleId = i;
    }
}
