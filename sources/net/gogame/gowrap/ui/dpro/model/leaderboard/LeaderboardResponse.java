package net.gogame.gowrap.p019ui.dpro.model.leaderboard;

import java.util.List;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LeaderboardEntry;

/* renamed from: net.gogame.gowrap.ui.dpro.model.leaderboard.LeaderboardResponse */
public interface LeaderboardResponse<T extends LeaderboardEntry> {
    String getDate();

    List<T> getRecords();

    Long getTotalRecordCount();
}
