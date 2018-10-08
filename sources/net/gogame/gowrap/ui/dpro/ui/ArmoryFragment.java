package net.gogame.gowrap.ui.dpro.ui;

import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import java.util.Locale;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.support.DownloadManager.Request.Builder;
import net.gogame.gowrap.ui.UIContext;
import net.gogame.gowrap.ui.dialog.CustomDialog;
import net.gogame.gowrap.ui.dialog.CustomDialog.Type;
import net.gogame.gowrap.ui.download.ImageViewTarget;
import net.gogame.gowrap.ui.dpro.C1155R;
import net.gogame.gowrap.ui.dpro.model.armory.Armory;
import net.gogame.gowrap.ui.dpro.model.armory.ArmoryResponse;
import net.gogame.gowrap.ui.dpro.model.armory.Equipment;
import net.gogame.gowrap.ui.dpro.model.armory.EquipmentSet;
import net.gogame.gowrap.ui.dpro.model.armory.SkillItem;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LeaderboardEntry;
import net.gogame.gowrap.ui.dpro.service.ArmoryAsyncTask;

public class ArmoryFragment extends Fragment {
    private static final String BASE_EQUIPMENT_ICON_URL = "https://appprd.dragonproject.gogame.net/app_icon/equip/EIC_";
    private static final String[] RARITY_NAMES = new String[]{null, "C", "B", "A", "S", "SS"};
    private static final Integer[] SKILL_ITEM_BACKGROUNDS = new Integer[]{null, Integer.valueOf(C1155R.drawable.net_gogame_gowrap_dpro_skillitem_90000001), Integer.valueOf(C1155R.drawable.net_gogame_gowrap_dpro_skillitem_90000002), Integer.valueOf(C1155R.drawable.net_gogame_gowrap_dpro_skillitem_90000003), null, null, null, null, Integer.valueOf(C1155R.drawable.net_gogame_gowrap_dpro_skillitem_90000004)};
    private final LeaderboardEntry leaderboardEntry;
    private ProgressBar progressBar;
    private int progressCount = 0;
    private UIContext uiContext;

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.ArmoryFragment$1 */
    class C11561 implements OnClickListener {
        C11561() {
        }

        public void onClick(View view) {
            if (ArmoryFragment.this.uiContext != null) {
                ArmoryFragment.this.uiContext.goBack();
            }
        }
    }

    private class CustomGetArmoryAsyncTask extends ArmoryAsyncTask {
        private CustomGetArmoryAsyncTask() {
        }

        protected void onPreExecute() {
            ArmoryFragment.this.onNetworkOperationStarted();
        }

