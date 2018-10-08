package net.gogame.chat;

import android.content.Context;
import android.database.DataSetObserver;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import com.zopim.android.sdk.C0785R;

public class ChatAdapter extends BaseAdapter {
    private final ChatContext chatContext;
    private final Context context;
    private final DataSetObserver dataSetObserver = new C09991();
    private final UIContext uiContext;
    private final ChatAdapterViewFactory viewFactory;

    /* renamed from: net.gogame.chat.ChatAdapter$1 */
    class C09991 extends DataSetObserver {
        C09991() {
        }

        public void onChanged() {
            ChatAdapter.this.notifyDataSetChanged();
        }
    }

    public ChatAdapter(Context context, UIContext uIContext, ChatAdapterViewFactory chatAdapterViewFactory, ChatContext chatContext) {
        this.context = context;
        this.uiContext = uIContext;
        this.viewFactory = chatAdapterViewFactory;
        this.chatContext = chatContext;
    }

    public ChatContext getChatContext() {
        return this.chatContext;
    }

    public void start() {
        this.chatContext.registerDataSetObserver(this.dataSetObserver);
    }

    public void stop() {
        this.chatContext.unregisterDataSetObserver(this.dataSetObserver);
    }

    public int getCount() {
        return this.chatContext.getChatEntryCount() + 1;
    }

    public Object getItem(int i) {
        if (i < 0) {
            return null;
        }
        if (i < this.chatContext.getChatEntryCount()) {
            return this.chatContext.getChatEntry(i);
        }
        if (i == this.chatContext.getChatEntryCount()) {
            return this.chatContext.getAgentTypingEntry();
        }
        return null;
    }

    public long getItemId(int i) {
        return (long) (i + 10000);
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        Object item = getItem(i);
        if (!(item instanceof AgentTypingEntry)) {
            View view2 = this.chatContext.getView(item, i, view, viewGroup);
            if (view2 == null) {
                return this.viewFactory.getEmptyView(view2, viewGroup);
            }
            return view2;
        } else if (((AgentTypingEntry) item).isTyping()) {
            return this.viewFactory.getNotificationView(view, viewGroup, this.context.getResources().getString(C0785R.string.net_gogame_chat_agent_typing_message));
        } else {
            return this.viewFactory.getNotificationView(view, viewGroup, null, true);
        }
    }
}
