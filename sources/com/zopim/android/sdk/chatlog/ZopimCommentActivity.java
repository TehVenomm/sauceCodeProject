package com.zopim.android.sdk.chatlog;

import android.annotation.TargetApi;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.api.Logger;

public class ZopimCommentActivity extends AppCompatActivity {
    public static final String EXTRA_COMMENT = "COMMENT";
    private static final String LOG_TAG = ZopimCommentActivity.class.getSimpleName();

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C0784R.layout.zopim_comment_activity);
        setSupportActionBar((Toolbar) findViewById(C0784R.id.toolbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        FragmentManager supportFragmentManager = getSupportFragmentManager();
        if (supportFragmentManager.findFragmentByTag(ZopimCommentFragment.class.getName()) == null) {
            String stringExtra = getIntent() != null ? getIntent().getStringExtra(EXTRA_COMMENT) : null;
            Fragment newInstance = stringExtra != null ? ZopimCommentFragment.newInstance(stringExtra) : new ZopimCommentFragment();
            FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
            beginTransaction.add(C0784R.id.comment_fragment_container, newInstance, ZopimCommentFragment.class.getName());
            beginTransaction.commit();
        }
    }

    protected void onDestroy() {
        Logger.m564v(LOG_TAG, "Activity destroyed");
        super.onDestroy();
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        if (16908332 == menuItem.getItemId()) {
            finish();
            return super.onOptionsItemSelected(menuItem);
        } else if (C0784R.id.send_comment != menuItem.getItemId()) {
            return false;
        } else {
            finish();
            return false;
        }
    }

    @TargetApi(11)
    protected void onStop() {
        super.onStop();
        boolean isFinishing = VERSION.SDK_INT >= 11 ? !isChangingConfigurations() : isFinishing();
        if (isFinishing) {
            finish();
        }
    }
}
