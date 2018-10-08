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
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
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
import com.zopim.android.sdk.C0785R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.ChatSession;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.attachment.ImagePicker;
import com.zopim.android.sdk.chatlog.ConnectionFragment.ConnectionListener;
import com.zopim.android.sdk.chatlog.ConnectionToastFragment.C0832a;
import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.Profile;
import com.zopim.android.sdk.prechat.ChatListener;
import com.zopim.android.sdk.widget.ChatWidgetService;
import java.io.File;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

public class ZopimChatLogFragment extends Fragment implements ConnectionListener, C0832a {
    private static final String LOG_TAG = ZopimChatLogFragment.class.getSimpleName();
    private static final String STATE_ATTACH_BUTTON_ENABLED = "ATTACH_BUTTON_ENABLED";
    private static final String STATE_INPUT_FIELD_ENABLED = "INPUT_FILED_ENABLED";
    private static final String STATE_INPUT_FIELD_TEXT = "INPUT_FILED_TEXT";
    private static final String STATE_NO_CONNECTION = "NO_CONNECTION";
    private static final String STATE_SEND_BUTTON_ENABLED = "SEND_BUTTON_ENABLED";
    private static final String STATE_SHOW_CHAT_END_CONFIRM_DIALOG = "SHOW_CHAT_END_CONFIRM_DIALOG";
    private static final String STATE_SHOW_EMAIL_TRANSCRIPT_DIALOG = "SHOW_EMAIL_TRANSCRIPT_DIALOG";
    private static final String STATE_SHOW_RECONNECT_TIMEOUT_DIALOG = "SHOW_RECONNECT_TIMEOUT_DIALOG";
    AgentsObserver mAgentsObserver = new ao(this);
    private ImageButton mAttachButton;
    List<File> mAttachmentErrorItems = new LinkedList();
    private Chat mChat;
    private AlertDialog mChatEndConfirmDialog;
    private ChatListener mChatListener;
    C0842i mChatLogAdapter;
    ChatLogObserver mChatLogObserver = new am(this);
    private final ChatTimeoutReceiver mChatTimeoutReceiver = new ChatTimeoutReceiver();
    private AlertDialog mEmailTranscriptDialog;
    private final Handler mHandler = new Handler(Looper.getMainLooper());
    private EditText mInputField;
    private InputMethodManager mInputManager;
    private boolean mNoConnection = false;
    private long mReconnectTimeout = ChatSession.DEFAULT_RECONNECT_TIMEOUT;
    private AlertDialog mReconnectTimeoutDialog;
    RecyclerView mRecyclerView;
    private ImageButton mSendButton;
    Runnable mShowReconnectFailed = new aj(this);

    public class ChatTimeoutReceiver extends BroadcastReceiver {
        public void onReceive(Context context, Intent intent) {
            if (intent == null || !ChatSession.ACTION_CHAT_SESSION_TIMEOUT.equals(intent.getAction())) {
                Log.w(ZopimChatLogFragment.LOG_TAG, "onReceive: intent was null or getAction() was mismatched");
                return;
            }
            Logger.m564v(ZopimChatLogFragment.LOG_TAG, "Received chat timeout. Disabling all input.");
            ZopimChatLogFragment.this.hideKeyboard(ZopimChatLogFragment.this.mInputField);
            ZopimChatLogFragment.this.mSendButton.setEnabled(false);
            ZopimChatLogFragment.this.mAttachButton.setEnabled(false);
            ZopimChatLogFragment.this.mInputField.setFocusable(false);
            ZopimChatLogFragment.this.mInputField.setEnabled(false);
        }
    }

    private boolean canChat() {
        return (!this.mChat.hasEnded()) && !this.mNoConnection;
    }

    private void close() {
        FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
        beginTransaction.remove(this);
        beginTransaction.commit();
    }

    private Adapter getListAdapter() {
        return this.mRecyclerView.getAdapter();
    }

