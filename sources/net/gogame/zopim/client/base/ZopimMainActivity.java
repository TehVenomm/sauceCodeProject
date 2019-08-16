package net.gogame.zopim.client.base;

import android.app.AlertDialog.Builder;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import com.facebook.share.internal.ShareConstants;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import net.gogame.chat.BaseActivity;
import net.gogame.chat.ChatAdapter;
import net.gogame.chat.ChatAdapterViewFactory;
import net.gogame.chat.ChatContext;
import net.gogame.chat.ChatFragment;
import net.gogame.chat.Constants;
import net.gogame.chat.ImageViewFragment;
import net.gogame.chat.MultiChatContext;
import net.gogame.chat.MultiChatContext.Listener;
import net.gogame.chat.UIContext;
import net.gogame.chat.chatbot.ChatBotChatContext;
import net.gogame.chat.chatbot.ChatBotConfig;
import net.gogame.chat.zopim.ZopimChatContext;

public class ZopimMainActivity extends BaseActivity {
    private static final String CHAT_FRAGMENT_TAG = "CHAT_FRAGMENT";
    private static final String EXTRA_CHATBOT_CONFIG = "chatbot.config";
    private static final String EXTRA_ZOPIM_SESSIONCONFIG = "zopim.sessionConfig";
    private ChatAdapter chatAdapter;
    /* access modifiers changed from: private */
    public MultiChatContext multiChatContext;
    /* access modifiers changed from: private */
    public final UIContext uiContext = new UIContext() {
        public void showImage(String str) {
            if (str != null) {
                Bundle bundle = new Bundle();
                bundle.putString(ShareConstants.MEDIA_URI, str);
                ImageViewFragment imageViewFragment = new ImageViewFragment();
                imageViewFragment.setArguments(bundle);
                ZopimMainActivity.this.replaceFragment(imageViewFragment, Constants.FRAGMENT_CONTAINER, "IMAGE_SHOW_FRAGMENT");
            }
        }

        public void registerReceiver(BroadcastReceiver broadcastReceiver, IntentFilter intentFilter) {
            ZopimMainActivity.this.registerReceiver(broadcastReceiver, intentFilter);
        }

        public void unregisterReceiver(BroadcastReceiver broadcastReceiver) {
            ZopimMainActivity.this.unregisterReceiver(broadcastReceiver);
        }
    };

    public static void startActivity(Context context, SessionConfig sessionConfig) {
        Intent intent = new Intent(context, ZopimMainActivity.class);
        intent.putExtra(EXTRA_ZOPIM_SESSIONCONFIG, sessionConfig);
        context.startActivity(intent);
    }

    public static void startActivity(Context context, ChatBotConfig chatBotConfig, SessionConfig sessionConfig) {
        Intent intent = new Intent(context, ZopimMainActivity.class);
        intent.putExtra(EXTRA_CHATBOT_CONFIG, chatBotConfig);
        intent.putExtra(EXTRA_ZOPIM_SESSIONCONFIG, sessionConfig);
        context.startActivity(intent);
    }

    public ChatAdapter getChatAdapter() {
        return this.chatAdapter;
    }

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestWindowFeature(1);
        setFinishOnTouchOutside(false);
        setTitle(null);
        setContentView(C1122R.C1126layout.net_gogame_chat_activity_main);
        ChatBotConfig chatBotConfig = (ChatBotConfig) getIntent().getSerializableExtra(EXTRA_CHATBOT_CONFIG);
        final SessionConfig sessionConfig = (SessionConfig) getIntent().getSerializableExtra(EXTRA_ZOPIM_SESSIONCONFIG);
        final ChatAdapterViewFactory chatAdapterViewFactory = new ChatAdapterViewFactory(this, this.uiContext, true);
        final Button button = (Button) findViewById(C1122R.C1125id.switchButton);
        if (sessionConfig != null) {
            button.setBackgroundResource(C1122R.C1124drawable.net_gogame_chat_outline_button);
            button.setTextColor(getResources().getColor(C1122R.color.net_gogame_chat_outline_button));
            button.setOnClickListener(new OnClickListener() {
                public void onClick(View view) {
                    ZopimChatContext zopimChatContext = new ZopimChatContext(ZopimMainActivity.this, sessionConfig, chatAdapterViewFactory, ZopimMainActivity.this.uiContext);
                    zopimChatContext.start();
                    ZopimMainActivity.this.multiChatContext.addChatContext(zopimChatContext);
                }
            });
        } else {
            button.setBackgroundResource(C1122R.C1124drawable.net_gogame_chat_outline_button_disabled);
            button.setTextColor(getResources().getColor(C1122R.color.net_gogame_chat_outline_button_disabled));
            button.setOnClickListener(new OnClickListener() {
                public void onClick(View view) {
                    new Builder(ZopimMainActivity.this).setTitle(C1122R.string.net_gogame_chat_title).setMessage(C1122R.string.net_gogame_chat_vip_only_message).show();
                }
            });
        }
        this.multiChatContext = new MultiChatContext(new Listener() {
            public void onChatContextAdded(ChatContext chatContext) {
                if ((chatContext instanceof ChatBotChatContext) && sessionConfig != null) {
                    button.setVisibility(0);
                }
                if (chatContext instanceof ZopimChatContext) {
                    button.setVisibility(8);
                }
            }
        });
        if (chatBotConfig != null) {
            this.multiChatContext.addChatContext(new ChatBotChatContext(this, chatBotConfig, chatAdapterViewFactory));
        } else {
            this.multiChatContext.addChatContext(new ZopimChatContext(this, sessionConfig, chatAdapterViewFactory, this.uiContext));
        }
        this.chatAdapter = new ChatAdapter(this, this.uiContext, chatAdapterViewFactory, this.multiChatContext);
        findViewById(C1122R.C1125id.closeButton).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ZopimMainActivity.this.createExitDialog();
            }
        });
        findViewById(C1122R.C1125id.backButton).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ZopimMainActivity.this.onBackPressed();
            }
        });
        if (bundle == null) {
            addFragment(new ChatFragment(), Constants.FRAGMENT_CONTAINER, CHAT_FRAGMENT_TAG);
        }
    }

    /* access modifiers changed from: private */
    public void createExitDialog() {
        Builder builder;
        if (VERSION.SDK_INT >= 22) {
            builder = new Builder(this, 16974546);
        } else {
            builder = new Builder(this);
        }
        builder.setTitle(C1122R.string.net_gogame_chat_exit_alert_dialog_title);
        builder.setMessage(C1122R.string.net_gogame_chat_exit_alert_dialog_message);
        builder.setPositiveButton(C1122R.string.net_gogame_chat_end_button_caption, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                ZopimMainActivity.this.finish();
            }
        });
        builder.setNegativeButton(C1122R.string.net_gogame_chat_cancel_button_caption, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
            }
        });
        builder.create().show();
    }

    /* access modifiers changed from: protected */
    public void onStart() {
        super.onStart();
        if (this.multiChatContext != null) {
            this.multiChatContext.start();
        }
        if (this.chatAdapter != null) {
            this.chatAdapter.start();
        }
    }

    /* access modifiers changed from: protected */
    public void onStop() {
        if (this.multiChatContext != null) {
            this.multiChatContext.stop();
        }
        if (this.chatAdapter != null) {
            this.chatAdapter.stop();
        }
        super.onStop();
    }
}
