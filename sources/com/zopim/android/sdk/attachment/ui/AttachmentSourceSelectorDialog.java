package com.zopim.android.sdk.attachment.ui;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.content.ContextCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import com.zopim.android.sdk.C0785R;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.attachment.ImagePicker;
import java.util.ArrayList;
import java.util.List;

public class AttachmentSourceSelectorDialog extends DialogFragment {
    private static final String TAG = AttachmentSourceSelectorDialog.class.getSimpleName();
    private ListView mListView;

    /* renamed from: com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog$a */
    enum C0827a {
        GALLERY,
        CAMERA
    }

    /* renamed from: com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog$b */
    class C0828b extends ArrayAdapter<C0829c> {
        /* renamed from: a */
        final /* synthetic */ AttachmentSourceSelectorDialog f694a;
        /* renamed from: b */
        private Context f695b;

        C0828b(AttachmentSourceSelectorDialog attachmentSourceSelectorDialog, Context context, int i, List<C0829c> list) {
            this.f694a = attachmentSourceSelectorDialog;
            super(context, i, list);
            this.f695b = context;
        }

        public View getView(int i, View view, ViewGroup viewGroup) {
            C0829c c0829c = (C0829c) getItem(i);
            if (view == null) {
                view = LayoutInflater.from(this.f695b).inflate(C0785R.layout.row_attachment_source_selector, viewGroup, false);
            }
            ((ImageView) view.findViewById(C0785R.id.attachment_selector_image)).setImageDrawable(ContextCompat.getDrawable(this.f695b, c0829c.m644a()));
            ((TextView) view.findViewById(C0785R.id.attachment_selector_text)).setText(c0829c.m645b());
            return view;
        }
    }

    /* renamed from: com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog$c */
    class C0829c {
        /* renamed from: a */
        final /* synthetic */ AttachmentSourceSelectorDialog f696a;
        /* renamed from: b */
        private final int f697b;
        /* renamed from: c */
        private final String f698c;
        /* renamed from: d */
        private final C0827a f699d;

        C0829c(AttachmentSourceSelectorDialog attachmentSourceSelectorDialog, int i, String str, C0827a c0827a) {
            this.f696a = attachmentSourceSelectorDialog;
            this.f697b = i;
            this.f698c = str;
            this.f699d = c0827a;
        }

        /* renamed from: a */
        public int m644a() {
            return this.f697b;
        }

        /* renamed from: b */
        public String m645b() {
            return this.f698c;
        }

        /* renamed from: c */
        public C0827a m646c() {
            return this.f699d;
        }
    }

    private void fillListView(Fragment fragment) {
        List arrayList = new ArrayList();
        Context activity = fragment.getActivity();
        if (ImagePicker.INSTANCE.hasPermissionForCamera(activity)) {
            arrayList.add(new C0829c(this, C0785R.drawable.ic_chat_action_camera, getString(C0785R.string.attachment_upload_source_camera_button), C0827a.CAMERA));
        }
        if (ImagePicker.INSTANCE.hasPermissionForGallery(activity)) {
            arrayList.add(new C0829c(this, C0785R.drawable.ic_chat_action_picture, getString(C0785R.string.attachment_upload_source_gallery_button), C0827a.GALLERY));
        }
        if (arrayList.size() < 1) {
            Logger.m558d(TAG, "No permissions for opening images, dismiss dialog");
            dismiss();
        }
        this.mListView.setAdapter(new C0828b(this, activity, C0785R.layout.row_attachment_source_selector, arrayList));
        this.mListView.setOnItemClickListener(new C0830a(this, fragment));
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
        View inflate = layoutInflater.inflate(C0785R.layout.fragment_dialog_attachment_source, viewGroup, false);
        this.mListView = (ListView) inflate.findViewById(C0785R.id.dialog_attachment_source_listview);
        return inflate;
    }
}