        protected void onPostExecute(ArmoryResponse armoryResponse) {
            try {
                ArmoryFragment.this.onNetworkOperationEnded();
                if (getExceptionToBeThrown() != null) {
                    CustomDialog.newBuilder(ArmoryFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1155R.string.net_gogame_gowrap_ranking_title).withMessage(getExceptionToBeThrown().getMessage()).build().show();
                    return;
                }
                if (armoryResponse != null) {
                    if (armoryResponse.getStatusCode() != 0) {
                        CustomDialog.newBuilder(ArmoryFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1155R.string.net_gogame_gowrap_ranking_title).withMessage(armoryResponse.getErrorMessage()).build().show();
                        return;
                    } else if (armoryResponse.getArmory() != null) {
                        Armory armory = armoryResponse.getArmory();
                        if (!(armory.getCurrentEquipSetNo() == null || armory.getEquipmentSets() == null || armory.getCurrentEquipSetNo().intValue() >= armory.getEquipmentSets().size())) {
                            EquipmentSet equipmentSet = (EquipmentSet) armory.getEquipmentSets().get(armory.getCurrentEquipSetNo().intValue());
                            if (equipmentSet != null && populate(equipmentSet)) {
                                return;
                            }
                        }
                    }
                }
                CustomDialog.newBuilder(ArmoryFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1155R.string.net_gogame_gowrap_ranking_title).withMessage(C1155R.string.net_gogame_gowrap_ranking_no_data_error_message).build().show();
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }

        private boolean populate(EquipmentSet equipmentSet) {
            if (equipmentSet == null || ArmoryFragment.this.getView() == null) {
                return false;
            }
            if (equipmentSet.getAtk() != null) {
                ((TextView) ArmoryFragment.this.getView().findViewById(C1155R.id.net_gogame_gowrap_armory_user_atk)).setText(String.format(Locale.getDefault(), "%,d", new Object[]{equipmentSet.getAtk()}));
            }
            if (equipmentSet.getDef() != null) {
                ((TextView) ArmoryFragment.this.getView().findViewById(C1155R.id.net_gogame_gowrap_armory_user_def)).setText(String.format(Locale.getDefault(), "%,d", new Object[]{equipmentSet.getDef()}));
            }
            if (equipmentSet.getHp() != null) {
                ((TextView) ArmoryFragment.this.getView().findViewById(C1155R.id.net_gogame_gowrap_armory_user_hp)).setText(String.format(Locale.getDefault(), "%,d", new Object[]{equipmentSet.getHp()}));
            }
            LinearLayout linearLayout = (LinearLayout) ArmoryFragment.this.getView().findViewById(C1155R.id.net_gogame_gowrap_armory_equipment_list);
            populate(linearLayout, equipmentSet.getWeapon0());
            populate(linearLayout, equipmentSet.getWeapon1());
            populate(linearLayout, equipmentSet.getWeapon2());
            populate(linearLayout, equipmentSet.getArmor());
            populate(linearLayout, equipmentSet.getHelm());
            populate(linearLayout, equipmentSet.getArm());
            populate(linearLayout, equipmentSet.getLeg());
            return true;
        }

        private void populate(LinearLayout linearLayout, Equipment equipment) {
            if (linearLayout != null && equipment != null) {
                LinearLayout linearLayout2 = (LinearLayout) ((LayoutInflater) ArmoryFragment.this.getActivity().getSystemService("layout_inflater")).inflate(C1155R.layout.net_gogame_gowrap_dpro_fragment_armory_equipment, linearLayout, false);
                if (!(ArmoryFragment.this.uiContext == null || ArmoryFragment.this.uiContext.getDownloadManager() == null)) {
                    Long iconId = getIconId(equipment);
                    if (iconId != null) {
                        ImageView imageView = (ImageView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_equipment_icon);
                        ArmoryFragment.this.uiContext.getDownloadManager().download(Builder.newBuilder(ArmoryFragment.BASE_EQUIPMENT_ICON_URL + iconId + "0.png").into(new ImageViewTarget(imageView)));
                    }
                }
                if (equipment.getBase() != null) {
                    ((TextView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_equipment_name)).setText(equipment.getBase().getStatItemName());
                    ((TextView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_equipment_rarity)).setText(getRarityName(equipment.getBase().getStatItemRarity()));
                    if (!(equipment.getBase().getLevel() == null || equipment.getBase().getMaxLevel() == null)) {
                        ((TextView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_equipment_level)).setText(String.format(Locale.getDefault(), "%,d / %,d", new Object[]{equipment.getBase().getLevel(), equipment.getBase().getMaxLevel()}));
                    }
                }
                if (equipment.getSkillItems() != null) {
                    for (SkillItem populate : equipment.getSkillItems()) {
                        populate(linearLayout2, populate);
                    }
                }
                linearLayout.addView(linearLayout2);
            }
        }

        private void populate(LinearLayout linearLayout, SkillItem skillItem) {
            if (linearLayout != null && skillItem != null) {
                LinearLayout linearLayout2 = (LinearLayout) ((LayoutInflater) ArmoryFragment.this.getActivity().getSystemService("layout_inflater")).inflate(C1155R.layout.net_gogame_gowrap_dpro_fragment_armory_equipment_skillitem, linearLayout, false);
                if (skillItem.getBase() != null) {
                    Integer iconId = getIconId(skillItem);
                    if (iconId != null) {
                        ((ImageView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_skillitem_icon)).setImageDrawable(ArmoryFragment.this.getActivity().getDrawable(iconId.intValue()));
                    }
                    ((TextView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_skillitem_name)).setText(skillItem.getBase().getStatItemName());
                    ((TextView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_skillitem_rarity)).setText(getRarityName(skillItem.getBase().getStatItemRarity()));
                    if (!(skillItem.getBase().getLevel() == null || skillItem.getBase().getMaxLevel() == null)) {
                        ((TextView) linearLayout2.findViewById(C1155R.id.net_gogame_gowrap_armory_skillitem_level)).setText(String.format(Locale.getDefault(), "%,d / %,d", new Object[]{skillItem.getBase().getLevel(), skillItem.getBase().getMaxLevel()}));
                    }
                }
                linearLayout.addView(linearLayout2);
            }
        }

        private String getRarityName(Integer num) {
            if (num == null || num.intValue() < 0 || num.intValue() >= ArmoryFragment.RARITY_NAMES.length) {
                return null;
            }
            return ArmoryFragment.RARITY_NAMES[num.intValue()];
        }

        private Long getIconId(Equipment equipment) {
            if (equipment == null || equipment.getBase() == null) {
                return null;
            }
            if (equipment.getBase().getMainItemIconId() == null || equipment.getBase().getMainItemIconId().longValue() <= 0) {
                return equipment.getBase().getMainItemId();
            }
            return equipment.getBase().getMainItemIconId();
        }

        private Integer getIconId(SkillItem skillItem) {
            if (skillItem == null || skillItem.getBase() == null || skillItem.getBase().getSubItemType() == null) {
                return null;
            }
            int intValue = skillItem.getBase().getSubItemType().intValue();
            if (intValue < 0 || intValue >= ArmoryFragment.SKILL_ITEM_BACKGROUNDS.length) {
                return null;
            }
            return ArmoryFragment.SKILL_ITEM_BACKGROUNDS[intValue];
        }
    }

