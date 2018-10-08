package net.gogame.gowrap.ui.dpro.service;

import java.io.IOException;
import net.gogame.gowrap.support.HttpException;
import net.gogame.gowrap.ui.dpro.model.leaderboard.FriendsLeaderboardRequest;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LeaderboardResponse;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LevelTierLeaderboardRequest;
import net.gogame.gowrap.ui.dpro.model.leaderboard.NewUsersLeaderboardRequest;

public interface LeaderboardService<T extends LeaderboardResponse> {
    T getLeaderboard(FriendsLeaderboardRequest friendsLeaderboardRequest) throws IOException, HttpException;

    T getLeaderboard(LevelTierLeaderboardRequest levelTierLeaderboardRequest) throws IOException, HttpException;

    T getLeaderboard(NewUsersLeaderboardRequest newUsersLeaderboardRequest) throws IOException, HttpException;
}
