package net.gogame.gowrap.p019ui.v2017_1;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Rect;
import android.os.Bundle;
import android.os.Handler;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnFocusChangeListener;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ExpandableListView;
import android.widget.ExpandableListView.OnChildClickListener;
import android.widget.ExpandableListView.OnGroupExpandListener;
import android.widget.TextView;
import android.widget.TextView.OnEditorActionListener;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Locale;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.model.faq.Article;
import net.gogame.gowrap.model.faq.Category;
import net.gogame.gowrap.p019ui.UIContext;
import net.gogame.gowrap.p019ui.VipListener;
import net.gogame.gowrap.p019ui.dialog.CustomDialog;
import net.gogame.gowrap.p019ui.dialog.CustomDialog.Type;
import net.gogame.gowrap.p019ui.utils.DisplayUtils;
import net.gogame.gowrap.p019ui.utils.UIUtils;
import net.gogame.gowrap.p019ui.view.AutoClosingExpandableListViewListener;
import net.gogame.gowrap.p019ui.view.RightDrawableOnTouchListener;
import net.gogame.gowrap.p021io.utils.IOUtils;
import net.gogame.gowrap.support.FaqSupport;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.v2017_1.SupportFragment */
public class SupportFragment extends Fragment implements VipListener {
    private static final String AUTOCLOSING_EXPANDABLELISTVIEW_LISTENER_BUNDLE_NAME = "autoClosingExpandableListViewListener";
    private static final String KEY_SHOW_BUTTONS = "showButtons";
    private static final String LISTVIEW_BUNDLE_NAME = "listView";
    private static final String LISTVIEW_BUNDLE_PROPERTY_NAME_STATE = "state";
    private static final long SEARCH_PERIOD = 1000;
    private static final int SEARCH_TERM_MIN_LENGTH = 1;
    private static final String SEARCH_TEXT_FIELD_BUNDLE_NAME = "searchTextField";
    private static final String SEARCH_TEXT_FIELD_BUNDLE_PROPERTY_NAME_TEXT = "text";
    /* access modifiers changed from: private */
    public AutoClosingExpandableListViewListener autoClosingExpandableListViewListener;
    /* access modifiers changed from: private */
    public Runnable autoSearchRunnable;
    private SupportCustomImageButton chatButton;
    private String currentQuery;
    private String[] currentSearchTerms;
    /* access modifiers changed from: private */
    public ExpandableListView expandableListView;
    /* access modifiers changed from: private */
    public Handler handler;
    /* access modifiers changed from: private */
    public FaqExpandableListAdapter listAdapter;
    private Bundle savedInstanceState;
    /* access modifiers changed from: private */
    public EditText searchTextField;
    /* access modifiers changed from: private */
    public boolean searchUpdating = false;
    private boolean showButtons;

    public static SupportFragment create(boolean z) {
        SupportFragment supportFragment = new SupportFragment();
        Bundle bundle = new Bundle();
        bundle.putBoolean(KEY_SHOW_BUTTONS, z);
        supportFragment.setArguments(bundle);
        return supportFragment;
    }

