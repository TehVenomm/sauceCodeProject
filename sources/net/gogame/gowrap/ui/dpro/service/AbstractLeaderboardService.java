package net.gogame.gowrap.ui.dpro.service;

import android.util.JsonReader;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Locale;
import net.gogame.gowrap.io.utils.IOUtils;
import net.gogame.gowrap.support.HttpException;
import net.gogame.gowrap.support.HttpUtils;
import net.gogame.gowrap.ui.dpro.model.leaderboard.FriendsLeaderboardRequest;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LeaderboardResponse;
import net.gogame.gowrap.ui.dpro.model.leaderboard.LevelTierLeaderboardRequest;
import net.gogame.gowrap.ui.dpro.model.leaderboard.NewUsersLeaderboardRequest;

public abstract class AbstractLeaderboardService<T extends LeaderboardResponse> implements LeaderboardService<T> {
    private final String leaderboardId;

    protected abstract T parseResponse(JsonReader jsonReader) throws IOException;

    public AbstractLeaderboardService(String str) {
        this.leaderboardId = str;
    }

    public T getLeaderboard(LevelTierLeaderboardRequest levelTierLeaderboardRequest) throws IOException, HttpException {
        int i = 0;
        Locale locale = Locale.ENGLISH;
        String str = "%s/leaderboards/%s/types/levelTiers/%d?pageNumber=%d&pageSize=%d";
        Object[] objArr = new Object[5];
        objArr[0] = ServiceConstants.BASE_URL;
        objArr[1] = this.leaderboardId;
        if (levelTierLeaderboardRequest.getLevelTier() != null) {
            i = levelTierLeaderboardRequest.getLevelTier().intValue();
        }
        objArr[2] = Integer.valueOf(i);
        objArr[3] = Integer.valueOf(levelTierLeaderboardRequest.getPageNumber());
        objArr[4] = Integer.valueOf(levelTierLeaderboardRequest.getPageSize());
        return getLeaderboard(String.format(locale, str, objArr));
    }

    public T getLeaderboard(NewUsersLeaderboardRequest newUsersLeaderboardRequest) throws IOException, HttpException {
        return getLeaderboard(String.format(Locale.ENGLISH, "%s/leaderboards/%s/types/newUsers?pageNumber=%d&pageSize=%d", new Object[]{ServiceConstants.BASE_URL, this.leaderboardId, Integer.valueOf(newUsersLeaderboardRequest.getPageNumber()), Integer.valueOf(newUsersLeaderboardRequest.getPageSize())}));
    }

    public T getLeaderboard(FriendsLeaderboardRequest friendsLeaderboardRequest) throws IOException, HttpException {
        return getLeaderboard(String.format(Locale.ENGLISH, "%s/leaderboards/%s/hunters/%s/friends", new Object[]{ServiceConstants.BASE_URL, this.leaderboardId, friendsLeaderboardRequest.getHunterId()}));
    }

    private T getLeaderboard(String str) throws IOException, HttpException {
        JsonReader jsonReader;
        HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str).openConnection();
        try {
            httpURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
            httpURLConnection.setConnectTimeout(10000);
            httpURLConnection.setReadTimeout(30000);
            if (HttpUtils.isSuccessful(httpURLConnection.getResponseCode())) {
                InputStream inputStream = httpURLConnection.getInputStream();
                T inputStreamReader;
                try {
                    inputStreamReader = new InputStreamReader(inputStream, "UTF-8");
                    jsonReader = new JsonReader(inputStreamReader);
                    inputStreamReader = parseResponse(jsonReader);
                    jsonReader.close();
                    IOUtils.closeQuietly(inputStream);
                    return inputStreamReader;
                } catch (Throwable th) {
                    IOUtils.closeQuietly(inputStream);
                }
            } else {
                HttpUtils.drainQuietly(httpURLConnection);
                throw new HttpException(httpURLConnection.getResponseCode(), httpURLConnection.getResponseMessage());
            }
        } finally {
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
        }
    }
}
