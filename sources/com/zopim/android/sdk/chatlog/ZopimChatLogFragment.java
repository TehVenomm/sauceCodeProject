package com.zopim.android.sdk.chatlog;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentTransaction;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.RecyclerView.Adapter;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.ChatSession;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.attachment.ImagePicker;
import com.zopim.android.sdk.chatlog.ConnectionFragment.ConnectionListener;
import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.Agent;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Option;
import com.zopim.android.sdk.model.Profile;
import com.zopim.android.sdk.prechat.ChatListener;
import com.zopim.android.sdk.widget.ChatWidgetService;
import java.io.File;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map.Entry;
import java.util.TreeMap;

public class ZopimChatLogFragment extends Fragment implements ConnectionListener, C1176a {
    /* access modifiers changed from: private */
    public static final String LOG_TAG = ZopimChatLogFragment.class.getSimpleName();
    private static final String STATE_ATTACH_BUTTON_ENABLED = "ATTACH_BUTTON_ENABLED";
    private static final String STATE_INPUT_FIELD_ENABLED = "INPUT_FILED_ENABLED";
    private static final String STATE_INPUT_FIELD_TEXT = "INPUT_FILED_TEXT";
    private static final String STATE_NO_CONNECTION = "NO_CONNECTION";
    private static final String STATE_SEND_BUTTON_ENABLED = "SEND_BUTTON_ENABLED";
    private static final String STATE_SHOW_CHAT_END_CONFIRM_DIALOG = "SHOW_CHAT_END_CONFIRM_DIALOG";
    private static final String STATE_SHOW_EMAIL_TRANSCRIPT_DIALOG = "SHOW_EMAIL_TRANSCRIPT_DIALOG";
    private static final String STATE_SHOW_RECONNECT_TIMEOUT_DIALOG = "SHOW_RECONNECT_TIMEOUT_DIALOG";
    AgentsObserver mAgentsObserver = new C1193ao(this);
    /* access modifiers changed from: private */
    public ImageButton mAttachButton;
    List<File> mAttachmentErrorItems = new LinkedList();
    /* access modifiers changed from: private */
    public Chat mChat;
    private AlertDialog mChatEndConfirmDialog;
    /* access modifiers changed from: private */
    public ChatListener mChatListener;
    C1211i mChatLogAdapter;
    ChatLogObserver mChatLogObserver = new C1191am(this);
    private final ChatTimeoutReceiver mChatTimeoutReceiver = new ChatTimeoutReceiver();
    /* access modifiers changed from: private */
    public AlertDialog mEmailTranscriptDialog;
    /* access modifiers changed from: private */
    public final Handler mHandler = new Handler(Looper.getMainLooper());
    /* access modifiers changed from: private */
    public EditText mInputField;
    private InputMethodManager mInputManager;
    /* access modifiers changed from: private */
    public boolean mNoConnection = false;
    private long mReconnectTimeout = ChatSession.DEFAULT_RECONNECT_TIMEOUT;
    /* access modifiers changed from: private */
    public AlertDialog mReconnectTimeoutDialog;
    RecyclerView mRecyclerView;
    /* access modifiers changed from: private */
    public ImageButton mSendButton;
    Runnable mShowReconnectFailed = new C1188aj(this);

    public class ChatTimeoutReceiver extends BroadcastReceiver {
        public ChatTimeoutReceiver() {
        }

        public void onReceive(Context context, Intent intent) {
            if (intent == null || !ChatSession.ACTION_CHAT_SESSION_TIMEOUT.equals(intent.getAction())) {
                Log.w(ZopimChatLogFragment.LOG_TAG, "onReceive: intent was null or getAction() was mismatched");
                return;
            }
            Logger.m577v(ZopimChatLogFragment.LOG_TAG, "Received chat timeout. Disabling all input.");
            ZopimChatLogFragment.this.hideKeyboard(ZopimChatLogFragment.this.mInputField);
            ZopimChatLogFragment.this.mSendButton.setEnabled(false);
            ZopimChatLogFragment.this.mAttachButton.setEnabled(false);
            ZopimChatLogFragment.this.mInputField.setFocusable(false);
            ZopimChatLogFragment.this.mInputField.setEnabled(false);
        }
    }

