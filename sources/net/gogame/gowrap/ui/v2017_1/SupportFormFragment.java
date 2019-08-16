package net.gogame.gowrap.p019ui.v2017_1;

import android.app.Dialog;
import android.app.Fragment;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.util.ArrayList;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.p019ui.UIContext;
import net.gogame.gowrap.p019ui.dialog.CustomDialog;
import net.gogame.gowrap.p019ui.dialog.CustomDialog.Listener;
import net.gogame.gowrap.p019ui.dialog.CustomDialog.Type;
import net.gogame.gowrap.p019ui.utils.DisplayUtils;
import net.gogame.gowrap.support.PreferenceUtils;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.support.SupportCategory;
import net.gogame.gowrap.support.SupportManager;
import net.gogame.gowrap.support.SupportRequest;

/* renamed from: net.gogame.gowrap.ui.v2017_1.SupportFormFragment */
public class SupportFormFragment extends Fragment {
    private static final int SELECT_PICTURE_REQUEST_CODE = 5001;
    /* access modifiers changed from: private */
    public Uri attachment = null;
    /* access modifiers changed from: private */
    public TextView attachmentView = null;
    /* access modifiers changed from: private */
    public Context context = null;
    /* access modifiers changed from: private */
    public View removeAttachmentView = null;
    /* access modifiers changed from: private */
    public SupportCategory supportCategory = null;
    /* access modifiers changed from: private */
    public UIContext uiContext = null;

    /* renamed from: net.gogame.gowrap.ui.v2017_1.SupportFormFragment$SubmitSupportRequestTask */
    private class SubmitSupportRequestTask extends AsyncTask<SupportRequest, Void, Void> {
        private CustomDialog progressDialog;
        private boolean successful;

        private SubmitSupportRequestTask() {
            this.progressDialog = null;
            this.successful = false;
        }

        /* access modifiers changed from: protected */
        public void onPreExecute() {
            this.progressDialog = CustomDialog.newBuilder(SupportFormFragment.this.context).withType(Type.PROGRESS).withTitle(C1423R.string.net_gogame_gowrap_support_form_title).withMessage(C1423R.string.net_gogame_gowrap_support_form_submitting_request_message).build();
            this.progressDialog.show();
        }

        /* access modifiers changed from: protected */
        public Void doInBackground(SupportRequest... supportRequestArr) {
            if (supportRequestArr != null && supportRequestArr.length > 0) {
                try {
                    SupportManager.send(SupportFormFragment.this.context, supportRequestArr[0]);
                    this.successful = true;
                } catch (Exception e) {
                    Log.e(Constants.TAG, "Exception", e);
                    this.successful = false;
                }
            }
            return null;
        }

