package com.zopim.android.sdk.chatlog;

import android.annotation.TargetApi;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.p000v4.app.FragmentManager;
import android.support.p000v4.app.FragmentTransaction;
import android.support.p003v7.app.AppCompatActivity;
import android.support.p003v7.widget.Toolbar;
import android.view.MenuItem;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Logger;

public class ZopimCommentActivity extends AppCompatActivity {
    public static final String EXTRA_COMMENT = "COMMENT";
    private static final String LOG_TAG = ZopimCommentActivity.class.getSimpleName();

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C1122R.C1126layout.zopim_comment_activity);
        setSupportActionBar((Toolbar) findViewById(C1122R.C1125id.toolbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        FragmentManager supportFragmentManager = getSupportFragmentManager();
        if (supportFragmentManager.findFragmentByTag(ZopimCommentFragment.class.getName()) == null) {
            String str = getIntent() != null ? getIntent().getStringExtra(EXTRA_COMMENT) : null;
            ZopimCommentFragment zopimCommentFragment = str != null ? ZopimCommentFragment.newInstance(str) : new ZopimCommentFragment();
            FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
            beginTransaction.add(C1122R.C1125id.comment_fragment_container, zopimCommentFragment, ZopimCommentFragment.class.getName());
            beginTransaction.commit();
        }
    }

    /* access modifiers changed from: protected */
    public void onDestroy() {
        Logger.m577v(LOG_TAG, "Activity destroyed");
        super.onDestroy();
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        if (16908332 == menuItem.getItemId()) {
            finish();
            return super.onOptionsItemSelected(menuItem);
        } else if (C1122R.C1125id.send_comment != menuItem.getItemId()) {
            return false;
        } else {
            finish();
            return false;
        }
    }

    /* access modifiers changed from: protected */
    @TargetApi(11)
    public void onStop() {
        super.onStop();
        boolean isFinishing = VERSION.SDK_INT >= 11 ? !isChangingConfigurations() : isFinishing();
        if (isFinishing) {
            finish();
        }
    }
}
