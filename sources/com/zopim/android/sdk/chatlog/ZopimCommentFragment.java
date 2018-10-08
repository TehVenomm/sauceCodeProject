package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.chatlog.ConnectionFragment.ConnectionListener;
import com.zopim.android.sdk.widget.ChatWidgetService;

public class ZopimCommentFragment extends Fragment implements ConnectionListener {
    private static final String EXTRA_COMMENT = "COMMENT";
    private static final String LOG_TAG = ZopimCommentFragment.class.getSimpleName();
    private static final String STATE_MENU_ITEM_ENABLED = "MENU_ITEM_ENABLED";
    private static final String STATE_NO_CONNECTION = "NO_CONNECTION";
    private Chat mChat;
    private EditText mCommentEditor;
    private Menu mMenu;
    private boolean mNoConnection;
    private boolean mStateMenuItemEnabled = true;

    public static ZopimCommentFragment newInstance(String str) {
        ZopimCommentFragment zopimCommentFragment = new ZopimCommentFragment();
        Bundle bundle = new Bundle();
        bundle.putSerializable("COMMENT", str);
        zopimCommentFragment.setArguments(bundle);
        return zopimCommentFragment;
    }

    public void onActivityCreated(@Nullable Bundle bundle) {
        super.onActivityCreated(bundle);
        if (getArguments() != null && getArguments().containsKey("COMMENT")) {
            this.mCommentEditor.setText((String) getArguments().getSerializable("COMMENT"));
        }
    }

    public void onConnected() {
        this.mNoConnection = false;
        if (this.mMenu != null) {
            MenuItem findItem = this.mMenu.findItem(C0784R.id.send_comment);
            if (findItem != null && !findItem.isEnabled()) {
                findItem.setEnabled(true);
            }
        }
    }

    public void onCreate(@Nullable Bundle bundle) {
        super.onCreate(bundle);
        this.mChat = ZopimChat.resume(getActivity());
        if (bundle == null) {
            Fragment connectionToastFragment = new ConnectionToastFragment();
            Fragment connectionFragment = new ConnectionFragment();
            FragmentTransaction beginTransaction = getChildFragmentManager().beginTransaction();
            beginTransaction.add(C0784R.id.toast_fragment_container, connectionToastFragment, ConnectionToastFragment.class.getName());
            beginTransaction.add(connectionFragment, ConnectionFragment.class.getName());
            beginTransaction.commit();
        }
    }

    public void onCreateOptionsMenu(Menu menu, MenuInflater menuInflater) {
        super.onCreateOptionsMenu(menu, menuInflater);
        menuInflater.inflate(C0784R.menu.chat_comment_menu, menu);
        menu.findItem(C0784R.id.send_comment).setEnabled(this.mStateMenuItemEnabled);
        this.mMenu = menu;
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        return layoutInflater.inflate(C0784R.layout.zopim_comment_fragment, viewGroup, false);
    }

    public void onDisconnected() {
        this.mNoConnection = true;
        if (this.mMenu != null) {
            MenuItem findItem = this.mMenu.findItem(C0784R.id.send_comment);
            if (findItem != null && findItem.isEnabled()) {
                findItem.setEnabled(false);
            }
        }
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        if (C0784R.id.send_comment != menuItem.getItemId()) {
            return super.onOptionsItemSelected(menuItem);
        }
        if (!this.mChat.hasEnded()) {
            this.mChat.sendChatComment(this.mCommentEditor.getText().toString().trim());
        }
        return true;
    }

    public void onPause() {
        Object obj = 1;
        super.onPause();
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
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putBoolean(STATE_NO_CONNECTION, this.mNoConnection);
        bundle.putBoolean("MENU_ITEM_ENABLED", this.mMenu.findItem(C0784R.id.send_comment).isEnabled());
    }

    public void onViewCreated(View view, @Nullable Bundle bundle) {
        super.onViewCreated(view, bundle);
        setHasOptionsMenu(true);
        this.mCommentEditor = (EditText) view.findViewById(C0784R.id.comment_editor);
    }

    public void onViewStateRestored(@Nullable Bundle bundle) {
        super.onViewStateRestored(bundle);
        if (bundle != null) {
            this.mNoConnection = bundle.getBoolean(STATE_NO_CONNECTION, false);
            this.mStateMenuItemEnabled = bundle.getBoolean("MENU_ITEM_ENABLED", true);
        }
    }
}
