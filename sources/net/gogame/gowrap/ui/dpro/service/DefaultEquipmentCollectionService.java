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
import net.gogame.gowrap.ui.dpro.model.equipmentcollection.EquipmentCollectionResponse;

public class DefaultEquipmentCollectionService implements EquipmentCollectionService {
    public static final DefaultEquipmentCollectionService INSTANCE = new DefaultEquipmentCollectionService();

    public EquipmentCollectionResponse getEquipmentCollection(String str) throws IOException, HttpException {
        HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(String.format(Locale.ENGLISH, "%s/hunters/%s/equipmentCollection", new Object[]{ServiceConstants.BASE_URL, str})).openConnection();
        try {
            httpURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
            httpURLConnection.setConnectTimeout(10000);
            httpURLConnection.setReadTimeout(30000);
            if (HttpUtils.isSuccessful(httpURLConnection.getResponseCode())) {
                InputStream inputStream = httpURLConnection.getInputStream();
                JsonReader jsonReader;
                try {
                    jsonReader = new JsonReader(new InputStreamReader(inputStream, "UTF-8"));
                    EquipmentCollectionResponse equipmentCollectionResponse = new EquipmentCollectionResponse(jsonReader);
                    jsonReader.close();
                    IOUtils.closeQuietly(inputStream);
                    return equipmentCollectionResponse;
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
