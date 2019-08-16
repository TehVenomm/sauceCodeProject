package com.zopim.android.sdk.prechat;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.p000v4.app.FragmentManager;
import android.support.p000v4.app.FragmentTransaction;
import android.support.p003v7.app.AppCompatActivity;
import android.support.p003v7.widget.Toolbar;
import android.view.MenuItem;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import com.zopim.android.sdk.chatlog.ZopimChatLogFragment;
import com.zopim.android.sdk.embeddable.ChatActions;
import com.zopim.android.sdk.widget.ChatWidgetService;

public class ZopimChatActivity extends AppCompatActivity implements ChatListener {
    private static final String EXTRA_CHAT_CONFIG = "CHAT_CONFIG";
    private static final String LOG_TAG = ZopimChatActivity.class.getSimpleName();
    private static final String STATE_CHAT_INITIALIZED = "CHAT_INITIALIZED";
    private Chat mChat;
    private boolean mChatInitialized = false;

    private void resumeChat() {
        Logger.m577v(LOG_TAG, "Resuming chat");
        this.mChat = ZopimChat.resume(this);
        this.mChatInitialized = !this.mChat.hasEnded();
        FragmentManager supportFragmentManager = getSupportFragmentManager();
        if (supportFragmentManager.findFragmentByTag(ZopimChatLogFragment.class.getName()) == null) {
            ZopimChatLogFragment zopimChatLogFragment = new ZopimChatLogFragment();
            FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
            beginTransaction.add(C1122R.C1125id.chat_fragment_container, zopimChatLogFragment, ZopimChatLogFragment.class.getName());
            beginTransaction.commit();
        }
    }

    public static void startActivity(Context context, SessionConfig sessionConfig) {
        Intent intent = new Intent(context, ZopimChatActivity.class);
        intent.putExtra(EXTRA_CHAT_CONFIG, sessionConfig);
        context.startActivity(intent);
    }

    public void onChatEnded() {
        finish();
    }

    public void onChatInitialized() {
        this.mChatInitialized = true;
    }

    public void onChatLoaded(Chat chat) {
        this.mChat = chat;
    }

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C1122R.C1126layout.zopim_chat_activity);
        setSupportActionBar((Toolbar) findViewById(C1122R.C1125id.toolbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        if (bundle != null) {
            this.mChatInitialized = bundle.getBoolean(STATE_CHAT_INITIALIZED, false);
            this.mChat = ZopimChat.resume(this);
        } else if (stopService(new Intent(this, ChatWidgetService.class))) {
            resumeChat();
        } else {
            if (getIntent() != null) {
                if (ChatActions.ACTION_RESUME_CHAT.equals(getIntent().getAction())) {
                    resumeChat();
                    return;
                }
            }
            FragmentManager supportFragmentManager = getSupportFragmentManager();
            if (supportFragmentManager.findFragmentByTag(ZopimChatFragment.class.getName()) == null) {
                SessionConfig sessionConfig = null;
                if (getIntent() != null && getIntent().hasExtra(EXTRA_CHAT_CONFIG)) {
                    sessionConfig = (SessionConfig) getIntent().getSerializableExtra(EXTRA_CHAT_CONFIG);
                }
                ZopimChatFragment zopimChatFragment = sessionConfig != null ? ZopimChatFragment.newInstance(sessionConfig) : new ZopimChatFragment();
                FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
                beginTransaction.add(C1122R.C1125id.chat_fragment_container, zopimChatFragment, ZopimChatFragment.class.getName());
                beginTransaction.commit();
            }
        }
    }

    /* access modifiers changed from: protected */
    public void onDestroy() {
        Logger.m577v(LOG_TAG, "Activity destroyed");
        super.onDestroy();
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        if (16908332 != menuItem.getItemId()) {
            return false;
        }
        finish();
        return super.onOptionsItemSelected(menuItem);
    }

    /* access modifiers changed from: protected */
    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putBoolean(STATE_CHAT_INITIALIZED, this.mChatInitialized);
    }

    /* access modifiers changed from: protected */
    public void onStart() {
        super.onStart();
        stopService(new Intent(this, ChatWidgetService.class));
    }

    /* access modifiers changed from: protected */
    @TargetApi(11)
    public void onStop() {
        super.onStop();
        boolean isFinishing = VERSION.SDK_INT >= 11 ? !isChangingConfigurations() : isFinishing();
        if (!isFinishing) {
            return;
        }
        if (!this.mChatInitialized) {
            this.mChat.endChat();
            Logger.m575i(LOG_TAG, "Chat initialization aborted. Ending chat.");
            finish();
        } else if (getSupportFragmentManager().findFragmentByTag(ZopimChatFragment.class.getName()) != null) {
            this.mChat.endChat();
            finish();
        }
    }
}
