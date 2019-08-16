package net.gogame.gowrap.p019ui.dpro.p020ui;

import android.app.Fragment;
import android.os.Bundle;
import android.os.Parcelable;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Locale;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.UIContext;
import net.gogame.gowrap.p019ui.dialog.CustomDialog;
import net.gogame.gowrap.p019ui.dpro.C1452R;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.AbstractLeaderboardResponse;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.EquipmentCollectionLeaderboardResponse;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.FriendsLeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LeaderboardEntry;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LevelTierLeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.NewUsersLeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.PowerRatingLeaderboardResponse;
import net.gogame.gowrap.p019ui.dpro.service.EquipmentCollectionLeaderboardAsyncTask;
import net.gogame.gowrap.p019ui.dpro.service.PowerRatingLeaderboardAsyncTask;
import net.gogame.gowrap.p019ui.utils.DisplayUtils;
import net.gogame.gowrap.p019ui.utils.UIUtils;
import net.gogame.gowrap.p019ui.view.RightDrawableOnTouchListener;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.ui.RankingFragment */
public class RankingFragment extends Fragment {
    private static final int DEFAULT_PAGE_SIZE = 50;
    private static final String KEY_LIST_ADAPTER = "listAdapter";
    private static final String KEY_LIST_VIEW = "listView";
    private static final String KEY_PAGE_NUMBER = "pageNumber";
    private static final String KEY_SEARCHED_ENTRY = "searchedEntry";
    private static final String KEY_TOTAL_RECORDS = "totalRecords";
    private static final String KEY_TYPE = "type";
    private static final long MAX_RECORDS = 1000;
    private View buttonContainer;
    /* access modifiers changed from: private */
    public View[] buttonGroup;
    private View equipmentCollectionAllUsersButton;
    private LinearLayout hunterEntry;
    private LinearLayout hunterEntryContainer;
    /* access modifiers changed from: private */
    public RankingListAdapter listAdapter;
    private ListView listView;
    /* access modifiers changed from: private */
    public int pageNumber = 0;
    private View pagerContainer;
    private View pagerNextButton;
    private View pagerPreviousButton;
    private TextView pagerText;
    private View powerRankingAllUsersRanksButton;
    private View powerRankingNewUsersButton;
    private ProgressBar progressBar;
    private int progressCount = 0;
    private View resultsContainer;
    private Bundle savedInstanceState;
    private View searchContainer;
    /* access modifiers changed from: private */
    public EditText searchEditText;
    /* access modifiers changed from: private */
    public View searchSubmitButton;
    /* access modifiers changed from: private */
    public LeaderboardEntry searchedEntry;
    private long totalRecords = 0;
    /* access modifiers changed from: private */
    public Type type = null;
    private UIContext uiContext;

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.RankingFragment$Category */
    private enum Category {
        POWER_RATING,
        EQUIPMENT_COLLECTION
    }

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.RankingFragment$CustomEquipmentCollectionLeaderboardAsyncTask */
    private class CustomEquipmentCollectionLeaderboardAsyncTask extends EquipmentCollectionLeaderboardAsyncTask {
        private final String hunterId;
        private final int newPageNumber;
        private final Type newType;

        public CustomEquipmentCollectionLeaderboardAsyncTask(Type type, int i, String str) {
            this.newType = type;
            this.newPageNumber = i;
            this.hunterId = str;
        }

        /* access modifiers changed from: protected */
        public void onPreExecute() {
            RankingFragment.this.onNetworkOperationStarted();
        }

