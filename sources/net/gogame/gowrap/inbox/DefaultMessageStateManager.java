package net.gogame.gowrap.inbox;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public class DefaultMessageStateManager implements MessageStateManager {
    private static final String[] TABLE_COLUMNS = {"message_type", "message_id", "message_timestamp", "message_read"};
    private static final String TABLE_NAME = "message_status";
    private final Context context;
    private final MessageStateDbHelper dbHelper;

    public DefaultMessageStateManager(Context context2) {
        this.context = context2;
        this.dbHelper = MessageStateDbHelper.getInstance(context2);
    }

    public List<MessageState> getMessageStates(String str, long j) {
        Cursor query;
        ArrayList arrayList = new ArrayList();
        SQLiteDatabase readableDatabase = this.dbHelper.getReadableDatabase();
        try {
            query = readableDatabase.query(TABLE_NAME, TABLE_COLUMNS, String.format(Locale.ENGLISH, "(message_type = ?) AND (message_timestamp >= %d)", new Object[]{Long.valueOf(j)}), new String[]{str}, null, null, null);
            query.moveToFirst();
            while (!query.isAfterLast()) {
                MessageState messageState = new MessageState();
                messageState.setType(query.getString(0));
                messageState.setId(query.getLong(1));
                messageState.setTimestamp(query.getLong(2));
                messageState.setRead(query.getShort(3) != 0);
                arrayList.add(messageState);
                query.moveToNext();
            }
            query.close();
            readableDatabase.close();
            return arrayList;
        } catch (Throwable th) {
            readableDatabase.close();
            throw th;
        }
    }

    public void setMessageState(String str, long j, long j2, boolean z) {
        SQLiteDatabase writableDatabase = this.dbHelper.getWritableDatabase();
        try {
            ContentValues contentValues = new ContentValues();
            contentValues.put("message_type", str);
            contentValues.put("message_id", Long.valueOf(j));
            contentValues.put("message_timestamp", Long.valueOf(j2));
            contentValues.put("message_read", Short.valueOf((short) (z ? 1 : 0)));
            writableDatabase.replace(TABLE_NAME, null, contentValues);
        } finally {
            writableDatabase.close();
        }
    }

    public void deleteMessageStates(String str, long j) {
        SQLiteDatabase writableDatabase = this.dbHelper.getWritableDatabase();
        String str2 = TABLE_NAME;
        try {
            writableDatabase.delete(str2, String.format(Locale.ENGLISH, "(message_type = ?) AND (message_timestamp < %d)", new Object[]{Long.valueOf(j)}), new String[]{str});
        } finally {
            writableDatabase.close();
        }
    }

    public void deleteMessageStates(long j) {
        SQLiteDatabase writableDatabase = this.dbHelper.getWritableDatabase();
        try {
            writableDatabase.delete(TABLE_NAME, String.format(Locale.ENGLISH, "(message_timestamp < %d)", new Object[]{Long.valueOf(j)}), null);
        } finally {
            writableDatabase.close();
        }
    }
}