    private void hideKeyboard(View view) {
        if (view != null) {
            view.clearFocus();
            this.mInputManager.hideSoftInputFromWindow(view.getWindowToken(), 0);
        }
    }

    private void showConfirmDialog() {
        this.mChatEndConfirmDialog = new Builder(getActivity()).setTitle(C0785R.string.chat_end_dialog_title).setMessage(C0785R.string.chat_end_dialog_message).setPositiveButton(C0785R.string.chat_end_dialog_confirm_button, new av(this)).setNegativeButton(C0785R.string.chat_end_dialog_cancel_button, new au(this)).show();
    }

    private void showEmailTranscriptDialog() {
        Profile profile = ZopimChat.getDataSource().getProfile();
        boolean z = (profile == null || profile.getEmail() == null || profile.getEmail().isEmpty()) ? false : true;
        EditText editText = (EditText) getActivity().getLayoutInflater().inflate(C0785R.layout.email_transcript_input_view, null);
        Builder negativeButton = new Builder(getActivity()).setPositiveButton(17039370, null).setTitle(C0785R.string.email_transcript_title).setMessage(C0785R.string.email_transcript_message).setPositiveButton(C0785R.string.email_transcript_confirm_button, new ax(this)).setNegativeButton(C0785R.string.email_transcript_cancel_button, new aw(this));
        if (z) {
            negativeButton.setPositiveButton(C0785R.string.email_transcript_confirm_button, new ay(this, profile));
            this.mEmailTranscriptDialog = negativeButton.show();
            return;
        }
        this.mEmailTranscriptDialog = negativeButton.setView(editText).show();
        TextView textView = (TextView) this.mEmailTranscriptDialog.findViewById(16908299);
        LayoutParams layoutParams = textView.getLayoutParams();
        if (textView != null) {
            LayoutParams layoutParams2 = new FrameLayout.LayoutParams(layoutParams);
            layoutParams2.leftMargin = textView.getPaddingLeft() - editText.getPaddingLeft();
            layoutParams2.rightMargin = textView.getPaddingRight() + editText.getPaddingRight();
            editText.setLayoutParams(layoutParams2);
        }
        Button button = this.mEmailTranscriptDialog.getButton(-1);
        if (button != null) {
            button.setEnabled(false);
            button.setOnClickListener(new ah(this, z, profile, editText));
            editText.addTextChangedListener(new ai(this, button));
        }
    }

