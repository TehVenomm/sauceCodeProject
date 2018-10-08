package net.gogame.zopim.client.base;

import android.app.AlertDialog.Builder;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import com.facebook.share.internal.ShareConstants;
import com.zopim.android.sdk.C0785R;
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
    private MultiChatContext multiChatContext;
    private final UIContext uiContext = new C12511();

    /* renamed from: net.gogame.zopim.client.base.ZopimMainActivity$1 */
    class C12511 implements UIContext {
        C12511() {
        }

        public void showImage(String str) {
            if (str != null) {
                Bundle bundle = new Bundle();
                bundle.putString(ShareConstants.MEDIA_URI, str);
                Fragment imageViewFragment = new ImageViewFragment();
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
    }

    /* renamed from: net.gogame.zopim.client.base.ZopimMainActivity$3 */
    class C12533 implements OnClickListener {
        C12533() {
        }

        public void onClick(View view) {
            new Builder(ZopimMainActivity.this).setTitle(C0785R.string.net_gogame_chat_title).setMessage(C0785R.string.net_gogame_chat_vip_only_message).show();
        }
    }

    /* renamed from: net.gogame.zopim.client.base.ZopimMainActivity$5 */
    class C12555 implements OnClickListener {
        C12555() {
        }

        public void onClick(View view) {
            ZopimMainActivity.this.createExitDialog();
        }
    }

    /* renamed from: net.gogame.zopim.client.base.ZopimMainActivity$6 */
    class C12566 implements OnClickListener {
        C12566() {
        }

        public void onClick(View view) {
            ZopimMainActivity.this.onBackPressed();
        }
    }

    /* renamed from: net.gogame.zopim.client.base.ZopimMainActivity$7 */
    class C12577 implements DialogInterface.OnClickListener {
        C12577() {
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            ZopimMainActivity.this.finish();
        }
    }

    /* renamed from: net.gogame.zopim.client.base.ZopimMainActivity$8 */
    class C12588 implements DialogInterface.OnClickListener {
        C12588() {
        }

        public void onClick(DialogInterface dialogInterface, int i) {
        }
    }

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

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestWindowFeature(1);
        setFinishOnTouchOutside(false);
        setTitle(null);
        setContentView(C0785R.layout.net_gogame_chat_activity_main);
        ChatBotConfig chatBotConfig = (ChatBotConfig) getIntent().getSerializableExtra(EXTRA_CHATBOT_CONFIG);
        final SessionConfig sessionConfig = (SessionConfig) getIntent().getSerializableExtra(EXTRA_ZOPIM_SESSIONCONFIG);
        final ChatAdapterViewFactory chatAdapterViewFactory = new ChatAdapterViewFactory(this, this.uiContext, true);
        final Button button = (Button) findViewById(C0785R.id.switchButton);
        if (sessionConfig != null) {
            button.setBackgroundResource(C0785R.drawable.net_gogame_chat_outline_button);
            button.setTextColor(getResources().getColor(C0785R.color.net_gogame_chat_outline_button));
            button.setOnClickListener(new OnClickListener() {
                public void onClick(View view) {
                    ChatContext zopimChatContext = new ZopimChatContext(ZopimMainActivity.this, sessionConfig, chatAdapterViewFactory, ZopimMainActivity.this.uiContext);
                    zopimChatContext.start();
                    ZopimMainActivity.this.multiChatContext.addChatContext(zopimChatContext);
                }
            });
        } else {
            button.setBackgroundResource(C0785R.drawable.net_gogame_chat_outline_button_disabled);
            button.setTextColor(getResources().getColor(C0785R.color.net_gogame_chat_outline_button_disabled));
            button.setOnClickListener(new C12533());
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
        findViewById(C0785R.id.closeButton).setOnClickListener(new C12555());
        findViewById(C0785R.id.backButton).setOnClickListener(new C12566());
        if (bundle == null) {
            addFragment(new ChatFragment(), Constants.FRAGMENT_CONTAINER, CHAT_FRAGMENT_TAG);
        }
    }

    private void createExitDialog() {
        Builder builder;
        if (VERSION.SDK_INT >= 22) {
            builder = new Builder(this, 16974546);
        } else {
            builder = new Builder(this);
        }
        builder.setTitle(C0785R.string.net_gogame_chat_exit_alert_dialog_title);
        builder.setMessage(C0785R.string.net_gogame_chat_exit_alert_dialog_message);
        builder.setPositiveButton(C0785R.string.net_gogame_chat_end_button_caption, new C12577());
        builder.setNegativeButton(C0785R.string.net_gogame_chat_cancel_button_caption, new C12588());
        builder.create().show();
    }

    protected void onStart() {
        super.onStart();
        if (this.multiChatContext != null) {
            this.multiChatContext.start();
        }
        if (this.chatAdapter != null) {
            this.chatAdapter.start();
        }
    }

    protected void onStop() {
        if (this.multiChatContext != null) {
            this.multiChatContext.stop();
        }
        if (this.chatAdapter != null) {
            this.chatAdapter.stop();
        }
        super.onStop();
    }
}
