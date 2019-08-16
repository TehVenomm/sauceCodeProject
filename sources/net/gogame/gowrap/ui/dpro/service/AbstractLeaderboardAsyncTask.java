package net.gogame.gowrap.p019ui.dpro.service;

import android.os.AsyncTask;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.FriendsLeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LeaderboardResponse;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LevelTierLeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.NewUsersLeaderboardRequest;

/* renamed from: net.gogame.gowrap.ui.dpro.service.AbstractLeaderboardAsyncTask */
public abstract class AbstractLeaderboardAsyncTask<T extends LeaderboardResponse> extends AsyncTask<LeaderboardRequest, Void, T> {
    private Exception exceptionToBeThrown;
    private final LeaderboardService<T> service;

    public AbstractLeaderboardAsyncTask(LeaderboardService<T> leaderboardService) {
        this.service = leaderboardService;
    }

    /* access modifiers changed from: protected */
    public T doInBackground(LeaderboardRequest... leaderboardRequestArr) {
        if (leaderboardRequestArr == null || leaderboardRequestArr.length == 0) {
            return null;
        }
        LevelTierLeaderboardRequest levelTierLeaderboardRequest = leaderboardRequestArr[0];
        if (levelTierLeaderboardRequest == null) {
            return null;
        }
        try {
            if (levelTierLeaderboardRequest instanceof LevelTierLeaderboardRequest) {
                return this.service.getLeaderboard(levelTierLeaderboardRequest);
            } else if (levelTierLeaderboardRequest instanceof NewUsersLeaderboardRequest) {
                return this.service.getLeaderboard((NewUsersLeaderboardRequest) levelTierLeaderboardRequest);
            } else if (levelTierLeaderboardRequest instanceof FriendsLeaderboardRequest) {
                return this.service.getLeaderboard((FriendsLeaderboardRequest) levelTierLeaderboardRequest);
            } else {
                Log.w(Constants.TAG, "Unknown leaderboard request: " + levelTierLeaderboardRequest.getClass().getName());
                return null;
            }
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
            this.exceptionToBeThrown = e;
            return null;
        }
    }

    public Exception getExceptionToBeThrown() {
        return this.exceptionToBeThrown;
    }
}