        /* access modifiers changed from: protected */
        public void onPostExecute(EquipmentCollectionLeaderboardResponse equipmentCollectionLeaderboardResponse) {
            try {
                RankingFragment.this.onNetworkOperationEnded();
                if (getExceptionToBeThrown() != null) {
                    CustomDialog.newBuilder(RankingFragment.this.getActivity()).withType(net.gogame.gowrap.p019ui.dialog.CustomDialog.Type.ALERT).withTitle(C1452R.string.net_gogame_gowrap_ranking_title).withMessage(getExceptionToBeThrown().getMessage()).build().show();
                } else if (equipmentCollectionLeaderboardResponse == null) {
                    CustomDialog.newBuilder(RankingFragment.this.getActivity()).withType(net.gogame.gowrap.p019ui.dialog.CustomDialog.Type.ALERT).withTitle(C1452R.string.net_gogame_gowrap_ranking_title).withMessage(C1452R.string.net_gogame_gowrap_ranking_no_data_error_message).build().show();
                } else if (equipmentCollectionLeaderboardResponse.getStatusCode() != 0) {
                    CustomDialog.newBuilder(RankingFragment.this.getActivity()).withType(net.gogame.gowrap.p019ui.dialog.CustomDialog.Type.ALERT).withTitle(C1452R.string.net_gogame_gowrap_ranking_title).withMessage(equipmentCollectionLeaderboardResponse.getErrorMessage()).build().show();
                } else {
                    RankingFragment.this.populate(equipmentCollectionLeaderboardResponse, this.newType, this.newPageNumber, this.hunterId);
                }
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.RankingFragment$CustomPowerRatingLeaderboardAsyncTask */
    private class CustomPowerRatingLeaderboardAsyncTask extends PowerRatingLeaderboardAsyncTask {
        private final String hunterId;
        private final int newPageNumber;
        private final Type newType;

        public CustomPowerRatingLeaderboardAsyncTask(Type type, int i, String str) {
            this.newType = type;
            this.newPageNumber = i;
            this.hunterId = str;
        }

        /* access modifiers changed from: protected */
        public void onPreExecute() {
            RankingFragment.this.onNetworkOperationStarted();
        }

        /* access modifiers changed from: protected */
        public void onPostExecute(PowerRatingLeaderboardResponse powerRatingLeaderboardResponse) {
            try {
                RankingFragment.this.onNetworkOperationEnded();
                if (getExceptionToBeThrown() != null) {
                    CustomDialog.newBuilder(RankingFragment.this.getActivity()).withType(net.gogame.gowrap.p019ui.dialog.CustomDialog.Type.ALERT).withTitle(C1452R.string.net_gogame_gowrap_ranking_title).withMessage(getExceptionToBeThrown().getMessage()).build().show();
                } else if (powerRatingLeaderboardResponse == null) {
                    CustomDialog.newBuilder(RankingFragment.this.getActivity()).withType(net.gogame.gowrap.p019ui.dialog.CustomDialog.Type.ALERT).withTitle(C1452R.string.net_gogame_gowrap_ranking_title).withMessage(C1452R.string.net_gogame_gowrap_ranking_no_data_error_message).build().show();
                } else if (powerRatingLeaderboardResponse.getStatusCode() != 0) {
                    CustomDialog.newBuilder(RankingFragment.this.getActivity()).withType(net.gogame.gowrap.p019ui.dialog.CustomDialog.Type.ALERT).withTitle(C1452R.string.net_gogame_gowrap_ranking_title).withMessage(powerRatingLeaderboardResponse.getErrorMessage()).build().show();
                } else {
                    RankingFragment.this.populate(powerRatingLeaderboardResponse, this.newType, this.newPageNumber, this.hunterId);
                }
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.RankingFragment$SubCategory */
    private enum SubCategory {
        ALL_USERS,
        NEW_USERS,
        FRIENDS
    }

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.RankingFragment$Type */
    private enum Type {
        POWER_RATING_ALL_USERS(Category.POWER_RATING, SubCategory.ALL_USERS),
        POWER_RATING_NEW_USERS(Category.POWER_RATING, SubCategory.NEW_USERS),
        POWER_RATING_FRIENDS(Category.POWER_RATING, SubCategory.FRIENDS),
        EQUIPMENT_COLLECTION_ALL_USERS(Category.EQUIPMENT_COLLECTION, SubCategory.ALL_USERS),
        EQUIPMENT_COLLECTION_FRIENDS(Category.EQUIPMENT_COLLECTION, SubCategory.FRIENDS);
        
        private final Category category;
        private final SubCategory subCategory;

        private Type(Category category2, SubCategory subCategory2) {
            this.category = category2;
            this.subCategory = subCategory2;
        }

        public Category getCategory() {
            return this.category;
        }

        public SubCategory getSubCategory() {
            return this.subCategory;
        }
    }

    private Parcelable getParcelable(String str) {
        if (this.savedInstanceState == null || str == null) {
            return null;
        }
        return this.savedInstanceState.getParcelable(str);
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        View inflate = layoutInflater.inflate(C1452R.C1454layout.net_gogame_gowrap_dpro_fragment_ranking, viewGroup, false);
        this.progressBar = (ProgressBar) inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_progress_indicator);
        this.listAdapter = new RankingListAdapter(getActivity());
        this.listAdapter.onRestoreInstanceState(getParcelable(KEY_LIST_ADAPTER));
        this.buttonContainer = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_button_container);
        this.powerRankingAllUsersRanksButton = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_power_rating_all_users_button);
        this.powerRankingNewUsersButton = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_power_rating_new_users_button);
        this.equipmentCollectionAllUsersButton = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_equipment_collection_all_users_button);
        this.buttonGroup = new View[]{this.powerRankingAllUsersRanksButton, this.powerRankingNewUsersButton, this.equipmentCollectionAllUsersButton};
        View findViewById = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_search_button);
        this.searchContainer = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_search_container);
        this.searchEditText = (EditText) inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_search);
        this.searchSubmitButton = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_search_submit_button);
        this.resultsContainer = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_results_container);
        this.hunterEntryContainer = (LinearLayout) inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_hunter_entry_container);
        this.hunterEntry = (LinearLayout) inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_hunter_entry);
        this.listView = (ListView) inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_listview);
        this.listView.setAdapter(this.listAdapter);
        this.listView.setOnItemClickListener(new OnItemClickListener() {
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
                RankingFragment.this.showUserDetails((LeaderboardEntry) RankingFragment.this.listAdapter.getItem(i));
            }
        });
        this.pagerContainer = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_pager_container);
        this.pagerPreviousButton = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_pager_previous_button);
        this.pagerNextButton = inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_pager_next_button);
        this.pagerText = (TextView) inflate.findViewById(C1452R.C1453id.net_gogame_gowrap_pager_text);
        this.powerRankingAllUsersRanksButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                RankingFragment.this.select(RankingFragment.this.buttonGroup, view);
                RankingFragment.this.hideSearch();
                RankingFragment.this.load(Type.POWER_RATING_ALL_USERS, 0, null);
            }
        });
        this.powerRankingNewUsersButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                RankingFragment.this.select(RankingFragment.this.buttonGroup, view);
                RankingFragment.this.hideSearch();
                RankingFragment.this.load(Type.POWER_RATING_NEW_USERS, 0, null);
            }
        });
        this.equipmentCollectionAllUsersButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                RankingFragment.this.select(RankingFragment.this.buttonGroup, view);
                RankingFragment.this.hideSearch();
                RankingFragment.this.load(Type.EQUIPMENT_COLLECTION_ALL_USERS, 0, null);
            }
        });
        findViewById.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                RankingFragment.this.select(RankingFragment.this.buttonGroup, null);
                RankingFragment.this.showSearch();
            }
        });
        UIUtils.setupRightDrawable(getActivity(), this.searchEditText, C1452R.array.net_gogame_gowrap_dpro_search_edittext_drawables);
        this.searchEditText.setText(this.uiContext.getGuid());
        this.searchEditText.addTextChangedListener(new TextWatcher() {
            public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
            }

            public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
                RankingFragment.this.searchSubmitButton.setEnabled(charSequence.length() == 10);
            }

            public void afterTextChanged(Editable editable) {
            }
        });
        this.searchEditText.setOnTouchListener(new RightDrawableOnTouchListener() {
            public boolean onDrawableTouch(MotionEvent motionEvent) {
                RankingFragment.this.searchEditText.setText(null);
                return true;
            }
        });
        this.searchSubmitButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                String trimToNull = StringUtils.trimToNull(RankingFragment.this.searchEditText.getText().toString());
                if (trimToNull != null) {
                    RankingFragment.this.hideSearch();
                    switch (RankingFragment.this.type.getCategory()) {
                        case POWER_RATING:
                            RankingFragment.this.load(Type.POWER_RATING_FRIENDS, 0, trimToNull);
                            return;
                        case EQUIPMENT_COLLECTION:
                            RankingFragment.this.load(Type.EQUIPMENT_COLLECTION_FRIENDS, 0, trimToNull);
                            return;
                        default:
                            return;
                    }
                }
            }
        });
        this.pagerPreviousButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                RankingFragment.this.load(RankingFragment.this.type, RankingFragment.this.pageNumber - 1, null);
            }
        });
        this.pagerNextButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                RankingFragment.this.load(RankingFragment.this.type, RankingFragment.this.pageNumber + 1, null);
            }
        });
        if (this.savedInstanceState != null) {
            if (getParcelable(KEY_LIST_VIEW) != null) {
                this.listView.onRestoreInstanceState(getParcelable(KEY_LIST_VIEW));
            }
            this.type = (Type) this.savedInstanceState.getSerializable("type");
            this.pageNumber = this.savedInstanceState.getInt(KEY_PAGE_NUMBER);
            this.totalRecords = this.savedInstanceState.getLong(KEY_TOTAL_RECORDS);
            this.searchedEntry = (LeaderboardEntry) this.savedInstanceState.getSerializable(KEY_SEARCHED_ENTRY);
            updateUI();
        } else {
            this.powerRankingAllUsersRanksButton.setSelected(true);
            load(Type.POWER_RATING_ALL_USERS, 0, null);
        }
        return inflate;
    }

    public void onPause() {
        super.onPause();
        Bundle bundle = new Bundle();
        if (this.listAdapter != null) {
            bundle.putParcelable(KEY_LIST_ADAPTER, this.listAdapter.onSaveInstanceState());
        }
        if (this.listView != null) {
            bundle.putParcelable(KEY_LIST_VIEW, this.listView.onSaveInstanceState());
        }
        bundle.putSerializable("type", this.type);
        bundle.putInt(KEY_PAGE_NUMBER, this.pageNumber);
        bundle.putLong(KEY_TOTAL_RECORDS, this.totalRecords);
        bundle.putSerializable(KEY_SEARCHED_ENTRY, this.searchedEntry);
        this.savedInstanceState = bundle;
    }

    /* access modifiers changed from: private */
    public void showUserDetails(LeaderboardEntry leaderboardEntry) {
        if (this.uiContext != null && leaderboardEntry != null) {
            switch (this.type.getCategory()) {
                case POWER_RATING:
                    this.uiContext.pushFragment(new ArmoryFragment(leaderboardEntry));
                    return;
                default:
                    return;
            }
        }
    }

    private LeaderboardRequest getLeaderboardRequest(Type type2, int i, String str) {
        switch (type2.getSubCategory()) {
            case ALL_USERS:
                return new LevelTierLeaderboardRequest(i, 50);
            case NEW_USERS:
                return new NewUsersLeaderboardRequest(i, 50);
            case FRIENDS:
                return new FriendsLeaderboardRequest(str);
            default:
                throw new IllegalArgumentException("Unknown sub-category: " + type2.getSubCategory());
        }
    }

    /* access modifiers changed from: private */
    public void load(Type type2, int i, String str) {
        switch (type2.getCategory()) {
            case POWER_RATING:
                new CustomPowerRatingLeaderboardAsyncTask(type2, i, str).execute(new LeaderboardRequest[]{getLeaderboardRequest(type2, i, str)});
                return;
            case EQUIPMENT_COLLECTION:
                new CustomEquipmentCollectionLeaderboardAsyncTask(type2, i, str).execute(new LeaderboardRequest[]{getLeaderboardRequest(type2, i, str)});
                return;
            default:
                return;
        }
    }

    /* access modifiers changed from: private */
    public void select(View[] viewArr, View view) {
        boolean z;
        for (View view2 : viewArr) {
            if (view2 == view) {
                z = true;
            } else {
                z = false;
            }
            view2.setSelected(z);
        }
    }

    /* access modifiers changed from: private */
    public void showSearch() {
        this.searchEditText.requestFocus();
        DisplayUtils.showSoftKeyboard(getActivity(), this.searchEditText);
    }

    /* access modifiers changed from: private */
    public void hideSearch() {
        DisplayUtils.hideSoftKeyboard(getActivity());
    }

    /* access modifiers changed from: private */
    public synchronized void onNetworkOperationStarted() {
        synchronized (this) {
            this.progressCount++;
            if (!(this.progressBar == null || this.progressCount <= 0 || this.progressBar.getVisibility() == 0)) {
                this.progressBar.setVisibility(0);
                this.buttonContainer.setEnabled(false);
                for (View enabled : this.buttonGroup) {
                    enabled.setEnabled(false);
                }
            }
        }
    }

    /* access modifiers changed from: private */
    public synchronized void onNetworkOperationEnded() {
        synchronized (this) {
            this.progressCount--;
            if (this.progressCount < 0) {
                this.progressCount = 0;
            }
            if (!(this.progressBar == null || this.progressCount != 0 || this.progressBar.getVisibility() == 8)) {
                this.progressBar.setVisibility(8);
                this.buttonContainer.setEnabled(true);
                for (View enabled : this.buttonGroup) {
                    enabled.setEnabled(true);
                }
            }
        }
    }

    private void updateUI() {
        boolean z;
        boolean z2;
        switch (this.type) {
            case POWER_RATING_ALL_USERS:
                select(this.buttonGroup, this.powerRankingAllUsersRanksButton);
                break;
            case POWER_RATING_NEW_USERS:
                select(this.buttonGroup, this.powerRankingNewUsersButton);
                break;
            case EQUIPMENT_COLLECTION_ALL_USERS:
                select(this.buttonGroup, this.equipmentCollectionAllUsersButton);
                break;
            default:
                select(this.buttonGroup, null);
                break;
        }
        if (this.searchedEntry != null) {
            this.hunterEntryContainer.setVisibility(0);
            this.hunterEntry.addView(this.listAdapter.getView(0, this.searchedEntry, null, this.hunterEntry));
            this.hunterEntryContainer.setOnClickListener(new OnClickListener() {
                public void onClick(View view) {
                    RankingFragment.this.showUserDetails(RankingFragment.this.searchedEntry);
                }
            });
            this.pagerContainer.setVisibility(8);
            return;
        }
        this.hunterEntryContainer.setVisibility(8);
        this.hunterEntry.removeAllViews();
        long min = Math.min(1000, this.totalRecords);
        int i = ((int) ((50 + min) - 1)) / 50;
        this.pagerContainer.setVisibility(0);
        View view = this.pagerPreviousButton;
        if (this.pageNumber > 0) {
            z = true;
        } else {
            z = false;
        }
        view.setEnabled(z);
        View view2 = this.pagerNextButton;
        if (this.pageNumber < i - 1) {
            z2 = true;
        } else {
            z2 = false;
        }
        view2.setEnabled(z2);
        long j = (long) ((this.pageNumber * 50) + 1);
        long j2 = (long) ((this.pageNumber + 1) * 50);
        this.pagerText.setText(String.format(Locale.getDefault(), "%,d-%,d/%,d", new Object[]{Long.valueOf(j), Long.valueOf(j2), Long.valueOf(min)}));
    }

    /* access modifiers changed from: private */
    public void populate(AbstractLeaderboardResponse<? extends LeaderboardEntry> abstractLeaderboardResponse, Type type2, int i, String str) {
        LeaderboardEntry leaderboardEntry;
        if (abstractLeaderboardResponse.getRecords() == null) {
            abstractLeaderboardResponse.setRecords(new ArrayList());
        }
        TextView textView = (TextView) getView().findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_value);
        switch (type2.getCategory()) {
            case POWER_RATING:
                textView.setText(C1452R.string.net_gogame_gowrap_ranking_power_header_caption);
                break;
            case EQUIPMENT_COLLECTION:
                textView.setText(C1452R.string.net_gogame_gowrap_ranking_points_header_caption);
                break;
            default:
                textView.setText(null);
                break;
        }
        if (str != null) {
            Iterator it = abstractLeaderboardResponse.getRecords().iterator();
            while (true) {
                if (it.hasNext()) {
                    leaderboardEntry = (LeaderboardEntry) it.next();
                    if (StringUtils.isEquals(leaderboardEntry.getHunterId(), str)) {
                        abstractLeaderboardResponse.getRecords().remove(leaderboardEntry);
                    }
                }
            }
        }
        leaderboardEntry = null;
        this.searchedEntry = leaderboardEntry;
        this.listAdapter.setEntries(abstractLeaderboardResponse.getRecords());
        this.listView.setSelectionAfterHeaderView();
        this.type = type2;
        this.pageNumber = i;
        this.totalRecords = abstractLeaderboardResponse.getTotalRecordCount() != null ? abstractLeaderboardResponse.getTotalRecordCount().longValue() : 0;
        updateUI();
    }
}