    public ArmoryFragment(LeaderboardEntry leaderboardEntry) {
        this.leaderboardEntry = leaderboardEntry;
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        View inflate = layoutInflater.inflate(C1155R.layout.net_gogame_gowrap_dpro_fragment_armory, viewGroup, false);
        this.progressBar = (ProgressBar) inflate.findViewById(C1155R.id.net_gogame_gowrap_progress_indicator);
        if (this.leaderboardEntry != null) {
            ((TextView) inflate.findViewById(C1155R.id.net_gogame_gowrap_armory_user_name)).setText(this.leaderboardEntry.getUserName());
            ((TextView) inflate.findViewById(C1155R.id.net_gogame_gowrap_armory_user_title)).setText(this.leaderboardEntry.getUserTitle());
            if (this.leaderboardEntry.getUserLevel() != null) {
                ((TextView) inflate.findViewById(C1155R.id.net_gogame_gowrap_armory_user_level)).setText(String.format(Locale.getDefault(), "Lv %,d", new Object[]{this.leaderboardEntry.getUserLevel()}));
            }
            if (this.leaderboardEntry.getValue() != null) {
                ((TextView) inflate.findViewById(C1155R.id.net_gogame_gowrap_armory_user_power_rating)).setText(String.format(Locale.getDefault(), "Power %,d", new Object[]{this.leaderboardEntry.getValue()}));
            }
            ((TextView) inflate.findViewById(C1155R.id.net_gogame_gowrap_armory_hunter_id)).setText(this.leaderboardEntry.getHunterId());
            if (this.leaderboardEntry.getHunterId() != null) {
                new CustomGetArmoryAsyncTask().execute(new String[]{this.leaderboardEntry.getHunterId()});
            }
        }
        inflate.findViewById(C1155R.id.net_gogame_gowrap_back_button).setOnClickListener(new C11561());
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
}