    /* access modifiers changed from: private */
    public void search(String str) {
        String trimToNull = StringUtils.trimToNull(str);
        if (!StringUtils.isEquals(this.currentQuery, trimToNull)) {
            this.currentQuery = trimToNull;
            if (this.listAdapter != null) {
                this.searchUpdating = true;
                try {
                    String[] split = StringUtils.split(this.currentQuery, " ");
                    if (split != null) {
                        ArrayList arrayList = new ArrayList();
                        for (String trimToNull2 : split) {
                            String trimToNull3 = StringUtils.trimToNull(trimToNull2);
                            if (trimToNull3 != null && trimToNull3.length() >= 1) {
                                arrayList.add(trimToNull3.toLowerCase());
                            }
                        }
                        if (arrayList.isEmpty()) {
                            split = null;
                        } else {
                            split = (String[]) arrayList.toArray(new String[arrayList.size()]);
                        }
                    }
                    this.currentSearchTerms = split;
                    if (split != null) {
                        this.expandableListView.expandGroup(0);
                    }
                    this.listAdapter.setSearchTerms(split);
                } finally {
                    this.searchUpdating = false;
                }
            }
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        final UIContext uIContext;
        Bundle bundle2;
        final String str;
        Bundle bundle3;
        Bundle bundle4 = null;
        View inflate = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_fragment_support, viewGroup, false);
        if (getArguments() != null) {
            this.showButtons = getArguments().getBoolean(KEY_SHOW_BUTTONS, false);
        }
        final Context context = viewGroup.getContext();
        if (context instanceof UIContext) {
            uIContext = (UIContext) context;
        } else {
            uIContext = null;
        }
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_back_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (uIContext != null) {
                    uIContext.goBack();
                }
            }
        });
        this.handler = new Handler();
        this.autoSearchRunnable = new Runnable() {
            public void run() {
                SupportFragment.this.search(SupportFragment.this.searchTextField.getText().toString());
                SupportFragment.this.handler.postDelayed(SupportFragment.this.autoSearchRunnable, 1000);
            }
        };
        this.expandableListView = (ExpandableListView) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_faq_listview);
        View inflate2 = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_include_faq_header, this.expandableListView, false);
        View findViewById = inflate2.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_button);
        if (findViewById != null) {
            findViewById.setOnClickListener(new OnClickListener() {
                public void onClick(View view) {
                    if (uIContext != null) {
                        uIContext.pushFragment(new SupportFormFragment());
                    }
                }
            });
        }
        this.chatButton = (SupportCustomImageButton) inflate2.findViewById(C1423R.C1424id.net_gogame_gowrap_support_chat_button);
        if (this.chatButton != null) {
            updateChatButton(uIContext.isVipChatEnabled(), Wrapper.INSTANCE.isChatBotEnabled());
            this.chatButton.setOnClickListener(new OnClickListener() {
                public void onClick(View view) {
                    SupportFragment.this.clearFocus();
                    if (uIContext.isVipChatEnabled() || Wrapper.INSTANCE.isChatBotEnabled()) {
                        GoWrapImpl.INSTANCE.startChat();
                    } else {
                        CustomDialog.newBuilder(context).withType(Type.ALERT).withTitle(C1423R.string.net_gogame_gowrap_support_title).withMessage(C1423R.string.net_gogame_gowrap_support_chat_vip_only_message).build().show();
                    }
                }
            });
        }
        if (this.savedInstanceState != null) {
            bundle2 = this.savedInstanceState.getBundle(SEARCH_TEXT_FIELD_BUNDLE_NAME);
        } else {
            bundle2 = null;
        }
        this.searchTextField = (EditText) inflate2.findViewById(C1423R.C1424id.net_gogame_gowrap_support_search_textfield);
        if (bundle2 != null) {
            this.searchTextField.setText(bundle2.getString(SEARCH_TEXT_FIELD_BUNDLE_PROPERTY_NAME_TEXT));
        }
        this.searchTextField.setOnTouchListener(new RightDrawableOnTouchListener() {
            public boolean onDrawableTouch(MotionEvent motionEvent) {
                SupportFragment.this.searchTextField.setText(null);
                SupportFragment.this.search(SupportFragment.this.searchTextField.getText().toString());
                return true;
            }
        });
        UIUtils.setupRightDrawable(getActivity(), this.searchTextField, C1423R.array.net_gogame_gowrap_search_edittext_drawables);
        this.searchTextField.setOnEditorActionListener(new OnEditorActionListener() {
            public boolean onEditorAction(TextView textView, int i, KeyEvent keyEvent) {
                SupportFragment.this.clearFocus();
                SupportFragment.this.search(SupportFragment.this.searchTextField.getText().toString());
                return true;
            }
        });
        this.searchTextField.setOnFocusChangeListener(new OnFocusChangeListener() {
            public void onFocusChange(View view, boolean z) {
                if (z) {
                    Rect rect = new Rect();
                    Rect rect2 = new Rect();
                    SupportFragment.this.expandableListView.getGlobalVisibleRect(rect);
                    view.getGlobalVisibleRect(rect2);
                    final int i = rect2.top - rect.top;
                    SupportFragment.this.expandableListView.post(new Runnable() {
                        public void run() {
                            SupportFragment.this.expandableListView.smoothScrollBy(i, 100);
                        }
                    });
                }
            }
        });
        this.expandableListView.addHeaderView(inflate2);
        try {
            str = IOUtils.assetToString(context, new String[]{"net/gogame/gowrap/faq-article-template.html", "net/gogame/gowrap/faq-article-template-default.html"}, "UTF-8");
        } catch (IOException e) {
            str = null;
        }
        Category faq = FaqSupport.getFaq(context, Wrapper.INSTANCE.getCurrentLocale(context));
        if (faq == null) {
            faq = new Category("", "", new ArrayList());
            this.searchTextField.setVisibility(8);
        }
        this.listAdapter = new FaqExpandableListAdapter(context, faq);
        this.listAdapter.setSearchTerms(this.currentSearchTerms);
        if (this.savedInstanceState != null) {
            bundle3 = this.savedInstanceState.getBundle(AUTOCLOSING_EXPANDABLELISTVIEW_LISTENER_BUNDLE_NAME);
        } else {
            bundle3 = null;
        }
        this.autoClosingExpandableListViewListener = new AutoClosingExpandableListViewListener(this.expandableListView, bundle3);
        this.expandableListView.setOnGroupExpandListener(new OnGroupExpandListener() {
            public void onGroupExpand(int i) {
                if (!SupportFragment.this.searchUpdating) {
                    SupportFragment.this.clearFocus();
                }
                SupportFragment.this.autoClosingExpandableListViewListener.onGroupExpand(i);
            }
        });
        this.expandableListView.setAdapter(this.listAdapter);
        this.expandableListView.setOnChildClickListener(new OnChildClickListener() {
            public boolean onChildClick(ExpandableListView expandableListView, View view, int i, int i2, long j) {
                Article article = (Article) SupportFragment.this.listAdapter.getChild(i, i2);
                if (!(article == null || uIContext == null || str == null)) {
                    uIContext.loadHtml(String.format(Locale.getDefault(), str, new Object[]{StringUtils.escapeHtml(article.getTitle()), article.getBody()}), null);
                }
                return true;
            }
        });
        View findViewById2 = inflate2.findViewById(C1423R.C1424id.net_gogame_gowrap_back_support_buttons);
        if (this.showButtons) {
            findViewById2.setVisibility(0);
        } else {
            findViewById2.setVisibility(8);
        }
        if (this.savedInstanceState != null) {
            bundle4 = this.savedInstanceState.getBundle(LISTVIEW_BUNDLE_NAME);
        }
        if (bundle4 != null) {
            this.expandableListView.onRestoreInstanceState(bundle4.getParcelable("state"));
        }
        return inflate;
    }

    /* access modifiers changed from: private */
    public void clearFocus() {
        getView().clearFocus();
        DisplayUtils.hideSoftKeyboard(getActivity());
    }

    public void onResume() {
        super.onResume();
        if (this.handler != null && this.autoSearchRunnable != null) {
            this.handler.postDelayed(this.autoSearchRunnable, 1000);
        }
    }

    public void onPause() {
        super.onPause();
        if (!(this.handler == null || this.autoSearchRunnable == null)) {
            this.handler.removeCallbacks(this.autoSearchRunnable);
        }
        Bundle bundle = new Bundle();
        if (this.searchTextField != null) {
            Bundle bundle2 = new Bundle();
            bundle2.putString(SEARCH_TEXT_FIELD_BUNDLE_PROPERTY_NAME_TEXT, this.searchTextField.getText().toString());
            bundle.putBundle(SEARCH_TEXT_FIELD_BUNDLE_NAME, bundle2);
        }
        if (this.autoClosingExpandableListViewListener != null) {
            Bundle bundle3 = new Bundle();
            this.autoClosingExpandableListViewListener.saveState(bundle3);
            bundle.putBundle(AUTOCLOSING_EXPANDABLELISTVIEW_LISTENER_BUNDLE_NAME, bundle3);
        }
        if (this.expandableListView != null) {
            Bundle bundle4 = new Bundle();
            bundle4.putParcelable("state", this.expandableListView.onSaveInstanceState());
            bundle.putBundle(LISTVIEW_BUNDLE_NAME, bundle4);
        }
        this.savedInstanceState = bundle;
    }

    private void updateChatButton(boolean z, boolean z2) {
        if (this.chatButton != null) {
            this.chatButton.setMasked(!z && !z2);
        }
    }

    public void onEnableVipChat() {
        updateChatButton(true, Wrapper.INSTANCE.isChatBotEnabled());
    }

    public void onDisableVipChat() {
        updateChatButton(false, Wrapper.INSTANCE.isChatBotEnabled());
    }
}
