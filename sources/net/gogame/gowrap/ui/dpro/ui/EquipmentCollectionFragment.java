package net.gogame.gowrap.ui.dpro.ui;

import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.ScrollView;
import android.widget.TextView;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.support.DownloadManager.Request.Builder;
import net.gogame.gowrap.support.DownloadManager.Target;
import net.gogame.gowrap.ui.UIContext;
import net.gogame.gowrap.ui.dialog.CustomDialog;
import net.gogame.gowrap.ui.dialog.CustomDialog.Type;
import net.gogame.gowrap.ui.download.ImageViewTarget;
import net.gogame.gowrap.ui.dpro.C1471R;
import net.gogame.gowrap.ui.dpro.model.equipmentcollection.Equipment;
import net.gogame.gowrap.ui.dpro.model.equipmentcollection.EquipmentCollection;
import net.gogame.gowrap.ui.dpro.model.equipmentcollection.EquipmentCollectionResponse;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LeaderboardEntry;
import net.gogame.gowrap.ui.dpro.service.EquipmentCollectionAsyncTask;

public class EquipmentCollectionFragment extends Fragment {
    private static final String BASE_EQUIPMENT_ICON_URL = "https://appprd.dragonproject.gogame.net/app_icon/equip/EIC_";
    private static final String[] RARITY_NAMES = new String[]{null, "C", "B", "A", "S", "SS"};
    private EquipmentCollection equipmentCollection;
    private final List<ImageViewTarget> imageViewTargetList = new ArrayList();
    private final LeaderboardEntry leaderboardEntry;
    private int pageNumber = 0;
    private TextView pagerText;
    private ProgressBar progressBar;
    private int progressCount = 0;
    private UIContext uiContext;

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.EquipmentCollectionFragment$1 */
    class C14731 implements OnClickListener {
        C14731() {
        }

        public void onClick(View view) {
            if (EquipmentCollectionFragment.this.uiContext != null) {
                EquipmentCollectionFragment.this.uiContext.goBack();
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.EquipmentCollectionFragment$2 */
    class C14742 implements OnClickListener {
        C14742() {
        }

        public void onClick(View view) {
            EquipmentCollectionFragment.this.showPage(EquipmentCollectionFragment.this.pageNumber - 1);
        }
    }

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.EquipmentCollectionFragment$3 */
    class C14753 implements OnClickListener {
        C14753() {
        }

        public void onClick(View view) {
            EquipmentCollectionFragment.this.showPage(EquipmentCollectionFragment.this.pageNumber + 1);
        }
    }

    private class CustomEquipmentCollectionAsyncTask extends EquipmentCollectionAsyncTask {
        private CustomEquipmentCollectionAsyncTask() {
        }

        protected void onPreExecute() {
            EquipmentCollectionFragment.this.onNetworkOperationStarted();
        }

        protected void onPostExecute(EquipmentCollectionResponse equipmentCollectionResponse) {
            try {
                EquipmentCollectionFragment.this.onNetworkOperationEnded();
                if (getExceptionToBeThrown() != null) {
                    CustomDialog.newBuilder(EquipmentCollectionFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1471R.string.net_gogame_gowrap_ranking_title).withMessage(getExceptionToBeThrown().getMessage()).build().show();
                } else if (equipmentCollectionResponse == null) {
                    CustomDialog.newBuilder(EquipmentCollectionFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1471R.string.net_gogame_gowrap_ranking_title).withMessage(C1471R.string.net_gogame_gowrap_ranking_no_data_error_message).build().show();
                } else if (equipmentCollectionResponse.getStatusCode() != 0) {
                    CustomDialog.newBuilder(EquipmentCollectionFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1471R.string.net_gogame_gowrap_ranking_title).withMessage(equipmentCollectionResponse.getErrorMessage()).build().show();
                } else {
                    EquipmentCollectionFragment.this.equipmentCollection = equipmentCollectionResponse.getEquipmentCollection();
                    EquipmentCollectionFragment.this.pageNumber = 0;
                    EquipmentCollectionFragment.this.showPage(EquipmentCollectionFragment.this.pageNumber);
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public EquipmentCollectionFragment(LeaderboardEntry leaderboardEntry) {
        this.leaderboardEntry = leaderboardEntry;
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        View inflate = layoutInflater.inflate(C1471R.layout.net_gogame_gowrap_dpro_fragment_equipment_collection, viewGroup, false);
        this.progressBar = (ProgressBar) inflate.findViewById(C1471R.id.net_gogame_gowrap_progress_indicator);
        ViewGroup viewGroup2 = (ViewGroup) inflate.findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_list);
        for (int i = 0; i < viewGroup2.getChildCount(); i++) {
            viewGroup2.getChildAt(i).setVisibility(8);
        }
        if (this.leaderboardEntry != null) {
            ((TextView) inflate.findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_user_name)).setText(this.leaderboardEntry.getUserName());
            ((TextView) inflate.findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_user_title)).setText(this.leaderboardEntry.getUserTitle());
            if (this.leaderboardEntry.getUserLevel() != null) {
                ((TextView) inflate.findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_user_level)).setText(String.format(Locale.getDefault(), "Lv %,d", new Object[]{this.leaderboardEntry.getUserLevel()}));
            }
            if (this.leaderboardEntry.getValue() != null) {
                ((TextView) inflate.findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_points)).setText(String.format(Locale.getDefault(), "Points %,d", new Object[]{this.leaderboardEntry.getValue()}));
            }
            ((TextView) inflate.findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_hunter_id)).setText(this.leaderboardEntry.getHunterId());
            if (this.leaderboardEntry.getHunterId() != null) {
                new CustomEquipmentCollectionAsyncTask().execute(new String[]{this.leaderboardEntry.getHunterId()});
            }
        }
        inflate.findViewById(C1471R.id.net_gogame_gowrap_back_button).setOnClickListener(new C14731());
        View findViewById = inflate.findViewById(C1471R.id.net_gogame_gowrap_pager_previous_button);
        View findViewById2 = inflate.findViewById(C1471R.id.net_gogame_gowrap_pager_next_button);
        this.pagerText = (TextView) inflate.findViewById(C1471R.id.net_gogame_gowrap_pager_text);
        findViewById.setOnClickListener(new C14742());
        findViewById2.setOnClickListener(new C14753());
        return inflate;
    }

