package net.gogame.gowrap.ui.dialog;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.PopupWindow;
import android.widget.TextView;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.ui.common.C1451R;

public class CustomDialog {
    private boolean canceledOnTouchOutside;
    private Listener listener;
    private String message;
    private final PopupWindow popupWindow = new PopupWindow(this.view, -1, -1);
    private String title;
    private Type type;
    private final View view;

    /* renamed from: net.gogame.gowrap.ui.dialog.CustomDialog$1 */
    class C14681 implements OnClickListener {
        C14681() {
        }

        public void onClick(View view) {
            CustomDialog.this.dismiss();
        }
    }

    /* renamed from: net.gogame.gowrap.ui.dialog.CustomDialog$2 */
    class C14692 implements OnClickListener {
        C14692() {
        }

        public void onClick(View view) {
            if (CustomDialog.this.canceledOnTouchOutside) {
                CustomDialog.this.dismiss();
            }
        }
    }

    public static class Builder {
        private boolean canceledOnTouchOutside;
        private final Context context;
        private Listener listener;
        private String message;
        private String title;
        private Type type;

        public Builder(Context context) {
            this.context = context;
        }

        public CustomDialog build() {
            CustomDialog customDialog = new CustomDialog(this.context);
            customDialog.setType(this.type);
            customDialog.setTitle(this.title);
            customDialog.setMessage(this.message);
            customDialog.setCanceledOnTouchOutside(this.canceledOnTouchOutside);
            customDialog.setListener(this.listener);
            return customDialog;
        }

        public Builder withType(Type type) {
            this.type = type;
            return this;
        }

        public Builder withTitle(String str) {
            this.title = str;
            return this;
        }

        public Builder withTitle(int i) {
            this.title = this.context.getResources().getString(i);
            return this;
        }

        public Builder withMessage(String str) {
            this.message = str;
            return this;
        }

        public Builder withMessage(int i) {
            this.message = this.context.getResources().getString(i);
            return this;
        }

        public Builder withCanceledOnTouchOutside(boolean z) {
            this.canceledOnTouchOutside = z;
            return this;
        }

        public Builder withListener(Listener listener) {
            this.listener = listener;
            return this;
        }
    }

    public interface Listener {
        void onClosed();
    }

    public enum Type {
        INFO,
        ALERT,
        PROGRESS
    }

    public CustomDialog(Context context) {
        this.view = ((LayoutInflater) context.getSystemService("layout_inflater")).inflate(C1451R.layout.net_gogame_gowrap_dialog, null, false);
        this.view.findViewById(C1451R.id.net_gogame_gowrap_dialog_close_button).setOnClickListener(new C14681());
        this.view.setOnClickListener(new C14692());
    }

    public static Builder newBuilder(Context context) {
        return new Builder(context);
    }

    public Type getType() {
        return this.type;
    }

    public void setType(Type type) {
        this.type = type;
    }

    public String getTitle() {
        return this.title;
    }

    public void setTitle(String str) {
        this.title = str;
    }

    public String getMessage() {
        return this.message;
    }

    public void setMessage(String str) {
        this.message = str;
    }

    public boolean isCanceledOnTouchOutside() {
        return this.canceledOnTouchOutside;
    }

    public void setCanceledOnTouchOutside(boolean z) {
        this.canceledOnTouchOutside = z;
    }

    public Listener getListener() {
        return this.listener;
    }

    public void setListener(Listener listener) {
        this.listener = listener;
    }

    public void show() {
        if (!this.popupWindow.isShowing()) {
            View findViewById = this.view.findViewById(C1451R.id.net_gogame_gowrap_dialog_close_button);
            ((TextView) this.view.findViewById(C1451R.id.net_gogame_gowrap_dialog_title)).setText(this.title);
            ((TextView) this.view.findViewById(C1451R.id.net_gogame_gowrap_dialog_message)).setText(this.message);
            View findViewById2 = this.view.findViewById(C1451R.id.net_gogame_gowrap_dialog_icon_container);
            View findViewById3 = this.view.findViewById(C1451R.id.net_gogame_gowrap_dialog_icon_info);
            View findViewById4 = this.view.findViewById(C1451R.id.net_gogame_gowrap_dialog_icon_alert);
            View findViewById5 = this.view.findViewById(C1451R.id.net_gogame_gowrap_progress_indicator);
            if (this.type != null) {
                switch (this.type) {
                    case INFO:
                        findViewById.setVisibility(0);
                        findViewById2.setVisibility(0);
                        findViewById3.setVisibility(0);
                        findViewById4.setVisibility(8);
                        findViewById5.setVisibility(8);
                        break;
                    case ALERT:
                        findViewById.setVisibility(0);
                        findViewById2.setVisibility(0);
                        findViewById3.setVisibility(8);
                        findViewById4.setVisibility(0);
                        findViewById5.setVisibility(8);
                        break;
                    case PROGRESS:
                        findViewById.setVisibility(8);
                        findViewById2.setVisibility(0);
                        findViewById3.setVisibility(8);
                        findViewById4.setVisibility(8);
                        findViewById5.setVisibility(0);
                        break;
                    default:
                        findViewById.setVisibility(0);
                        findViewById2.setVisibility(8);
                        findViewById3.setVisibility(8);
                        findViewById4.setVisibility(8);
                        findViewById5.setVisibility(8);
                        break;
                }
            }
            findViewById2.setVisibility(8);
            this.popupWindow.showAtLocation(this.view, 17, 0, 0);
        }
    }

    public void dismiss() {
        if (this.popupWindow.isShowing()) {
            this.popupWindow.dismiss();
            if (this.listener != null) {
                try {
                    this.listener.onClosed();
                } catch (Throwable e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        }
    }
}