    private void showKeyboard(View view) {
        if (view != null && view.isEnabled()) {
            view.requestFocus();
            this.mInputManager.showSoftInput(view, 1);
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void updateChatLogAdapter(java.util.LinkedHashMap<java.lang.String, com.zopim.android.sdk.model.ChatLog> r13) {
        /*
        r12 = this;
        r5 = 0;
        r3 = 1;
        r4 = 0;
        r0 = r12.getListAdapter();
        r0 = r0 instanceof com.zopim.android.sdk.chatlog.C0842i;
        if (r0 != 0) goto L_0x0026;
    L_0x000b:
        r0 = LOG_TAG;
        r1 = new java.lang.StringBuilder;
        r1.<init>();
        r2 = "Aborting update. Adapter must be of type ";
        r1 = r1.append(r2);
        r2 = com.zopim.android.sdk.chatlog.C0842i.class;
        r1 = r1.append(r2);
        r1 = r1.toString();
        android.util.Log.w(r0, r1);
    L_0x0025:
        return;
    L_0x0026:
        r7 = new java.util.TreeMap;
        r7.<init>();
        r0 = r13.entrySet();
        r6 = r0.iterator();
        r2 = r3;
    L_0x0034:
        r0 = r6.hasNext();
        if (r0 == 0) goto L_0x02a9;
    L_0x003a:
        r0 = r6.next();
        r0 = (java.util.Map.Entry) r0;
        r1 = r0.getKey();
        r1 = (java.lang.String) r1;
        r0 = r0.getValue();
        r0 = (com.zopim.android.sdk.model.ChatLog) r0;
        r8 = new com.zopim.android.sdk.chatlog.aa;
        r8.<init>();
        r8.f743g = r1;
        r1 = r0.getMessage();
        r8.f745i = r1;
        r1 = r0.getDisplayName();
        r8.f746j = r1;
        r1 = r0.getNick();
        r8.f747k = r1;
        r1 = r0.getTimestamp();
        r8.f748l = r1;
        r1 = com.zopim.android.sdk.chatlog.aq.f792b;
        r9 = r0.getType();
        r9 = r9.ordinal();
        r1 = r1[r9];
        switch(r1) {
            case 1: goto L_0x0099;
            case 2: goto L_0x00b7;
            case 3: goto L_0x014a;
            case 4: goto L_0x016e;
            case 5: goto L_0x017a;
            case 6: goto L_0x01c6;
            case 7: goto L_0x020e;
            case 8: goto L_0x0290;
            default: goto L_0x007a;
        };
    L_0x007a:
        r1 = LOG_TAG;
        r8 = new java.lang.StringBuilder;
        r8.<init>();
        r9 = "Not showing this item in list view: ";
        r8 = r8.append(r9);
        r0 = r0.getType();
        r0 = r8.append(r0);
        r0 = r0.toString();
        android.util.Log.v(r1, r0);
    L_0x0096:
        r0 = r2;
    L_0x0097:
        r2 = r0;
        goto L_0x0034;
    L_0x0099:
        r1 = new com.zopim.android.sdk.chatlog.ab;
        r1.<init>(r8);
        r9 = r0.isFailed();
        if (r9 != 0) goto L_0x00ae;
    L_0x00a4:
        r0 = r4;
    L_0x00a5:
        r1.f769e = r0;
        r0 = r8.f743g;
        r7.put(r0, r1);
        r0 = r2;
        goto L_0x0097;
    L_0x00ae:
        r0 = r0.isFailed();
        r0 = r0.booleanValue();
        goto L_0x00a5;
    L_0x00b7:
        r9 = new com.zopim.android.sdk.chatlog.a;
        r9.<init>(r8);
        r1 = r0.getAttachment();
        if (r1 == 0) goto L_0x0141;
    L_0x00c2:
        r1 = r0.getAttachment();
        r1 = r1.getUrl();
    L_0x00ca:
        r9.f749a = r1;
        r1 = r0.getAttachment();
        if (r1 == 0) goto L_0x0143;
    L_0x00d2:
        r1 = r0.getAttachment();
        r1 = r1.getName();
    L_0x00da:
        r9.f751c = r1;
        r1 = r0.getFile();
        r9.f752d = r1;
        r1 = r0.getAttachment();
        if (r1 == 0) goto L_0x0145;
    L_0x00e8:
        r1 = r0.getAttachment();
        r1 = r1.getSize();
    L_0x00f0:
        r9.f750b = r1;
        r1 = com.zopim.android.sdk.api.ZopimChat.getDataSource();
        r1 = r1.getAgents();
        r8 = r9.k;
        r1 = r1.get(r8);
        r1 = (com.zopim.android.sdk.model.Agent) r1;
        if (r1 == 0) goto L_0x010a;
    L_0x0104:
        r1 = r1.getAvatarUri();
        r9.f753e = r1;
    L_0x010a:
        r1 = r0.getOptions();
        r1 = r1.length;
        r1 = new java.lang.String[r1];
        r9.f754f = r1;
        r1 = r4;
    L_0x0114:
        r8 = r0.getOptions();
        r8 = r8.length;
        if (r1 >= r8) goto L_0x0139;
    L_0x011b:
        r8 = r0.getOptions();
        r8 = r8[r1];
        r10 = r9.f754f;
        r11 = r8.getLabel();
        r10[r1] = r11;
        r10 = r8.isSelected();
        if (r10 == 0) goto L_0x0147;
    L_0x012f:
        r0 = new java.lang.String[r3];
        r1 = r8.getLabel();
        r0[r4] = r1;
        r9.f754f = r0;
    L_0x0139:
        r0 = r9.g;
        r7.put(r0, r9);
        r0 = r2;
        goto L_0x0097;
    L_0x0141:
        r1 = r5;
        goto L_0x00ca;
    L_0x0143:
        r1 = r5;
        goto L_0x00da;
    L_0x0145:
        r1 = r5;
        goto L_0x00f0;
    L_0x0147:
        r1 = r1 + 1;
        goto L_0x0114;
    L_0x014a:
        r1 = com.zopim.android.sdk.chatlog.aa.C0834a.CHAT_EVENT;
        r8.f744h = r1;
        r1 = r12.getResources();
        r9 = com.zopim.android.sdk.C0785R.string.chat_visitor_queue_message;
        r1 = r1.getString(r9);
        r9 = new java.lang.Object[r3];
        r0 = r0.getVisitorQueue();
        r9[r4] = r0;
        r0 = java.lang.String.format(r1, r9);
        r8.f745i = r0;
        r0 = r8.f743g;
        r7.put(r0, r8);
        r0 = r2;
        goto L_0x0097;
    L_0x016e:
        r0 = com.zopim.android.sdk.chatlog.aa.C0834a.CHAT_EVENT;
        r8.f744h = r0;
        r0 = r8.f743g;
        r7.put(r0, r8);
        r0 = r2;
        goto L_0x0097;
    L_0x017a:
        r1 = com.zopim.android.sdk.api.ZopimChat.getDataSource();
        r1 = r1.getAgents();
        r1 = r1.keySet();
        r9 = r1.iterator();
    L_0x018a:
        r1 = r9.hasNext();
        if (r1 == 0) goto L_0x01c3;
    L_0x0190:
        r1 = r9.next();
        r1 = (java.lang.String) r1;
        r10 = r8.f747k;
        r1 = r1.equals(r10);
        if (r1 == 0) goto L_0x018a;
    L_0x019e:
        if (r2 == 0) goto L_0x01a2;
    L_0x01a0:
        r2 = r4;
        goto L_0x018a;
    L_0x01a2:
        r1 = com.zopim.android.sdk.chatlog.aa.C0834a.MEMBER_EVENT;
        r8.f744h = r1;
        r1 = r12.getResources();
        r9 = com.zopim.android.sdk.C0785R.string.chat_agent_joined_message;
        r1 = r1.getString(r9);
        r9 = new java.lang.Object[r3];
        r0 = r0.getDisplayName();
        r9[r4] = r0;
        r0 = java.lang.String.format(r1, r9);
        r8.f745i = r0;
        r0 = r8.f743g;
        r7.put(r0, r8);
    L_0x01c3:
        r0 = r2;
        goto L_0x0097;
    L_0x01c6:
        r1 = com.zopim.android.sdk.api.ZopimChat.getDataSource();
        r1 = r1.getAgents();
        r1 = r1.keySet();
        r9 = r1.iterator();
    L_0x01d6:
        r1 = r9.hasNext();
        if (r1 == 0) goto L_0x020b;
    L_0x01dc:
        r1 = r9.next();
        r1 = (java.lang.String) r1;
        r10 = r8.f747k;
        r1 = r1.equals(r10);
        if (r1 == 0) goto L_0x01d6;
    L_0x01ea:
        r1 = com.zopim.android.sdk.chatlog.aa.C0834a.MEMBER_EVENT;
        r8.f744h = r1;
        r1 = r12.getResources();
        r9 = com.zopim.android.sdk.C0785R.string.chat_agent_left_message;
        r1 = r1.getString(r9);
        r9 = new java.lang.Object[r3];
        r0 = r0.getDisplayName();
        r9[r4] = r0;
        r0 = java.lang.String.format(r1, r9);
        r8.f745i = r0;
        r0 = r8.f743g;
        r7.put(r0, r8);
    L_0x020b:
        r0 = r2;
        goto L_0x0097;
    L_0x020e:
        r1 = com.zopim.android.sdk.chatlog.aq.f791a;
        r9 = r0.getError();
        r9 = r9.ordinal();
        r1 = r1[r9];
        switch(r1) {
            case 1: goto L_0x021d;
            case 2: goto L_0x024a;
            case 3: goto L_0x026d;
            default: goto L_0x021d;
        };
    L_0x021d:
        r1 = new com.zopim.android.sdk.chatlog.ab;
        r1.<init>(r8);
        r8 = r0.getUploadUrl();
        r1.f766b = r8;
        r8 = r0.getFile();
        r1.f765a = r8;
        r8 = r0.getProgress();
        r1.f767c = r8;
        r0 = r0.getError();
        r0 = r0.getValue();
        r1.f768d = r0;
        r0 = r1.f766b;
        if (r0 == 0) goto L_0x0096;
    L_0x0242:
        r0 = r1.g;
        r7.put(r0, r1);
        r0 = r2;
        goto L_0x0097;
    L_0x024a:
        r1 = r12.mAttachmentErrorItems;
        r9 = r0.getFile();
        r1 = r1.contains(r9);
        if (r1 != 0) goto L_0x021d;
    L_0x0256:
        r1 = r12.getActivity();
        r9 = com.zopim.android.sdk.C0785R.string.attachment_upload_size_limit_error_message;
        r1 = android.widget.Toast.makeText(r1, r9, r3);
        r1.show();
        r1 = r12.mAttachmentErrorItems;
        r9 = r0.getFile();
        r1.add(r9);
        goto L_0x021d;
    L_0x026d:
        r1 = r12.mAttachmentErrorItems;
        r9 = r0.getFile();
        r1 = r1.contains(r9);
        if (r1 != 0) goto L_0x021d;
    L_0x0279:
        r1 = r12.getActivity();
        r9 = com.zopim.android.sdk.C0785R.string.attachment_upload_type_error_message;
        r1 = android.widget.Toast.makeText(r1, r9, r3);
        r1.show();
        r1 = r12.mAttachmentErrorItems;
        r9 = r0.getFile();
        r1.add(r9);
        goto L_0x021d;
    L_0x0290:
        r1 = new com.zopim.android.sdk.chatlog.t;
        r1.<init>(r8);
        r8 = r0.getRating();
        r1.f831a = r8;
        r0 = r0.getComment();
        r1.f832b = r0;
        r0 = r1.g;
        r7.put(r0, r1);
        r0 = r2;
        goto L_0x0097;
    L_0x02a9:
        r0 = r12.getListAdapter();
        r0 = (com.zopim.android.sdk.chatlog.C0842i) r0;
        r6 = r4;
    L_0x02b0:
        r1 = r0.getItemCount();
        if (r6 >= r1) goto L_0x038b;
    L_0x02b6:
        r2 = r0.m692b(r6);
        r1 = r2.f743g;
        if (r1 != 0) goto L_0x02e5;
    L_0x02be:
        r4 = r5;
    L_0x02bf:
        if (r4 != 0) goto L_0x02ef;
    L_0x02c1:
        r1 = LOG_TAG;
        r3 = new java.lang.StringBuilder;
        r3.<init>();
        r4 = "Removed row item ";
        r3 = r3.append(r4);
        r2 = r2.f744h;
        r2 = r3.append(r2);
        r2 = r2.toString();
        com.zopim.android.sdk.api.Logger.m564v(r1, r2);
        r0.m689a(r6);
        r0.notifyItemChanged(r6);
    L_0x02e1:
        r4 = r6 + 1;
        r6 = r4;
        goto L_0x02b0;
    L_0x02e5:
        r1 = r2.f743g;
        r1 = r7.get(r1);
        r1 = (com.zopim.android.sdk.chatlog.aa) r1;
        r4 = r1;
        goto L_0x02bf;
    L_0x02ef:
        r1 = r2 instanceof com.zopim.android.sdk.chatlog.ab;
        if (r1 == 0) goto L_0x0321;
    L_0x02f3:
        r1 = r4 instanceof com.zopim.android.sdk.chatlog.ab;
        if (r1 == 0) goto L_0x0321;
    L_0x02f7:
        r1 = r2;
        r1 = (com.zopim.android.sdk.chatlog.ab) r1;
        r3 = r4;
        r3 = (com.zopim.android.sdk.chatlog.ab) r3;
        r8 = r1.equals(r3);
        if (r8 != 0) goto L_0x0321;
    L_0x0303:
        r1.m674a(r3);
        r3 = LOG_TAG;
        r8 = new java.lang.StringBuilder;
        r8.<init>();
        r9 = "Update VisitorItem ";
        r8 = r8.append(r9);
        r1 = r8.append(r1);
        r1 = r1.toString();
        com.zopim.android.sdk.api.Logger.m564v(r3, r1);
        r0.notifyItemChanged(r6);
    L_0x0321:
        r1 = r2 instanceof com.zopim.android.sdk.chatlog.C0833a;
        if (r1 == 0) goto L_0x0353;
    L_0x0325:
        r1 = r4 instanceof com.zopim.android.sdk.chatlog.C0833a;
        if (r1 == 0) goto L_0x0353;
    L_0x0329:
        r1 = r2;
        r1 = (com.zopim.android.sdk.chatlog.C0833a) r1;
        r3 = r4;
        r3 = (com.zopim.android.sdk.chatlog.C0833a) r3;
        r8 = r1.equals(r3);
        if (r8 != 0) goto L_0x0353;
    L_0x0335:
        r1.m669a(r3);
        r3 = LOG_TAG;
        r8 = new java.lang.StringBuilder;
        r8.<init>();
        r9 = "Update AgentItem ";
        r8 = r8.append(r9);
        r1 = r8.append(r1);
        r1 = r1.toString();
        com.zopim.android.sdk.api.Logger.m564v(r3, r1);
        r0.notifyItemChanged(r6);
    L_0x0353:
        r1 = r2 instanceof com.zopim.android.sdk.chatlog.C0853t;
        if (r1 == 0) goto L_0x0384;
    L_0x0357:
        r1 = r4 instanceof com.zopim.android.sdk.chatlog.C0853t;
        if (r1 == 0) goto L_0x0384;
    L_0x035b:
        r2 = (com.zopim.android.sdk.chatlog.C0853t) r2;
        r1 = r4;
        r1 = (com.zopim.android.sdk.chatlog.C0853t) r1;
        r3 = r2.equals(r1);
        if (r3 != 0) goto L_0x0384;
    L_0x0366:
        r2.m695a(r1);
        r1 = LOG_TAG;
        r3 = new java.lang.StringBuilder;
        r3.<init>();
        r8 = "Update ChatRatingItem ";
        r3 = r3.append(r8);
        r2 = r3.append(r2);
        r2 = r2.toString();
        com.zopim.android.sdk.api.Logger.m564v(r1, r2);
        r0.notifyItemChanged(r6);
    L_0x0384:
        r1 = r4.f743g;
        r7.remove(r1);
        goto L_0x02e1;
    L_0x038b:
        r1 = r7.values();
        r2 = r1.iterator();
    L_0x0393:
        r1 = r2.hasNext();
        if (r1 == 0) goto L_0x0025;
    L_0x0399:
        r1 = r2.next();
        r1 = (com.zopim.android.sdk.chatlog.aa) r1;
        r3 = LOG_TAG;
        r4 = new java.lang.StringBuilder;
        r4.<init>();
        r5 = "Added RowItem ";
        r4 = r4.append(r5);
        r4 = r4.append(r1);
        r4 = r4.toString();
        com.zopim.android.sdk.api.Logger.m564v(r3, r4);
        r0.m691a(r1);
        r1 = r0.getItemCount();
        r0.notifyItemChanged(r1);
        r1 = LOG_TAG;
        r3 = "Auto-scroll";
        com.zopim.android.sdk.api.Logger.m564v(r1, r3);
        r1 = r12.mRecyclerView;
        r1 = r1.getLayoutManager();
        r3 = r12.getListAdapter();
        r3 = r3.getItemCount();
        r3 = r3 + -1;
        r1.scrollToPosition(r3);
        goto L_0x0393;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.chatlog.ZopimChatLogFragment.updateChatLogAdapter(java.util.LinkedHashMap):void");
    }

    public void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        ImagePicker.INSTANCE.getFilesFromActivityOnResult(getActivity(), i, i2, intent, new at(this));
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
            if (!(this.mSendButton == null || this.mSendButton.isEnabled() || this.mInputField.getText().length() <= 0)) {
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
            Fragment connectionToastFragment = new ConnectionToastFragment();
            Fragment connectionFragment = new ConnectionFragment();
            FragmentTransaction beginTransaction = getChildFragmentManager().beginTransaction();
            beginTransaction.add(C0785R.id.toast_fragment_container, connectionToastFragment, ConnectionToastFragment.class.getName());
            beginTransaction.add(connectionFragment, ConnectionFragment.class.getName());
            beginTransaction.commit();
        }
        this.mInputManager = (InputMethodManager) getActivity().getSystemService("input_method");
    }

    public void onCreateOptionsMenu(Menu menu, MenuInflater menuInflater) {
        super.onCreateOptionsMenu(menu, menuInflater);
        menuInflater.inflate(C0785R.menu.chat_log_menu, menu);
    }

    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        View inflate = layoutInflater.inflate(C0785R.layout.zopim_chat_log_fragment, viewGroup, false);
        this.mRecyclerView = (RecyclerView) inflate.findViewById(C0785R.id.recycler_view);
        this.mRecyclerView.setLayoutManager(new LinearLayoutManager(getActivity(), 1, false));
        this.mChatLogAdapter = new C0842i(getActivity(), new ArrayList());
        this.mChatLogAdapter.m690a(this.mChat);
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
        } else if (C0785R.id.end_chat != itemId) {
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
        Object obj = 1;
        super.onPause();
        hideKeyboard(this.mInputField);
        Object obj2 = !this.mChat.hasEnded() ? 1 : null;
        if (VERSION.SDK_INT >= 11) {
            if (obj2 == null || getActivity().isChangingConfigurations()) {
                obj = null;
            }
        } else if (obj2 == null || !getActivity().isFinishing()) {
            obj = null;
        }
        if (obj != null) {
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
            Logger.m564v(LOG_TAG, "Resuming expired chat. Disable all input elements.");
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
        this.mInputField = (EditText) view.findViewById(C0785R.id.input_field);
        this.mSendButton = (ImageButton) view.findViewById(C0785R.id.send_button);
        this.mAttachButton = (ImageButton) view.findViewById(C0785R.id.attach_button);
        this.mInputField.addTextChangedListener(new ag(this));
        this.mSendButton.setOnClickListener(new ar(this));
        this.mAttachButton.setOnClickListener(new as(this));
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
            z = bundle.getBoolean(STATE_SHOW_RECONNECT_TIMEOUT_DIALOG, false);
            boolean z2 = bundle.getBoolean(STATE_SHOW_CHAT_END_CONFIRM_DIALOG, false);
            boolean z3 = bundle.getBoolean(STATE_SHOW_EMAIL_TRANSCRIPT_DIALOG, false);
            if (z) {
                this.mHandler.post(this.mShowReconnectFailed);
            }
            if (z2) {
                showConfirmDialog();
            }
            if (z3) {
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
