package com.zopim.android.sdk.prechat;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentTransaction;
import android.util.Log;
import android.util.Patterns;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.ChatConfig;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.chatlog.ConnectionFragment;
import com.zopim.android.sdk.chatlog.ConnectionFragment.ConnectionListener;
import com.zopim.android.sdk.chatlog.ConnectionToastFragment;
import com.zopim.android.sdk.chatlog.ZopimChatLogFragment;
import com.zopim.android.sdk.data.LivechatDepartmentsPath;
import com.zopim.android.sdk.model.Department;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm.Field;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collection;

public class ZopimPreChatFragment extends Fragment implements ConnectionListener {
    private static final String EXTRA_PRE_CHAT_CONFIG = "PRE_CHAT_CONFIG";
    private static final String LOG_TAG = ZopimPreChatFragment.class.getSimpleName();
    private static final String STATE_MENU_ITEM_ENABLED = "MENU_ITEM_ENABLED";
    private Chat mChat;
    private ChatListener mChatListener;
    /* access modifiers changed from: private */
    public Spinner mDepartmentSpinner;
    private EditText mEmailEdit;
    private Handler mHandler = new Handler(Looper.getMainLooper());
    private Menu mMenu;
    private EditText mMessageEdit;
    private EditText mNameEdit;
    private EditText mPhoneNumberEdit;
    private PreChatForm mPreChatForm;
    private boolean mStateMenuItemEnabled = true;

    private void close() {
        FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
        beginTransaction.remove(this);
        beginTransaction.commit();
    }

    public static ZopimPreChatFragment newInstance(PreChatForm preChatForm) {
        if (preChatForm == null) {
            Log.e(LOG_TAG, "Pre chat form must not be null. Will use default pre chat form.");
            return new ZopimPreChatFragment();
        }
        Bundle bundle = new Bundle();
        bundle.putSerializable(EXTRA_PRE_CHAT_CONFIG, preChatForm);
        ZopimPreChatFragment zopimPreChatFragment = new ZopimPreChatFragment();
        zopimPreChatFragment.setArguments(bundle);
        return zopimPreChatFragment;
    }

    private boolean safeIsEmpty(String str) {
        return str == null || str.isEmpty();
    }

