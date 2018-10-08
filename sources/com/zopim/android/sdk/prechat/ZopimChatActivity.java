package com.zopim.android.sdk.prechat;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import com.zopim.android.sdk.C0784R;
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
        Logger.m564v(LOG_TAG, "Resuming chat");
        this.mChat = ZopimChat.resume(this);
        this.mChatInitialized = !this.mChat.hasEnded();
        FragmentManager supportFragmentManager = getSupportFragmentManager();
        if (supportFragmentManager.findFragmentByTag(ZopimChatLogFragment.class.getName()) == null) {
            Fragment zopimChatLogFragment = new ZopimChatLogFragment();
            FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
            beginTransaction.add(C0784R.id.chat_fragment_container, zopimChatLogFragment, ZopimChatLogFragment.class.getName());
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

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C0784R.layout.zopim_chat_activity);
        setSupportActionBar((Toolbar) findViewById(C0784R.id.toolbar));
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
                Fragment newInstance = sessionConfig != null ? ZopimChatFragment.newInstance(sessionConfig) : new ZopimChatFragment();
                FragmentTransaction beginTransaction = supportFragmentManager.beginTransaction();
                beginTransaction.add(C0784R.id.chat_fragment_container, newInstance, ZopimChatFragment.class.getName());
                beginTransaction.commit();
            }
        }
    }

    protected void onDestroy() {
        Logger.m564v(LOG_TAG, "Activity destroyed");
        super.onDestroy();
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        if (16908332 != menuItem.getItemId()) {
            return false;
        }
        finish();
        return super.onOptionsItemSelected(menuItem);
    }

    protected void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putBoolean(STATE_CHAT_INITIALIZED, this.mChatInitialized);
    }

    protected void onStart() {
        super.onStart();
        stopService(new Intent(this, ChatWidgetService.class));
    }

    @TargetApi(11)
    protected void onStop() {
        super.onStop();
        boolean isFinishing = VERSION.SDK_INT >= 11 ? !isChangingConfigurations() : isFinishing();
        if (!isFinishing) {
            return;
        }
        if (!this.mChatInitialized) {
            this.mChat.endChat();
            Logger.m562i(LOG_TAG, "Chat initialization aborted. Ending chat.");
            finish();
        } else if (getSupportFragmentManager().findFragmentByTag(ZopimChatFragment.class.getName()) != null) {
            this.mChat.endChat();
            finish();
        }
    }
}
