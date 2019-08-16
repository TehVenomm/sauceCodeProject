package net.gogame.gowrap.p019ui.dpro.p020ui;

import android.content.Context;
import android.os.Bundle;
import android.os.Parcelable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import net.gogame.gowrap.p019ui.dpro.C1452R;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LeaderboardEntry;

/* renamed from: net.gogame.gowrap.ui.dpro.ui.RankingListAdapter */
public class RankingListAdapter extends BaseAdapter {
    private static final int[] BACKGROUND_COLORS = {0, -2147310171};
    private static final String KEY_ENTRIES = "entries";
    private static final int[] NAME_TEXT_COLORS = {-244890, -208061, -5477923, -16076881};
    private final Context context;
    private ArrayList<? extends LeaderboardEntry> entries;

    public RankingListAdapter(Context context2) {
        this.context = context2;
    }

    public List<? extends LeaderboardEntry> getEntries() {
        return this.entries;
    }

    public void setEntries(List<? extends LeaderboardEntry> list) {
        if (list instanceof ArrayList) {
            this.entries = (ArrayList) list;
        } else {
            this.entries = new ArrayList<>(list);
        }
        notifyDataSetChanged();
    }

    public int getCount() {
        if (this.entries == null) {
            return 0;
        }
        return this.entries.size();
    }

    public Object getItem(int i) {
        if (i >= this.entries.size()) {
            return null;
        }
        return this.entries.get(i);
    }

    public long getItemId(int i) {
        if (i >= this.entries.size()) {
            return -1;
        }
        return ((LeaderboardEntry) this.entries.get(i)).getOffset().longValue();
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        return getView(i, (LeaderboardEntry) this.entries.get(i), view, viewGroup);
    }

    public View getView(int i, LeaderboardEntry leaderboardEntry, View view, ViewGroup viewGroup) {
        if (view == null) {
            view = ((LayoutInflater) this.context.getSystemService("layout_inflater")).inflate(C1452R.C1454layout.net_gogame_gowrap_dpro_ranking_list_item, viewGroup, false);
        }
        view.setBackgroundColor(BACKGROUND_COLORS[i % BACKGROUND_COLORS.length]);
        TextView textView = (TextView) view.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_position);
        TextView textView2 = (TextView) view.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_name);
        TextView textView3 = (TextView) view.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_title);
        TextView textView4 = (TextView) view.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_hunter_id);
        TextView textView5 = (TextView) view.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_level);
        TextView textView6 = (TextView) view.findViewById(C1452R.C1453id.net_gogame_gowrap_ranking_value);
        if (leaderboardEntry != null) {
            if (leaderboardEntry.getPosition() != null) {
                textView.setText(String.format(Locale.getDefault(), "%,d", new Object[]{leaderboardEntry.getPosition()}));
            }
            textView2.setText(leaderboardEntry.getUserName());
            textView2.setTextColor(NAME_TEXT_COLORS[i % NAME_TEXT_COLORS.length]);
            textView3.setText(leaderboardEntry.getUserTitle());
            textView4.setText(leaderboardEntry.getHunterId());
            if (leaderboardEntry.getUserLevel() != null) {
                textView5.setText(String.format(Locale.getDefault(), "%,d", new Object[]{leaderboardEntry.getUserLevel()}));
            }
            if (leaderboardEntry.getValue() != null) {
                textView6.setText(String.format(Locale.getDefault(), "%,d", new Object[]{leaderboardEntry.getValue()}));
            }
        } else {
            textView.setText(null);
            textView2.setText(null);
            textView3.setText(null);
            textView4.setText(null);
            textView5.setText(null);
            textView6.setText(null);
        }
        return view;
    }

    public Parcelable onSaveInstanceState() {
        Bundle bundle = new Bundle();
        bundle.putSerializable(KEY_ENTRIES, this.entries);
        return bundle;
    }

    public void onRestoreInstanceState(Parcelable parcelable) {
        if (parcelable != null && (parcelable instanceof Bundle)) {
            this.entries = (ArrayList) ((Bundle) parcelable).getSerializable(KEY_ENTRIES);
        }
    }
}
