package com.zopim.android.sdk.attachment.p016ui;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.p000v4.app.DialogFragment;
import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentActivity;
import android.support.p000v4.app.FragmentManager;
import android.support.p000v4.content.ContextCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.attachment.ImagePicker;
import java.util.ArrayList;
import java.util.List;

/* renamed from: com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog */
public class AttachmentSourceSelectorDialog extends DialogFragment {
    private static final String TAG = AttachmentSourceSelectorDialog.class.getSimpleName();
    private ListView mListView;

    /* renamed from: com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog$a */
    enum C1171a {
        GALLERY,
        CAMERA
    }

    /* renamed from: com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog$b */
    class C1172b extends ArrayAdapter<C1173c> {

        /* renamed from: b */
        private Context f739b;

        C1172b(Context context, int i, List<C1173c> list) {
            super(context, i, list);
            this.f739b = context;
        }

        public View getView(int i, View view, ViewGroup viewGroup) {
            C1173c cVar = (C1173c) getItem(i);
            if (view == null) {
                view = LayoutInflater.from(this.f739b).inflate(C1122R.C1126layout.row_attachment_source_selector, viewGroup, false);
            }
            ((ImageView) view.findViewById(C1122R.C1125id.attachment_selector_image)).setImageDrawable(ContextCompat.getDrawable(this.f739b, cVar.mo20701a()));
            ((TextView) view.findViewById(C1122R.C1125id.attachment_selector_text)).setText(cVar.mo20702b());
            return view;
        }
    }

    /* renamed from: com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog$c */
    class C1173c {

        /* renamed from: b */
        private final int f741b;

        /* renamed from: c */
        private final String f742c;

        /* renamed from: d */
        private final C1171a f743d;

        C1173c(int i, String str, C1171a aVar) {
            this.f741b = i;
            this.f742c = str;
            this.f743d = aVar;
        }

        /* renamed from: a */
        public int mo20701a() {
            return this.f741b;
        }

        /* renamed from: b */
        public String mo20702b() {
            return this.f742c;
        }

        /* renamed from: c */
        public C1171a mo20703c() {
            return this.f743d;
        }
    }

    private void fillListView(Fragment fragment) {
        ArrayList arrayList = new ArrayList();
        FragmentActivity activity = fragment.getActivity();
        if (ImagePicker.INSTANCE.hasPermissionForCamera(activity)) {
            arrayList.add(new C1173c(C1122R.C1124drawable.ic_chat_action_camera, getString(C1122R.string.attachment_upload_source_camera_button), C1171a.CAMERA));
        }
        if (ImagePicker.INSTANCE.hasPermissionForGallery(activity)) {
            arrayList.add(new C1173c(C1122R.C1124drawable.ic_chat_action_picture, getString(C1122R.string.attachment_upload_source_gallery_button), C1171a.GALLERY));
        }
        if (arrayList.size() < 1) {
            Logger.m571d(TAG, "No permissions for opening images, dismiss dialog");
            dismiss();
        }
        this.mListView.setAdapter(new C1172b(activity, C1122R.C1126layout.row_attachment_source_selector, arrayList));
        this.mListView.setOnItemClickListener(new C1174a(this, fragment));
    }

    public static void showDialog(FragmentManager fragmentManager) {
        new AttachmentSourceSelectorDialog().show(fragmentManager.beginTransaction(), TAG);
    }

    public void onActivityCreated(Bundle bundle) {
        super.onActivityCreated(bundle);
        fillListView(getParentFragment());
    }

    public void onCreate(@Nullable Bundle bundle) {
        super.onCreate(bundle);
        setStyle(1, getTheme());
    }

    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        View inflate = layoutInflater.inflate(C1122R.C1126layout.fragment_dialog_attachment_source, viewGroup, false);
        this.mListView = (ListView) inflate.findViewById(C1122R.C1125id.dialog_attachment_source_listview);
        return inflate;
    }
}
