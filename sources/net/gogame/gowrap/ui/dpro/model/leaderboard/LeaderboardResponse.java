package net.gogame.gowrap.ui.dpro.model.leaderboard;

import java.util.List;

public interface LeaderboardResponse<T extends LeaderboardEntry> {
    String getDate();

    List<T> getRecords();

    Long getTotalRecordCount();
}
