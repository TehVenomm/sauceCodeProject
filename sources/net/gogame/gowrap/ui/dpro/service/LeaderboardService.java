package net.gogame.gowrap.p019ui.dpro.service;

import java.io.IOException;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.FriendsLeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LeaderboardResponse;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.LevelTierLeaderboardRequest;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.NewUsersLeaderboardRequest;
import net.gogame.gowrap.support.HttpException;

/* renamed from: net.gogame.gowrap.ui.dpro.service.LeaderboardService */
public interface LeaderboardService<T extends LeaderboardResponse> {
    T getLeaderboard(FriendsLeaderboardRequest friendsLeaderboardRequest) throws IOException, HttpException;

    T getLeaderboard(LevelTierLeaderboardRequest levelTierLeaderboardRequest) throws IOException, HttpException;

    T getLeaderboard(NewUsersLeaderboardRequest newUsersLeaderboardRequest) throws IOException, HttpException;
}