    private void setupVisitorField(Field field, EditText editText, String str) {
        switch (C1266s.f958a[field.ordinal()]) {
            case 1:
                editText.setVisibility(8);
                return;
            case 2:
                if (!safeIsEmpty(str)) {
                    editText.setVisibility(8);
                    return;
                }
                return;
            case 3:
                if (!safeIsEmpty(str)) {
                    editText.setText(str);
                    return;
                }
                return;
            case 4:
                if (!safeIsEmpty(str)) {
                    editText.setText(str);
                    break;
                }
                break;
            case 5:
                break;
            default:
                Logger.m579w(LOG_TAG, "Unknown pre chat forn config type.");
                return;
        }
        if (!safeIsEmpty(str)) {
            editText.setVisibility(8);
            return;
        }
        editText.setHint(String.format(getResources().getString(C1122R.string.required_field_template), new Object[]{editText.getHint()}));
    }

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (activity instanceof ChatListener) {
            this.mChatListener = (ChatListener) activity;
        }
    }

    public void onConnected() {
        if (this.mMenu != null) {
            MenuItem findItem = this.mMenu.findItem(C1122R.C1125id.start_chat);
            if (findItem != null && !findItem.isEnabled()) {
                findItem.setEnabled(true);
            }
        }
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setHasOptionsMenu(true);
        this.mChat = ZopimChat.resume(getActivity());
        if (getArguments() != null) {
            Serializable serializable = getArguments().getSerializable(EXTRA_PRE_CHAT_CONFIG);
            if (serializable instanceof PreChatForm) {
                this.mPreChatForm = (PreChatForm) serializable;
            }
        }
        if (bundle == null) {
            ConnectionToastFragment connectionToastFragment = new ConnectionToastFragment();
            ConnectionFragment connectionFragment = new ConnectionFragment();
            FragmentTransaction beginTransaction = getChildFragmentManager().beginTransaction();
            beginTransaction.add(C1122R.C1125id.toast_fragment_container, connectionToastFragment, ConnectionToastFragment.class.getName());
            beginTransaction.add((Fragment) connectionFragment, ConnectionFragment.class.getName());
            beginTransaction.commit();
        }
    }

    public void onCreateOptionsMenu(Menu menu, MenuInflater menuInflater) {
        super.onCreateOptionsMenu(menu, menuInflater);
        menuInflater.inflate(C1122R.menu.pre_chat_menu, menu);
        menu.findItem(C1122R.C1125id.start_chat).setEnabled(this.mStateMenuItemEnabled);
        this.mMenu = menu;
    }

    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        return layoutInflater.inflate(C1122R.C1126layout.zopim_pre_chat_fragment, viewGroup, false);
    }

    public void onDisconnected() {
        if (this.mMenu != null) {
            MenuItem findItem = this.mMenu.findItem(C1122R.C1125id.start_chat);
            if (findItem != null && findItem.isEnabled()) {
                findItem.setEnabled(false);
            }
        }
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        boolean z;
        if (16908332 == menuItem.getItemId()) {
            close();
        }
        if (C1122R.C1125id.start_chat != menuItem.getItemId()) {
            return super.onOptionsItemSelected(menuItem);
        }
        if (this.mDepartmentSpinner.getVisibility() != 0 || ((!Field.REQUIRED.equals(this.mPreChatForm.getDepartment()) && !Field.REQUIRED_EDITABLE.equals(this.mPreChatForm.getDepartment())) || this.mDepartmentSpinner.getSelectedItemPosition() <= this.mDepartmentSpinner.getCount() - 1)) {
            z = true;
        } else {
            TextView textView = (TextView) this.mDepartmentSpinner.getSelectedView();
            textView.setError(getResources().getText(C1122R.string.pre_chat_departments_error_message));
            textView.setText(C1122R.string.pre_chat_departments_error_hint);
            z = false;
        }
        if (this.mEmailEdit.getVisibility() == 0) {
            String trim = this.mEmailEdit.getText().toString().trim();
            switch (C1266s.f958a[this.mPreChatForm.getEmail().ordinal()]) {
                case 2:
                case 3:
                    if (!trim.isEmpty() && !Patterns.EMAIL_ADDRESS.matcher(trim).matches()) {
                        this.mEmailEdit.setError(getResources().getString(C1122R.string.pre_chat_email_error_message));
                        this.mEmailEdit.setHint(C1122R.string.pre_chat_email_error_hint);
                        z = false;
                        break;
                    }
                case 4:
                case 5:
                    if (!Patterns.EMAIL_ADDRESS.matcher(trim).matches()) {
                        this.mEmailEdit.setError(getResources().getString(C1122R.string.pre_chat_email_error_message));
                        this.mEmailEdit.setHint(C1122R.string.pre_chat_email_error_hint);
                        z = false;
                        break;
                    }
                    break;
            }
        }
        if (this.mNameEdit.getVisibility() == 0 && ((Field.REQUIRED.equals(this.mPreChatForm.getName()) || Field.REQUIRED_EDITABLE.equals(this.mPreChatForm.getName())) && this.mNameEdit.getText().toString().trim().isEmpty())) {
            this.mNameEdit.setError(getResources().getString(C1122R.string.pre_chat_name_error_message));
            this.mNameEdit.setHint(C1122R.string.pre_chat_name_error_hint);
            z = false;
        }
        if (this.mPhoneNumberEdit.getVisibility() == 0 && ((Field.REQUIRED.equals(this.mPreChatForm.getPhoneNumber()) || Field.REQUIRED_EDITABLE.equals(this.mPreChatForm.getPhoneNumber())) && this.mPhoneNumberEdit.getText().toString().trim().isEmpty())) {
            this.mPhoneNumberEdit.setError(getResources().getString(C1122R.string.pre_chat_phone_error_message));
            this.mPhoneNumberEdit.setHint(C1122R.string.pre_chat_phone_error_hint);
            z = false;
        }
        if (this.mMessageEdit.getVisibility() == 0 && ((Field.REQUIRED.equals(this.mPreChatForm.getMessage()) || Field.REQUIRED_EDITABLE.equals(this.mPreChatForm.getMessage())) && this.mMessageEdit.getText().toString().trim().isEmpty())) {
            this.mMessageEdit.setError(getResources().getString(C1122R.string.pre_chat_message_error_message));
            this.mMessageEdit.setHint(C1122R.string.pre_chat_message_error_hint);
            z = false;
        }
        if (z) {
            String trim2 = this.mNameEdit.getText().toString().trim();
            String trim3 = this.mEmailEdit.getText().toString().trim();
            String trim4 = this.mPhoneNumberEdit.getText().toString().trim();
            String str = (String) this.mDepartmentSpinner.getSelectedItem();
            String trim5 = this.mMessageEdit.getText().toString().trim();
            if (!trim2.isEmpty()) {
                this.mChat.setName(trim2);
            }
            if (!trim3.isEmpty()) {
                this.mChat.setEmail(trim3);
            }
            if (!trim4.isEmpty()) {
                this.mChat.setPhoneNumber(trim4);
            }
            if (str != null && !str.isEmpty()) {
                this.mChat.setDepartment(str);
            }
            if (!trim5.isEmpty()) {
                this.mChat.send(trim5);
            } else {
                this.mChat.send(" ");
            }
            ZopimChatLogFragment zopimChatLogFragment = new ZopimChatLogFragment();
            FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
            beginTransaction.replace(C1122R.C1125id.chat_fragment_container, zopimChatLogFragment, ZopimChatLogFragment.class.getName());
            beginTransaction.remove(this);
            beginTransaction.commit();
            return true;
        }
        Toast.makeText(getActivity(), C1122R.string.pre_chat_validation_error_message, 1).show();
        return true;
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putBoolean("MENU_ITEM_ENABLED", this.mMenu.findItem(C1122R.C1125id.start_chat).isEnabled());
    }

    public void onStop() {
        super.onStop();
        this.mHandler.removeCallbacksAndMessages(null);
        if (getActivity().isFinishing()) {
            Logger.m575i(LOG_TAG, "Chat aborted. Ending chat.");
            this.mChat.endChat();
            if (this.mChatListener != null) {
                this.mChatListener.onChatEnded();
            }
        }
    }

    public void onViewCreated(View view, @Nullable Bundle bundle) {
        String str = null;
        super.onViewCreated(view, bundle);
        VisitorInfo visitorInfo = this.mChat.getConfig().getVisitorInfo();
        this.mNameEdit = (EditText) view.findViewById(C1122R.C1125id.name);
        this.mEmailEdit = (EditText) view.findViewById(C1122R.C1125id.email);
        this.mPhoneNumberEdit = (EditText) view.findViewById(C1122R.C1125id.phoneNumber);
        this.mDepartmentSpinner = (Spinner) view.findViewById(C1122R.C1125id.departments);
        this.mMessageEdit = (EditText) view.findViewById(C1122R.C1125id.message);
        switch (C1266s.f958a[this.mPreChatForm.getDepartment().ordinal()]) {
            case 1:
                this.mDepartmentSpinner.setVisibility(8);
                break;
            default:
                Collection<Department> values = ZopimChat.getDataSource().getDepartments().values();
                if (values != null && !values.isEmpty()) {
                    ArrayList arrayList = new ArrayList();
                    for (Department name : values) {
                        arrayList.add(name.getName());
                    }
                    String string = getResources().getString(C1122R.string.pre_chat_departments_hint);
                    if (Field.REQUIRED.equals(this.mPreChatForm.getDepartment()) || Field.REQUIRED_EDITABLE.equals(this.mPreChatForm.getDepartment())) {
                        string = String.format(getResources().getString(C1122R.string.required_field_template), new Object[]{string});
                    }
                    arrayList.add(string);
                    C1264q qVar = new C1264q(this, getActivity(), C1122R.C1126layout.spinner_list_item, arrayList);
                    qVar.setDropDownViewResource(C1122R.C1126layout.support_simple_spinner_dropdown_item);
                    this.mDepartmentSpinner.setAdapter(qVar);
                    this.mDepartmentSpinner.setSelection(arrayList.size() - 1);
                    this.mDepartmentSpinner.setOnItemSelectedListener(new C1265r(this));
                    break;
                } else {
                    this.mDepartmentSpinner.setVisibility(8);
                    break;
                }
        }
        setupVisitorField(this.mPreChatForm.getName(), this.mNameEdit, visitorInfo.getName());
        setupVisitorField(this.mPreChatForm.getEmail(), this.mEmailEdit, visitorInfo.getEmail());
        setupVisitorField(this.mPreChatForm.getPhoneNumber(), this.mPhoneNumberEdit, visitorInfo.getPhoneNumber());
        setupVisitorField(this.mPreChatForm.getMessage(), this.mMessageEdit, null);
        ChatConfig config = this.mChat.getConfig();
        if (config != null) {
            str = config.getDepartment();
        }
        if (str != null) {
            for (Department department : LivechatDepartmentsPath.getInstance().getData().values()) {
                if (department != null && str.equals(department.getName())) {
                    this.mDepartmentSpinner.setVisibility(8);
                    return;
                }
            }
        }
    }

    public void onViewStateRestored(@Nullable Bundle bundle) {
        super.onViewStateRestored(bundle);
        if (bundle != null) {
            this.mStateMenuItemEnabled = bundle.getBoolean("MENU_ITEM_ENABLED", true);
        }
    }
}
