package net.gogame.gowrap.ui.dpro.service;

import android.os.AsyncTask;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.ui.dpro.model.leaderboard.FriendsLeaderboardRequest;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LeaderboardRequest;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LeaderboardResponse;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LevelTierLeaderboardRequest;
import net.gogame.gowrap.ui.dpro.model.leaderboard.NewUsersLeaderboardRequest;

public abstract class AbstractLeaderboardAsyncTask<T extends LeaderboardResponse> extends AsyncTask<LeaderboardRequest, Void, T> {
    private Exception exceptionToBeThrown;
    private final LeaderboardService<T> service;

    public AbstractLeaderboardAsyncTask(LeaderboardService<T> leaderboardService) {
        this.service = leaderboardService;
    }

    protected T doInBackground(LeaderboardRequest... leaderboardRequestArr) {
        if (leaderboardRequestArr == null || leaderboardRequestArr.length == 0) {
            return null;
        }
        Object obj = leaderboardRequestArr[0];
        if (obj == null) {
            return null;
        }
        try {
            if (obj instanceof LevelTierLeaderboardRequest) {
                return this.service.getLeaderboard((LevelTierLeaderboardRequest) obj);
            } else if (obj instanceof NewUsersLeaderboardRequest) {
                return this.service.getLeaderboard((NewUsersLeaderboardRequest) obj);
            } else if (obj instanceof FriendsLeaderboardRequest) {
                return this.service.getLeaderboard((FriendsLeaderboardRequest) obj);
            } else {
                Log.w(Constants.TAG, "Unknown leaderboard request: " + obj.getClass().getName());
                return null;
            }
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
            this.exceptionToBeThrown = e;
            return null;
        }
    }

    public Exception getExceptionToBeThrown() {
        return this.exceptionToBeThrown;
    }
}
