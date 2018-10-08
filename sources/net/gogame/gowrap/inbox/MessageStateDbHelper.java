package net.gogame.gowrap.inbox;

import android.content.Context;
import android.database.DatabaseErrorHandler;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.database.sqlite.SQLiteOpenHelper;

public class MessageStateDbHelper extends SQLiteOpenHelper {
    private static final String DATABASE_NAME = "goWrap-messages";
    private static final int DATABASE_VERSION = 1;
    private static MessageStateDbHelper instance;

    public MessageStateDbHelper(Context context) {
        super(context, DATABASE_NAME, null, 1);
    }

    public MessageStateDbHelper(Context context, CursorFactory cursorFactory) {
        super(context, DATABASE_NAME, cursorFactory, 1);
    }

    public MessageStateDbHelper(Context context, CursorFactory cursorFactory, DatabaseErrorHandler databaseErrorHandler) {
        super(context, DATABASE_NAME, cursorFactory, 1, databaseErrorHandler);
    }

    public static synchronized MessageStateDbHelper getInstance(Context context) {
        MessageStateDbHelper messageStateDbHelper;
        synchronized (MessageStateDbHelper.class) {
            if (instance == null) {
                instance = new MessageStateDbHelper(context.getApplicationContext());
            }
            messageStateDbHelper = instance;
        }
        return messageStateDbHelper;
    }

    public void onCreate(SQLiteDatabase sQLiteDatabase) {
        sQLiteDatabase.execSQL("CREATE TABLE IF NOT EXISTS message_status (    message_type VARCHAR(255),    message_id BIGINT,    message_timestamp BIGINT,    message_read TINYINT,    UNIQUE(message_type, message_id) ON CONFLICT REPLACE);");
    }

    public void onUpgrade(SQLiteDatabase sQLiteDatabase, int i, int i2) {
        if (i != i2) {
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS message_status");
            onCreate(sQLiteDatabase);
        }
    }
}