    /* access modifiers changed from: private */
    public boolean canChat() {
        return (!this.mChat.hasEnded()) && !this.mNoConnection;
    }

    /* access modifiers changed from: private */
    public void close() {
        FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
        beginTransaction.remove(this);
        beginTransaction.commit();
    }

    /* access modifiers changed from: private */
    public Adapter getListAdapter() {
        return this.mRecyclerView.getAdapter();
    }

    /* access modifiers changed from: private */
    public void hideKeyboard(View view) {
        if (view != null) {
            view.clearFocus();
            this.mInputManager.hideSoftInputFromWindow(view.getWindowToken(), 0);
        }
    }

    private void showConfirmDialog() {
        this.mChatEndConfirmDialog = new Builder(getActivity()).setTitle(C1122R.string.chat_end_dialog_title).setMessage(C1122R.string.chat_end_dialog_message).setPositiveButton(C1122R.string.chat_end_dialog_confirm_button, new C1200av(this)).setNegativeButton(C1122R.string.chat_end_dialog_cancel_button, new C1199au(this)).show();
    }

    /* access modifiers changed from: private */
    public void showEmailTranscriptDialog() {
        Profile profile = ZopimChat.getDataSource().getProfile();
        boolean z = (profile == null || profile.getEmail() == null || profile.getEmail().isEmpty()) ? false : true;
        EditText editText = (EditText) getActivity().getLayoutInflater().inflate(C1122R.C1126layout.email_transcript_input_view, null);
        Builder negativeButton = new Builder(getActivity()).setPositiveButton(17039370, null).setTitle(C1122R.string.email_transcript_title).setMessage(C1122R.string.email_transcript_message).setPositiveButton(C1122R.string.email_transcript_confirm_button, new C1202ax(this)).setNegativeButton(C1122R.string.email_transcript_cancel_button, new C1201aw(this));
        if (z) {
            negativeButton.setPositiveButton(C1122R.string.email_transcript_confirm_button, new C1203ay(this, profile));
            this.mEmailTranscriptDialog = negativeButton.show();
            return;
        }
        this.mEmailTranscriptDialog = negativeButton.setView(editText).show();
        TextView textView = (TextView) this.mEmailTranscriptDialog.findViewById(16908299);
        LayoutParams layoutParams = textView.getLayoutParams();
        if (textView != null) {
            FrameLayout.LayoutParams layoutParams2 = new FrameLayout.LayoutParams(layoutParams);
            layoutParams2.leftMargin = textView.getPaddingLeft() - editText.getPaddingLeft();
            layoutParams2.rightMargin = textView.getPaddingRight() + editText.getPaddingRight();
            editText.setLayoutParams(layoutParams2);
        }
        Button button = this.mEmailTranscriptDialog.getButton(-1);
        if (button != null) {
            button.setEnabled(false);
            button.setOnClickListener(new C1186ah(this, z, profile, editText));
            editText.addTextChangedListener(new C1187ai(this, button));
        }
    }

    private void showKeyboard(View view) {
        if (view != null && view.isEnabled()) {
            view.requestFocus();
            this.mInputManager.showSoftInput(view, 1);
        }
    }

