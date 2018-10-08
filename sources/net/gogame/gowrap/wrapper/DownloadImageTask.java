package net.gogame.gowrap.wrapper;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.ImageView;
import java.io.InputStream;
import java.net.URL;
import net.gogame.gowrap.Constants;

public class DownloadImageTask extends AsyncTask<String, Void, Bitmap> {
    private ImageView imageView;

    public DownloadImageTask(ImageView imageView) {
        this.imageView = imageView;
    }

    protected Bitmap doInBackground(String... strArr) {
        Throwable e;
        Throwable th;
        Bitmap bitmap = null;
        InputStream openStream;
        try {
            openStream = new URL(strArr[0]).openStream();
            try {
                bitmap = BitmapFactory.decodeStream(openStream);
                if (openStream != null) {
                    try {
                        openStream.close();
                    } catch (Exception e2) {
                    }
                }
            } catch (Exception e3) {
                e = e3;
                try {
                    Log.e(Constants.TAG, "Error", e);
                    if (openStream != null) {
                        try {
                            openStream.close();
                        } catch (Exception e4) {
                        }
                    }
                    return bitmap;
                } catch (Throwable th2) {
                    th = th2;
                    if (openStream != null) {
                        try {
                            openStream.close();
                        } catch (Exception e5) {
                        }
                    }
                    throw th;
                }
            }
        } catch (Exception e6) {
            e = e6;
            openStream = bitmap;
            Log.e(Constants.TAG, "Error", e);
            if (openStream != null) {
                openStream.close();
            }
            return bitmap;
        } catch (Throwable e7) {
            openStream = bitmap;
            th = e7;
            if (openStream != null) {
                openStream.close();
            }
            throw th;
        }
        return bitmap;
    }

    protected void onPostExecute(Bitmap bitmap) {
        this.imageView.setImageBitmap(bitmap);
    }
}
