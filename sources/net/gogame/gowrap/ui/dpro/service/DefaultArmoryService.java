package net.gogame.gowrap.p019ui.dpro.service;

import android.util.JsonReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Locale;
import net.gogame.gowrap.p019ui.dpro.model.armory.ArmoryResponse;
import net.gogame.gowrap.p021io.utils.IOUtils;
import net.gogame.gowrap.support.HttpException;
import net.gogame.gowrap.support.HttpUtils;
import p017io.fabric.sdk.android.services.network.HttpRequest;

/* renamed from: net.gogame.gowrap.ui.dpro.service.DefaultArmoryService */
public class DefaultArmoryService implements ArmoryService {
    public static final DefaultArmoryService INSTANCE = new DefaultArmoryService();

    public ArmoryResponse getArmory(String str) throws IOException, HttpException {
        JsonReader jsonReader;
        HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(String.format(Locale.ENGLISH, "%s/hunters/%s/armory", new Object[]{ServiceConstants.BASE_URL, str})).openConnection();
        try {
            httpURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
            httpURLConnection.setConnectTimeout(10000);
            httpURLConnection.setReadTimeout(30000);
            if (!HttpUtils.isSuccessful(httpURLConnection.getResponseCode())) {
                HttpUtils.drainQuietly(httpURLConnection);
                throw new HttpException(httpURLConnection.getResponseCode(), httpURLConnection.getResponseMessage());
            }
            InputStream inputStream = httpURLConnection.getInputStream();
            try {
                jsonReader = new JsonReader(new InputStreamReader(inputStream, "UTF-8"));
                ArmoryResponse armoryResponse = new ArmoryResponse(jsonReader);
                jsonReader.close();
                IOUtils.closeQuietly(inputStream);
                return armoryResponse;
            } catch (Throwable th) {
                IOUtils.closeQuietly(inputStream);
                throw th;
            }
        } finally {
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
        }
    }
}