    /* access modifiers changed from: private */
    public void updateChatLogAdapter(LinkedHashMap<String, ChatLog> linkedHashMap) {
        int i = 0;
        if (!(getListAdapter() instanceof C1211i)) {
            Log.w(LOG_TAG, "Aborting update. Adapter must be of type " + C1211i.class);
            return;
        }
        TreeMap treeMap = new TreeMap();
        boolean z = true;
        for (Entry entry : linkedHashMap.entrySet()) {
            String str = (String) entry.getKey();
            ChatLog chatLog = (ChatLog) entry.getValue();
            C1178aa aaVar = new C1178aa();
            aaVar.f793g = str;
            aaVar.f795i = chatLog.getMessage();
            aaVar.f796j = chatLog.getDisplayName();
            aaVar.f797k = chatLog.getNick();
            aaVar.f798l = chatLog.getTimestamp();
            switch (C1195aq.f836b[chatLog.getType().ordinal()]) {
                case 1:
                    C1180ab abVar = new C1180ab(aaVar);
                    abVar.f813e = chatLog.isFailed() == null ? false : chatLog.isFailed().booleanValue();
                    treeMap.put(aaVar.f793g, abVar);
                    break;
                case 2:
                    C1177a aVar = new C1177a(aaVar);
                    aVar.f787a = chatLog.getAttachment() != null ? chatLog.getAttachment().getUrl() : null;
                    aVar.f789c = chatLog.getAttachment() != null ? chatLog.getAttachment().getName() : null;
                    aVar.f790d = chatLog.getFile();
                    aVar.f788b = chatLog.getAttachment() != null ? chatLog.getAttachment().getSize() : null;
                    Agent agent = (Agent) ZopimChat.getDataSource().getAgents().get(aVar.f797k);
                    if (agent != null) {
                        aVar.f791e = agent.getAvatarUri();
                    }
                    aVar.f792f = new String[chatLog.getOptions().length];
                    int i2 = 0;
                    while (true) {
                        if (i2 < chatLog.getOptions().length) {
                            Option option = chatLog.getOptions()[i2];
                            aVar.f792f[i2] = option.getLabel();
                            if (option.isSelected()) {
                                aVar.f792f = new String[]{option.getLabel()};
                            } else {
                                i2++;
                            }
                        }
                    }
                    treeMap.put(aVar.f793g, aVar);
                    break;
                case 3:
                    aaVar.f794h = C1179a.CHAT_EVENT;
                    aaVar.f795i = String.format(getResources().getString(C1122R.string.chat_visitor_queue_message), new Object[]{chatLog.getVisitorQueue()});
                    treeMap.put(aaVar.f793g, aaVar);
                    break;
                case 4:
                    aaVar.f794h = C1179a.CHAT_EVENT;
                    treeMap.put(aaVar.f793g, aaVar);
                    break;
                case 5:
                    Iterator it = ZopimChat.getDataSource().getAgents().keySet().iterator();
                    while (true) {
                        if (it.hasNext()) {
                            if (((String) it.next()).equals(aaVar.f797k)) {
                                if (!z) {
                                    aaVar.f794h = C1179a.MEMBER_EVENT;
                                    aaVar.f795i = String.format(getResources().getString(C1122R.string.chat_agent_joined_message), new Object[]{chatLog.getDisplayName()});
                                    treeMap.put(aaVar.f793g, aaVar);
                                    break;
                                } else {
                                    z = false;
                                }
                            }
                        } else {
                            break;
                        }
                    }
                case 6:
                    Iterator it2 = ZopimChat.getDataSource().getAgents().keySet().iterator();
                    while (true) {
                        if (it2.hasNext()) {
                            if (((String) it2.next()).equals(aaVar.f797k)) {
                                aaVar.f794h = C1179a.MEMBER_EVENT;
                                aaVar.f795i = String.format(getResources().getString(C1122R.string.chat_agent_left_message), new Object[]{chatLog.getDisplayName()});
                                treeMap.put(aaVar.f793g, aaVar);
                                break;
                            }
                        } else {
                            break;
                        }
                    }
                case 7:
                    switch (C1195aq.f835a[chatLog.getError().ordinal()]) {
                        case 2:
                            if (!this.mAttachmentErrorItems.contains(chatLog.getFile())) {
                                Toast.makeText(getActivity(), C1122R.string.attachment_upload_size_limit_error_message, 1).show();
                                this.mAttachmentErrorItems.add(chatLog.getFile());
                                break;
                            }
                            break;
                        case 3:
                            if (!this.mAttachmentErrorItems.contains(chatLog.getFile())) {
                                Toast.makeText(getActivity(), C1122R.string.attachment_upload_type_error_message, 1).show();
                                this.mAttachmentErrorItems.add(chatLog.getFile());
                                break;
                            }
                            break;
                    }
                    C1180ab abVar2 = new C1180ab(aaVar);
                    abVar2.f810b = chatLog.getUploadUrl();
                    abVar2.f809a = chatLog.getFile();
                    abVar2.f811c = chatLog.getProgress();
                    abVar2.f812d = chatLog.getError().getValue();
                    if (abVar2.f810b == null) {
                        break;
                    } else {
                        treeMap.put(abVar2.f793g, abVar2);
                        break;
                    }
                case 8:
                    C1222t tVar = new C1222t(aaVar);
                    tVar.f875a = chatLog.getRating();
                    tVar.f876b = chatLog.getComment();
                    treeMap.put(tVar.f793g, tVar);
                    break;
                default:
                    Log.v(LOG_TAG, "Not showing this item in list view: " + chatLog.getType());
                    break;
            }
            z = z;
        }
        C1211i listAdapter = getListAdapter();
        while (true) {
            int i3 = i;
            if (i3 < listAdapter.getItemCount()) {
                C1178aa b = listAdapter.mo20759b(i3);
                C1178aa aaVar2 = b.f793g == null ? null : (C1178aa) treeMap.get(b.f793g);
                if (aaVar2 == null) {
                    Logger.m577v(LOG_TAG, "Removed row item " + b.f794h);
                    listAdapter.mo20756a(i3);
                    listAdapter.notifyItemChanged(i3);
                } else {
                    if ((b instanceof C1180ab) && (aaVar2 instanceof C1180ab)) {
                        C1180ab abVar3 = (C1180ab) b;
                        C1180ab abVar4 = (C1180ab) aaVar2;
                        if (!abVar3.equals(abVar4)) {
                            abVar3.mo20720a(abVar4);
                            Logger.m577v(LOG_TAG, "Update VisitorItem " + abVar3);
                            listAdapter.notifyItemChanged(i3);
                        }
                    }
                    if ((b instanceof C1177a) && (aaVar2 instanceof C1177a)) {
                        C1177a aVar2 = (C1177a) b;
                        C1177a aVar3 = (C1177a) aaVar2;
                        if (!aVar2.equals(aVar3)) {
                            aVar2.mo20720a(aVar3);
                            Logger.m577v(LOG_TAG, "Update AgentItem " + aVar2);
                            listAdapter.notifyItemChanged(i3);
                        }
                    }
                    if ((b instanceof C1222t) && (aaVar2 instanceof C1222t)) {
                        C1222t tVar2 = (C1222t) b;
                        C1222t tVar3 = (C1222t) aaVar2;
                        if (!tVar2.equals(tVar3)) {
                            tVar2.mo20720a(tVar3);
                            Logger.m577v(LOG_TAG, "Update ChatRatingItem " + tVar2);
                            listAdapter.notifyItemChanged(i3);
                        }
                    }
                    treeMap.remove(aaVar2.f793g);
                }
                i = i3 + 1;
            } else {
                for (C1178aa aaVar3 : treeMap.values()) {
                    Logger.m577v(LOG_TAG, "Added RowItem " + aaVar3);
                    listAdapter.mo20758a(aaVar3);
                    listAdapter.notifyItemChanged(listAdapter.getItemCount());
                    Logger.m577v(LOG_TAG, "Auto-scroll");
                    this.mRecyclerView.getLayoutManager().scrollToPosition(getListAdapter().getItemCount() - 1);
                }
                return;
            }
        }
    }