        /* access modifiers changed from: protected */
        public void onPostExecute(Void voidR) {
            if (this.progressDialog != null) {
                this.progressDialog.dismiss();
            }
            if (this.successful) {
                CustomDialog.newBuilder(SupportFormFragment.this.context).withType(Type.INFO).withTitle(C1423R.string.net_gogame_gowrap_support_form_title).withMessage(C1423R.string.net_gogame_gowrap_support_form_request_submitted_message).withListener(new Listener() {
                    public void onClosed() {
                        SupportFormFragment.this.getActivity().onBackPressed();
                    }
                }).build().show();
            } else {
                CustomDialog.newBuilder(SupportFormFragment.this.context).withType(Type.ALERT).withTitle(C1423R.string.net_gogame_gowrap_support_form_title).withMessage(C1423R.string.net_gogame_gowrap_support_form_request_failed_message).build().show();
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_1.SupportFormFragment$SupportCategoryWrapper */
    public static class SupportCategoryWrapper {
        private final String label;
        private final SupportCategory supportCategory;

        public SupportCategoryWrapper(SupportCategory supportCategory2, String str) {
            this.supportCategory = supportCategory2;
            this.label = str;
        }

        public SupportCategory getSupportCategory() {
            return this.supportCategory;
        }

        public String getLabel() {
            return this.label;
        }

        public String toString() {
            return this.label;
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_1.SupportFormFragment$SupportRequestCollector */
    private interface SupportRequestCollector {
        SupportRequest collect();
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_fragment_support_form, viewGroup, false);
        this.context = viewGroup.getContext();
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_back_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (SupportFormFragment.this.uiContext != null) {
                    SupportFormFragment.this.uiContext.goBack();
                }
            }
        });
        boolean isEquals = StringUtils.isEquals(CoreSupport.INSTANCE.getAppId(), "DisneyCrossyRoad-PH-Globe");
        View findViewById = inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_group_mobile_number);
        if (isEquals) {
            findViewById.setVisibility(0);
        } else {
            findViewById.setVisibility(8);
        }
        final EditText editText = (EditText) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_field_name);
        final EditText editText2 = (EditText) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_field_email);
        final EditText editText3 = (EditText) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_field_mobile_number);
        TextView textView = (TextView) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_field_category);
        final EditText editText4 = (EditText) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_field_body);
        this.attachmentView = (TextView) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_field_attachment);
        this.removeAttachmentView = inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_field_remove_attachment);
        final View findViewById2 = inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_button_send);
        String preference = PreferenceUtils.getPreference(this.context, InternalConstants.SAVED_NAME);
        String preference2 = PreferenceUtils.getPreference(this.context, InternalConstants.SAVED_EMAIL);
        String preference3 = PreferenceUtils.getPreference(this.context, InternalConstants.SAVED_MOBILE_NUMBER);
        if (preference != null) {
            editText.setText(preference);
        }
        if (preference2 != null) {
            editText2.setText(preference2);
        }
        if (preference3 != null && isEquals) {
            editText3.setText(preference3);
        }
        ArrayList arrayList = new ArrayList();
        for (SupportCategory supportCategory2 : SupportManager.getCategories()) {
            arrayList.add(new SupportCategoryWrapper(supportCategory2, this.context.getString(supportCategory2.getStringResourceId())));
        }
        ArrayAdapter arrayAdapter = new ArrayAdapter(this.context, C1423R.C1425layout.net_gogame_gowrap_default_listview_item, C1423R.C1424id.net_gogame_gowrap_text_view, arrayList);
        final C17242 r0 = new SupportRequestCollector() {
            public SupportRequest collect() {
                return new SupportRequest(StringUtils.trimToNull(editText.getText().toString()), StringUtils.trimToNull(editText2.getText().toString()), StringUtils.trimToNull(editText3.getText().toString()), SupportFormFragment.this.supportCategory, StringUtils.trimToNull(editText4.getText().toString()), SupportFormFragment.this.attachment);
            }
        };
        C17253 r1 = new TextWatcher() {
            public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
            }

            public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
            }

            public void afterTextChanged(Editable editable) {
                findViewById2.setSelected(SupportManager.isValid(r0.collect()));
            }
        };
        editText.addTextChangedListener(r1);
        editText2.addTextChangedListener(r1);
        editText4.addTextChangedListener(r1);
        View inflate2 = getActivity().getLayoutInflater().inflate(C1423R.C1425layout.net_gogame_gowrap_default_listview, null, false);
        final ListView listView = (ListView) inflate2.findViewById(C1423R.C1424id.net_gogame_gowrap_list_view);
        listView.setAdapter(arrayAdapter);
        final Dialog dialog = new Dialog(getActivity(), C1423R.style.net_gogame_gowrap_dialog);
        dialog.setCanceledOnTouchOutside(true);
        dialog.setContentView(inflate2);
        final ArrayAdapter arrayAdapter2 = arrayAdapter;
        final TextView textView2 = textView;
        final C17242 r6 = r0;
        listView.setOnItemClickListener(new OnItemClickListener() {
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
                listView.setItemChecked(i, true);
                SupportCategoryWrapper supportCategoryWrapper = (SupportCategoryWrapper) arrayAdapter2.getItem(i);
                if (supportCategoryWrapper != null) {
                    SupportFormFragment.this.supportCategory = supportCategoryWrapper.getSupportCategory();
                }
                if (SupportFormFragment.this.supportCategory != null) {
                    textView2.setText(SupportFormFragment.this.supportCategory.getStringResourceId());
                } else {
                    textView2.setText(null);
                }
                findViewById2.setSelected(SupportManager.isValid(r6.collect()));
                dialog.dismiss();
            }
        });
        textView.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                dialog.show();
            }
        });
        this.attachmentView.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent();
                intent.setType("image/*");
                intent.setAction("android.intent.action.GET_CONTENT");
                SupportFormFragment.this.startActivityForResult(Intent.createChooser(intent, SupportFormFragment.this.context.getResources().getString(C1423R.string.net_gogame_gowrap_support_form_select_picture_prompt)), 5001);
            }
        });
        this.removeAttachmentView.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                SupportFormFragment.this.attachment = null;
                SupportFormFragment.this.removeAttachmentView.setVisibility(8);
                SupportFormFragment.this.attachmentView.setText(C1423R.string.net_gogame_gowrap_support_form_attachment_no_file_caption);
            }
        });
        findViewById2.setSelected(false);
        findViewById2.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                SupportRequest collect = r0.collect();
                if (!SupportManager.isValid(collect)) {
                    findViewById2.setSelected(false);
                    CustomDialog.newBuilder(SupportFormFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1423R.string.net_gogame_gowrap_support_form_title).withMessage(C1423R.string.net_gogame_gowrap_support_form_invalid_message).withCanceledOnTouchOutside(true).build().show();
                    return;
                }
                DisplayUtils.hideSoftKeyboard(SupportFormFragment.this.getActivity());
                PreferenceUtils.setPreference(SupportFormFragment.this.context, InternalConstants.SAVED_NAME, collect.getName());
                PreferenceUtils.setPreference(SupportFormFragment.this.context, InternalConstants.SAVED_EMAIL, collect.getEmail());
                PreferenceUtils.setPreference(SupportFormFragment.this.context, InternalConstants.SAVED_MOBILE_NUMBER, collect.getMobileNumber());
                new SubmitSupportRequestTask().execute(new SupportRequest[]{collect});
            }
        });
        return inflate;
    }

    public void onActivityResult(int i, int i2, Intent intent) {
        if (i == 5001 && i2 == -1 && intent != null) {
            this.attachment = intent.getData();
            this.attachmentView.setText(getFilename(this.attachment));
            this.removeAttachmentView.setVisibility(0);
        }
        super.onActivityResult(i, i2, intent);
    }

    private String getFilename(Uri uri) {
        Cursor cursor = null;
        String scheme = uri.getScheme();
        if (scheme.equals("file")) {
            return uri.getLastPathSegment();
        }
        if (scheme.equals(Param.CONTENT)) {
            try {
                Cursor query = getActivity().getContentResolver().query(uri, new String[]{"_display_name"}, null, null, null);
                if (query != null) {
                    try {
                        if (query.getCount() != 0) {
                            query.moveToFirst();
                            String string = query.getString(query.getColumnIndexOrThrow("_display_name"));
                            if (string != null) {
                                if (query == null) {
                                    return string;
                                }
                                query.close();
                                return string;
                            }
                        }
                    } catch (Throwable th) {
                        th = th;
                        cursor = query;
                        if (cursor != null) {
                            cursor.close();
                        }
                        throw th;
                    }
                }
                if (query != null) {
                    query.close();
                }
            } catch (Throwable th2) {
                th = th2;
            }
        }
        return null;
    }
}