    private synchronized void onNetworkOperationStarted() {
        this.progressCount++;
        if (!(this.progressBar == null || this.progressCount <= 0 || this.progressBar.getVisibility() == 0)) {
            this.progressBar.setVisibility(0);
        }
    }

    private synchronized void onNetworkOperationEnded() {
        this.progressCount--;
        if (this.progressCount < 0) {
            this.progressCount = 0;
        }
        if (!(this.progressBar == null || this.progressCount != 0 || this.progressBar.getVisibility() == 8)) {
            this.progressBar.setVisibility(8);
        }
    }

    private boolean showPage(int i) {
        if (getView() == null || this.equipmentCollection == null || this.equipmentCollection.getEquipmentList() == null) {
            return false;
        }
        int intValue;
        for (ImageViewTarget cancelled : this.imageViewTargetList) {
            cancelled.setCancelled(true);
        }
        this.imageViewTargetList.clear();
        ((ScrollView) getView().findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_container)).scrollTo(0, 0);
        if (this.equipmentCollection.getForgedEquipmentCount() != null) {
            ((TextView) getView().findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_forged_count)).setText(String.format(Locale.getDefault(), "Forged %,d", new Object[]{this.equipmentCollection.getForgedEquipmentCount()}));
        }
        int size = this.equipmentCollection.getEquipmentList().size();
        if (this.equipmentCollection.getTotalEquipmentCount() != null) {
            ((TextView) getView().findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_total_count)).setText(String.format(Locale.getDefault(), "Total %,d", new Object[]{this.equipmentCollection.getTotalEquipmentCount()}));
            intValue = this.equipmentCollection.getTotalEquipmentCount().intValue();
        } else {
            intValue = size;
        }
        ViewGroup viewGroup = (ViewGroup) getView().findViewById(C1471R.id.net_gogame_gowrap_equipment_collection_list);
        int childCount = viewGroup.getChildCount();
        int i2 = ((intValue + childCount) - 1) / childCount;
        if (i < 0) {
            i = i2 - 1;
        } else if (i >= i2) {
            i = 0;
        }
        this.pageNumber = i;
        this.pagerText.setText(String.format(Locale.ENGLISH, "%03d/%03d", new Object[]{Integer.valueOf(this.pageNumber + 1), Integer.valueOf(i2)}));
        int i3 = this.pageNumber * childCount;
        int i4 = i3 + childCount;
        for (int i5 = i3; i5 < i4; i5++) {
            int i6 = i5 - i3;
            View childAt = viewGroup.getChildAt(i6);
            ImageView imageView = (ImageView) childAt.findViewById(C1471R.id.net_gogame_gowrap_armory_equipment_icon);
            ImageView imageView2 = (ImageView) childAt.findViewById(C1471R.id.net_gogame_gowrap_armory_equipment_frame);
            ImageView imageView3 = (ImageView) childAt.findViewById(C1471R.id.net_gogame_gowrap_armory_equipment_rarity);
            ImageView imageView4 = (ImageView) childAt.findViewById(C1471R.id.net_gogame_gowrap_armory_equipment_element);
            TextView textView = (TextView) childAt.findViewById(C1471R.id.net_gogame_gowrap_armory_equipment_index);
            if (i5 < intValue) {
                childAt.setVisibility(0);
                textView.setText(String.format(Locale.ENGLISH, "No.%04d", new Object[]{Integer.valueOf(i5 + 1)}));
                Equipment equipment = null;
                if (i5 < this.equipmentCollection.getEquipmentList().size()) {
                    equipment = (Equipment) this.equipmentCollection.getEquipmentList().get(i5);
                }
                if (equipment == null || this.uiContext == null || i6 >= childCount) {
                    imageView.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_question_mark);
                    imageView2.setBackgroundResource(0);
                    imageView3.setImageResource(0);
                    imageView4.setImageResource(0);
                } else {
                    String str = BASE_EQUIPMENT_ICON_URL + getIconId(equipment) + "0.png";
                    imageView.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_blank);
                    imageView2.setBackgroundResource(C1471R.drawable.net_gogame_gowrap_dpro_equip_item_frame);
                    if (equipment.getRarity() != null) {
                        switch (equipment.getRarity().intValue()) {
                            case 1:
                                imageView3.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_rarity_c);
                                break;
                            case 2:
                                imageView3.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_rarity_b);
                                break;
                            case 3:
                                imageView3.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_rarity_a);
                                break;
                            case 4:
                                imageView3.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_rarity_s);
                                break;
                            case 5:
                                imageView3.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_rarity_ss);
                                break;
                            default:
                                imageView3.setImageResource(0);
                                break;
                        }
                    }
                    imageView3.setImageResource(0);
                    if (equipment.getElement() != null) {
                        switch (equipment.getElement().intValue()) {
                            case 1:
                                imageView4.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_element_fire);
                                break;
                            case 2:
                                imageView4.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_element_ice);
                                break;
                            case 3:
                                imageView4.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_element_wind);
                                break;
                            case 4:
                                imageView4.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_element_earth);
                                break;
                            case 5:
                                imageView4.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_element_light);
                                break;
                            case 6:
                                imageView4.setImageResource(C1471R.drawable.net_gogame_gowrap_dpro_icon_element_dark);
                                break;
                            default:
                                imageView4.setImageResource(0);
                                break;
                        }
                    }
                    imageView4.setImageResource(0);
                    Target imageViewTarget = new ImageViewTarget(imageView);
                    this.imageViewTargetList.add(imageViewTarget);
                    this.uiContext.getDownloadManager().download(Builder.newBuilder(str).into(imageViewTarget));
                }
            } else {
                childAt.setVisibility(8);
                imageView.setImageResource(0);
                imageView2.setBackgroundResource(0);
                imageView3.setImageResource(0);
                imageView4.setImageResource(0);
                textView.setText(null);
            }
        }
        return true;
    }

    private String getRarityName(Integer num) {
        if (num == null || num.intValue() < 0 || num.intValue() >= RARITY_NAMES.length) {
            return null;
        }
        return RARITY_NAMES[num.intValue()];
    }

    private Long getIconId(Equipment equipment) {
        if (equipment == null) {
            return null;
        }
        if (equipment.getIconId() == null || equipment.getIconId().longValue() <= 0) {
            return equipment.getId();
        }
        return equipment.getIconId();
    }
}