    public void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        ImagePicker.INSTANCE.getFilesFromActivityOnResult(getActivity(), i, i2, intent, new C1198at(this));
    }

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (activity instanceof ChatListener) {
            this.mChatListener = (ChatListener) activity;
        }
    }

    public void onConnected() {
        this.mNoConnection = false;
        if (canChat()) {
            if (this.mSendButton != null && !this.mSendButton.isEnabled() && this.mInputField.getText().length() > 0) {
                this.mSendButton.setEnabled(true);
            }
            if (this.mAttachButton != null && !this.mAttachButton.isEnabled()) {
                this.mAttachButton.setEnabled(true);
            }
        }
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setHasOptionsMenu(true);
        this.mChat = ZopimChat.resume(getActivity());
        this.mReconnectTimeout = ZopimChat.getReconnectTimeout().longValue();
        if (bundle == null) {
            ConnectionToastFragment connectionToastFragment = new ConnectionToastFragment();
            ConnectionFragment connectionFragment = new ConnectionFragment();
            FragmentTransaction beginTransaction = getChildFragmentManager().beginTransaction();
            beginTransaction.add(C1122R.C1125id.toast_fragment_container, connectionToastFragment, ConnectionToastFragment.class.getName());
            beginTransaction.add((Fragment) connectionFragment, ConnectionFragment.class.getName());
            beginTransaction.commit();
        }
        this.mInputManager = (InputMethodManager) getActivity().getSystemService("input_method");
    }

    public void onCreateOptionsMenu(Menu menu, MenuInflater menuInflater) {
        super.onCreateOptionsMenu(menu, menuInflater);
        menuInflater.inflate(C1122R.menu.chat_log_menu, menu);
    }

    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        View inflate = layoutInflater.inflate(C1122R.C1126layout.zopim_chat_log_fragment, viewGroup, false);
        this.mRecyclerView = inflate.findViewById(C1122R.C1125id.recycler_view);
        this.mRecyclerView.setLayoutManager(new LinearLayoutManager(getActivity(), 1, false));
        this.mChatLogAdapter = new C1211i(getActivity(), new ArrayList());
        this.mChatLogAdapter.mo20757a(this.mChat);
        this.mRecyclerView.setAdapter(this.mChatLogAdapter);
        return inflate;
    }

    public void onDisconnected() {
        this.mNoConnection = true;
        if (this.mSendButton != null && this.mSendButton.isEnabled()) {
            this.mSendButton.setEnabled(false);
        }
        if (this.mAttachButton != null && this.mAttachButton.isEnabled()) {
            this.mAttachButton.setEnabled(false);
        }
    }

    public void onHideToast() {
        this.mHandler.removeCallbacks(this.mShowReconnectFailed);
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        int itemId = menuItem.getItemId();
        if (16908332 == itemId && this.mChat.hasEnded()) {
            close();
            return super.onOptionsItemSelected(menuItem);
        } else if (C1122R.C1125id.end_chat != itemId) {
            return super.onOptionsItemSelected(menuItem);
        } else {
            if (this.mChat.hasEnded()) {
                close();
                if (this.mChatListener != null) {
                    this.mChatListener.onChatEnded();
                }
            } else {
                showConfirmDialog();
            }
            return true;
        }
    }

    public void onPause() {
        boolean z = true;
        super.onPause();
        hideKeyboard(this.mInputField);
        boolean z2 = !this.mChat.hasEnded();
        if (VERSION.SDK_INT >= 11) {
            if (!z2 || getActivity().isChangingConfigurations()) {
                z = false;
            }
        } else if (!z2 || !getActivity().isFinishing()) {
            z = false;
        }
        if (z) {
            getActivity().startService(new Intent(getActivity(), ChatWidgetService.class));
        }
    }

    public void onResume() {
        super.onResume();
        getActivity().stopService(new Intent(getActivity(), ChatWidgetService.class));
        if (this.mChat.hasEnded()) {
            hideKeyboard(this.mInputField);
            this.mSendButton.setEnabled(false);
            this.mAttachButton.setEnabled(false);
            this.mInputField.setFocusable(false);
            this.mInputField.setEnabled(false);
            Logger.m577v(LOG_TAG, "Resuming expired chat. Disable all input elements.");
        }
    }

    public void onSaveInstanceState(Bundle bundle) {
        boolean z = false;
        super.onSaveInstanceState(bundle);
        bundle.putBoolean(STATE_SEND_BUTTON_ENABLED, this.mSendButton.isEnabled());
        bundle.putBoolean(STATE_ATTACH_BUTTON_ENABLED, this.mAttachButton.isEnabled());
        bundle.putBoolean(STATE_INPUT_FIELD_ENABLED, this.mInputField.isEnabled());
        bundle.putString(STATE_INPUT_FIELD_TEXT, this.mInputField.getText().toString().trim());
        bundle.putBoolean(STATE_NO_CONNECTION, this.mNoConnection);
        bundle.putBoolean(STATE_SHOW_RECONNECT_TIMEOUT_DIALOG, this.mReconnectTimeoutDialog != null ? this.mReconnectTimeoutDialog.isShowing() : false);
        bundle.putBoolean(STATE_SHOW_CHAT_END_CONFIRM_DIALOG, this.mChatEndConfirmDialog != null ? this.mChatEndConfirmDialog.isShowing() : false);
        String str = STATE_SHOW_EMAIL_TRANSCRIPT_DIALOG;
        if (this.mEmailTranscriptDialog != null) {
            z = this.mEmailTranscriptDialog.isShowing();
        }
        bundle.putBoolean(str, z);
    }

    public void onShowToast() {
        this.mHandler.removeCallbacks(this.mShowReconnectFailed);
        this.mHandler.postDelayed(this.mShowReconnectFailed, this.mReconnectTimeout);
    }

    public void onStart() {
        super.onStart();
        updateChatLogAdapter(ZopimChat.getDataSource().getChatLog());
        ZopimChat.getDataSource().addChatLogObserver(this.mChatLogObserver);
        ZopimChat.getDataSource().addAgentsObserver(this.mAgentsObserver);
        getActivity().registerReceiver(this.mChatTimeoutReceiver, new IntentFilter(ChatSession.ACTION_CHAT_SESSION_TIMEOUT));
    }

    public void onStop() {
        super.onStop();
        this.mHandler.removeCallbacksAndMessages(null);
        if (this.mReconnectTimeoutDialog != null && this.mReconnectTimeoutDialog.isShowing()) {
            this.mReconnectTimeoutDialog.dismiss();
        }
        if (this.mChatEndConfirmDialog != null && this.mChatEndConfirmDialog.isShowing()) {
            this.mChatEndConfirmDialog.dismiss();
        }
        if (this.mEmailTranscriptDialog != null && this.mEmailTranscriptDialog.isShowing()) {
            this.mEmailTranscriptDialog.dismiss();
        }
        ZopimChat.getDataSource().deleteChatLogObserver(this.mChatLogObserver);
        ZopimChat.getDataSource().deleteAgentsObserver(this.mAgentsObserver);
        getActivity().unregisterReceiver(this.mChatTimeoutReceiver);
    }

    public void onViewCreated(View view, Bundle bundle) {
        super.onViewCreated(view, bundle);
        this.mInputField = (EditText) view.findViewById(C1122R.C1125id.input_field);
        this.mSendButton = (ImageButton) view.findViewById(C1122R.C1125id.send_button);
        this.mAttachButton = (ImageButton) view.findViewById(C1122R.C1125id.attach_button);
        this.mInputField.addTextChangedListener(new C1185ag(this));
        this.mSendButton.setOnClickListener(new C1196ar(this));
        this.mAttachButton.setOnClickListener(new C1197as(this));
    }

    public void onViewStateRestored(@Nullable Bundle bundle) {
        super.onViewStateRestored(bundle);
        if (bundle != null) {
            this.mSendButton.setEnabled(bundle.getBoolean(STATE_SEND_BUTTON_ENABLED, true));
            this.mAttachButton.setEnabled(bundle.getBoolean(STATE_ATTACH_BUTTON_ENABLED, true));
            boolean z = bundle.getBoolean(STATE_INPUT_FIELD_ENABLED, true);
            this.mInputField.setEnabled(z);
            this.mInputField.setFocusable(z);
            this.mInputField.setText(bundle.getString(STATE_INPUT_FIELD_TEXT));
            this.mNoConnection = bundle.getBoolean(STATE_NO_CONNECTION, false);
            boolean z2 = bundle.getBoolean(STATE_SHOW_RECONNECT_TIMEOUT_DIALOG, false);
            boolean z3 = bundle.getBoolean(STATE_SHOW_CHAT_END_CONFIRM_DIALOG, false);
            boolean z4 = bundle.getBoolean(STATE_SHOW_EMAIL_TRANSCRIPT_DIALOG, false);
            if (z2) {
                this.mHandler.post(this.mShowReconnectFailed);
            }
            if (z3) {
                showConfirmDialog();
            }
            if (z4) {
                showEmailTranscriptDialog();
            }
        } else {
            this.mSendButton.setEnabled(false);
        }
        if (this.mInputField.isEnabled()) {
            showKeyboard(this.mInputField);
        }
    }
}
